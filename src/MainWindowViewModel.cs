using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.Json;
using System.Windows;
using HavenheimManager.Containers;
using HavenheimManager.Enums;
using HavenheimManager.Extensions;
using HavenheimManager.Handlers;
using HavenheimManager.Import;
using Microsoft.Win32;
using Condition = HavenheimManager.Enums.Condition;

namespace HavenheimManager;

public class MainWindowViewModel : INotifyPropertyChanged
{
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
        

        // Items
        ItemHandler = new ItemHandler(this);
        CoreItemCheckboxCommand = new DelegateCommand(ItemHandler.ItemCoreFilterAction);
        SkillItemCheckboxCommand = new DelegateCommand(ItemHandler.ItemSkillFilterAction);
        ClassItemCheckboxCommand = new DelegateCommand(ItemHandler.ItemClassFilterAction);
        CombatItemCheckboxCommand = new DelegateCommand(ItemHandler.ItemCombatFilterAction);
        RoleItemCheckboxCommand = new DelegateCommand(ItemHandler.ItemRoleFilterAction);
        MagicItemCheckboxCommand = new DelegateCommand(ItemHandler.ItemMagicFilterAction);
        BonusItemCheckboxCommand = new DelegateCommand(ItemHandler.ItemBonusFilterAction);
        ConditionItemCheckboxCommand = new DelegateCommand(ItemHandler.ItemConditionFilterAction);
        SourceItemCheckboxCommand = new DelegateCommand(ItemHandler.ItemSourceFilterAction);
        CustomItemCheckboxCommand = new DelegateCommand(ItemHandler.ItemCustomFilterAction);
        AddFavoriteItemCommand = new DelegateCommand(ItemHandler.AddFavoriteItemAction);
        AddHiddenItemCommand = new DelegateCommand(ItemHandler.AddHiddenItemAction);
        EditItemCommand = new DelegateCommand(ItemHandler.EditItemAction);
        NewItemCommand = new DelegateCommand(ItemHandler.NewItemAction);
        RemoveItemCommand = new DelegateCommand(ItemHandler.RemoveItemAction);
        RemoveFavoriteItemCommand = new DelegateCommand(ItemHandler.RemoveFavoriteItemAction);
        RemoveHiddenItemCommand = new DelegateCommand(ItemHandler.RemoveHiddenItemAction);
        ItemSearchRemovePlaceholderTextCommand = new DelegateCommand(ItemHandler.ItemSearchRemovePlaceholderTextAction);
        ItemSearchAddPlaceholderTextCommand = new DelegateCommand(ItemHandler.ItemSearchAddPlaceholderTextAction);

