using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using HavenheimManager.Calculators;
using HavenheimManager.Containers;
using HavenheimManager.Enums;
using HavenheimManager.Extensions;

namespace HavenheimManager.Editors;

public class BonusCalcViewModel : INotifyPropertyChanged
{
    private readonly BonusCalculator _bonusCalculator;

    private int _bonusTotal;

    private BonusTsv _selectedActiveBonus;

    private BonusSvp _selectedAlchemicalBonus;

    private BonusSvp _selectedArmorBonus;

    private BonusSvp _selectedBabBonus;

    private BonusScv _selectedCircumstanceBonus;

    private BonusSvp _selectedCompetenceBonus;

    private BonusSvp _selectedDeflectionBonus;

    private BonusSvp _selectedDodgeBonus;

    private BonusSvp _selectedEnhancementBonus;

    private BonusSvp _selectedInherentBonus;

    private BonusSvp _selectedInsightBonus;

    private BonusSvp _selectedLuckBonus;

    private BonusSvp _selectedMoraleBonus;

    private BonusScv _selectedNaturalArmorBonus;

    private BonusSvp _selectedProfaneBonus;

    private BonusSvp _selectedRacialBonus;

    private BonusSvp _selectedResistanceBonus;

    private BonusSvp _selectedSacredBonus;

    private BonusSvp _selectedShieldBonus;

    private BonusSvp _selectedSizeBonus;

    private BonusSvp _selectedTraitBonus;

    private BonusSvp _selectedUntypedBonus;

