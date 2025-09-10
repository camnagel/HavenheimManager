using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using HavenheimManager.Containers;
using HavenheimManager.Editors;
using HavenheimManager.Enums;
using HavenheimManager.Extensions;
using Condition = HavenheimManager.Enums.Condition;

namespace HavenheimManager.Handlers;

public class ItemHandler : INotifyPropertyChanged
{
    // Item Filter Lists
    private readonly HashSet<Bonus> _bonusItemFilters = new();
    private readonly HashSet<Class> _classItemFilters = new();
    private readonly HashSet<Combat> _combatItemFilters = new();
    private readonly HashSet<Condition> _conditionItemFilters = new();
    private readonly HashSet<Core> _coreItemFilters = new();
    private readonly HashSet<string> _customItemFilters = new();
    private readonly HashSet<Magic> _magicItemFilters = new();
    private readonly HashSet<Role> _roleItemFilters = new();
    private readonly HashSet<Skill> _skillItemFilters = new();
    private readonly HashSet<Source> _sourceItemFilters = new();

    private string _itemSearchText = RegexHandler.SearchPlaceholderText;

    private Item? _selectedItem;

    private string _selectedItemReq;

    internal void Clear()
    {
        CoreItemFilterList.Clear();
        SourceItemFilterList.Clear();
        SkillItemFilterList.Clear();
        CombatItemFilterList.Clear();
        RoleItemFilterList.Clear();
        MagicItemFilterList.Clear();
        BonusItemFilterList.Clear();
        ConditionItemFilterList.Clear();
        ClassItemFilterList.Clear();
    }

    internal void InitializePathfinder()
    {

    }

    internal void InitializeHavenheim()
    {
        CoreItemFilterList.Fill<Core>(typeof(Core));
        SourceItemFilterList.Fill<Source>(typeof(Source));
        SkillItemFilterList.Fill<Skill>(typeof(Skill));
        CombatItemFilterList.Fill<Combat>(typeof(Combat));
        RoleItemFilterList.Fill<Role>(typeof(Role));
        MagicItemFilterList.Fill<Magic>(typeof(Magic));
        BonusItemFilterList.Fill<Bonus>(typeof(Bonus));
        ConditionItemFilterList.Fill<Condition>(typeof(Condition));
        ClassItemFilterList.Fill<Class>(typeof(Class));
    }

    public ItemHandler()
    {
        CoreItemCheckboxCommand = new DelegateCommand(ItemCoreFilterAction);
        SkillItemCheckboxCommand = new DelegateCommand(ItemSkillFilterAction);
        ClassItemCheckboxCommand = new DelegateCommand(ItemClassFilterAction);
        CombatItemCheckboxCommand = new DelegateCommand(ItemCombatFilterAction);
        RoleItemCheckboxCommand = new DelegateCommand(ItemRoleFilterAction);
        MagicItemCheckboxCommand = new DelegateCommand(ItemMagicFilterAction);
        BonusItemCheckboxCommand = new DelegateCommand(ItemBonusFilterAction);
        ConditionItemCheckboxCommand = new DelegateCommand(ItemConditionFilterAction);
        SourceItemCheckboxCommand = new DelegateCommand(ItemSourceFilterAction);
        CustomItemCheckboxCommand = new DelegateCommand(ItemCustomFilterAction);
        AddFavoriteItemCommand = new DelegateCommand(AddFavoriteItemAction);
        AddHiddenItemCommand = new DelegateCommand(AddHiddenItemAction);
        EditItemCommand = new DelegateCommand(EditItemAction);
        NewItemCommand = new DelegateCommand(NewItemAction);
        RemoveItemCommand = new DelegateCommand(RemoveItemAction);
        RemoveFavoriteItemCommand = new DelegateCommand(RemoveFavoriteItemAction);
        RemoveHiddenItemCommand = new DelegateCommand(RemoveHiddenItemAction);
        ItemSearchRemovePlaceholderTextCommand = new DelegateCommand(ItemSearchRemovePlaceholderTextAction);
        ItemSearchAddPlaceholderTextCommand = new DelegateCommand(ItemSearchAddPlaceholderTextAction);
    }

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
    public ObservableCollection<string> ConditionItemFilterList { get; set; } = new();

