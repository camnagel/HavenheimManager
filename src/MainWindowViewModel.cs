using System;
using System.ComponentModel;
using System.Text.Json;
using System.Windows;
using HavenheimManager.Containers;
using HavenheimManager.Enums;
using HavenheimManager.Extensions;
using HavenheimManager.Handlers;
using HavenheimManager.Import;
using Microsoft.Win32;

namespace HavenheimManager;

public class MainWindowViewModel : INotifyPropertyChanged
{
    private string _appMode = string.Empty;
    private string _saveFileName = "";

    public MainWindowViewModel()
    {
        LoadCommand = new DelegateCommand(LoadAction);
        SaveCommand = new DelegateCommand(SaveAction);
        SaveAsCommand = new DelegateCommand(SaveAsAction);
        ImportCommand = new DelegateCommand(ImportAction);

        // Handlers
        TraitHandler = new TraitHandler();
        FeatHandler = new FeatHandler();
        ItemHandler = new ItemHandler();
        SpellHandler = new SpellHandler();
        CraftHandler = new CraftHandler();
    }

    // Asset Commands
    public DelegateCommand LoadCommand { get; }
    public DelegateCommand SaveCommand { get; }
    public DelegateCommand SaveAsCommand { get; }
    public DelegateCommand ImportCommand { get; }

    // Handlers
    public FeatHandler FeatHandler { get; }
    public TraitHandler TraitHandler { get; }
    public ItemHandler ItemHandler { get; }
    public CraftHandler CraftHandler { get; }
    public SpellHandler SpellHandler { get; }

    public string AppMode
    {
        get => _appMode;
        set
        {
            if (value == _appMode)
            {
                return;
            }

            _appMode = value;
            SetAppMode(value.StringToEnum<AppMode>());
            OnPropertyChanged("AppMode");
        }
    }


    public event PropertyChangedEventHandler? PropertyChanged;

    public void RefreshButtonState()
    {
        LoadCommand.RaiseCanExecuteChanged();
        SaveCommand.RaiseCanExecuteChanged();
        LoadCommand.RaiseCanExecuteChanged();
        SaveCommand.RaiseCanExecuteChanged();
        SaveAsCommand.RaiseCanExecuteChanged();
        ImportCommand.RaiseCanExecuteChanged();

        // Handlers
        TraitHandler.RefreshButtonState();
        FeatHandler.RefreshButtonState();
        ItemHandler.RefreshButtonState();
        CraftHandler.RefreshButtonState();
        SpellHandler.RefreshButtonState();
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void LoadAction(object arg)
    {
        OpenFileDialog dialog = new()
        {
            Title = "Select File",
            Multiselect = false
        };

        if (dialog.ShowDialog() == true)
        {
            //SerialBin bin = JsonSerializer.Deserialize<SerialBin>(File.ReadAllText(dialog.FileName))!;

            /*foreach (Clip clip in bin.ClipList)
            {
                ClipList.Add(clip);
            }*/
        }
    }

    private void SaveAction(object arg)
    {
        if (_saveFileName.Length == 0)
        {
            SaveAsAction(arg);
            return;
        }

        SerializeAndSave(_saveFileName);
    }

    private void SerializeAndSave(string filename)
    {
        _saveFileName = filename;
        JsonSerializerOptions jsonOpts = new() { WriteIndented = true };
        //var bin = new SerialBin(ClipList, ReadyList, ExportedList, HeadList);
        //string serialBin = JsonSerializer.Serialize(bin, jsonOpts);

        //File.WriteAllText(filename, serialBin);
    }

    private void SaveAsAction(object arg)
    {
        SaveFileDialog dialog = new()
        {
            Title = "Enter Filename"
        };

        if (dialog.ShowDialog() == true)
        {
            SerializeAndSave(dialog.FileName);
        }
    }

    private void ImportAction(object arg)
    {
        try
        {
            ImportViewModel vm = new();
            ImportView configWindow = new(vm);

            if (configWindow.ShowDialog() == true)
            {
                string importPath = vm.GetSourcePath();
                SourceType type = vm.GetSourceType();

                switch (type)
                {
                    case SourceType.Trait:
                        foreach (Trait trait in ImportReader.ReadTraitCsv(importPath))
                        {
                            TraitHandler.MasterTraitList.Add(trait);
                        }

                        TraitHandler.ApplyFilters();
                        break;
                    case SourceType.Feat:
                        foreach (Feat feat in ImportReader.ReadFeatCsv(importPath))
                        {
                            FeatHandler.MasterFeatList.Add(feat);
                        }

                        FeatHandler.UpdateFeatReqs();
                        FeatHandler.UpdateFeatCustomTags();
                        FeatHandler.ApplyFeatFilters();
                        break;
                    case SourceType.Item:
                        ImportReader.ReadItemCsv(importPath);
                        break;
                    case SourceType.Spell:
                        ImportReader.ReadSpellCsv(importPath);
                        break;
                }
            }
        }
        catch (ArgumentOutOfRangeException)
        {
            string messageBoxText = "Source type was not selected";
            string caption = "Error";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Error;

            MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.OK);
        }

        RefreshButtonState();
    }

    private void SetAppMode(AppMode mode)
    {
        ClearApp();
        AppSettings.Mode = mode;

        TraitHandler.RefreshMode();
    }

    private void ClearApp()
    {
        TraitHandler.Clear();
        FeatHandler.Clear();
        ItemHandler.Clear();
        SpellHandler.Clear();
        CraftHandler.Clear();
    }
}