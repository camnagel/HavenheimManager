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

public class TraitHandler : INotifyPropertyChanged
{
    private readonly HashSet<Bonus> _bonusTraitFilters = new();
    private readonly HashSet<Class> _classTraitFilters = new();
    private readonly HashSet<Combat> _combatTraitFilters = new();
    private readonly HashSet<Condition> _conditionTraitFilters = new();

    // Trait Filter Lists
    private readonly HashSet<Core> _coreTraitFilters = new();
    private readonly HashSet<string> _customTraitFilters = new();
    private readonly HashSet<Magic> _magicTraitFilters = new();
    private readonly HashSet<Role> _roleTraitFilters = new();
    private readonly HashSet<Skill> _skillTraitFilters = new();
    private readonly HashSet<Source> _sourceTraitFilters = new();

    private Trait? _selectedTrait;

    private string _traitSearchText = RegexHandler.SearchPlaceholderText;

    public TraitHandler()
    {
        CoreTraitCheckboxCommand = new DelegateCommand(TraitCoreFilterAction);
        SkillTraitCheckboxCommand = new DelegateCommand(TraitSkillFilterAction);
        ClassTraitCheckboxCommand = new DelegateCommand(TraitClassFilterAction);
        CombatTraitCheckboxCommand = new DelegateCommand(TraitCombatFilterAction);
        RoleTraitCheckboxCommand = new DelegateCommand(TraitRoleFilterAction);
        MagicTraitCheckboxCommand = new DelegateCommand(TraitMagicFilterAction);
        BonusTraitCheckboxCommand = new DelegateCommand(TraitBonusFilterAction);
        ConditionTraitCheckboxCommand = new DelegateCommand(TraitConditionFilterAction);
        SourceTraitCheckboxCommand = new DelegateCommand(TraitSourceFilterAction);
        CustomTraitCheckboxCommand = new DelegateCommand(TraitCustomFilterAction);
        AddFavoriteTraitCommand = new DelegateCommand(AddFavoriteTraitAction);
        AddHiddenTraitCommand = new DelegateCommand(AddHiddenTraitAction);
        EditTraitCommand = new DelegateCommand(EditTraitAction);
        NewTraitCommand = new DelegateCommand(NewTraitAction);
        RemoveTraitCommand = new DelegateCommand(RemoveTraitAction);
        RemoveFavoriteTraitCommand = new DelegateCommand(RemoveFavoriteTraitAction);
        RemoveHiddenTraitCommand = new DelegateCommand(RemoveHiddenTraitAction);
        TraitSearchRemovePlaceholderTextCommand =
            new DelegateCommand(TraitSearchRemovePlaceholderTextAction);
        TraitSearchAddPlaceholderTextCommand = new DelegateCommand(TraitSearchAddPlaceholderTextAction);
    }

    // Trait Checkbox Commands
    public DelegateCommand CoreTraitCheckboxCommand { get; }
    public DelegateCommand SkillTraitCheckboxCommand { get; }
    public DelegateCommand ClassTraitCheckboxCommand { get; }
    public DelegateCommand CombatTraitCheckboxCommand { get; }
    public DelegateCommand RoleTraitCheckboxCommand { get; }
    public DelegateCommand MagicTraitCheckboxCommand { get; }
    public DelegateCommand BonusTraitCheckboxCommand { get; }
    public DelegateCommand ConditionTraitCheckboxCommand { get; }
    public DelegateCommand SourceTraitCheckboxCommand { get; }
    public DelegateCommand CustomTraitCheckboxCommand { get; }

    // Trait Control Bar Commands
    public DelegateCommand TraitSearchRemovePlaceholderTextCommand { get; }
    public DelegateCommand TraitSearchAddPlaceholderTextCommand { get; }
    public DelegateCommand AddFavoriteTraitCommand { get; }
    public DelegateCommand AddHiddenTraitCommand { get; }
    public DelegateCommand EditTraitCommand { get; }
    public DelegateCommand NewTraitCommand { get; }
    public DelegateCommand RemoveTraitCommand { get; }
    public DelegateCommand RemoveFavoriteTraitCommand { get; }
    public DelegateCommand RemoveHiddenTraitCommand { get; }

    // Filtered Trait Collections
    public ObservableCollection<Trait> FilteredTraitList { get; set; } = new();
    public ObservableCollection<Trait> FavoriteTraitList { get; set; } = new();
    public ObservableCollection<Trait> HiddenTraitList { get; set; } = new();