    public ObservableCollection<Item> CurrentItem { get; set; } = new();

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

    public string ItemSearchText
    {
        get => _itemSearchText;
        set
        {
            _itemSearchText = value;
            ApplyItemFilters();

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

    public List<Item> MasterItemList { get; } = new();

    public event PropertyChangedEventHandler? PropertyChanged;

    // Item Checkbox Actions
    internal void ItemCoreFilterAction(object arg)
    {
        if (arg is string filter)
        {
            Core toggleCore = filter.StringToCore();

            if (_coreItemFilters.Contains(toggleCore))
            {
                _coreItemFilters.Remove(toggleCore);
            }
            else
            {
                _coreItemFilters.Add(toggleCore);
            }

            ApplyItemFilters();
        }
    }

    internal void ItemSkillFilterAction(object arg)
    {
        if (arg is string filter)
        {
            Skill toggleSkill = filter.StringToSkill();

            if (_skillItemFilters.Contains(toggleSkill))
            {
                _skillItemFilters.Remove(toggleSkill);
            }
            else
            {
                _skillItemFilters.Add(toggleSkill);
            }

            ApplyItemFilters();
        }
    }

    internal void ItemClassFilterAction(object arg)
    {
        if (arg is string filter)
        {
            Class toggleClass = filter.StringToClass();

            if (_classItemFilters.Contains(toggleClass))
            {
                _classItemFilters.Remove(toggleClass);
            }
            else
            {
                _classItemFilters.Add(toggleClass);
            }

            ApplyItemFilters();
        }
    }

    internal void ItemCombatFilterAction(object arg)
    {
        if (arg is string filter)
        {
            Combat toggleCombat = filter.StringToCombat();

            if (_combatItemFilters.Contains(toggleCombat))
            {
                _combatItemFilters.Remove(toggleCombat);
            }
            else
            {
                _combatItemFilters.Add(toggleCombat);
            }

            ApplyItemFilters();
        }
    }

    internal void ItemRoleFilterAction(object arg)
    {
        if (arg is string filter)
        {
            Role toggleRole = filter.StringToRole();

            if (_roleItemFilters.Contains(toggleRole))
            {
                _roleItemFilters.Remove(toggleRole);
            }
            else
            {
                _roleItemFilters.Add(toggleRole);
            }

            ApplyItemFilters();
        }
    }

    internal void ItemMagicFilterAction(object arg)
    {
        if (arg is string filter)
        {
            Magic toggleMagic = filter.StringToMagic();

            if (_magicItemFilters.Contains(toggleMagic))
            {
                _magicItemFilters.Remove(toggleMagic);
            }
            else
            {
                _magicItemFilters.Add(toggleMagic);
            }

            ApplyItemFilters();
        }
    }

    internal void ItemBonusFilterAction(object arg)
    {
        if (arg is string filter)
        {
            Bonus toggleBonus = filter.StringToBonus();

            if (_bonusItemFilters.Contains(toggleBonus))
            {
                _bonusItemFilters.Remove(toggleBonus);
            }
            else
            {
                _bonusItemFilters.Add(toggleBonus);
            }

            ApplyItemFilters();
        }
    }

    internal void ItemSourceFilterAction(object arg)
    {
        if (arg is string filter)
        {
            Source toggleSource = filter.StringToSource();

            if (_sourceItemFilters.Contains(toggleSource))
            {
                _sourceItemFilters.Remove(toggleSource);
            }
            else
            {
                _sourceItemFilters.Add(toggleSource);
            }

            ApplyItemFilters();
        }
    }

    internal void ItemCustomFilterAction(object arg)
    {
        if (arg is string filter)
        {
            if (_customItemFilters.Contains(filter))
            {
                _customItemFilters.Remove(filter);
            }
            else
            {
                _customItemFilters.Add(filter);
            }

            ApplyItemFilters();
        }
    }

    internal void ItemConditionFilterAction(object arg)
    {
        if (arg is string filter)
        {
            Condition toggleCondition = filter.StringToCondition();

            if (_conditionItemFilters.Contains(toggleCondition))
            {
                _conditionItemFilters.Remove(toggleCondition);
            }
            else
            {
                _conditionItemFilters.Add(toggleCondition);
            }

            ApplyItemFilters();
        }
    }

    internal void ApplyItemFilters()
    {
        FilteredItemList.Clear();
        List<Item> possibleItems = ItemSearchText != RegexHandler.SearchPlaceholderText && ItemSearchText != ""
            ? MasterItemList.Where(x => x.Name.Sanitize()
                .Contains(ItemSearchText.Sanitize())).ToList()
            : MasterItemList;

        foreach (Core filter in _coreItemFilters)
        {
            possibleItems = possibleItems.Where(x => x.CoreTags.Contains(filter)).ToList();
        }

        foreach (Skill filter in _skillItemFilters)
        {
            possibleItems = possibleItems.Where(x => x.SkillTags.Contains(filter)).ToList();
        }

        foreach (Class filter in _classItemFilters)
        {
            possibleItems = possibleItems.Where(x => x.ClassTags.Contains(filter)).ToList();
        }

        foreach (Combat filter in _combatItemFilters)
        {
            possibleItems = possibleItems.Where(x => x.CombatTags.Contains(filter)).ToList();
        }

        foreach (Role filter in _roleItemFilters)
        {
            possibleItems = possibleItems.Where(x => x.RoleTags.Contains(filter)).ToList();
        }

        foreach (Magic filter in _magicItemFilters)
        {
            possibleItems = possibleItems.Where(x => x.MagicTags.Contains(filter)).ToList();
        }

        foreach (Bonus filter in _bonusItemFilters)
        {
            possibleItems = possibleItems.Where(x => x.BonusTags.Contains(filter)).ToList();
        }

        foreach (Source filter in _sourceItemFilters)
        {
            possibleItems = possibleItems.Where(x => x.Source == filter).ToList();
        }

        foreach (string filter in _customItemFilters)
        {
            possibleItems = possibleItems.Where(x => x.CustomTags.Contains(filter)).ToList();
        }

        foreach (Item Item in possibleItems)
        {
            FilteredItemList.Add(Item);
        }
    }

    internal void UpdateItemCustomTags()
    {
        CustomItemFilterList.Clear();
        foreach (Item Item in MasterItemList)
        {
            foreach (string tag in Item.CustomTags)
            {
                if (!CustomItemFilterList.Contains(tag))
                {
                    CustomItemFilterList.Add(tag);
                }
            }
        }
    }

    internal void AddFavoriteItemAction(object arg)
    {
        if (SelectedItem != null && !FavoriteItemList.Contains(SelectedItem))
        {
            FavoriteItemList.Add(SelectedItem);
            MasterItemList.Remove(SelectedItem);
            HiddenItemList.Remove(SelectedItem);

            UpdateItemCustomTags();
            ApplyItemFilters();
            SelectedItem = null;
        }
    }

    internal void AddHiddenItemAction(object arg)
    {
        if (SelectedItem != null && !HiddenItemList.Contains(SelectedItem))
        {
            HiddenItemList.Add(SelectedItem);
            MasterItemList.Remove(SelectedItem);
            FavoriteItemList.Remove(SelectedItem);

            UpdateItemCustomTags();
            ApplyItemFilters();
            SelectedItem = null;
        }
    }

    internal void EditItemAction(object arg)
    {
        try
        {
            if (SelectedItem != null)
            {
                ItemViewModel vm = new(SelectedItem);
                ItemView configWindow = new(vm);

                if (configWindow.ShowDialog() == true)
                {
                    Item newItem = vm.GetItem();

                    if (MasterItemList.Contains(SelectedItem))
                    {
                        MasterItemList.Remove(SelectedItem);
                        MasterItemList.Add(newItem);

                        UpdateItemCustomTags();
                        ApplyItemFilters();
                        if (FilteredItemList.Contains(newItem))
                        {
                            SelectedItem = newItem;
                        }
                    }

                    if (FavoriteItemList.Contains(SelectedItem))
                    {
                        FavoriteItemList.Remove(SelectedItem);
                        FavoriteItemList.Add(newItem);
                        SelectedItem = newItem;
                    }

                    else if (HiddenItemList.Contains(SelectedItem))
                    {
                        HiddenItemList.Remove(SelectedItem);
                        HiddenItemList.Add(newItem);
                        SelectedItem = newItem;
                    }
                }
            }
            else
            {
                string messageBoxText = "No Item selected to edit";
                string caption = "Select Item";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Exclamation;
                MessageBox.Show(messageBoxText, caption, button, icon);
            }
        }
        catch (ArgumentOutOfRangeException)
        {
            string messageBoxText = "Exception when adding Item";
            string caption = "Exception";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Error;
            MessageBox.Show(messageBoxText, caption, button, icon);
        }

        RefreshButtonState();
    }

    public void RefreshButtonState()
    {
        CoreItemCheckboxCommand.RaiseCanExecuteChanged();
    }

    internal void NewItemAction(object arg)
    {
        try
        {
            ItemViewModel vm = new(new Item());
            ItemView configWindow = new(vm);

            if (configWindow.ShowDialog() == true)
            {
                Item newItem = vm.GetItem();
                if (MasterItemList.Select(x => x.Name).Contains(newItem.Name) ||
                    FavoriteItemList.Select(x => x.Name).Contains(newItem.Name) ||
                    HiddenItemList.Select(x => x.Name).Contains(newItem.Name))
                {
                    string messageBoxText = "Item with same name already exists";
                    string caption = "Duplicate";
                    MessageBoxButton button = MessageBoxButton.OK;
                    MessageBoxImage icon = MessageBoxImage.Exclamation;
                    MessageBox.Show(messageBoxText, caption, button, icon);
                }
                else
                {
                    MasterItemList.Add(newItem);

                    UpdateItemCustomTags();
                    ApplyItemFilters();
                    if (FilteredItemList.Contains(newItem))
                    {
                        SelectedItem = newItem;
                    }
                }
            }
        }
        catch (ArgumentOutOfRangeException)
        {
            string messageBoxText = "Exception when adding Item";
            string caption = "Exception";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Error;
            MessageBox.Show(messageBoxText, caption, button, icon);
        }

        RefreshButtonState();
    }

    internal void ItemSearchRemovePlaceholderTextAction(object arg)
    {
        if (ItemSearchText == RegexHandler.SearchPlaceholderText)
        {
            ItemSearchText = "";
        }
    }

    internal void ItemSearchAddPlaceholderTextAction(object arg)
    {
        if (string.IsNullOrWhiteSpace(ItemSearchText))
        {
            ItemSearchText = RegexHandler.SearchPlaceholderText;
        }
    }

    internal void RemoveItemAction(object arg)
    {
        if (SelectedItem != null)
        {
            string messageBoxText = "Item will be removed. Are you sure?";
            string caption = "Warning";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            if (result == MessageBoxResult.Yes)
            {
                MasterItemList.Remove(SelectedItem);
                HiddenItemList.Remove(SelectedItem);
                FavoriteItemList.Remove(SelectedItem);
                SelectedItem = null;
                UpdateItemCustomTags();
                ApplyItemFilters();
            }
        }
    }

    internal void RemoveFavoriteItemAction(object arg)
    {
        if (SelectedItem != null && FavoriteItemList.Contains(SelectedItem))
        {
            MasterItemList.Add(SelectedItem);
            FavoriteItemList.Remove(SelectedItem);

            SelectedItem = null;
            UpdateItemCustomTags();
            ApplyItemFilters();
        }
    }

    internal void RemoveHiddenItemAction(object arg)
    {
        if (SelectedItem != null && HiddenItemList.Contains(SelectedItem))
        {
            MasterItemList.Add(SelectedItem);
            HiddenItemList.Remove(SelectedItem);

            SelectedItem = null;
            UpdateItemCustomTags();
            ApplyItemFilters();
        }
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}