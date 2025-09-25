using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using HavenheimManager.Containers;
using HavenheimManager.Descriptors;
using HavenheimManager.Enums;
using HavenheimManager.Extensions;

namespace HavenheimManager.Handlers;

public class FilterHandler : INotifyPropertyChanged
{
    // Filter Lists
    private readonly HashSet<DescAbility> _abilityFilters = new();
    private readonly HashSet<AbilityType> _abilityTypeFilters = new();
    private readonly HashSet<DescBuff> _buffFilters = new();
    private readonly HashSet<DescContent> _contentFilters = new();
    private readonly HashSet<CraftSkill> _craftSkillFilters = new();
    private readonly HashSet<Creature> _creatureFilters = new();
    private readonly HashSet<CreatureSubType> _creatureSubTypeFilters = new();
    private readonly HashSet<CreatureType> _creatureTypeFilters = new();
    private readonly HashSet<DescDuration> _durationFilters = new();
    private readonly HashSet<DescFeat> _featFilters = new();
    private readonly HashSet<Knowledge> _knowledgeFilters = new();
    private readonly HashSet<MagicAura> _magicAuraFilters = new();
    private readonly HashSet<DescMagic> _magicFilters = new();
    private readonly HashSet<PerformSkill> _performSkillFilters = new();
    private readonly HashSet<DescSave> _saveFilters = new();
    private readonly HashSet<DescSkill> _skillFilters = new();
    private readonly HashSet<SpellSchool> _spellSchoolFilters = new();
    private readonly HashSet<Stimulus> _stimulusFilters = new();
    private readonly HashSet<DescSystem> _systemFilters = new();
    private readonly HashSet<Terrain> _terrainFilters = new();
    private readonly HashSet<DescTrait> _traitFilters = new();
    private readonly HashSet<DescUsage> _usageFilters = new();
    private readonly HashSet<string> _customFilters = new();
    
    // Value source
    private readonly IFilterable _source;

    // Level filter string backing
    private string _maxLevel = AppSettings.MaxLevelPlaceholder.ToString();

    private string _minLevel = AppSettings.MinLevelPlaceholder.ToString();

    #region Visibility
    private Visibility _levelVisibility;
    public Visibility LevelVisibility
    {
        get => _levelVisibility;
        set
        {
            if (value == _levelVisibility) return;
            _levelVisibility = value;
            OnPropertyChanged("LevelVisibility");
        }
    }

    private Visibility _abilityVisibility;
    public Visibility AbilityVisibility
    {
        get => _abilityVisibility;
        set
        {
            if (value == _abilityVisibility) return;
            _abilityVisibility = value;
            OnPropertyChanged("AbilityVisibility");
        }
    }

    private Visibility _abilityTypeVisibility;
    public Visibility AbilityTypeVisibility
    {
        get => _abilityTypeVisibility;
        set
        {
            if (value == _abilityTypeVisibility) return;
            _abilityTypeVisibility = value;
            OnPropertyChanged("AbilityTypeVisibility");
        }
    }

    private Visibility _buffVisibility;
    public Visibility BuffVisibility
    {
        get => _buffVisibility;
        set
        {
            if (value == _buffVisibility) return;
            _buffVisibility = value;
            OnPropertyChanged("BuffVisibility");
        }
    }

    private Visibility _contentVisibility;
    public Visibility ContentVisibility
    {
        get => _contentVisibility;
        set
        {
            if (value == _contentVisibility) return;
            _contentVisibility = value;
            OnPropertyChanged("ContentVisibility");
        }
    }

    private Visibility _craftSkillVisibility;
    public Visibility CraftSkillVisibility
    {
        get => _craftSkillVisibility;
        set
        {
            if (value == _craftSkillVisibility) return;
            _craftSkillVisibility = value;
            OnPropertyChanged("CraftSkillVisibility");
        }
    }

    private Visibility _creatureSubTypeVisibility;
    public Visibility CreatureSubTypeVisibility
    {
        get => _creatureSubTypeVisibility;
        set
        {
            if (value == _creatureSubTypeVisibility) return;
            _creatureSubTypeVisibility = value;
            OnPropertyChanged("CreatureSubTypeVisibility");
        }
    }

    private Visibility _creatureTypeVisibility;
    public Visibility CreatureTypeVisibility
    {
        get => _creatureTypeVisibility;
        set
        {
            if (value == _creatureTypeVisibility) return;
            _creatureTypeVisibility = value;
            OnPropertyChanged("CreatureTypeVisibility");
        }
    }

    private Visibility _creatureVisibility;
    public Visibility CreatureVisibility
    {
        get => _creatureVisibility;
        set
        {
            if (value == _creatureVisibility) return;
            _creatureVisibility = value;
            OnPropertyChanged("CreatureVisibility");
        }
    }

    private Visibility _customVisibility;
    public Visibility CustomVisibility
    {
        get => _customVisibility;
        set
        {
            if (value == _customVisibility) return;
            _customVisibility = value;
            OnPropertyChanged("CustomVisibility");
        }
    }