    // Master trait list
    public List<Trait> MasterTraitList { get; } = new();

    // Trait Tag Collections
    public ObservableCollection<string> CustomTraitFilterList { get; set; } = new();
    public ObservableCollection<string> CoreTraitFilterList { get; set; } = new();
    public ObservableCollection<string> SkillTraitFilterList { get; set; } = new();
    public ObservableCollection<string> ClassTraitFilterList { get; set; } = new();
    public ObservableCollection<string> CombatTraitFilterList { get; set; } = new();
    public ObservableCollection<string> RoleTraitFilterList { get; set; } = new();
    public ObservableCollection<string> MagicTraitFilterList { get; set; } = new();
    public ObservableCollection<string> BonusTraitFilterList { get; set; } = new();
    public ObservableCollection<string> ConditionTraitFilterList { get; set; } = new();
    public ObservableCollection<string> SourceTraitFilterList { get; set; } = new();

    public Trait? SelectedTrait
    {
        get => _selectedTrait;
        set
        {
            if (value != null)
            {
                SelectedTrait = null;
            }

            _selectedTrait = value;
            CurrentTrait.Clear();
            if (value != null)
            {
                CurrentTrait.Add(value);
            }

            OnPropertyChanged("SelectedTrait");
        }
    }

    public ObservableCollection<Trait> CurrentTrait { get; set; } = new();

    public string TraitSearchText
    {
        get => _traitSearchText;
        set
        {
            _traitSearchText = value;
            ApplyTraitFilters();

            OnPropertyChanged("TraitSearchText");
        }
    }

    private bool ActiveTraitFilters => _coreTraitFilters.Any() ||
                                        _skillTraitFilters.Any() ||
                                        _classTraitFilters.Any() ||
                                        _combatTraitFilters.Any() ||
                                        _roleTraitFilters.Any() ||
                                        _conditionTraitFilters.Any() ||
                                        _sourceTraitFilters.Any() ||
                                        _customTraitFilters.Any() ||
                                        _magicTraitFilters.Any() ||
                                        _bonusTraitFilters.Any();

    public event PropertyChangedEventHandler? PropertyChanged;

    internal void TraitCoreFilterAction(object arg)
    {
        if (arg is string filter)
        {
            Core toggleCore = filter.StringToCore();

            if (_coreTraitFilters.Contains(toggleCore))
            {
                _coreTraitFilters.Remove(toggleCore);
            }
            else
            {
                _coreTraitFilters.Add(toggleCore);
            }

            ApplyTraitFilters();
        }
    }

    public void RefreshButtonState()
    {
        CoreTraitCheckboxCommand.RaiseCanExecuteChanged();
    }

    internal void TraitSkillFilterAction(object arg)
    {
        if (arg is string filter)
        {
            Skill toggleSkill = filter.StringToSkill();

            if (_skillTraitFilters.Contains(toggleSkill))
            {
                _skillTraitFilters.Remove(toggleSkill);
            }
            else
            {
                _skillTraitFilters.Add(toggleSkill);
            }

            ApplyTraitFilters();
        }
    }

    internal void TraitClassFilterAction(object arg)
    {
        if (arg is string filter)
        {
            Class toggleClass = filter.StringToClass();

            if (_classTraitFilters.Contains(toggleClass))
            {
                _classTraitFilters.Remove(toggleClass);
            }
            else
            {
                _classTraitFilters.Add(toggleClass);
            }

            ApplyTraitFilters();
        }
    }

    internal void TraitCombatFilterAction(object arg)
    {
        if (arg is string filter)
        {
            Combat toggleCombat = filter.StringToCombat();

            if (_combatTraitFilters.Contains(toggleCombat))
            {
                _combatTraitFilters.Remove(toggleCombat);
            }
            else
            {
                _combatTraitFilters.Add(toggleCombat);
            }

            ApplyTraitFilters();
        }
    }

    internal void TraitRoleFilterAction(object arg)
    {
        if (arg is string filter)
        {
            Role toggleRole = filter.StringToRole();

            if (_roleTraitFilters.Contains(toggleRole))
            {
                _roleTraitFilters.Remove(toggleRole);
            }
            else
            {
                _roleTraitFilters.Add(toggleRole);
            }

            ApplyTraitFilters();
        }
    }

