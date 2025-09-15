using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using HavenheimManager.Calculators;
using HavenheimManager.Containers;
using HavenheimManager.Enums;
using HavenheimManager.Extensions;

namespace HavenheimManager.Editors;

public class DescriptorViewModel : INotifyPropertyChanged
{
    private bool _showContent;

    private bool _showCreature;

    private bool _showCreatureType;

    private bool _showCreatureSubType;

    private bool _showCraftSkill;

    private bool _showAbilityScore;

    private bool _showAbilityType;

    private bool _showBuff;

    private bool _showFeat;

    private bool _showMagic;

    private bool _showSave;

    private bool _showSkill;

    private bool _showSystem;

    private bool _showTrait;

    private bool _showUsage;

    private bool _showDuration;

    private bool _showKnowledge;

    private bool _showMagicAura;

    private bool _showPerform;

    private bool _showSpellSchool;

    private bool _showStimulus;

    private bool _showTerrain;

    internal DescriptorViewModel()
    {
        CancelCommand = new DelegateCommand(CancelAction);
        AcceptChangesCommand = new DelegateCommand(AcceptChangesAction);
        ShowContentCheckboxCommand = new DelegateCommand(ShowContentCheckboxAction);

    }/*

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
    }*/

    internal DelegateCommand AcceptChangesCommand { get; }

    internal DelegateCommand CancelCommand { get; }/*

    internal BonusSvp? SelectedAlchemicalBonus
    {
        get => _selectedAlchemicalBonus;
        set
        {
            _selectedAlchemicalBonus = value;
            OnPropertyChanged("SelectedAlchemicalBonus");
        }
    }*/

