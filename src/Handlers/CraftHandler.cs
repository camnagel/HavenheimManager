using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using HavenheimManager.Calculators;
using HavenheimManager.Containers;
using HavenheimManager.Editors;
using HavenheimManager.Enums;
using HavenheimManager.Extensions;

namespace HavenheimManager.Handlers;

public class CraftHandler : INotifyPropertyChanged
{
    private readonly BonusCalculator _craftingModifierCalculator;

    private int _craftAlchemical;

    private int _craftEnhancement;

    private Item _craftItem = new();

    private string _craftModifier = "N/A";

    private string _craftObjectName = "Item Name...";

    private int _craftRanks;

    private Tool _currentTool = Tool.None;

    private string _itemName;

    public CraftHandler()
    {
        _craftingModifierCalculator = new BonusCalculator();

        CraftToolCheckboxCommand = new DelegateCommand(CraftToolAction);
        CraftWorkshopCheckboxCommand = new DelegateCommand(CraftWorkshopAction);
        CraftModifierBonusCalculatorCommand = new DelegateCommand(CraftModifierBonusCalcAction);
    }

    // Craft Checkbox Commands
    public DelegateCommand CraftToolCheckboxCommand { get; }
    public DelegateCommand CraftWorkshopCheckboxCommand { get; }
    public DelegateCommand CraftModifierBonusCalculatorCommand { get; }

    // Crafting Modifiers Collections
    public ObservableCollection<string> ActiveTool { get; set; } = new();
    public ObservableCollection<string> CraftingToolSelectionList { get; set; } = new();
    public ObservableCollection<string> ActiveWorkshop { get; set; } = new();
    public ObservableCollection<string> CraftingWorkshopSelectionList { get; set; } = new();

    public Item? CraftItem
    {
        get => _craftItem;
        private set
        {
            _craftItem = value ?? throw new ArgumentNullException(nameof(value));
            OnPropertyChanged("SelectedItem");
        }
    }

    public string CraftObjectName
    {
        get => _craftObjectName;
        set
        {
            _craftObjectName = value;
            SetItemName(value);

            OnPropertyChanged("CraftObjectName");
        }
    }

    public int CraftRanks
    {
        get => _craftRanks;
        set
        {
            _craftRanks = value;
            SetCraftRanks(value);

            OnPropertyChanged("CraftRanks");
        }
    }

    public int CraftEnhancement
    {
        get => _craftEnhancement;
        set
        {
            _craftEnhancement = value;
            SetCraftEnhancementBonus(value);

            OnPropertyChanged("CraftEnhancement");
        }
    }

    public int CraftAlchemical
    {
        get => _craftAlchemical;
        set
        {
            _craftAlchemical = value;
            SetCraftAlchemicalBonus(value);

            OnPropertyChanged("CraftAlchemical");
        }
    }

    public string CraftModifier
    {
        get => _craftModifier;
        set
        {
            _craftModifier = value;
            OnPropertyChanged("CraftModifier");
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    internal void SetItemName(string itemName)
    {
        _itemName = itemName;
    }

    internal void SetCraftRanks(int ranks)
    {
        _craftingModifierCalculator.AddBonus(Bonus.Untyped, "Skill Ranks", ranks);
        UpdateCraftingModifier();
    }

    internal void SetCraftEnhancementBonus(int value)
    {
        _craftingModifierCalculator.AddBonus(Bonus.Enhancement, "Crafting Tab", value);
        UpdateCraftingModifier();
    }

    internal void SetCraftAlchemicalBonus(int value)
    {
        _craftingModifierCalculator.AddBonus(Bonus.Alchemical, "Crafting Tab", value);
        UpdateCraftingModifier();
    }

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
            BonusCalcViewModel vm = new(_craftingModifierCalculator);
            BonusCalcView configWindow = new(vm);

            if (configWindow.ShowDialog() == true)
            {
                UpdateCraftingModifier();
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

    internal void Clear()
    {
    }

    internal void InitializePathfinder()
    {
    }

    internal void InitializeHavenheim()
    {
    }

    public void RefreshButtonState()
    {
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void UpdateCraftingModifier()
    {
        if (_currentTool == Tool.None)
        {
            CraftModifier = "N/A";
        }
        else
        {
            CraftModifier = _craftingModifierCalculator.CurrentBonus.ToString();
        }
    }
}