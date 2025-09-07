using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using AssetManager.Containers;
using AssetManager.Editors;
using AssetManager.Enums;
using AssetManager.Extensions;
using Condition = AssetManager.Enums.Condition;

namespace AssetManager.Handlers;

public class ItemHandler
{
    private readonly MainWindowViewModel _vm;

    // Item Filter Lists
    private readonly HashSet<Bonus> BonusItemFilters = new();
    private readonly HashSet<Class> ClassItemFilters = new();
    private readonly HashSet<Combat> CombatItemFilters = new();
    private readonly HashSet<Condition> ConditionItemFilters = new();
    private readonly HashSet<Core> CoreItemFilters = new();
    private readonly HashSet<string> CustomItemFilters = new();
    private readonly HashSet<Magic> MagicItemFilters = new();
    private readonly HashSet<Role> RoleItemFilters = new();
    private readonly HashSet<Skill> SkillItemFilters = new();
    private readonly HashSet<Source> SourceItemFilters = new();

    public ItemHandler(MainWindowViewModel vm)
    {
        _vm = vm;
    }

    // Item Checkbox Actions
    internal void ItemCoreFilterAction(object arg)
    {
        if (arg is string filter)
        {
            Core toggleCore = filter.StringToCore();

            if (CoreItemFilters.Contains(toggleCore))
            {
                CoreItemFilters.Remove(toggleCore);
            }
            else
            {
                CoreItemFilters.Add(toggleCore);
            }

            ApplyItemFilters();
        }
    }

    internal void ItemSkillFilterAction(object arg)
    {
        if (arg is string filter)
        {
            Skill toggleSkill = filter.StringToSkill();

            if (SkillItemFilters.Contains(toggleSkill))
            {
                SkillItemFilters.Remove(toggleSkill);
            }
            else
            {
                SkillItemFilters.Add(toggleSkill);
            }

            ApplyItemFilters();
        }
    }

    internal void ItemClassFilterAction(object arg)
    {
        if (arg is string filter)
        {
            Class toggleClass = filter.StringToClass();

            if (ClassItemFilters.Contains(toggleClass))
            {
                ClassItemFilters.Remove(toggleClass);
            }
            else
            {
                ClassItemFilters.Add(toggleClass);
            }

            ApplyItemFilters();
        }
    }

    internal void ItemCombatFilterAction(object arg)
    {
        if (arg is string filter)
        {
            Combat toggleCombat = filter.StringToCombat();

            if (CombatItemFilters.Contains(toggleCombat))
            {
                CombatItemFilters.Remove(toggleCombat);
            }
            else
            {
                CombatItemFilters.Add(toggleCombat);
            }

            ApplyItemFilters();
        }
    }

    internal void ItemRoleFilterAction(object arg)
    {
        if (arg is string filter)
        {
            Role toggleRole = filter.StringToRole();

            if (RoleItemFilters.Contains(toggleRole))
            {
                RoleItemFilters.Remove(toggleRole);
            }
            else
            {
                RoleItemFilters.Add(toggleRole);
            }

            ApplyItemFilters();
        }
    }

    internal void ItemMagicFilterAction(object arg)
    {
        if (arg is string filter)
        {
            Magic toggleMagic = filter.StringToMagic();

            if (MagicItemFilters.Contains(toggleMagic))
            {
                MagicItemFilters.Remove(toggleMagic);
            }
            else
            {
                MagicItemFilters.Add(toggleMagic);
            }

            ApplyItemFilters();
        }
    }

    internal void ItemBonusFilterAction(object arg)
    {
        if (arg is string filter)
        {
            Bonus toggleBonus = filter.StringToBonus();

            if (BonusItemFilters.Contains(toggleBonus))
            {
                BonusItemFilters.Remove(toggleBonus);
            }
            else
            {
                BonusItemFilters.Add(toggleBonus);
            }

            ApplyItemFilters();
        }
    }

    internal void ItemSourceFilterAction(object arg)
    {
        if (arg is string filter)
        {
            Source toggleSource = filter.StringToSource();

            if (SourceItemFilters.Contains(toggleSource))
            {
                SourceItemFilters.Remove(toggleSource);
            }
            else
            {
                SourceItemFilters.Add(toggleSource);
            }

            ApplyItemFilters();
        }
    }