    private Visibility _durationVisibility;
    public Visibility DurationVisibility
    {
        get => _durationVisibility;
        set
        {
            if (value == _durationVisibility) return;
            _durationVisibility = value;
            OnPropertyChanged("DurationVisibility");
        }
    }

    private Visibility _featVisibility;
    public Visibility FeatVisibility
    {
        get => _featVisibility;
        set
        {
            if (value == _featVisibility) return;
            _featVisibility = value;
            OnPropertyChanged("FeatVisibility");
        }
    }

    private Visibility _knowledgeVisibility;
    public Visibility KnowledgeVisibility
    {
        get => _knowledgeVisibility;
        set
        {
            if (value == _knowledgeVisibility) return;
            _knowledgeVisibility = value;
            OnPropertyChanged("KnowledgeVisibility");
        }
    }

    private Visibility _magicAuraVisibility;
    public Visibility MagicAuraVisibility
    {
        get => _magicAuraVisibility;
        set
        {
            if (value == _magicAuraVisibility) return;
            _magicAuraVisibility = value;
            OnPropertyChanged("MagicAuraVisibility");
        }
    }

    private Visibility _magicVisibility;
    public Visibility MagicVisibility
    {
        get => _magicVisibility;
        set
        {
            if (value == _magicVisibility) return;
            _magicVisibility = value;
            OnPropertyChanged("MagicVisibility");
        }
    }

    private Visibility _performVisibility;
    public Visibility PerformVisibility
    {
        get => _performVisibility;
        set
        {
            if (value == _performVisibility) return;
            _performVisibility = value;
            OnPropertyChanged("PerformVisibility");
        }
    }

    private Visibility _saveVisibility;
    public Visibility SaveVisibility
    {
        get => _saveVisibility;
        set
        {
            if (value == _saveVisibility) return;
            _saveVisibility = value;
            OnPropertyChanged("SaveVisibility");
        }
    }

    private Visibility _skillVisibility;
    public Visibility SkillVisibility
    {
        get => _skillVisibility;
        set
        {
            if (value == _skillVisibility) return;
            _skillVisibility = value;
            OnPropertyChanged("SkillVisibility");
        }
    }

    private Visibility _spellSchoolVisibility;
    public Visibility SpellSchoolVisibility
    {
        get => _spellSchoolVisibility;
        set
        {
            if (value == _spellSchoolVisibility) return;
            _spellSchoolVisibility = value;
            OnPropertyChanged("SpellSchoolVisibility");
        }
    }

    private Visibility _stimulusVisibility;
    public Visibility StimulusVisibility
    {
        get => _stimulusVisibility;
        set
        {
            if (value == _stimulusVisibility) return;
            _stimulusVisibility = value;
            OnPropertyChanged("StimulusVisibility");
        }
    }

    private Visibility _systemVisibility;
    public Visibility SystemVisibility
    {
        get => _systemVisibility;
        set
        {
            if (value == _systemVisibility) return;
            _systemVisibility = value;
            OnPropertyChanged("SystemVisibility");
        }
    }

    private Visibility _terrainVisibility;
    public Visibility TerrainVisibility
    {
        get => _terrainVisibility;
        set
        {
            if (value == _terrainVisibility) return;
            _terrainVisibility = value;
            OnPropertyChanged("TerrainVisibility");
        }
    }

    private Visibility _traitVisibility;
    public Visibility TraitVisibility
    {
        get => _traitVisibility;
        set
        {
            if (value == _traitVisibility) return;
            _traitVisibility = value;
            OnPropertyChanged("TraitVisibility");
        }
    }

    private Visibility _usageVisibility;
    public Visibility UsageVisibility
    {
        get => _usageVisibility;
        set
        {
            if (value == _usageVisibility) return;
            _usageVisibility = value;
            OnPropertyChanged("UsageVisibility");
        }
    }
    #endregion