    internal void TraitMagicFilterAction(object arg)
    {
        if (arg is string filter)
        {
            Magic toggleMagic = filter.StringToMagic();

            if (_magicTraitFilters.Contains(toggleMagic))
            {
                _magicTraitFilters.Remove(toggleMagic);
            }
            else
            {
                _magicTraitFilters.Add(toggleMagic);
            }

            ApplyTraitFilters();
        }
    }

    internal void TraitBonusFilterAction(object arg)
    {
        if (arg is string filter)
        {
            Bonus toggleBonus = filter.StringToBonus();

            if (_bonusTraitFilters.Contains(toggleBonus))
            {
                _bonusTraitFilters.Remove(toggleBonus);
            }
            else
            {
                _bonusTraitFilters.Add(toggleBonus);
            }

            ApplyTraitFilters();
        }
    }

    internal void TraitConditionFilterAction(object arg)
    {
        if (arg is string filter)
        {
            Condition toggleCondition = filter.StringToCondition();

            if (_conditionTraitFilters.Contains(toggleCondition))
            {
                _conditionTraitFilters.Remove(toggleCondition);
            }
            else
            {
                _conditionTraitFilters.Add(toggleCondition);
            }

            ApplyTraitFilters();
        }
    }

    internal void TraitSourceFilterAction(object arg)
    {
        if (arg is string filter)
        {
            Source toggleSource = filter.StringToSource();

            if (_sourceTraitFilters.Contains(toggleSource))
            {
                _sourceTraitFilters.Remove(toggleSource);
            }
            else
            {
                _sourceTraitFilters.Add(toggleSource);
            }

            ApplyTraitFilters();
        }
    }

    internal void TraitCustomFilterAction(object arg)
    {
        if (arg is string filter)
        {
            if (_customTraitFilters.Contains(filter))
            {
                _customTraitFilters.Remove(filter);
            }
            else
            {
                _customTraitFilters.Add(filter);
            }

            ApplyTraitFilters();
        }
    }

    internal void ApplyTraitFilters()
    {
        FilteredTraitList.Clear();
        List<Trait> possibleTraits =
            (TraitSearchText != RegexHandler.SearchPlaceholderText && TraitSearchText != ""
                ? MasterTraitList.Where(x => x.Name.Sanitize().Contains(TraitSearchText.Sanitize())).ToList()
                : MasterTraitList)
            .Where(x => !FavoriteTraitList.Contains(x) && !HiddenTraitList.Contains(x)).ToList();

        foreach (Source filter in _sourceTraitFilters)
        {
            possibleTraits = possibleTraits.Where(x => x.Source == filter).ToList();
        }

        foreach (Trait trait in BroadTraitFilter(possibleTraits))
        {
            FilteredTraitList.Add(trait);
        }
    }

    internal void Clear()
    {
        CoreTraitFilterList.Clear();
        SourceTraitFilterList.Clear();
        SkillTraitFilterList.Clear();
        CombatTraitFilterList.Clear();
        RoleTraitFilterList.Clear();
        MagicTraitFilterList.Clear();
        BonusTraitFilterList.Clear();
        ConditionTraitFilterList.Clear();
        ClassTraitFilterList.Clear();
    }

    internal void InitializePathfinder()
    {

    }

    internal void InitializeHavenheim()
    {
        CoreTraitFilterList.Fill<Core>(typeof(Core));
        SourceTraitFilterList.Fill<Source>(typeof(Source));
        SkillTraitFilterList.Fill<Skill>(typeof(Skill));
        CombatTraitFilterList.Fill<Combat>(typeof(Combat));
        RoleTraitFilterList.Fill<Role>(typeof(Role));
        MagicTraitFilterList.Fill<Magic>(typeof(Magic));
        BonusTraitFilterList.Fill<Bonus>(typeof(Bonus));
        ConditionTraitFilterList.Fill<Condition>(typeof(Condition));
        ClassTraitFilterList.Fill<Class>(typeof(Class));
    }