    internal BonusCalcViewModel(BonusCalculator bonusCalculator)
    {
        _bonusCalculator = bonusCalculator;

        CancelCommand = new DelegateCommand(CancelAction);
        AcceptChangesCommand = new DelegateCommand(AcceptChangesAction);
        AddAlchemicalBonusCommand = new DelegateCommand(AddAlchemicalBonusAction);
        RemoveAlchemicalBonusCommand = new DelegateCommand(RemoveAlchemicalBonusAction);
        AddArmorBonusCommand = new DelegateCommand(AddArmorBonusAction);
        RemoveArmorBonusCommand = new DelegateCommand(RemoveArmorBonusAction);
        AddBabBonusCommand = new DelegateCommand(AddBabBonusAction);
        RemoveBabBonusCommand = new DelegateCommand(RemoveBabBonusAction);
        AddCompetenceBonusCommand = new DelegateCommand(AddCompetenceBonusAction);
        RemoveCompetenceBonusCommand = new DelegateCommand(RemoveCompetenceBonusAction);
        AddEnhancementBonusCommand = new DelegateCommand(AddEnhancementBonusAction);
        RemoveEnhancementBonusCommand = new DelegateCommand(RemoveEnhancementBonusAction);
        AddDeflectionBonusCommand = new DelegateCommand(AddDeflectionBonusAction);
        RemoveDeflectionBonusCommand = new DelegateCommand(RemoveDeflectionBonusAction);
        AddInherentBonusCommand = new DelegateCommand(AddInherentBonusAction);
        RemoveInherentBonusCommand = new DelegateCommand(RemoveInherentBonusAction);
        AddInsightBonusCommand = new DelegateCommand(AddInsightBonusAction);
        RemoveInsightBonusCommand = new DelegateCommand(RemoveInsightBonusAction);
        AddLuckBonusCommand = new DelegateCommand(AddLuckBonusAction);
        RemoveLuckBonusCommand = new DelegateCommand(RemoveLuckBonusAction);
        AddMoraleBonusCommand = new DelegateCommand(AddMoraleBonusAction);
        RemoveMoraleBonusCommand = new DelegateCommand(RemoveMoraleBonusAction);
        AddProfaneBonusCommand = new DelegateCommand(AddProfaneBonusAction);
        RemoveProfaneBonusCommand = new DelegateCommand(RemoveProfaneBonusAction);
        AddRacialBonusCommand = new DelegateCommand(AddRacialBonusAction);
        RemoveRacialBonusCommand = new DelegateCommand(RemoveRacialBonusAction);
        AddResistanceBonusCommand = new DelegateCommand(AddResistanceBonusAction);
        RemoveResistanceBonusCommand = new DelegateCommand(RemoveResistanceBonusAction);
        AddSacredBonusCommand = new DelegateCommand(AddSacredBonusAction);
        RemoveSacredBonusCommand = new DelegateCommand(RemoveSacredBonusAction);
        AddShieldBonusCommand = new DelegateCommand(AddShieldBonusAction);
        RemoveShieldBonusCommand = new DelegateCommand(RemoveShieldBonusAction);
        AddSizeBonusCommand = new DelegateCommand(AddSizeBonusAction);
        RemoveSizeBonusCommand = new DelegateCommand(RemoveSizeBonusAction);
        AddTraitBonusCommand = new DelegateCommand(AddTraitBonusAction);
        RemoveTraitBonusCommand = new DelegateCommand(RemoveTraitBonusAction);
        AddDodgeBonusCommand = new DelegateCommand(AddDodgeBonusAction);
        RemoveDodgeBonusCommand = new DelegateCommand(RemoveDodgeBonusAction);
        AddUntypedBonusCommand = new DelegateCommand(AddUntypedBonusAction);
        RemoveUntypedBonusCommand = new DelegateCommand(RemoveUntypedBonusAction);
        AddCircumstanceBonusCommand = new DelegateCommand(AddCircumstanceBonusAction);
        RemoveCircumstanceBonusCommand = new DelegateCommand(RemoveCircumstanceBonusAction);
        AddNaturalArmorBonusCommand = new DelegateCommand(AddNaturalArmorBonusAction);
        RemoveNaturalArmorBonusCommand = new DelegateCommand(RemoveNaturalArmorBonusAction);

        foreach (KeyValuePair<string, KeyValuePair<string, int>> bonus in bonusCalculator.NatArmorBonusList)
        {
            NaturalArmorBonuses.Add(new BonusScv(this, bonus.Key, bonus.Value.Key, bonus.Value.Value));
        }

        foreach (KeyValuePair<string, KeyValuePair<string, int>> bonus in bonusCalculator.CircumstanceBonusList)
        {
            CircumstanceBonuses.Add(new BonusScv(this, bonus.Key, bonus.Value.Key, bonus.Value.Value));
        }

        foreach (KeyValuePair<string, int> bonus in bonusCalculator.UntypedBonusList)
        {
            UntypedBonuses.Add(new BonusSvp(this, bonus.Key, bonus.Value));
        }

        foreach (KeyValuePair<string, int> bonus in bonusCalculator.DodgeBonusList)
        {
            DodgeBonuses.Add(new BonusSvp(this, bonus.Key, bonus.Value));
        }

        foreach (KeyValuePair<string, int> bonus in bonusCalculator.TraitBonusList)
        {
            TraitBonuses.Add(new BonusSvp(this, bonus.Key, bonus.Value));
        }

        foreach (KeyValuePair<string, int> bonus in bonusCalculator.SizeBonusList)
        {
            SizeBonuses.Add(new BonusSvp(this, bonus.Key, bonus.Value));
        }

        foreach (KeyValuePair<string, int> bonus in bonusCalculator.ShieldBonusList)
        {
            ShieldBonuses.Add(new BonusSvp(this, bonus.Key, bonus.Value));
        }

        foreach (KeyValuePair<string, int> bonus in bonusCalculator.SacredBonusList)
        {
            SacredBonuses.Add(new BonusSvp(this, bonus.Key, bonus.Value));
        }

        foreach (KeyValuePair<string, int> bonus in bonusCalculator.ResistanceBonusList)
        {
            ResistanceBonuses.Add(new BonusSvp(this, bonus.Key, bonus.Value));
        }

        foreach (KeyValuePair<string, int> bonus in bonusCalculator.RacialBonusList)
        {
            RacialBonuses.Add(new BonusSvp(this, bonus.Key, bonus.Value));
        }

        foreach (KeyValuePair<string, int> bonus in bonusCalculator.ProfaneBonusList)
        {
            ProfaneBonuses.Add(new BonusSvp(this, bonus.Key, bonus.Value));
        }

        foreach (KeyValuePair<string, int> bonus in bonusCalculator.MoraleBonusList)
        {
            MoraleBonuses.Add(new BonusSvp(this, bonus.Key, bonus.Value));
        }

        foreach (KeyValuePair<string, int> bonus in bonusCalculator.LuckBonusList)
        {
            LuckBonuses.Add(new BonusSvp(this, bonus.Key, bonus.Value));
        }

        foreach (KeyValuePair<string, int> bonus in bonusCalculator.InsightBonusList)
        {
            InsightBonuses.Add(new BonusSvp(this, bonus.Key, bonus.Value));
        }

        foreach (KeyValuePair<string, int> bonus in bonusCalculator.InherentBonusList)
        {
            InherentBonuses.Add(new BonusSvp(this, bonus.Key, bonus.Value));
        }

        foreach (KeyValuePair<string, int> bonus in bonusCalculator.DeflectionBonusList)
        {
            DeflectionBonuses.Add(new BonusSvp(this, bonus.Key, bonus.Value));
        }

        foreach (KeyValuePair<string, int> bonus in bonusCalculator.EnhanceBonusList)
        {
            EnhancementBonuses.Add(new BonusSvp(this, bonus.Key, bonus.Value));
        }

        foreach (KeyValuePair<string, int> bonus in bonusCalculator.CompetenceBonusList)
        {
            CompetenceBonuses.Add(new BonusSvp(this, bonus.Key, bonus.Value));
        }

        foreach (KeyValuePair<string, int> bonus in bonusCalculator.BabBonusList)
        {
            BabBonuses.Add(new BonusSvp(this, bonus.Key, bonus.Value));
        }

        foreach (KeyValuePair<string, int> bonus in bonusCalculator.AlchemicalBonusList)
        {
            AlchemicalBonuses.Add(new BonusSvp(this, bonus.Key, bonus.Value));
        }

        foreach (KeyValuePair<string, int> bonus in bonusCalculator.ArmorBonusList)
        {
            ArmorBonuses.Add(new BonusSvp(this, bonus.Key, bonus.Value));
        }

        UpdateActiveBonuses();
    }

