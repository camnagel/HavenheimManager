using System.Linq;
using System.Windows;
using System;
using AssetManager.Calculators;
using AssetManager.Containers;
using AssetManager.Editors;
using AssetManager.Enums;
using AssetManager.Extensions;

namespace AssetManager.Handlers;

public class CraftHandler
{
    private readonly BonusCalculator _craftingModifierCalculator;
    private readonly MainWindowViewModel _vm;

    private string _itemName;

    public CraftHandler(MainWindowViewModel vm)
    {
        _vm = vm;
        _craftingModifierCalculator = new BonusCalculator();
    }

    internal void SetItemName(string itemName)
    {
        _itemName = itemName;
    }

    internal void SetCraftRanks(int ranks)
    {
        _craftingModifierCalculator.AddBonus(Bonus.Untyped, "Skill Ranks", ranks);
        UpdateCraftingModifier();
    }

    private Tool _currentTool = Tool.None;

    internal void CraftToolAction(object arg)
    {
        if (arg is string toolAsString)
        {
            Tool tool = toolAsString.StringToTool();
            _currentTool = tool;
            _craftingModifierCalculator.AddCircumstanceBonus(nameof(Tool), toolAsString, tool.ToolToBonus());
            UpdateCraftingModifier();
        }
    }

    internal void CraftWorkshopAction(object arg)
    {
        if (arg is string workshopAsString)
        {
            Workshop workshop = workshopAsString.StringToWorkshop();
            _craftingModifierCalculator.AddCircumstanceBonus(nameof(Workshop), workshopAsString,
                workshop.WorkshopToBonus());
            UpdateCraftingModifier();
        }
    }

    internal void CraftModifierBonusCalcAction(object arg)
    {
        try
        {
            var vm = new BonusCalcViewModel(_craftingModifierCalculator);
            var configWindow = new BonusCalcView(vm);

            if (configWindow.ShowDialog() == true)
            {
                // Update calculator values
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

    private void UpdateCraftingModifier()
    {
        if (_currentTool == Tool.None)
        {
            _vm.CraftModifier = "N/A";
        }
        else {
            _vm.CraftModifier = _craftingModifierCalculator.CurrentBonus.ToString();
        }
    }
}