    internal void ItemCustomFilterAction(object arg)
    {
        if (arg is string filter)
        {
            if (CustomItemFilters.Contains(filter))
            {
                CustomItemFilters.Remove(filter);
            }
            else
            {
                CustomItemFilters.Add(filter);
            }

            ApplyItemFilters();
        }
    }

    internal void ItemConditionFilterAction(object arg)
    {
        if (arg is string filter)
        {
            Condition toggleCondition = filter.StringToCondition();

            if (ConditionItemFilters.Contains(toggleCondition))
            {
                ConditionItemFilters.Remove(toggleCondition);
            }
            else
            {
                ConditionItemFilters.Add(toggleCondition);
            }

            ApplyItemFilters();
        }
    }

    internal void ApplyItemFilters()
    {
        _vm.FilteredItemList.Clear();
        List<Item> possibleItems = _vm.ItemSearchText != RegexHandler.SearchPlaceholderText && _vm.ItemSearchText != ""
            ? _vm.MasterItemList.Where(x => x.Name.Sanitize()
                .Contains(_vm.ItemSearchText.Sanitize())).ToList()
            : _vm.MasterItemList;

        foreach (Core filter in CoreItemFilters)
        {
            possibleItems = possibleItems.Where(x => x.CoreTags.Contains(filter)).ToList();
        }

        foreach (Skill filter in SkillItemFilters)
        {
            possibleItems = possibleItems.Where(x => x.SkillTags.Contains(filter)).ToList();
        }

        foreach (Class filter in ClassItemFilters)
        {
            possibleItems = possibleItems.Where(x => x.ClassTags.Contains(filter)).ToList();
        }

        foreach (Combat filter in CombatItemFilters)
        {
            possibleItems = possibleItems.Where(x => x.CombatTags.Contains(filter)).ToList();
        }

        foreach (Role filter in RoleItemFilters)
        {
            possibleItems = possibleItems.Where(x => x.RoleTags.Contains(filter)).ToList();
        }

        foreach (Magic filter in MagicItemFilters)
        {
            possibleItems = possibleItems.Where(x => x.MagicTags.Contains(filter)).ToList();
        }

        foreach (Bonus filter in BonusItemFilters)
        {
            possibleItems = possibleItems.Where(x => x.BonusTags.Contains(filter)).ToList();
        }

        foreach (Source filter in SourceItemFilters)
        {
            possibleItems = possibleItems.Where(x => x.Source == filter).ToList();
        }

        foreach (string filter in CustomItemFilters)
        {
            possibleItems = possibleItems.Where(x => x.CustomTags.Contains(filter)).ToList();
        }

        foreach (Item Item in possibleItems)
        {
            _vm.FilteredItemList.Add(Item);
        }
    }

    internal void UpdateItemCustomTags()
    {
        _vm.CustomItemFilterList.Clear();
        foreach (Item Item in _vm.MasterItemList)
        {
            foreach (string tag in Item.CustomTags)
            {
                if (!_vm.CustomItemFilterList.Contains(tag))
                {
                    _vm.CustomItemFilterList.Add(tag);
                }
            }
        }
    }

    internal void AddFavoriteItemAction(object arg)
    {
        if (_vm.SelectedItem != null && !_vm.FavoriteItemList.Contains(_vm.SelectedItem))
        {
            _vm.FavoriteItemList.Add(_vm.SelectedItem);
            _vm.MasterItemList.Remove(_vm.SelectedItem);
            _vm.HiddenItemList.Remove(_vm.SelectedItem);

            UpdateItemCustomTags();
            ApplyItemFilters();
            _vm.SelectedItem = null;
        }
    }

    internal void AddHiddenItemAction(object arg)
    {
        if (_vm.SelectedItem != null && !_vm.HiddenItemList.Contains(_vm.SelectedItem))
        {
            _vm.HiddenItemList.Add(_vm.SelectedItem);
            _vm.MasterItemList.Remove(_vm.SelectedItem);
            _vm.FavoriteItemList.Remove(_vm.SelectedItem);

            UpdateItemCustomTags();
            ApplyItemFilters();
            _vm.SelectedItem = null;
        }
    }

