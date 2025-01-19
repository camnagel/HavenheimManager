using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AssetManager.Calculators;
using AssetManager.Containers;

namespace AssetManager.Editors;

public class BonusCalcViewModel : INotifyPropertyChanged
{
    private BonusCalculator _bonusCalculator;

    private BonusSvp _selectedAlchemicalBonus;

    internal BonusCalcViewModel(BonusCalculator bonusCalculator)
    {
        _bonusCalculator = bonusCalculator;

        AddAlchemicalBonusCommand = new DelegateCommand(AddAlchemicalBonusAction);
        RemoveAlchemicalBonusCommand = new DelegateCommand(RemoveAlchemicalBonusAction);

        foreach (KeyValuePair<string, int> bonus in bonusCalculator.AlchemicalBonusList)
        {
            AlchemicalBonuses.Add(new BonusSvp(bonus.Key, bonus.Value));
        }
    }

    public BonusSvp? SelectedAlchemicalBonus
    {
        get => _selectedAlchemicalBonus;
        set
        {
            _selectedAlchemicalBonus = value;
            OnPropertyChanged("SelectedAlchemicalBonus");
        }
    }

    public DelegateCommand AddAlchemicalBonusCommand { get; }

    public DelegateCommand RemoveAlchemicalBonusCommand { get; }

    private void AddAlchemicalBonusAction(object arg)
    {
        AlchemicalBonuses.Add(new BonusSvp("Source", 0));
    }

    private void RemoveAlchemicalBonusAction(object arg)
    {
        if (SelectedAlchemicalBonus != null)
        {
            AlchemicalBonuses.Remove(SelectedAlchemicalBonus);
            SelectedAlchemicalBonus = null;
        }
    }

    public ObservableCollection<BonusSvp> AlchemicalBonuses { get; set; } = new();

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}