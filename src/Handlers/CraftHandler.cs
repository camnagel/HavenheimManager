using AssetManager.Calculators;
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