    private IEnumerable<Trait> BroadTraitFilter(List<Trait> possibleTraits)
    {
        if (!ActiveTraitFilters)
        {
            foreach (Trait trait in possibleTraits)
            {
                yield return trait;
            }

            yield break;
        }

        foreach (Trait trait in possibleTraits)
        {
            if (_coreTraitFilters.Any(filter => trait.CoreTags.Contains(filter)))
            {
                yield return trait;
                continue;
            }

            if (_skillTraitFilters.Any(filter => trait.SkillTags.Contains(filter)))
            {
                yield return trait;
                continue;
            }

            if (_classTraitFilters.Any(filter => trait.ClassTags.Contains(filter)))
            {
                yield return trait;
                continue;
            }

            if (_combatTraitFilters.Any(filter => trait.CombatTags.Contains(filter)))
            {
                yield return trait;
                continue;
            }

            if (_roleTraitFilters.Any(filter => trait.RoleTags.Contains(filter)))
            {
                yield return trait;
                continue;
            }

            if (_magicTraitFilters.Any(filter => trait.MagicTags.Contains(filter)))
            {
                yield return trait;
                continue;
            }

            if (_bonusTraitFilters.Any(filter => trait.BonusTags.Contains(filter)))
            {
                yield return trait;
                continue;
            }

            if (_conditionTraitFilters.Any(filter => trait.ConditionTags.Contains(filter)))
            {
                yield return trait;
                continue;
            }

            if (_customTraitFilters.Any(filter => trait.CustomTags.Contains(filter)))
            {
                yield return trait;
            }
        }
    }

    private IEnumerable<Trait> StrictTraitFilter(List<Trait> possibleTraits)
    {
        foreach (Core filter in _coreTraitFilters)
        {
            possibleTraits = possibleTraits.Where(x => x.CoreTags.Contains(filter)).ToList();
        }

        foreach (Skill filter in _skillTraitFilters)
        {
            possibleTraits = possibleTraits.Where(x => x.SkillTags.Contains(filter)).ToList();
        }

        foreach (Class filter in _classTraitFilters)
        {
            possibleTraits = possibleTraits.Where(x => x.ClassTags.Contains(filter)).ToList();
        }

        foreach (Combat filter in _combatTraitFilters)
        {
            possibleTraits = possibleTraits.Where(x => x.CombatTags.Contains(filter)).ToList();
        }

        foreach (Role filter in _roleTraitFilters)
        {
            possibleTraits = possibleTraits.Where(x => x.RoleTags.Contains(filter)).ToList();
        }

        foreach (Magic filter in _magicTraitFilters)
        {
            possibleTraits = possibleTraits.Where(x => x.MagicTags.Contains(filter)).ToList();
        }

        foreach (Bonus filter in _bonusTraitFilters)
        {
            possibleTraits = possibleTraits.Where(x => x.BonusTags.Contains(filter)).ToList();
        }

        foreach (Condition filter in _conditionTraitFilters)
        {
            possibleTraits = possibleTraits.Where(x => x.ConditionTags.Contains(filter)).ToList();
        }

        foreach (string filter in _customTraitFilters)
        {
            possibleTraits = possibleTraits.Where(x => x.CustomTags.Contains(filter)).ToList();
        }

        foreach (Trait trait in possibleTraits)
        {
            yield return trait;
        }
    }

    internal void UpdateTraitCustomTags()
    {
        CustomTraitFilterList.Clear();
        foreach (Trait trait in MasterTraitList)
        {
            if (FavoriteTraitList.Contains(trait) || HiddenTraitList.Contains(trait))
            {
                continue;
            }

            foreach (string tag in trait.CustomTags)
            {
                if (!CustomTraitFilterList.Contains(tag))
                {
                    CustomTraitFilterList.Add(tag);
                }
            }
        }
    }

    internal void AddFavoriteTraitAction(object arg)
    {
        if (SelectedTrait != null && !FavoriteTraitList.Contains(SelectedTrait))
        {
            FavoriteTraitList.Add(SelectedTrait);
            HiddenTraitList.Remove(SelectedTrait);

            UpdateTraitCustomTags();
            ApplyTraitFilters();
            SelectedTrait = null;
        }
    }

    internal void AddHiddenTraitAction(object arg)
    {
        if (SelectedTrait != null && !HiddenTraitList.Contains(SelectedTrait))
        {
            HiddenTraitList.Add(SelectedTrait);
            FavoriteTraitList.Remove(SelectedTrait);

            UpdateTraitCustomTags();
            ApplyTraitFilters();
            SelectedTrait = null;
        }
    }