    public FilterHandler(IFilterable source)
    {
        _source = source;

        ContentCheckboxCommand = new DelegateCommand(ContentFilterAction);
        CreatureCheckboxCommand = new DelegateCommand(CreatureFilterAction);
        CreatureTypeCheckboxCommand = new DelegateCommand(CreatureTypeFilterAction);
        CreatureSubTypeCheckboxCommand = new DelegateCommand(CreatureSubTypeFilterAction);
        CraftSkillCheckboxCommand = new DelegateCommand(CraftSkillFilterAction);
        AbilityCheckboxCommand = new DelegateCommand(AbilityFilterAction);
        AbilityTypeCheckboxCommand = new DelegateCommand(AbilityTypeFilterAction);
        BuffCheckboxCommand = new DelegateCommand(BuffFilterAction);
        FeatCheckboxCommand = new DelegateCommand(FeatFilterAction);
        MagicCheckboxCommand = new DelegateCommand(MagicFilterAction);
        SaveCheckboxCommand = new DelegateCommand(SaveFilterAction);
        SkillCheckboxCommand = new DelegateCommand(SkillFilterAction);
        SystemCheckboxCommand = new DelegateCommand(SystemFilterAction);
        TraitCheckboxCommand = new DelegateCommand(TraitFilterAction);
        UsageCheckboxCommand = new DelegateCommand(UsageFilterAction);
        DurationCheckboxCommand = new DelegateCommand(DurationFilterAction);
        KnowledgeCheckboxCommand = new DelegateCommand(KnowledgeFilterAction);
        MagicAuraCheckboxCommand = new DelegateCommand(MagicAuraFilterAction);
        PerformCheckboxCommand = new DelegateCommand(PerformFilterAction);
        SpellSchoolCheckboxCommand = new DelegateCommand(SpellSchoolFilterAction);
        StimulusCheckboxCommand = new DelegateCommand(StimulusFilterAction);
        TerrainCheckboxCommand = new DelegateCommand(TerrainFilterAction);
        CustomCheckboxCommand = new DelegateCommand(CustomFilterAction);
        MinLevelRemovePlaceholderTextCommand = new DelegateCommand(MinLevelRemovePlaceholderTextAction);
        MinLevelAddPlaceholderTextCommand = new DelegateCommand(MinLevelAddPlaceholderTextAction);
        MaxLevelRemovePlaceholderTextCommand = new DelegateCommand(MaxLevelRemovePlaceholderTextAction);
        MaxLevelAddPlaceholderTextCommand = new DelegateCommand(MaxLevelAddPlaceholderTextAction);

        SetDescriptorVisibility(_source.DescriptorSettings);
    }

    // Checkbox Commands
    public DelegateCommand ContentCheckboxCommand { get; }
    public DelegateCommand CreatureCheckboxCommand { get; }
    public DelegateCommand CreatureTypeCheckboxCommand { get; }
    public DelegateCommand CreatureSubTypeCheckboxCommand { get; }
    public DelegateCommand CraftSkillCheckboxCommand { get; }
    public DelegateCommand AbilityCheckboxCommand { get; }
    public DelegateCommand AbilityTypeCheckboxCommand { get; }
    public DelegateCommand BuffCheckboxCommand { get; }
    public DelegateCommand FeatCheckboxCommand { get; }
    public DelegateCommand MagicCheckboxCommand { get; }
    public DelegateCommand SaveCheckboxCommand { get; }
    public DelegateCommand SkillCheckboxCommand { get; }
    public DelegateCommand SystemCheckboxCommand { get; }
    public DelegateCommand TraitCheckboxCommand { get; }
    public DelegateCommand UsageCheckboxCommand { get; }
    public DelegateCommand DurationCheckboxCommand { get; }
    public DelegateCommand KnowledgeCheckboxCommand { get; }
    public DelegateCommand MagicAuraCheckboxCommand { get; }
    public DelegateCommand PerformCheckboxCommand { get; }
    public DelegateCommand SpellSchoolCheckboxCommand { get; }
    public DelegateCommand StimulusCheckboxCommand { get; }
    public DelegateCommand TerrainCheckboxCommand { get; }
    public DelegateCommand CustomCheckboxCommand { get; }
    public DelegateCommand MinLevelRemovePlaceholderTextCommand { get; }
    public DelegateCommand MinLevelAddPlaceholderTextCommand { get; }
    public DelegateCommand MaxLevelRemovePlaceholderTextCommand { get; }
    public DelegateCommand MaxLevelAddPlaceholderTextCommand { get; }

    // Descriptor collections
    public ObservableCollection<string> ContentDescriptorList { get; } = new();
    public ObservableCollection<string> CreatureDescriptorList { get; } = new();
    public ObservableCollection<string> CreatureTypeDescriptorList { get; } = new();
    public ObservableCollection<string> CreatureSubTypeDescriptorList { get; } = new();
    public ObservableCollection<string> CraftSkillDescriptorList { get; } = new();
    public ObservableCollection<string> AbilityDescriptorList { get; } = new();
    public ObservableCollection<string> AbilityTypeDescriptorList { get; } = new();
    public ObservableCollection<string> BuffDescriptorList { get; } = new();
    public ObservableCollection<string> FeatDescriptorList { get; } = new();
    public ObservableCollection<string> MagicDescriptorList { get; } = new();
    public ObservableCollection<string> SaveDescriptorList { get; } = new();
    public ObservableCollection<string> SkillDescriptorList { get; } = new();
    public ObservableCollection<string> SystemDescriptorList { get; } = new();
    public ObservableCollection<string> TraitDescriptorList { get; } = new();
    public ObservableCollection<string> UsageDescriptorList { get; } = new();
    public ObservableCollection<string> DurationDescriptorList { get; } = new();
    public ObservableCollection<string> KnowledgeDescriptorList { get; } = new();
    public ObservableCollection<string> MagicAuraDescriptorList { get; } = new();
    public ObservableCollection<string> PerformDescriptorList { get; } = new();
    public ObservableCollection<string> SpellSchoolDescriptorList { get; } = new();
    public ObservableCollection<string> StimulusDescriptorList { get; } = new();
    public ObservableCollection<string> TerrainDescriptorList { get; } = new();
    public ObservableCollection<string> CustomDescriptorList { get; } = new();