    public DelegateCommand AcceptChangesCommand { get; }

    public DelegateCommand CancelCommand { get; }

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

    public ObservableCollection<BonusSvp> AlchemicalBonuses { get; set; } = new();

    public BonusSvp? SelectedArmorBonus
    {
        get => _selectedArmorBonus;
        set
        {
            _selectedArmorBonus = value;
            OnPropertyChanged("SelectedArmorBonus");
        }
    }

    public DelegateCommand AddArmorBonusCommand { get; }

    public DelegateCommand RemoveArmorBonusCommand { get; }

    public ObservableCollection<BonusSvp> ArmorBonuses { get; set; } = new();

    public BonusSvp? SelectedBabBonus
    {
        get => _selectedBabBonus;
        set
        {
            _selectedBabBonus = value;
            OnPropertyChanged("SelectedBabBonus");
        }
    }

    public DelegateCommand AddBabBonusCommand { get; }

    public DelegateCommand RemoveBabBonusCommand { get; }

    public ObservableCollection<BonusSvp> BabBonuses { get; set; } = new();

    public BonusSvp? SelectedCompetenceBonus
    {
        get => _selectedCompetenceBonus;
        set
        {
            _selectedCompetenceBonus = value;
            OnPropertyChanged("SelectedCompetenceBonus");
        }
    }

    public DelegateCommand AddCompetenceBonusCommand { get; }

    public DelegateCommand RemoveCompetenceBonusCommand { get; }

    public ObservableCollection<BonusSvp> CompetenceBonuses { get; set; } = new();

    public BonusSvp? SelectedEnhancementBonus
    {
        get => _selectedEnhancementBonus;
        set
        {
            _selectedEnhancementBonus = value;
            OnPropertyChanged("SelectedEnhancementBonus");
        }
    }

    public DelegateCommand AddEnhancementBonusCommand { get; }

    public DelegateCommand RemoveEnhancementBonusCommand { get; }

    public ObservableCollection<BonusSvp> EnhancementBonuses { get; set; } = new();

    public BonusSvp? SelectedDeflectionBonus
    {
        get => _selectedDeflectionBonus;
        set
        {
            _selectedDeflectionBonus = value;
            OnPropertyChanged("SelectedDeflectionBonus");
        }
    }

    public DelegateCommand AddDeflectionBonusCommand { get; }

    public DelegateCommand RemoveDeflectionBonusCommand { get; }

    public ObservableCollection<BonusSvp> DeflectionBonuses { get; set; } = new();

    public BonusSvp? SelectedInherentBonus
    {
        get => _selectedInherentBonus;
        set
        {
            _selectedInherentBonus = value;
            OnPropertyChanged("SelectedInherentBonus");
        }
    }

    public DelegateCommand AddInherentBonusCommand { get; }

    public DelegateCommand RemoveInherentBonusCommand { get; }

    public ObservableCollection<BonusSvp> InherentBonuses { get; set; } = new();

    public BonusSvp? SelectedInsightBonus
    {
        get => _selectedInsightBonus;
        set
        {
            _selectedInsightBonus = value;
            OnPropertyChanged("SelectedInsightBonus");
        }
    }

    public DelegateCommand AddInsightBonusCommand { get; }

    public DelegateCommand RemoveInsightBonusCommand { get; }

    public ObservableCollection<BonusSvp> InsightBonuses { get; set; } = new();

    public BonusSvp? SelectedLuckBonus
    {
        get => _selectedLuckBonus;
        set
        {
            _selectedLuckBonus = value;
            OnPropertyChanged("SelectedLuckBonus");
        }
    }

    public DelegateCommand AddLuckBonusCommand { get; }

    public DelegateCommand RemoveLuckBonusCommand { get; }

    public ObservableCollection<BonusSvp> LuckBonuses { get; set; } = new();

    public BonusSvp? SelectedMoraleBonus
    {
        get => _selectedMoraleBonus;
        set
        {
            _selectedMoraleBonus = value;
            OnPropertyChanged("SelectedMoraleBonus");
        }
    }

    public DelegateCommand AddMoraleBonusCommand { get; }

    public DelegateCommand RemoveMoraleBonusCommand { get; }

    public ObservableCollection<BonusSvp> MoraleBonuses { get; set; } = new();

    public BonusSvp? SelectedProfaneBonus
    {
        get => _selectedProfaneBonus;
        set
        {
            _selectedProfaneBonus = value;
            OnPropertyChanged("SelectedProfaneBonus");
        }
    }

    public DelegateCommand AddProfaneBonusCommand { get; }

    public DelegateCommand RemoveProfaneBonusCommand { get; }

    public ObservableCollection<BonusSvp> ProfaneBonuses { get; set; } = new();

    public BonusSvp? SelectedRacialBonus
    {
        get => _selectedRacialBonus;
        set
        {
            _selectedRacialBonus = value;
            OnPropertyChanged("SelectedRacialBonus");
        }
    }

