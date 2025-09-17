using HavenheimManager.Containers;
using HavenheimManager.Descriptors;
using HavenheimManager.Editors;
using HavenheimManager.Enums;
using HavenheimManager.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace HavenheimManager.Handlers;

public class TraitHandler : IFilterable
{
    private Trait? _selectedTrait;

    private string _traitSearchText = RegexHandler.SearchPlaceholderText;

    public TraitHandler()
    {
        AddFavoriteTraitCommand = new DelegateCommand(AddFavoriteTraitAction);
        AddHiddenTraitCommand = new DelegateCommand(AddHiddenTraitAction);
        EditTraitCommand = new DelegateCommand(EditTraitAction);
        NewTraitCommand = new DelegateCommand(NewTraitAction);
        RemoveTraitCommand = new DelegateCommand(RemoveTraitAction);
        RemoveFavoriteTraitCommand = new DelegateCommand(RemoveFavoriteTraitAction);
        RemoveHiddenTraitCommand = new DelegateCommand(RemoveHiddenTraitAction);
        TraitSearchRemovePlaceholderTextCommand =
            new DelegateCommand(TraitSearchRemovePlaceholderTextAction);
        TraitSearchAddPlaceholderTextCommand = new DelegateCommand(TraitSearchAddPlaceholderTextAction);
        ShowHideFilterCommand = new DelegateCommand(ShowHideFilterAction);

        DescriptorSettings = new DescriptorSettings();

        FilterHandler = new FilterHandler(this);
    }

    // Trait Control Bar Commands
    public DelegateCommand TraitSearchRemovePlaceholderTextCommand { get; }
    public DelegateCommand TraitSearchAddPlaceholderTextCommand { get; }
    public DelegateCommand ShowHideFilterCommand { get; }
    public DelegateCommand AddFavoriteTraitCommand { get; }
    public DelegateCommand AddHiddenTraitCommand { get; }
    public DelegateCommand EditTraitCommand { get; }
    public DelegateCommand NewTraitCommand { get; }
    public DelegateCommand RemoveTraitCommand { get; }
    public DelegateCommand RemoveFavoriteTraitCommand { get; }
    public DelegateCommand RemoveHiddenTraitCommand { get; }

    // Filtered Trait Collections
    public ObservableCollection<Trait> FilteredTraitList { get; set; } = new();
    public ObservableCollection<Trait> FavoriteTraitList { get; set; } = new();
    public ObservableCollection<Trait> HiddenTraitList { get; set; } = new();

    // Master trait list
    public List<Trait> MasterTraitList { get; } = new();

    public Trait? SelectedTrait
    {
        get => _selectedTrait;
        set
        {
            if (value != null)
            {
                SelectedTrait = null;
            }

            _selectedTrait = value;
            CurrentTrait.Clear();
            if (value != null)
            {
                CurrentTrait.Add(value);
            }

            OnPropertyChanged("SelectedTrait");
        }
    }

    public ObservableCollection<Trait> CurrentTrait { get; set; } = new();

    public string TraitSearchText
    {
        get => _traitSearchText;
        set
        {
            _traitSearchText = value;
            ApplyFilters();

            OnPropertyChanged("TraitSearchText");
        }
    }

    public DescriptorSettings DescriptorSettings { get; }

    public FilterHandler FilterHandler { get; }

    public event PropertyChangedEventHandler? PropertyChanged;

    public void ApplyFilters()
    {
        FilteredTraitList.Clear();
        List<Trait> possibleTraits =
            (TraitSearchText != RegexHandler.SearchPlaceholderText && TraitSearchText != ""
                ? MasterTraitList.Where(x => x.Name.Sanitize().Contains(TraitSearchText.Sanitize())).ToList()
                : MasterTraitList)
            .Where(x => !FavoriteTraitList.Contains(x) && !HiddenTraitList.Contains(x)).ToList();

        foreach (Trait trait in FilterHandler.BroadFilter(possibleTraits))
        {
            FilteredTraitList.Add(trait);
        }
    }

    public void RefreshButtonState()
    {
        FilterHandler.RefreshButtonState();
    }

    internal void ShowHideFilterAction(object arg)
    {
        try
        {
            DescriptorViewModel vm = new(DescriptorSettings);
            DescriptorView configWindow = new(vm);

            // Wait here until window returns
            if (configWindow.ShowDialog() == true)
            {
            }
        }
        catch (ArgumentOutOfRangeException)
        {
            // How did you get here?
            string messageBoxText = "Exception when modifying descriptor settings";
            string caption = "Exception";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Error;
            MessageBox.Show(messageBoxText, caption, button, icon);
        }

        RefreshButtonState();
    }