    private bool ActiveTraitFilters => _contentFilters.Any() ||
                                       _creatureFilters.Any() ||
                                       _creatureTypeFilters.Any() ||
                                       _creatureSubTypeFilters.Any() ||
                                       _craftSkillFilters.Any() ||
                                       _abilityFilters.Any() ||
                                       _abilityTypeFilters.Any() ||
                                       _buffFilters.Any() ||
                                       _featFilters.Any() ||
                                       _magicFilters.Any() ||
                                       _saveFilters.Any() ||
                                       _skillFilters.Any() ||
                                       _systemFilters.Any() ||
                                       _traitFilters.Any() ||
                                       _usageFilters.Any() ||
                                       _durationFilters.Any() ||
                                       _knowledgeFilters.Any() ||
                                       _magicAuraFilters.Any() ||
                                       _performSkillFilters.Any() ||
                                       _spellSchoolFilters.Any() ||
                                       _stimulusFilters.Any() ||
                                       _terrainFilters.Any() ||
                                       _customFilters.Any();

    public event PropertyChangedEventHandler? PropertyChanged;

    public void SetDescriptorVisibility(DescriptorSettings settings)
    {
        LevelVisibility = settings.ShowLevel ? Visibility.Visible : Visibility.Collapsed;
        ContentVisibility = settings.ShowContent ? Visibility.Visible : Visibility.Collapsed;
        CreatureVisibility = settings.ShowCreature ? Visibility.Visible : Visibility.Collapsed;
        CreatureTypeVisibility = settings.ShowCreatureType ? Visibility.Visible : Visibility.Collapsed;
        CreatureSubTypeVisibility = settings.ShowCreatureSubType ? Visibility.Visible : Visibility.Collapsed;
        CraftSkillVisibility = settings.ShowCraftSkill ? Visibility.Visible : Visibility.Collapsed;
        AbilityVisibility = settings.ShowAbility ? Visibility.Visible : Visibility.Collapsed;
        AbilityTypeVisibility = settings.ShowAbilityType ? Visibility.Visible : Visibility.Collapsed;
        BuffVisibility = settings.ShowBuff ? Visibility.Visible : Visibility.Collapsed;
        FeatVisibility = settings.ShowFeat ? Visibility.Visible : Visibility.Collapsed;
        MagicVisibility = settings.ShowMagic ? Visibility.Visible : Visibility.Collapsed;
        SaveVisibility = settings.ShowSave ? Visibility.Visible : Visibility.Collapsed;
        SkillVisibility = settings.ShowSkill ? Visibility.Visible : Visibility.Collapsed;
        SystemVisibility = settings.ShowSystem ? Visibility.Visible : Visibility.Collapsed;
        TraitVisibility = settings.ShowTrait ? Visibility.Visible : Visibility.Collapsed;
        UsageVisibility = settings.ShowUsage ? Visibility.Visible : Visibility.Collapsed;
        DurationVisibility = settings.ShowDuration ? Visibility.Visible : Visibility.Collapsed;
        KnowledgeVisibility = settings.ShowKnowledge ? Visibility.Visible : Visibility.Collapsed;
        MagicAuraVisibility = settings.ShowMagicAura ? Visibility.Visible : Visibility.Collapsed;
        PerformVisibility = settings.ShowPerform ? Visibility.Visible : Visibility.Collapsed;
        SpellSchoolVisibility = settings.ShowSpellSchool ? Visibility.Visible : Visibility.Collapsed;
        StimulusVisibility = settings.ShowStimulus ? Visibility.Visible : Visibility.Collapsed;
        TerrainVisibility = settings.ShowTerrain ? Visibility.Visible : Visibility.Collapsed;
        CustomVisibility = settings.ShowCustom ? Visibility.Visible : Visibility.Collapsed;
    }

    public void RefreshButtonState()
    {
        ContentCheckboxCommand.RaiseCanExecuteChanged();
        CreatureCheckboxCommand.RaiseCanExecuteChanged();
        CreatureTypeCheckboxCommand.RaiseCanExecuteChanged();
        CreatureSubTypeCheckboxCommand.RaiseCanExecuteChanged();
        CraftSkillCheckboxCommand.RaiseCanExecuteChanged();
        AbilityCheckboxCommand.RaiseCanExecuteChanged();
        AbilityTypeCheckboxCommand.RaiseCanExecuteChanged();
        BuffCheckboxCommand.RaiseCanExecuteChanged();
        FeatCheckboxCommand.RaiseCanExecuteChanged();
        MagicCheckboxCommand.RaiseCanExecuteChanged();
        SaveCheckboxCommand.RaiseCanExecuteChanged();
        SkillCheckboxCommand.RaiseCanExecuteChanged();
        SystemCheckboxCommand.RaiseCanExecuteChanged();
        TraitCheckboxCommand.RaiseCanExecuteChanged();
        UsageCheckboxCommand.RaiseCanExecuteChanged();
        DurationCheckboxCommand.RaiseCanExecuteChanged();
        KnowledgeCheckboxCommand.RaiseCanExecuteChanged();
        MagicAuraCheckboxCommand.RaiseCanExecuteChanged();
        PerformCheckboxCommand.RaiseCanExecuteChanged();
        SpellSchoolCheckboxCommand.RaiseCanExecuteChanged();
        StimulusCheckboxCommand.RaiseCanExecuteChanged();
        TerrainCheckboxCommand.RaiseCanExecuteChanged();
        CustomCheckboxCommand.RaiseCanExecuteChanged();
    }