    public DelegateCommand AddRacialBonusCommand { get; }

    public DelegateCommand RemoveRacialBonusCommand { get; }

    public ObservableCollection<BonusSvp> RacialBonuses { get; set; } = new();

    public BonusSvp? SelectedResistanceBonus
    {
        get => _selectedResistanceBonus;
        set
        {
            _selectedResistanceBonus = value;
            OnPropertyChanged("SelectedResistanceBonus");
        }
    }

    public DelegateCommand AddResistanceBonusCommand { get; }

    public DelegateCommand RemoveResistanceBonusCommand { get; }

    public ObservableCollection<BonusSvp> ResistanceBonuses { get; set; } = new();

    public BonusSvp? SelectedSacredBonus
    {
        get => _selectedSacredBonus;
        set
        {
            _selectedSacredBonus = value;
            OnPropertyChanged("SelectedSacredBonus");
        }
    }

    public DelegateCommand AddSacredBonusCommand { get; }

    public DelegateCommand RemoveSacredBonusCommand { get; }

    public ObservableCollection<BonusSvp> SacredBonuses { get; set; } = new();

    public BonusSvp? SelectedShieldBonus
    {
        get => _selectedShieldBonus;
        set
        {
            _selectedShieldBonus = value;
            OnPropertyChanged("SelectedShieldBonus");
        }
    }

    public DelegateCommand AddShieldBonusCommand { get; }

    public DelegateCommand RemoveShieldBonusCommand { get; }

    public ObservableCollection<BonusSvp> ShieldBonuses { get; set; } = new();

    public BonusSvp? SelectedSizeBonus
    {
        get => _selectedSizeBonus;
        set
        {
            _selectedSizeBonus = value;
            OnPropertyChanged("SelectedSizeBonus");
        }
    }

    public DelegateCommand AddSizeBonusCommand { get; }

    public DelegateCommand RemoveSizeBonusCommand { get; }

    public ObservableCollection<BonusSvp> SizeBonuses { get; set; } = new();

    public BonusSvp? SelectedTraitBonus
    {
        get => _selectedTraitBonus;
        set
        {
            _selectedTraitBonus = value;
            OnPropertyChanged("SelectedTraitBonus");
        }
    }

    public DelegateCommand AddTraitBonusCommand { get; }

    public DelegateCommand RemoveTraitBonusCommand { get; }

    public ObservableCollection<BonusSvp> TraitBonuses { get; set; } = new();

    public BonusSvp? SelectedDodgeBonus
    {
        get => _selectedDodgeBonus;
        set
        {
            _selectedDodgeBonus = value;
            OnPropertyChanged("SelectedDodgeBonus");
        }
    }

    public DelegateCommand AddDodgeBonusCommand { get; }

    public DelegateCommand RemoveDodgeBonusCommand { get; }

    public ObservableCollection<BonusSvp> DodgeBonuses { get; set; } = new();

    public BonusSvp? SelectedUntypedBonus
    {
        get => _selectedUntypedBonus;
        set
        {
            _selectedUntypedBonus = value;
            OnPropertyChanged("SelectedUntypedBonus");
        }
    }

    public DelegateCommand AddUntypedBonusCommand { get; }

    public DelegateCommand RemoveUntypedBonusCommand { get; }

    public ObservableCollection<BonusSvp> UntypedBonuses { get; set; } = new();

    public BonusScv? SelectedCircumstanceBonus
    {
        get => _selectedCircumstanceBonus;
        set
        {
            _selectedCircumstanceBonus = value;
            OnPropertyChanged("SelectedCircumstanceBonus");
        }
    }

    public DelegateCommand AddCircumstanceBonusCommand { get; }

    public DelegateCommand RemoveCircumstanceBonusCommand { get; }

    public ObservableCollection<BonusScv> CircumstanceBonuses { get; set; } = new();

    public BonusScv? SelectedNaturalArmorBonus
    {
        get => _selectedNaturalArmorBonus;
        set
        {
            _selectedNaturalArmorBonus = value;
            OnPropertyChanged("SelectedNaturalArmorBonus");
        }
    }

    public DelegateCommand AddNaturalArmorBonusCommand { get; }

    public DelegateCommand RemoveNaturalArmorBonusCommand { get; }

    public ObservableCollection<BonusScv> NaturalArmorBonuses { get; set; } = new();

    public ObservableCollection<BonusTsv> ActiveBonuses { get; set; } = new();

    public BonusTsv? SelectedActiveBonus
    {
        get => _selectedActiveBonus;
        set
        {
            _selectedActiveBonus = value;
            OnPropertyChanged("SelectedActiveBonus");
        }
    }