        // Crafting
        CraftHandler = new CraftHandler(this);
        CraftToolCheckboxCommand = new DelegateCommand(CraftHandler.CraftToolAction);
        CraftWorkshopCheckboxCommand = new DelegateCommand(CraftHandler.CraftWorkshopAction);
        CraftModifierBonusCalculatorCommand = new DelegateCommand(CraftHandler.CraftModifierBonusCalcAction);
    }

    // Primary Object Collections
    
    
    public List<Item> MasterItemList { get; } = new();

    // Selected Object Backing Collections
    
    public ObservableCollection<Spell> CurrentSpell { get; set; } = new();
    public ObservableCollection<Item> CurrentItem { get; set; } = new();

    // Asset Commands
    public DelegateCommand LoadCommand { get; }
    public DelegateCommand SaveCommand { get; }
    public DelegateCommand SaveAsCommand { get; }
    public DelegateCommand ImportCommand { get; }

    // Handlers
    public FeatHandler FeatHandler { get; set; }
    public TraitHandler TraitHandler { get; set; }
    public ItemHandler ItemHandler { get; set; }
    public CraftHandler CraftHandler { get; set; }

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
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    

    #region Items

    // Filtered Item Collections
    public ObservableCollection<Item> FilteredItemList { get; set; } = new();
    public ObservableCollection<Item> FavoriteItemList { get; set; } = new();
    public ObservableCollection<Item> HiddenItemList { get; set; } = new();

    // Item Tag Collections
    public ObservableCollection<string> CustomItemFilterList { get; set; } = new();
    public ObservableCollection<string> CoreItemFilterList { get; set; } = new();
    public ObservableCollection<string> SkillItemFilterList { get; set; } = new();
    public ObservableCollection<string> ClassItemFilterList { get; set; } = new();
    public ObservableCollection<string> CombatItemFilterList { get; set; } = new();
    public ObservableCollection<string> RoleItemFilterList { get; set; } = new();
    public ObservableCollection<string> MagicItemFilterList { get; set; } = new();
    public ObservableCollection<string> BonusItemFilterList { get; set; } = new();
    public ObservableCollection<string> SourceItemFilterList { get; set; } = new();

    private Item? _selectedItem;

    public Item? SelectedItem
    {
        get => _selectedItem;
        set
        {
            if (value != null)
            {
                SelectedItem = null;
            }

            _selectedItem = value;
            CurrentItem.Clear();
            if (value != null)
            {
                CurrentItem.Add(value);
            }

            OnPropertyChanged("SelectedItem");
        }
    }

    private string _selectedItemReq;

    public string? SelectedItemReq
    {
        get => _selectedItemReq;
        set
        {
            _selectedItemReq = value ?? "";
            string sanitizedSelection = _selectedItemReq.Sanitize();
            foreach (Item possibleItem in MasterItemList)
            {
                if (possibleItem.Name.Sanitize() == sanitizedSelection)
                {
                    SelectedItem = possibleItem;
                    _selectedItemReq = "";
                }
            }

            OnPropertyChanged("SelectedItemReq");
        }
    }

    private string _itemSearchText = RegexHandler.SearchPlaceholderText;

    public string ItemSearchText
    {
        get => _itemSearchText;
        set
        {
            _itemSearchText = value;
            ItemHandler.ApplyItemFilters();

            OnPropertyChanged("ItemSearchText");
        }
    }

    // Item Checkbox Commands
    public DelegateCommand CoreItemCheckboxCommand { get; }
    public DelegateCommand SkillItemCheckboxCommand { get; }
    public DelegateCommand ClassItemCheckboxCommand { get; }
    public DelegateCommand CombatItemCheckboxCommand { get; }
    public DelegateCommand RoleItemCheckboxCommand { get; }
    public DelegateCommand MagicItemCheckboxCommand { get; }
    public DelegateCommand BonusItemCheckboxCommand { get; }
    public DelegateCommand SourceItemCheckboxCommand { get; }
    public DelegateCommand CustomItemCheckboxCommand { get; }
    public DelegateCommand ConditionItemCheckboxCommand { get; }

    // Item Control Bar Commands
    public DelegateCommand ItemSearchRemovePlaceholderTextCommand { get; }
    public DelegateCommand ItemSearchAddPlaceholderTextCommand { get; }
    public DelegateCommand AddFavoriteItemCommand { get; }
    public DelegateCommand AddHiddenItemCommand { get; }
    public DelegateCommand EditItemCommand { get; }
    public DelegateCommand NewItemCommand { get; }
    public DelegateCommand RemoveItemCommand { get; }
    public DelegateCommand RemoveFavoriteItemCommand { get; }
    public DelegateCommand RemoveHiddenItemCommand { get; }

    #endregion

    #region Crafting

    // Crafting Modifiers Collections
    public ObservableCollection<string> ActiveTool { get; set; } = new();
    public ObservableCollection<string> CraftingToolSelectionList { get; set; } = new();
    public ObservableCollection<string> ActiveWorkshop { get; set; } = new();
    public ObservableCollection<string> CraftingWorkshopSelectionList { get; set; } = new();

    private Item _craftItem = new();

    public Item? CraftItem
    {
        get => _craftItem;
        private set
        {
            _craftItem = value ?? throw new ArgumentNullException(nameof(value));
            OnPropertyChanged("SelectedItem");
        }
    }

    private string _craftObjectName = "Item Name...";

    public string CraftObjectName
    {
        get => _craftObjectName;
        set
        {
            _craftObjectName = value;
            CraftHandler.SetItemName(value);

            OnPropertyChanged("CraftObjectName");
        }
    }

    private int _craftRanks;

    public int CraftRanks
    {
        get => _craftRanks;
        set
        {
            _craftRanks = value;
            CraftHandler.SetCraftRanks(value);

            OnPropertyChanged("CraftRanks");
        }
    }

    private int _craftEnhancement;

    public int CraftEnhancement
    {
        get => _craftEnhancement;
        set
        {
            _craftEnhancement = value;
            CraftHandler.SetCraftEnhancementBonus(value);

            OnPropertyChanged("CraftEnhancement");
        }
    }

    private int _craftAlchemical;

    public int CraftAlchemical
    {
        get => _craftAlchemical;
        set
        {
            _craftAlchemical = value;
            CraftHandler.SetCraftAlchemicalBonus(value);

            OnPropertyChanged("CraftAlchemical");
        }
    }

    private string _craftModifier = "N/A";

    public string CraftModifier
    {
        get => _craftModifier;
        set
        {
            _craftModifier = value;
            OnPropertyChanged("CraftModifier");
        }
    }

    // Craft Checkbox Commands
    public DelegateCommand CraftToolCheckboxCommand { get; }
    public DelegateCommand CraftWorkshopCheckboxCommand { get; }
    public DelegateCommand CraftModifierBonusCalculatorCommand { get; }

    #endregion

    #region Menu

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

                        TraitHandler.UpdateTraitCustomTags();
                        TraitHandler.ApplyTraitFilters();
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

    private string _appMode = string.Empty;

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
            SetAppMode(value.StringToAppMode());
            OnPropertyChanged("AppMode");
        }
    }

    private void SetAppMode(AppMode mode)
    {
        ClearApp();

        switch (mode)
        {
            case Enums.AppMode.Pathfinder:
                InitializePathfinder();
                break;
            case Enums.AppMode.Havenheim:
                InitializeHavenheim();
                break;
        }
    }

    private void ClearApp()
    {
        TraitHandler.Clear();
        FeatHandler.Clear();
    }

    private void InitializePathfinder()
    {
        TraitHandler.InitializePathfinder();
        FeatHandler.InitializeHavenheim();
    }

    private void InitializeHavenheim()
    {
        TraitHandler.InitializeHavenheim();
        FeatHandler.InitializeHavenheim();
    }

    #endregion
}