    internal void Clear()
    {
        ContentDescriptorList.Clear();
        CreatureDescriptorList.Clear();
        CreatureTypeDescriptorList.Clear();
        CreatureSubTypeDescriptorList.Clear();
        CraftSkillDescriptorList.Clear();
        AbilityDescriptorList.Clear();
        AbilityTypeDescriptorList.Clear();
        BuffDescriptorList.Clear();
        FeatDescriptorList.Clear();
        MagicDescriptorList.Clear();
        SaveDescriptorList.Clear();
        SkillDescriptorList.Clear();
        SystemDescriptorList.Clear();
        TraitDescriptorList.Clear();
        UsageDescriptorList.Clear();
        DurationDescriptorList.Clear();
        KnowledgeDescriptorList.Clear();
        MagicAuraDescriptorList.Clear();
        PerformDescriptorList.Clear();
        SpellSchoolDescriptorList.Clear();
        StimulusDescriptorList.Clear();
        TerrainDescriptorList.Clear();
    }

    internal void GetDescriptors(AppMode mode)
    {
        ContentDescriptorList.Fill(DescriptorUtils.GetContent(mode));
        CreatureDescriptorList.Fill(DescriptorUtils.GetCreature(mode));
        CreatureTypeDescriptorList.Fill(DescriptorUtils.GetCreatureType(mode));
        CreatureSubTypeDescriptorList.Fill(DescriptorUtils.GetCreatureSubType(mode));
        CraftSkillDescriptorList.Fill(DescriptorUtils.GetCraftSkill(mode));
        AbilityDescriptorList.Fill(DescriptorUtils.GetAbilityScore(mode));
        AbilityTypeDescriptorList.Fill(DescriptorUtils.GetAbilityType(mode));
        BuffDescriptorList.Fill(DescriptorUtils.GetBuff(mode));
        FeatDescriptorList.Fill(DescriptorUtils.GetDescFeat(mode));
        MagicDescriptorList.Fill(DescriptorUtils.GetDescMagic(mode));
        SaveDescriptorList.Fill(DescriptorUtils.GetSave(mode));
        SkillDescriptorList.Fill(DescriptorUtils.GetDescSkill(mode));
        SystemDescriptorList.Fill(DescriptorUtils.GetSystem(mode));
        TraitDescriptorList.Fill(DescriptorUtils.GetDescTrait(mode));
        UsageDescriptorList.Fill(DescriptorUtils.GetUsage(mode));
        DurationDescriptorList.Fill(DescriptorUtils.GetDuration(mode));
        KnowledgeDescriptorList.Fill(DescriptorUtils.GetKnowledge(mode));
        MagicAuraDescriptorList.Fill(DescriptorUtils.GetMagicAura(mode));
        PerformDescriptorList.Fill(DescriptorUtils.GetPerformSkill(mode));
        SpellSchoolDescriptorList.Fill(DescriptorUtils.GetSpellSchool(mode));
        StimulusDescriptorList.Fill(DescriptorUtils.GetStimulus(mode));
        TerrainDescriptorList.Fill(DescriptorUtils.GetTerrain(mode));
    }

    internal IEnumerable<T> BroadFilter<T>(ICollection<T> possibleValues) where T : ITaggable
    {
        if (!ActiveTraitFilters)
        {
            foreach (T value in possibleValues)
            {
                yield return value;
            }

            yield break;
        }

        foreach (T value in possibleValues)
        {
            if (_contentFilters.Any(filter => value.ContentDescriptors.Contains(filter)) ||
                _creatureFilters.Any(filter => value.CreatureDescriptors.Contains(filter)) ||
                _creatureTypeFilters.Any(filter => value.CreatureTypeDescriptors.Contains(filter)) ||
                _creatureSubTypeFilters.Any(filter => value.CreatureSubTypeDescriptors.Contains(filter)) ||
                _craftSkillFilters.Any(filter => value.CraftSkillDescriptors.Contains(filter)) ||
                _abilityFilters.Any(filter => value.AbilityDescriptors.Contains(filter)) ||
                _abilityTypeFilters.Any(filter => value.AbilityTypeDescriptors.Contains(filter)) ||
                _buffFilters.Any(filter => value.BuffDescriptors.Contains(filter)) ||
                _featFilters.Any(filter => value.FeatDescriptors.Contains(filter)) ||
                _magicFilters.Any(filter => value.MagicDescriptors.Contains(filter)) ||
                _saveFilters.Any(filter => value.SaveDescriptors.Contains(filter)) ||
                _skillFilters.Any(filter => value.SkillDescriptors.Contains(filter)) ||
                _systemFilters.Any(filter => value.SystemDescriptors.Contains(filter)) ||
                _traitFilters.Any(filter => value.TraitDescriptors.Contains(filter)) ||
                _usageFilters.Any(filter => value.UsageDescriptors.Contains(filter)) ||
                _durationFilters.Any(filter => value.DurationDescriptors.Contains(filter)) ||
                _knowledgeFilters.Any(filter => value.KnowledgeDescriptors.Contains(filter)) ||
                _magicAuraFilters.Any(filter => value.MagicAuraDescriptors.Contains(filter)) ||
                _performSkillFilters.Any(filter => value.PerformSkillDescriptors.Contains(filter)) ||
                _spellSchoolFilters.Any(filter => value.SpellSchoolDescriptors.Contains(filter)) ||
                _stimulusFilters.Any(filter => value.StimulusDescriptors.Contains(filter)) ||
                _terrainFilters.Any(filter => value.TerrainDescriptors.Contains(filter)) ||
                _customFilters.Any(filter => value.CustomDescriptors.Contains(filter)))
            {
                yield return value;
            }
        }
    }