    internal void EditItemAction(object arg)
    {
        try
        {
            if (_vm.SelectedItem != null)
            {
                ItemViewModel vm = new(_vm.SelectedItem);
                ItemView configWindow = new(vm);

                if (configWindow.ShowDialog() == true)
                {
                    Item newItem = vm.GetItem();

                    if (_vm.MasterItemList.Contains(_vm.SelectedItem))
                    {
                        _vm.MasterItemList.Remove(_vm.SelectedItem);
                        _vm.MasterItemList.Add(newItem);

                        UpdateItemCustomTags();
                        ApplyItemFilters();
                        if (_vm.FilteredItemList.Contains(newItem))
                        {
                            _vm.SelectedItem = newItem;
                        }
                    }

                    if (_vm.FavoriteItemList.Contains(_vm.SelectedItem))
                    {
                        _vm.FavoriteItemList.Remove(_vm.SelectedItem);
                        _vm.FavoriteItemList.Add(newItem);
                        _vm.SelectedItem = newItem;
                    }

                    else if (_vm.HiddenItemList.Contains(_vm.SelectedItem))
                    {
                        _vm.HiddenItemList.Remove(_vm.SelectedItem);
                        _vm.HiddenItemList.Add(newItem);
                        _vm.SelectedItem = newItem;
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

        _vm.RefreshButtonState();
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
                if (_vm.MasterItemList.Select(x => x.Name).Contains(newItem.Name) ||
                    _vm.FavoriteItemList.Select(x => x.Name).Contains(newItem.Name) ||
                    _vm.HiddenItemList.Select(x => x.Name).Contains(newItem.Name))
                {
                    string messageBoxText = "Item with same name already exists";
                    string caption = "Duplicate";
                    MessageBoxButton button = MessageBoxButton.OK;
                    MessageBoxImage icon = MessageBoxImage.Exclamation;
                    MessageBox.Show(messageBoxText, caption, button, icon);
                }
                else
                {
                    _vm.MasterItemList.Add(newItem);

                    UpdateItemCustomTags();
                    ApplyItemFilters();
                    if (_vm.FilteredItemList.Contains(newItem))
                    {
                        _vm.SelectedItem = newItem;
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

        _vm.RefreshButtonState();
    }

    internal void ItemSearchRemovePlaceholderTextAction(object arg)
    {
        if (_vm.ItemSearchText == RegexHandler.SearchPlaceholderText)
        {
            _vm.ItemSearchText = "";
        }
    }

    internal void ItemSearchAddPlaceholderTextAction(object arg)
    {
        if (string.IsNullOrWhiteSpace(_vm.ItemSearchText))
        {
            _vm.ItemSearchText = RegexHandler.SearchPlaceholderText;
        }
    }

    internal void RemoveItemAction(object arg)
    {
        if (_vm.SelectedItem != null)
        {
            string messageBoxText = "Item will be removed. Are you sure?";
            string caption = "Warning";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            if (result == MessageBoxResult.Yes)
            {
                _vm.MasterItemList.Remove(_vm.SelectedItem);
                _vm.HiddenItemList.Remove(_vm.SelectedItem);
                _vm.FavoriteItemList.Remove(_vm.SelectedItem);
                _vm.SelectedItem = null;
                UpdateItemCustomTags();
                ApplyItemFilters();
            }
        }
    }

    internal void RemoveFavoriteItemAction(object arg)
    {
        if (_vm.SelectedItem != null && _vm.FavoriteItemList.Contains(_vm.SelectedItem))
        {
            _vm.MasterItemList.Add(_vm.SelectedItem);
            _vm.FavoriteItemList.Remove(_vm.SelectedItem);

            _vm.SelectedItem = null;
            UpdateItemCustomTags();
            ApplyItemFilters();
        }
    }

    internal void RemoveHiddenItemAction(object arg)
    {
        if (_vm.SelectedItem != null && _vm.HiddenItemList.Contains(_vm.SelectedItem))
        {
            _vm.MasterItemList.Add(_vm.SelectedItem);
            _vm.HiddenItemList.Remove(_vm.SelectedItem);

            _vm.SelectedItem = null;
            UpdateItemCustomTags();
            ApplyItemFilters();
        }
    }
}