    internal void EditTraitAction(object arg)
    {
        try
        {
            if (SelectedTrait != null)
            {
                TraitViewModel vm = new(SelectedTrait);
                TraitView configWindow = new(vm);

                if (configWindow.ShowDialog() == true)
                {
                    Trait newTrait = vm.GetTrait();

                    if (MasterTraitList.Contains(SelectedTrait))
                    {
                        MasterTraitList.Remove(SelectedTrait);
                        MasterTraitList.Add(newTrait);

                        if (FavoriteTraitList.Contains(SelectedTrait))
                        {
                            FavoriteTraitList.Remove(SelectedTrait);
                            FavoriteTraitList.Add(newTrait);
                        }

                        else if (HiddenTraitList.Contains(SelectedTrait))
                        {
                            HiddenTraitList.Remove(SelectedTrait);
                            HiddenTraitList.Add(newTrait);
                        }

                        UpdateTraitCustomTags();
                        ApplyTraitFilters();

                        SelectedTrait = newTrait;
                    }
                }
            }
            else
            {
                string messageBoxText = "No trait selected to edit";
                string caption = "Select Trait";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Exclamation;
                MessageBox.Show(messageBoxText, caption, button, icon);
            }
        }
        catch (ArgumentOutOfRangeException)
        {
            string messageBoxText = "Exception when adding trait";
            string caption = "Exception";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Error;
            MessageBox.Show(messageBoxText, caption, button, icon);
        }

        RefreshButtonState();
    }

    internal void NewTraitAction(object arg)
    {
        try
        {
            TraitViewModel vm = new(new Trait());
            TraitView configWindow = new(vm);

            if (configWindow.ShowDialog() == true)
            {
                Trait newTrait = vm.GetTrait();
                if (MasterTraitList.Select(x => x.Name).Contains(newTrait.Name))
                {
                    string messageBoxText = "Trait with same name already exists";
                    string caption = "Duplicate";
                    MessageBoxButton button = MessageBoxButton.OK;
                    MessageBoxImage icon = MessageBoxImage.Exclamation;
                    MessageBox.Show(messageBoxText, caption, button, icon);
                }
                else
                {
                    MasterTraitList.Add(newTrait);

                    UpdateTraitCustomTags();
                    ApplyTraitFilters();
                    if (FilteredTraitList.Contains(newTrait))
                    {
                        SelectedTrait = newTrait;
                    }
                }
            }
        }
        catch (ArgumentOutOfRangeException)
        {
            string messageBoxText = "Exception when adding trait";
            string caption = "Exception";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Error;
            MessageBox.Show(messageBoxText, caption, button, icon);
        }

        RefreshButtonState();
    }

    internal void TraitSearchRemovePlaceholderTextAction(object arg)
    {
        if (TraitSearchText == RegexHandler.SearchPlaceholderText)
        {
            TraitSearchText = "";
        }
    }

    internal void TraitSearchAddPlaceholderTextAction(object arg)
    {
        if (string.IsNullOrWhiteSpace(TraitSearchText))
        {
            TraitSearchText = RegexHandler.SearchPlaceholderText;
        }
    }

    internal void RemoveTraitAction(object arg)
    {
        if (SelectedTrait != null)
        {
            string messageBoxText = "Trait will be removed. Are you sure?";
            string caption = "Warning";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            if (result == MessageBoxResult.Yes)
            {
                MasterTraitList.Remove(SelectedTrait);
                HiddenTraitList.Remove(SelectedTrait);
                FavoriteTraitList.Remove(SelectedTrait);
                SelectedTrait = null;
                UpdateTraitCustomTags();
                ApplyTraitFilters();
            }
        }
    }

    internal void RemoveFavoriteTraitAction(object arg)
    {
        if (SelectedTrait != null && FavoriteTraitList.Contains(SelectedTrait))
        {
            FavoriteTraitList.Remove(SelectedTrait);

            SelectedTrait = null;
            UpdateTraitCustomTags();
            ApplyTraitFilters();
        }
    }

    internal void RemoveHiddenTraitAction(object arg)
    {
        if (SelectedTrait != null && HiddenTraitList.Contains(SelectedTrait))
        {
            HiddenTraitList.Remove(SelectedTrait);

            SelectedTrait = null;
            UpdateTraitCustomTags();
            ApplyTraitFilters();
        }
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}