    private IEnumerable<T> StrictFilter<T>(ICollection<T> possibleValues) where T : ITaggable
    {
        possibleValues = _contentFilters.Aggregate(possibleValues,
            (current, filter) => current.Where(x => x.ContentDescriptors.Contains(filter)).ToList());
        possibleValues = _creatureFilters.Aggregate(possibleValues,
            (current, filter) => current.Where(x => x.CreatureDescriptors.Contains(filter)).ToList());
        possibleValues = _creatureTypeFilters.Aggregate(possibleValues,
            (current, filter) => current.Where(x => x.CreatureTypeDescriptors.Contains(filter)).ToList());
        possibleValues = _creatureSubTypeFilters.Aggregate(possibleValues,
            (current, filter) => current.Where(x => x.CreatureSubTypeDescriptors.Contains(filter)).ToList());
        possibleValues = _craftSkillFilters.Aggregate(possibleValues,
            (current, filter) => current.Where(x => x.CraftSkillDescriptors.Contains(filter)).ToList());
        possibleValues = _abilityFilters.Aggregate(possibleValues,
            (current, filter) => current.Where(x => x.AbilityDescriptors.Contains(filter)).ToList());
        possibleValues = _abilityTypeFilters.Aggregate(possibleValues,
            (current, filter) => current.Where(x => x.AbilityTypeDescriptors.Contains(filter)).ToList());
        possibleValues = _buffFilters.Aggregate(possibleValues,
            (current, filter) => current.Where(x => x.BuffDescriptors.Contains(filter)).ToList());
        possibleValues = _featFilters.Aggregate(possibleValues,
            (current, filter) => current.Where(x => x.FeatDescriptors.Contains(filter)).ToList());
        possibleValues = _magicFilters.Aggregate(possibleValues,
            (current, filter) => current.Where(x => x.MagicDescriptors.Contains(filter)).ToList());
        possibleValues = _saveFilters.Aggregate(possibleValues,
            (current, filter) => current.Where(x => x.SaveDescriptors.Contains(filter)).ToList());
        possibleValues = _skillFilters.Aggregate(possibleValues,
            (current, filter) => current.Where(x => x.SkillDescriptors.Contains(filter)).ToList());
        possibleValues = _systemFilters.Aggregate(possibleValues,
            (current, filter) => current.Where(x => x.SystemDescriptors.Contains(filter)).ToList());
        possibleValues = _traitFilters.Aggregate(possibleValues,
            (current, filter) => current.Where(x => x.TraitDescriptors.Contains(filter)).ToList());
        possibleValues = _usageFilters.Aggregate(possibleValues,
            (current, filter) => current.Where(x => x.UsageDescriptors.Contains(filter)).ToList());
        possibleValues = _durationFilters.Aggregate(possibleValues,
            (current, filter) => current.Where(x => x.DurationDescriptors.Contains(filter)).ToList());
        possibleValues = _knowledgeFilters.Aggregate(possibleValues,
            (current, filter) => current.Where(x => x.KnowledgeDescriptors.Contains(filter)).ToList());
        possibleValues = _magicAuraFilters.Aggregate(possibleValues,
            (current, filter) => current.Where(x => x.MagicAuraDescriptors.Contains(filter)).ToList());
        possibleValues = _performSkillFilters.Aggregate(possibleValues,
            (current, filter) => current.Where(x => x.PerformSkillDescriptors.Contains(filter)).ToList());
        possibleValues = _spellSchoolFilters.Aggregate(possibleValues,
            (current, filter) => current.Where(x => x.SpellSchoolDescriptors.Contains(filter)).ToList());
        possibleValues = _stimulusFilters.Aggregate(possibleValues,
            (current, filter) => current.Where(x => x.StimulusDescriptors.Contains(filter)).ToList());
        possibleValues = _terrainFilters.Aggregate(possibleValues,
            (current, filter) => current.Where(x => x.TerrainDescriptors.Contains(filter)).ToList());
        possibleValues = _customFilters.Aggregate(possibleValues,
            (current, filter) => current.Where(x => x.CustomDescriptors.Contains(filter)).ToList());

        foreach (T value in possibleValues)
        {
            yield return value;
        }
    }