    internal DelegateCommand ShowContentCheckboxCommand { get; }/*

    internal DelegateCommand RemoveAlchemicalBonusCommand { get; }

    internal ObservableCollection<BonusSvp> AlchemicalBonuses { get; set; } = new();

    internal BonusSvp? SelectedArmorBonus
    {
        get => _selectedArmorBonus;
        set
        {
            _selectedArmorBonus = value;
            OnPropertyChanged("SelectedArmorBonus");
        }
    }

    internal DelegateCommand AddArmorBonusCommand { get; }

    internal DelegateCommand RemoveArmorBonusCommand { get; }

    internal ObservableCollection<BonusSvp> ArmorBonuses { get; set; } = new();

    internal BonusSvp? SelectedBabBonus
    {
        get => _selectedBabBonus;
        set
        {
            _selectedBabBonus = value;
            OnPropertyChanged("SelectedBabBonus");
        }
    }

    internal DelegateCommand AddBabBonusCommand { get; }

    internal DelegateCommand RemoveBabBonusCommand { get; }

    internal ObservableCollection<BonusSvp> BabBonuses { get; set; } = new();

    internal BonusSvp? SelectedCompetenceBonus
    {
        get => _selectedCompetenceBonus;
        set
        {
            _selectedCompetenceBonus = value;
            OnPropertyChanged("SelectedCompetenceBonus");
        }
    }

    internal DelegateCommand AddCompetenceBonusCommand { get; }

    internal DelegateCommand RemoveCompetenceBonusCommand { get; }

    internal ObservableCollection<BonusSvp> CompetenceBonuses { get; set; } = new();

    internal BonusSvp? SelectedEnhancementBonus
    {
        get => _selectedEnhancementBonus;
        set
        {
            _selectedEnhancementBonus = value;
            OnPropertyChanged("SelectedEnhancementBonus");
        }
    }

    internal DelegateCommand AddEnhancementBonusCommand { get; }

    internal DelegateCommand RemoveEnhancementBonusCommand { get; }

    internal ObservableCollection<BonusSvp> EnhancementBonuses { get; set; } = new();

    internal BonusSvp? SelectedDeflectionBonus
    {
        get => _selectedDeflectionBonus;
        set
        {
            _selectedDeflectionBonus = value;
            OnPropertyChanged("SelectedDeflectionBonus");
        }
    }

    internal DelegateCommand AddDeflectionBonusCommand { get; }

    internal DelegateCommand RemoveDeflectionBonusCommand { get; }

    internal ObservableCollection<BonusSvp> DeflectionBonuses { get; set; } = new();

    internal BonusSvp? SelectedInherentBonus
    {
        get => _selectedInherentBonus;
        set
        {
            _selectedInherentBonus = value;
            OnPropertyChanged("SelectedInherentBonus");
        }
    }

    internal DelegateCommand AddInherentBonusCommand { get; }

    internal DelegateCommand RemoveInherentBonusCommand { get; }

    internal ObservableCollection<BonusSvp> InherentBonuses { get; set; } = new();

    internal BonusSvp? SelectedInsightBonus
    {
        get => _selectedInsightBonus;
        set
        {
            _selectedInsightBonus = value;
            OnPropertyChanged("SelectedInsightBonus");
        }
    }

    internal DelegateCommand AddInsightBonusCommand { get; }

    internal DelegateCommand RemoveInsightBonusCommand { get; }

    internal ObservableCollection<BonusSvp> InsightBonuses { get; set; } = new();

    internal BonusSvp? SelectedLuckBonus
    {
        get => _selectedLuckBonus;
        set
        {
            _selectedLuckBonus = value;
            OnPropertyChanged("SelectedLuckBonus");
        }
    }

    internal DelegateCommand AddLuckBonusCommand { get; }

    internal DelegateCommand RemoveLuckBonusCommand { get; }

    internal ObservableCollection<BonusSvp> LuckBonuses { get; set; } = new();

    internal BonusSvp? SelectedMoraleBonus
    {
        get => _selectedMoraleBonus;
        set
        {
            _selectedMoraleBonus = value;
            OnPropertyChanged("SelectedMoraleBonus");
        }
    }

    internal DelegateCommand AddMoraleBonusCommand { get; }

    internal DelegateCommand RemoveMoraleBonusCommand { get; }

    internal ObservableCollection<BonusSvp> MoraleBonuses { get; set; } = new();

    internal BonusSvp? SelectedProfaneBonus
    {
        get => _selectedProfaneBonus;
        set
        {
            _selectedProfaneBonus = value;
            OnPropertyChanged("SelectedProfaneBonus");
        }
    }

    internal DelegateCommand AddProfaneBonusCommand { get; }

    internal DelegateCommand RemoveProfaneBonusCommand { get; }

    internal ObservableCollection<BonusSvp> ProfaneBonuses { get; set; } = new();

    internal BonusSvp? SelectedRacialBonus
    {
        get => _selectedRacialBonus;
        set
        {
            _selectedRacialBonus = value;
            OnPropertyChanged("SelectedRacialBonus");
        }
    }

    internal DelegateCommand AddRacialBonusCommand { get; }

    internal DelegateCommand RemoveRacialBonusCommand { get; }

    internal ObservableCollection<BonusSvp> RacialBonuses { get; set; } = new();

    internal BonusSvp? SelectedResistanceBonus
    {
        get => _selectedResistanceBonus;
        set
        {
            _selectedResistanceBonus = value;
            OnPropertyChanged("SelectedResistanceBonus");
        }
    }

    internal DelegateCommand AddResistanceBonusCommand { get; }

    internal DelegateCommand RemoveResistanceBonusCommand { get; }

    internal ObservableCollection<BonusSvp> ResistanceBonuses { get; set; } = new();

    internal BonusSvp? SelectedSacredBonus
    {
        get => _selectedSacredBonus;
        set
        {
            _selectedSacredBonus = value;
            OnPropertyChanged("SelectedSacredBonus");
        }
    }

    internal DelegateCommand AddSacredBonusCommand { get; }

    internal DelegateCommand RemoveSacredBonusCommand { get; }

    internal ObservableCollection<BonusSvp> SacredBonuses { get; set; } = new();

    internal BonusSvp? SelectedShieldBonus
    {
        get => _selectedShieldBonus;
        set
        {
            _selectedShieldBonus = value;
            OnPropertyChanged("SelectedShieldBonus");
        }
    }

    internal DelegateCommand AddShieldBonusCommand { get; }

    internal DelegateCommand RemoveShieldBonusCommand { get; }

    internal ObservableCollection<BonusSvp> ShieldBonuses { get; set; } = new();

    internal BonusSvp? SelectedSizeBonus
    {
        get => _selectedSizeBonus;
        set
        {
            _selectedSizeBonus = value;
            OnPropertyChanged("SelectedSizeBonus");
        }
    }

    internal DelegateCommand AddSizeBonusCommand { get; }

    internal DelegateCommand RemoveSizeBonusCommand { get; }

    internal ObservableCollection<BonusSvp> SizeBonuses { get; set; } = new();

    internal BonusSvp? SelectedTraitBonus
    {
        get => _selectedTraitBonus;
        set
        {
            _selectedTraitBonus = value;
            OnPropertyChanged("SelectedTraitBonus");
        }
    }

    internal DelegateCommand AddTraitBonusCommand { get; }

    internal DelegateCommand RemoveTraitBonusCommand { get; }

    internal ObservableCollection<BonusSvp> TraitBonuses { get; set; } = new();

    internal BonusSvp? SelectedDodgeBonus
    {
        get => _selectedDodgeBonus;
        set
        {
            _selectedDodgeBonus = value;
            OnPropertyChanged("SelectedDodgeBonus");
        }
    }

    internal DelegateCommand AddDodgeBonusCommand { get; }

    internal DelegateCommand RemoveDodgeBonusCommand { get; }

    internal ObservableCollection<BonusSvp> DodgeBonuses { get; set; } = new();

    internal BonusSvp? SelectedUntypedBonus
    {
        get => _selectedUntypedBonus;
        set
        {
            _selectedUntypedBonus = value;
            OnPropertyChanged("SelectedUntypedBonus");
        }
    }

    internal DelegateCommand AddUntypedBonusCommand { get; }

    internal DelegateCommand RemoveUntypedBonusCommand { get; }

    internal ObservableCollection<BonusSvp> UntypedBonuses { get; set; } = new();

    internal BonusScv? SelectedCircumstanceBonus
    {
        get => _selectedCircumstanceBonus;
        set
        {
            _selectedCircumstanceBonus = value;
            OnPropertyChanged("SelectedCircumstanceBonus");
        }
    }

    internal DelegateCommand AddCircumstanceBonusCommand { get; }

    internal DelegateCommand RemoveCircumstanceBonusCommand { get; }

    internal ObservableCollection<BonusScv> CircumstanceBonuses { get; set; } = new();

    internal BonusScv? SelectedNaturalArmorBonus
    {
        get => _selectedNaturalArmorBonus;
        set
        {
            _selectedNaturalArmorBonus = value;
            OnPropertyChanged("SelectedNaturalArmorBonus");
        }
    }

    internal DelegateCommand AddNaturalArmorBonusCommand { get; }

    internal DelegateCommand RemoveNaturalArmorBonusCommand { get; }

    internal ObservableCollection<BonusScv> NaturalArmorBonuses { get; set; } = new();

    internal ObservableCollection<BonusTsv> ActiveBonuses { get; set; } = new();

    internal BonusTsv? SelectedActiveBonus
    {
        get => _selectedActiveBonus;
        set
        {
            _selectedActiveBonus = value;
            OnPropertyChanged("SelectedActiveBonus");
        
    }

    internal int BonusTotal
    {
        get => _bonusTotal;
        set
        {
            _bonusTotal = value;
            OnPropertyChanged("BonusTotal");
        }
    }*/

    public event PropertyChangedEventHandler? PropertyChanged;

    private void ShowContentCheckboxAction(object arg)
    {
        _showContent = !_showContent;
    }/*

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
    }*/

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
    }/*
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
    }*/

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}