    internal void AddFavoriteTraitAction(object arg)
    {
        if (SelectedTrait != null && !FavoriteTraitList.Contains(SelectedTrait))
        {
            FavoriteTraitList.Add(SelectedTrait);
            HiddenTraitList.Remove(SelectedTrait);

            ApplyFilters();
            SelectedTrait = null;
        }
    }

    internal void AddHiddenTraitAction(object arg)
    {
        if (SelectedTrait != null && !HiddenTraitList.Contains(SelectedTrait))
        {
            HiddenTraitList.Add(SelectedTrait);
            FavoriteTraitList.Remove(SelectedTrait);

            ApplyFilters();
            SelectedTrait = null;
        }
    }

    internal void EditTraitAction(object arg)
    {
        try
        {
            if (SelectedTrait != null)
            {
                TraitViewModel vm = new(SelectedTrait);
                TraitView configWindow = new(vm);

                if (configWindow.ShowDialog() == true)
                {
                    Trait newTrait = vm.GetTrait();

                    if (MasterTraitList.Contains(SelectedTrait))
                    {
                        MasterTraitList.Remove(SelectedTrait);
                        MasterTraitList.Add(newTrait);

                        if (FavoriteTraitList.Contains(SelectedTrait))
                        {
                            FavoriteTraitList.Remove(SelectedTrait);
                            FavoriteTraitList.Add(newTrait);
                        }

                        else if (HiddenTraitList.Contains(SelectedTrait))
                        {
                            HiddenTraitList.Remove(SelectedTrait);
                            HiddenTraitList.Add(newTrait);
                        }

                        ApplyFilters();

                        SelectedTrait = newTrait;
                    }
                }
            }
            else
            {
                string messageBoxText = "No trait selected to edit";
                string caption = "Select Trait";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Exclamation;
                MessageBox.Show(messageBoxText, caption, button, icon);
            }
        }
        catch (ArgumentOutOfRangeException)
        {
            string messageBoxText = "Exception when adding trait";
            string caption = "Exception";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Error;
            MessageBox.Show(messageBoxText, caption, button, icon);
        }

        RefreshButtonState();
    }

    internal void NewTraitAction(object arg)
    {
        try
        {
            TraitViewModel vm = new(new Trait());
            TraitView configWindow = new(vm);

            if (configWindow.ShowDialog() == true)
            {
                Trait newTrait = vm.GetTrait();
                if (MasterTraitList.Select(x => x.Name).Contains(newTrait.Name))
                {
                    string messageBoxText = "Trait with same name already exists";
                    string caption = "Duplicate";
                    MessageBoxButton button = MessageBoxButton.OK;
                    MessageBoxImage icon = MessageBoxImage.Exclamation;
                    MessageBox.Show(messageBoxText, caption, button, icon);
                }
                else
                {
                    MasterTraitList.Add(newTrait);

                    ApplyFilters();
                    if (FilteredTraitList.Contains(newTrait))
                    {
                        SelectedTrait = newTrait;
                    }
                }
            }
        }
        catch (ArgumentOutOfRangeException)
        {
            string messageBoxText = "Exception when adding trait";
            string caption = "Exception";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Error;
            MessageBox.Show(messageBoxText, caption, button, icon);
        }

        RefreshButtonState();
    }

    internal void TraitSearchRemovePlaceholderTextAction(object arg)
    {
        if (TraitSearchText == RegexHandler.SearchPlaceholderText)
        {
            TraitSearchText = "";
        }
    }

    internal void TraitSearchAddPlaceholderTextAction(object arg)
    {
        if (string.IsNullOrWhiteSpace(TraitSearchText))
        {
            TraitSearchText = RegexHandler.SearchPlaceholderText;
        }
    }

    internal void RemoveTraitAction(object arg)
    {
        if (SelectedTrait != null)
        {
            string messageBoxText = "Trait will be removed. Are you sure?";
            string caption = "Warning";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            if (result == MessageBoxResult.Yes)
            {
                MasterTraitList.Remove(SelectedTrait);
                HiddenTraitList.Remove(SelectedTrait);
                FavoriteTraitList.Remove(SelectedTrait);
                SelectedTrait = null;
                ApplyFilters();
            }
        }
    }

    internal void RemoveFavoriteTraitAction(object arg)
    {
        if (SelectedTrait != null && FavoriteTraitList.Contains(SelectedTrait))
        {
            FavoriteTraitList.Remove(SelectedTrait);

            SelectedTrait = null;
            ApplyFilters();
        }
    }

    internal void RemoveHiddenTraitAction(object arg)
    {
        if (SelectedTrait != null && HiddenTraitList.Contains(SelectedTrait))
        {
            HiddenTraitList.Remove(SelectedTrait);

            SelectedTrait = null;
            ApplyFilters();
        }
    }

    internal void Clear()
    {
        FilterHandler.Clear();
    }

    internal void InitializeMode(AppMode mode)
    {
        FilterHandler.InitializeMode(mode);
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}