    public string MinLevel
    {
        get => _minLevel;
        set
        {
            if (value == "")
            {
                _minLevel = value;
                OnPropertyChanged("FeatMinLevel");
                return;
            }

            if (!RegexHandler.NumberFilter.IsMatch(value))
            {
                int input = int.Parse(value);
                if (value == _minLevel)
                {
                    return;
                }

                _minLevel = input > int.Parse(_maxLevel) ? _maxLevel : value;
                _source.ApplyFilters();
                OnPropertyChanged("MinLevel");
            }
            else
            {
                string messageBoxText = "Level must be an integer between 0 and 20";
                string caption = "Error";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;

                MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.OK);
            }
        }
    }

    public string MaxLevel
    {
        get => _maxLevel;
        set
        {
            if (value == "")
            {
                _maxLevel = value;
                OnPropertyChanged("MaxLevel");
                return;
            }

            if (!RegexHandler.NumberFilter.IsMatch(value) && int.Parse(value) <= 20)
            {
                int input = int.Parse(value);
                if (value == _maxLevel)
                {
                    return;
                }

                _maxLevel = input < int.Parse(_minLevel) ? _minLevel : value;
                _source.ApplyFilters();
                OnPropertyChanged("MaxLevel");
            }
            else
            {
                string messageBoxText = "Level must be an integer between 0 and 20";
                string caption = "Error";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;

                MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.OK);
            }
        }
    }

    internal void RefreshMode()
    {
        GetDescriptors(AppSettings.Mode);
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    #region Actions

    private void ContentFilterAction(object arg)
    {
        if (arg is string filter)
        {
            DescContent toggle = filter.StringToEnum<DescContent>();

            if (!_contentFilters.Add(toggle))
            {
                _contentFilters.Remove(toggle);
            }

            _source.ApplyFilters();
        }
    }

    private void CreatureFilterAction(object arg)
    {
        if (arg is string filter)
        {
            Creature toggle = filter.StringToEnum<Creature>();

            if (!_creatureFilters.Add(toggle))
            {
                _creatureFilters.Remove(toggle);
            }

            _source.ApplyFilters();
        }
    }

    private void CreatureTypeFilterAction(object arg)
    {
        if (arg is string filter)
        {
            CreatureType toggle = filter.StringToEnum<CreatureType>();

            if (!_creatureTypeFilters.Add(toggle))
            {
                _creatureTypeFilters.Remove(toggle);
            }

            _source.ApplyFilters();
        }
    }

    private void CreatureSubTypeFilterAction(object arg)
    {
        if (arg is string filter)
        {
            CreatureSubType toggle = filter.StringToEnum<CreatureSubType>();

            if (!_creatureSubTypeFilters.Add(toggle))
            {
                _creatureSubTypeFilters.Remove(toggle);
            }

            _source.ApplyFilters();
        }
    }

    private void CraftSkillFilterAction(object arg)
    {
        if (arg is string filter)
        {
            CraftSkill toggle = filter.StringToEnum<CraftSkill>();

            if (!_craftSkillFilters.Add(toggle))
            {
                _craftSkillFilters.Remove(toggle);
            }

            _source.ApplyFilters();
        }
    }

    private void AbilityFilterAction(object arg)
    {
        if (arg is string filter)
        {
            DescAbility toggle = filter.StringToEnum<DescAbility>();

            if (!_abilityFilters.Add(toggle))
            {
                _abilityFilters.Remove(toggle);
            }

            _source.ApplyFilters();
        }
    }

    private void AbilityTypeFilterAction(object arg)
    {
        if (arg is string filter)
        {
            AbilityType toggle = filter.StringToEnum<AbilityType>();

            if (!_abilityTypeFilters.Add(toggle))
            {
                _abilityTypeFilters.Remove(toggle);
            }

            _source.ApplyFilters();
        }
    }

    private void BuffFilterAction(object arg)
    {
        if (arg is string filter)
        {
            DescBuff toggle = filter.StringToEnum<DescBuff>();

            if (!_buffFilters.Add(toggle))
            {
                _buffFilters.Remove(toggle);
            }

            _source.ApplyFilters();
        }
    }

    private void FeatFilterAction(object arg)
    {
        if (arg is string filter)
        {
            DescFeat toggle = filter.StringToEnum<DescFeat>();

            if (!_featFilters.Add(toggle))
            {
                _featFilters.Remove(toggle);
            }

            _source.ApplyFilters();
        }
    }

    private void MagicFilterAction(object arg)
    {
        if (arg is string filter)
        {
            DescMagic toggle = filter.StringToEnum<DescMagic>();

            if (!_magicFilters.Add(toggle))
            {
                _magicFilters.Remove(toggle);
            }

            _source.ApplyFilters();
        }
    }

    private void SaveFilterAction(object arg)
    {
        if (arg is string filter)
        {
            DescSave toggle = filter.StringToEnum<DescSave>();

            if (!_saveFilters.Add(toggle))
            {
                _saveFilters.Remove(toggle);
            }

            _source.ApplyFilters();
        }
    }

    private void SkillFilterAction(object arg)
    {
        if (arg is string filter)
        {
            DescSkill toggle = filter.StringToEnum<DescSkill>();

            if (!_skillFilters.Add(toggle))
            {
                _skillFilters.Remove(toggle);
            }

            _source.ApplyFilters();
        }
    }

    private void SystemFilterAction(object arg)
    {
        if (arg is string filter)
        {
            DescSystem toggle = filter.StringToEnum<DescSystem>();

            if (!_systemFilters.Add(toggle))
            {
                _systemFilters.Remove(toggle);
            }

            _source.ApplyFilters();
        }
    }

    private void TraitFilterAction(object arg)
    {
        if (arg is string filter)
        {
            DescTrait toggle = filter.StringToEnum<DescTrait>();

            if (!_traitFilters.Add(toggle))
            {
                _traitFilters.Remove(toggle);
            }

            _source.ApplyFilters();
        }
    }

    private void UsageFilterAction(object arg)
    {
        if (arg is string filter)
        {
            DescUsage toggle = filter.StringToEnum<DescUsage>();

            if (!_usageFilters.Add(toggle))
            {
                _usageFilters.Remove(toggle);
            }

            _source.ApplyFilters();
        }
    }

    private void DurationFilterAction(object arg)
    {
        if (arg is string filter)
        {
            DescDuration toggle = filter.StringToEnum<DescDuration>();

            if (!_durationFilters.Add(toggle))
            {
                _durationFilters.Remove(toggle);
            }

            _source.ApplyFilters();
        }
    }

    private void KnowledgeFilterAction(object arg)
    {
        if (arg is string filter)
        {
            Knowledge toggle = filter.StringToEnum<Knowledge>();

            if (!_knowledgeFilters.Add(toggle))
            {
                _knowledgeFilters.Remove(toggle);
            }

            _source.ApplyFilters();
        }
    }

    private void MagicAuraFilterAction(object arg)
    {
        if (arg is string filter)
        {
            MagicAura toggle = filter.StringToEnum<MagicAura>();

            if (!_magicAuraFilters.Add(toggle))
            {
                _magicAuraFilters.Remove(toggle);
            }

            _source.ApplyFilters();
        }
    }

    private void PerformFilterAction(object arg)
    {
        if (arg is string filter)
        {
            PerformSkill toggle = filter.StringToEnum<PerformSkill>();

            if (!_performSkillFilters.Add(toggle))
            {
                _performSkillFilters.Remove(toggle);
            }

            _source.ApplyFilters();
        }
    }

    private void SpellSchoolFilterAction(object arg)
    {
        if (arg is string filter)
        {
            SpellSchool toggle = filter.StringToEnum<SpellSchool>();

            if (!_spellSchoolFilters.Add(toggle))
            {
                _spellSchoolFilters.Remove(toggle);
            }

            _source.ApplyFilters();
        }
    }

    private void StimulusFilterAction(object arg)
    {
        if (arg is string filter)
        {
            Stimulus toggle = filter.StringToEnum<Stimulus>();

            if (!_stimulusFilters.Add(toggle))
            {
                _stimulusFilters.Remove(toggle);
            }

            _source.ApplyFilters();
        }
    }

    private void TerrainFilterAction(object arg)
    {
        if (arg is string filter)
        {
            Terrain toggle = filter.StringToEnum<Terrain>();

            if (!_terrainFilters.Add(toggle))
            {
                _terrainFilters.Remove(toggle);
            }

            _source.ApplyFilters();
        }
    }

    private void CustomFilterAction(object arg)
    {
        if (arg is string filter)
        {
            if (_customFilters.Contains(filter))
            {
                _customFilters.Remove(filter);
            }
            else
            {
                _customFilters.Add(filter);
            }

            _source.ApplyFilters();
        }
    }

    private void MinLevelRemovePlaceholderTextAction(object arg)
    {
        if (MinLevel == AppSettings.MinLevelPlaceholder.ToString())
        {
            MinLevel = "";
        }
    }

    private void MinLevelAddPlaceholderTextAction(object arg)
    {
        if (string.IsNullOrWhiteSpace(MinLevel))
        {
            MinLevel = AppSettings.MinLevelPlaceholder.ToString();
        }
    }

    private void MaxLevelRemovePlaceholderTextAction(object arg)
    {
        if (MaxLevel == AppSettings.MaxLevelPlaceholder.ToString())
        {
            MaxLevel = "";
        }
    }

    private void MaxLevelAddPlaceholderTextAction(object arg)
    {
        if (string.IsNullOrWhiteSpace(MaxLevel))
        {
            MaxLevel = AppSettings.MaxLevelPlaceholder.ToString();
        }
    }

    #endregion
}