    public int BonusTotal
    {
        get => _bonusTotal;
        set
        {
            _bonusTotal = value;
            OnPropertyChanged("BonusTotal");
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void AddAlchemicalBonusAction(object arg)
    {
        AlchemicalBonuses.Add(new BonusSvp(this, "Source", 0));

        UpdateActiveBonuses();
    }

    private void RemoveAlchemicalBonusAction(object arg)
    {
        if (SelectedAlchemicalBonus != null)
        {
            AlchemicalBonuses.Remove(SelectedAlchemicalBonus);
            SelectedAlchemicalBonus = null;

            UpdateActiveBonuses();
        }
    }

    private void AddArmorBonusAction(object arg)
    {
        ArmorBonuses.Add(new BonusSvp(this, "Source", 0));

        UpdateActiveBonuses();
    }

    private void RemoveArmorBonusAction(object arg)
    {
        if (SelectedArmorBonus != null)
        {
            ArmorBonuses.Remove(SelectedArmorBonus);
            SelectedArmorBonus = null;

            UpdateActiveBonuses();
        }
    }

    private void AddBabBonusAction(object arg)
    {
        BabBonuses.Add(new BonusSvp(this, "Source", 0));

        UpdateActiveBonuses();
    }

    private void RemoveBabBonusAction(object arg)
    {
        if (SelectedBabBonus != null)
        {
            BabBonuses.Remove(SelectedBabBonus);
            SelectedBabBonus = null;

            UpdateActiveBonuses();
        }
    }

    private void AddCompetenceBonusAction(object arg)
    {
        CompetenceBonuses.Add(new BonusSvp(this, "Source", 0));

        UpdateActiveBonuses();
    }

    private void RemoveCompetenceBonusAction(object arg)
    {
        if (SelectedCompetenceBonus != null)
        {
            CompetenceBonuses.Remove(SelectedCompetenceBonus);
            SelectedCompetenceBonus = null;

            UpdateActiveBonuses();
        }
    }

    private void AddEnhancementBonusAction(object arg)
    {
        EnhancementBonuses.Add(new BonusSvp(this, "Source", 0));

        UpdateActiveBonuses();
    }

    private void RemoveEnhancementBonusAction(object arg)
    {
        if (SelectedEnhancementBonus != null)
        {
            EnhancementBonuses.Remove(SelectedEnhancementBonus);
            SelectedEnhancementBonus = null;

            UpdateActiveBonuses();
        }
    }

    private void AddDeflectionBonusAction(object arg)
    {
        DeflectionBonuses.Add(new BonusSvp(this, "Source", 0));

        UpdateActiveBonuses();
    }

    private void RemoveDeflectionBonusAction(object arg)
    {
        if (SelectedDeflectionBonus != null)
        {
            DeflectionBonuses.Remove(SelectedDeflectionBonus);
            SelectedDeflectionBonus = null;

            UpdateActiveBonuses();
        }
    }

    private void AddInherentBonusAction(object arg)
    {
        InherentBonuses.Add(new BonusSvp(this, "Source", 0));

        UpdateActiveBonuses();
    }

    private void RemoveInherentBonusAction(object arg)
    {
        if (SelectedInherentBonus != null)
        {
            InherentBonuses.Remove(SelectedInherentBonus);
            SelectedInherentBonus = null;

            UpdateActiveBonuses();
        }
    }

    private void AddInsightBonusAction(object arg)
    {
        InsightBonuses.Add(new BonusSvp(this, "Source", 0));

        UpdateActiveBonuses();
    }

    private void RemoveInsightBonusAction(object arg)
    {
        if (SelectedInsightBonus != null)
        {
            InsightBonuses.Remove(SelectedInsightBonus);
            SelectedInsightBonus = null;

            UpdateActiveBonuses();
        }
    }

    private void AddLuckBonusAction(object arg)
    {
        LuckBonuses.Add(new BonusSvp(this, "Source", 0));

        UpdateActiveBonuses();
    }

    private void RemoveLuckBonusAction(object arg)
    {
        if (SelectedLuckBonus != null)
        {
            LuckBonuses.Remove(SelectedLuckBonus);
            SelectedLuckBonus = null;

            UpdateActiveBonuses();
        }
    }

    private void AddMoraleBonusAction(object arg)
    {
        MoraleBonuses.Add(new BonusSvp(this, "Source", 0));

        UpdateActiveBonuses();
    }

    private void RemoveMoraleBonusAction(object arg)
    {
        if (SelectedMoraleBonus != null)
        {
            MoraleBonuses.Remove(SelectedMoraleBonus);
            SelectedMoraleBonus = null;

            UpdateActiveBonuses();
        }
    }

    private void AddProfaneBonusAction(object arg)
    {
        ProfaneBonuses.Add(new BonusSvp(this, "Source", 0));

        UpdateActiveBonuses();
    }

    private void RemoveProfaneBonusAction(object arg)
    {
        if (SelectedProfaneBonus != null)
        {
            ProfaneBonuses.Remove(SelectedProfaneBonus);
            SelectedProfaneBonus = null;

            UpdateActiveBonuses();
        }
    }

    private void AddRacialBonusAction(object arg)
    {
        RacialBonuses.Add(new BonusSvp(this, "Source", 0));

        UpdateActiveBonuses();
    }

    private void RemoveRacialBonusAction(object arg)
    {
        if (SelectedRacialBonus != null)
        {
            RacialBonuses.Remove(SelectedRacialBonus);
            SelectedRacialBonus = null;

            UpdateActiveBonuses();
        }
    }

    private void AddResistanceBonusAction(object arg)
    {
        ResistanceBonuses.Add(new BonusSvp(this, "Source", 0));

        UpdateActiveBonuses();
    }

    private void RemoveResistanceBonusAction(object arg)
    {
        if (SelectedResistanceBonus != null)
        {
            ResistanceBonuses.Remove(SelectedResistanceBonus);
            SelectedResistanceBonus = null;

            UpdateActiveBonuses();
        }
    }

    private void AddSacredBonusAction(object arg)
    {
        SacredBonuses.Add(new BonusSvp(this, "Source", 0));

        UpdateActiveBonuses();
    }

    private void RemoveSacredBonusAction(object arg)
    {
        if (SelectedSacredBonus != null)
        {
            SacredBonuses.Remove(SelectedSacredBonus);
            SelectedSacredBonus = null;

            UpdateActiveBonuses();
        }
    }

    private void AddShieldBonusAction(object arg)
    {
        ShieldBonuses.Add(new BonusSvp(this, "Source", 0));

        UpdateActiveBonuses();
    }

    private void RemoveShieldBonusAction(object arg)
    {
        if (SelectedShieldBonus != null)
        {
            ShieldBonuses.Remove(SelectedShieldBonus);
            SelectedShieldBonus = null;

            UpdateActiveBonuses();
        }
    }

    private void AddSizeBonusAction(object arg)
    {
        SizeBonuses.Add(new BonusSvp(this, "Source", 0));

        UpdateActiveBonuses();
    }

    private void RemoveSizeBonusAction(object arg)
    {
        if (SelectedSizeBonus != null)
        {
            SizeBonuses.Remove(SelectedSizeBonus);
            SelectedSizeBonus = null;

            UpdateActiveBonuses();
        }
    }

    private void AddTraitBonusAction(object arg)
    {
        TraitBonuses.Add(new BonusSvp(this, "Source", 0));

        UpdateActiveBonuses();
    }

    private void RemoveTraitBonusAction(object arg)
    {
        if (SelectedTraitBonus != null)
        {
            TraitBonuses.Remove(SelectedTraitBonus);
            SelectedTraitBonus = null;

            UpdateActiveBonuses();
        }
    }

    private void AddDodgeBonusAction(object arg)
    {
        DodgeBonuses.Add(new BonusSvp(this, "Source", 0));

        UpdateActiveBonuses();
    }

    private void RemoveDodgeBonusAction(object arg)
    {
        if (SelectedDodgeBonus != null)
        {
            DodgeBonuses.Remove(SelectedDodgeBonus);
            SelectedDodgeBonus = null;

            UpdateActiveBonuses();
        }
    }

    private void AddUntypedBonusAction(object arg)
    {
        UntypedBonuses.Add(new BonusSvp(this, "Source", 0));

        UpdateActiveBonuses();
    }

    private void RemoveUntypedBonusAction(object arg)
    {
        if (SelectedUntypedBonus != null)
        {
            UntypedBonuses.Remove(SelectedUntypedBonus);
            SelectedUntypedBonus = null;

            UpdateActiveBonuses();
        }
    }

    private void AddCircumstanceBonusAction(object arg)
    {
        CircumstanceBonuses.Add(new BonusScv(this, "Source", "Circumstance", 0));

        UpdateActiveBonuses();
    }

    private void RemoveCircumstanceBonusAction(object arg)
    {
        if (SelectedCircumstanceBonus != null)
        {
            CircumstanceBonuses.Remove(SelectedCircumstanceBonus);
            SelectedCircumstanceBonus = null;

            UpdateActiveBonuses();
        }
    }

    private void AddNaturalArmorBonusAction(object arg)
    {
        NaturalArmorBonuses.Add(new BonusScv(this, "Source", "Circumstance", 0));

        UpdateActiveBonuses();
    }

    private void RemoveNaturalArmorBonusAction(object arg)
    {
        if (SelectedNaturalArmorBonus != null)
        {
            NaturalArmorBonuses.Remove(SelectedNaturalArmorBonus);
            SelectedNaturalArmorBonus = null;

            UpdateActiveBonuses();
        }
    }

    internal void UpdateActiveBonuses()
    {
        List<BonusTsv> activeBonuses = new();

        BonusSvp? alchemicalBonus = AlchemicalBonuses.Max();
        if (alchemicalBonus != null)
        {
            activeBonuses.Add(new BonusTsv(Bonus.Alchemical.GetEnumDescription(),
                alchemicalBonus.Source, alchemicalBonus.Value));
        }

        BonusSvp? armorBonus = ArmorBonuses.Max();
        if (armorBonus != null)
        {
            activeBonuses.Add(new BonusTsv(Bonus.Armor.GetEnumDescription(),
                armorBonus.Source, armorBonus.Value));
        }

        BonusSvp? babBonus = BabBonuses.Max();
        if (babBonus != null)
        {
            activeBonuses.Add(new BonusTsv(Bonus.Bab.GetEnumDescription(),
                babBonus.Source, babBonus.Value));
        }

        BonusSvp? competenceBonus = CompetenceBonuses.Max();
        if (competenceBonus != null)
        {
            activeBonuses.Add(new BonusTsv(Bonus.Competence.GetEnumDescription(),
                competenceBonus.Source, competenceBonus.Value));
        }

        BonusSvp? deflectionBonus = DeflectionBonuses.Max();
        if (deflectionBonus != null)
        {
            activeBonuses.Add(new BonusTsv(Bonus.Deflection.GetEnumDescription(),
                deflectionBonus.Source, deflectionBonus.Value));
        }

        BonusSvp? enhancementBonus = EnhancementBonuses.Max();
        if (enhancementBonus != null)
        {
            activeBonuses.Add(new BonusTsv(Bonus.Enhancement.GetEnumDescription(),
                enhancementBonus.Source, enhancementBonus.Value));
        }

        BonusSvp? inherentBonus = InherentBonuses.Max();
        if (inherentBonus != null)
        {
            activeBonuses.Add(new BonusTsv(Bonus.Inherent.GetEnumDescription(),
                inherentBonus.Source, inherentBonus.Value));
        }

        BonusSvp? insightBonus = InsightBonuses.Max();
        if (insightBonus != null)
        {
            activeBonuses.Add(new BonusTsv(Bonus.Inherent.GetEnumDescription(),
                insightBonus.Source, insightBonus.Value));
        }

        BonusSvp? luckBonus = LuckBonuses.Max();
        if (luckBonus != null)
        {
            activeBonuses.Add(new BonusTsv(Bonus.Luck.GetEnumDescription(),
                luckBonus.Source, luckBonus.Value));
        }

        BonusSvp? moraleBonus = MoraleBonuses.Max();
        if (moraleBonus != null)
        {
            activeBonuses.Add(new BonusTsv(Bonus.Morale.GetEnumDescription(),
                moraleBonus.Source, moraleBonus.Value));
        }

        BonusSvp? profaneBonus = ProfaneBonuses.Max();
        if (profaneBonus != null)
        {
            activeBonuses.Add(new BonusTsv(Bonus.Profane.GetEnumDescription(),
                profaneBonus.Source, profaneBonus.Value));
        }

        BonusSvp? racialBonus = RacialBonuses.Max();
        if (racialBonus != null)
        {
            activeBonuses.Add(new BonusTsv(Bonus.Racial.GetEnumDescription(),
                racialBonus.Source, racialBonus.Value));
        }

        BonusSvp? resistanceBonus = ResistanceBonuses.Max();
        if (resistanceBonus != null)
        {
            activeBonuses.Add(new BonusTsv(Bonus.Resistance.GetEnumDescription(),
                resistanceBonus.Source, resistanceBonus.Value));
        }

        BonusSvp? sacredBonus = SacredBonuses.Max();
        if (sacredBonus != null)
        {
            activeBonuses.Add(new BonusTsv(Bonus.Sacred.GetEnumDescription(),
                sacredBonus.Source, sacredBonus.Value));
        }

        BonusSvp? shieldBonus = ShieldBonuses.Max();
        if (shieldBonus != null)
        {
            activeBonuses.Add(new BonusTsv(Bonus.Shield.GetEnumDescription(),
                shieldBonus.Source, shieldBonus.Value));
        }

        BonusSvp? sizeBonus = SizeBonuses.Max();
        if (sizeBonus != null)
        {
            activeBonuses.Add(new BonusTsv(Bonus.Size.GetEnumDescription(),
                sizeBonus.Source, sizeBonus.Value));
        }

        BonusSvp? traitBonus = TraitBonuses.Max();
        if (traitBonus != null)
        {
            activeBonuses.Add(new BonusTsv(Bonus.Trait.GetEnumDescription(),
                traitBonus.Source, traitBonus.Value));
        }

        foreach (BonusSvp bonus in DodgeBonuses)
        {
            activeBonuses.Add(new BonusTsv(Bonus.Dodge.GetEnumDescription(),
                bonus.Source, bonus.Value));
        }

        foreach (BonusSvp bonus in UntypedBonuses)
        {
            activeBonuses.Add(new BonusTsv(Bonus.Untyped.GetEnumDescription(),
                bonus.Source, bonus.Value));
        }

        foreach (BonusTsv bonus in FilterComplexBonuses(CircumstanceBonuses, Bonus.Circumstance))
        {
            activeBonuses.Add(bonus);
        }

        foreach (BonusTsv bonus in FilterComplexBonuses(NaturalArmorBonuses, Bonus.NaturalArmor))
        {
            activeBonuses.Add(bonus);
        }

        int totalBonus = 0;
        ActiveBonuses.Clear();
        foreach (BonusTsv activeBonus in activeBonuses)
        {
            ActiveBonuses.Add(activeBonus);
            totalBonus += activeBonus.Value;
        }

        BonusTotal = totalBonus;
    }

    private ICollection<BonusTsv> FilterComplexBonuses(ICollection<BonusScv> list, Bonus bonusType)
    {
        // Filter bonuses to eliminate duplicates
        Dictionary<string, BonusTsv> filteredBonuses = new();
        foreach (BonusScv bonus in list)
        {
            // Handle multiple bonuses from same circumstance
            if (filteredBonuses.ContainsKey(bonus.Source))
            {
                if (filteredBonuses[bonus.Source].Value < bonus.Value)
                {
                    filteredBonuses[bonus.Source].Value = bonus.Value;
                }
            }

            // Just add it
            else
            {
                filteredBonuses.Add(bonus.Source, new BonusTsv(
                    bonusType.GetEnumDescription(),
                    bonus.Source, bonus.Value));
            }
        }

        return filteredBonuses.Values;
    }

    private void CancelAction(object arg)
    {
        string messageBoxText = "Unsaved changes will be lost. Are you sure?";
        string caption = "Warning";
        MessageBoxButton button = MessageBoxButton.YesNo;
        MessageBoxImage icon = MessageBoxImage.Warning;
        MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

        if (result == MessageBoxResult.Yes && arg is Window window)
        {
            window.DialogResult = false;
            window.Close();
        }
    }

    private void AcceptChangesAction(object arg)
    {
        if (arg is Window window)
        {
            _bonusCalculator.Clear();

            foreach (BonusSvp bonus in AlchemicalBonuses)
            {
                _bonusCalculator.AddBonus(Bonus.Alchemical, bonus.Source, bonus.Value);
            }

            foreach (BonusSvp bonus in ArmorBonuses)
            {
                _bonusCalculator.AddBonus(Bonus.Armor, bonus.Source, bonus.Value);
            }

            foreach (BonusSvp bonus in BabBonuses)
            {
                _bonusCalculator.AddBonus(Bonus.Bab, bonus.Source, bonus.Value);
            }

            foreach (BonusSvp bonus in CompetenceBonuses)
            {
                _bonusCalculator.AddBonus(Bonus.Competence, bonus.Source, bonus.Value);
            }

            foreach (BonusSvp bonus in DeflectionBonuses)
            {
                _bonusCalculator.AddBonus(Bonus.Deflection, bonus.Source, bonus.Value);
            }

            foreach (BonusSvp bonus in DodgeBonuses)
            {
                _bonusCalculator.AddBonus(Bonus.Dodge, bonus.Source, bonus.Value);
            }

            foreach (BonusSvp bonus in EnhancementBonuses)
            {
                _bonusCalculator.AddBonus(Bonus.Enhancement, bonus.Source, bonus.Value);
            }

            foreach (BonusSvp bonus in InherentBonuses)
            {
                _bonusCalculator.AddBonus(Bonus.Inherent, bonus.Source, bonus.Value);
            }

            foreach (BonusSvp bonus in InsightBonuses)
            {
                _bonusCalculator.AddBonus(Bonus.Insight, bonus.Source, bonus.Value);
            }

            foreach (BonusSvp bonus in LuckBonuses)
            {
                _bonusCalculator.AddBonus(Bonus.Luck, bonus.Source, bonus.Value);
            }

            foreach (BonusSvp bonus in MoraleBonuses)
            {
                _bonusCalculator.AddBonus(Bonus.Morale, bonus.Source, bonus.Value);
            }

            foreach (BonusSvp bonus in ProfaneBonuses)
            {
                _bonusCalculator.AddBonus(Bonus.Profane, bonus.Source, bonus.Value);
            }

            foreach (BonusSvp bonus in RacialBonuses)
            {
                _bonusCalculator.AddBonus(Bonus.Racial, bonus.Source, bonus.Value);
            }

            foreach (BonusSvp bonus in ResistanceBonuses)
            {
                _bonusCalculator.AddBonus(Bonus.Resistance, bonus.Source, bonus.Value);
            }

            foreach (BonusSvp bonus in SacredBonuses)
            {
                _bonusCalculator.AddBonus(Bonus.Sacred, bonus.Source, bonus.Value);
            }

            foreach (BonusSvp bonus in ShieldBonuses)
            {
                _bonusCalculator.AddBonus(Bonus.Shield, bonus.Source, bonus.Value);
            }

            foreach (BonusSvp bonus in SizeBonuses)
            {
                _bonusCalculator.AddBonus(Bonus.Size, bonus.Source, bonus.Value);
            }

            foreach (BonusSvp bonus in TraitBonuses)
            {
                _bonusCalculator.AddBonus(Bonus.Trait, bonus.Source, bonus.Value);
            }

            foreach (BonusSvp bonus in UntypedBonuses)
            {
                _bonusCalculator.AddBonus(Bonus.Untyped, bonus.Source, bonus.Value);
            }

            foreach (BonusScv bonus in CircumstanceBonuses)
            {
                _bonusCalculator.AddCircumstanceBonus(bonus.Source, bonus.Circumstance, bonus.Value);
            }

            foreach (BonusScv bonus in NaturalArmorBonuses)
            {
                _bonusCalculator.AddNaturalArmorBonus(bonus.Source, bonus.Circumstance, bonus.Value);
            }

            window.DialogResult = true;
        }
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}