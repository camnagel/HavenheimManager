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

public class FeatHandler : INotifyPropertyChanged
{
    // Feat Filter Lists
    private readonly HashSet<Bonus> _bonusFeatFilters = new();
    private readonly HashSet<Class> _classFeatFilters = new();
    private readonly HashSet<Combat> _combatFeatFilters = new();
    private readonly HashSet<Condition> _conditionFeatFilters = new();
    private readonly HashSet<Core> _coreFeatFilters = new();
    private readonly HashSet<string> _customFeatFilters = new();
    private readonly HashSet<Magic> _magicFeatFilters = new();
    private readonly HashSet<Role> _roleFeatFilters = new();
    private readonly HashSet<Skill> _skillFeatFilters = new();
    private readonly HashSet<Source> _sourceFeatFilters = new();

    private string _featMaxLevel = RegexHandler.FeatMaxLevelPlaceholder.ToString();

    private string _featMinLevel = RegexHandler.FeatMinLevelPlaceholder.ToString();

    private string _featSearchText = RegexHandler.SearchPlaceholderText;

    private Feat? _selectedFeat;

    private string _selectedFeatReq;

    public FeatHandler()
    {
        CoreFeatCheckboxCommand = new DelegateCommand(FeatCoreFilterAction);
        SkillFeatCheckboxCommand = new DelegateCommand(FeatSkillFilterAction);
        ClassFeatCheckboxCommand = new DelegateCommand(FeatClassFilterAction);
        CombatFeatCheckboxCommand = new DelegateCommand(FeatCombatFilterAction);
        RoleFeatCheckboxCommand = new DelegateCommand(FeatRoleFilterAction);
        MagicFeatCheckboxCommand = new DelegateCommand(FeatMagicFilterAction);
        BonusFeatCheckboxCommand = new DelegateCommand(FeatBonusFilterAction);
        ConditionFeatCheckboxCommand = new DelegateCommand(FeatConditionFilterAction);
        SourceFeatCheckboxCommand = new DelegateCommand(FeatSourceFilterAction);
        CustomFeatCheckboxCommand = new DelegateCommand(FeatCustomFilterAction);
        AddFavoriteFeatCommand = new DelegateCommand(AddFavoriteFeatAction);
        AddHiddenFeatCommand = new DelegateCommand(AddHiddenFeatAction);
        EditFeatCommand = new DelegateCommand(EditFeatAction);
        NewFeatCommand = new DelegateCommand(NewFeatAction);
        RemoveFeatCommand = new DelegateCommand(RemoveFeatAction);
        RemoveFavoriteFeatCommand = new DelegateCommand(RemoveFavoriteFeatAction);
        RemoveHiddenFeatCommand = new DelegateCommand(RemoveHiddenFeatAction);
        FeatSearchRemovePlaceholderTextCommand = new DelegateCommand(FeatSearchRemovePlaceholderTextAction);
        FeatSearchAddPlaceholderTextCommand = new DelegateCommand(FeatSearchAddPlaceholderTextAction);
        FeatMinLevelRemovePlaceholderTextCommand =
            new DelegateCommand(FeatMinLevelRemovePlaceholderTextAction);
        FeatMinLevelAddPlaceholderTextCommand = new DelegateCommand(FeatMinLevelAddPlaceholderTextAction);
        FeatMaxLevelRemovePlaceholderTextCommand =
            new DelegateCommand(FeatMaxLevelRemovePlaceholderTextAction);
        FeatMaxLevelAddPlaceholderTextCommand = new DelegateCommand(FeatMaxLevelAddPlaceholderTextAction);
    }

    // Filtered Feat Collections
    public ObservableCollection<Feat> FilteredFeatList { get; set; } = new();
    public ObservableCollection<Feat> FavoriteFeatList { get; set; } = new();
    public ObservableCollection<Feat> HiddenFeatList { get; set; } = new();

    // Feat Tag Collections
    public ObservableCollection<string> CustomFeatFilterList { get; set; } = new();
    public ObservableCollection<string> CoreFeatFilterList { get; set; } = new();
    public ObservableCollection<string> SkillFeatFilterList { get; set; } = new();
    public ObservableCollection<string> ClassFeatFilterList { get; set; } = new();
    public ObservableCollection<string> CombatFeatFilterList { get; set; } = new();
    public ObservableCollection<string> RoleFeatFilterList { get; set; } = new();
    public ObservableCollection<string> MagicFeatFilterList { get; set; } = new();
    public ObservableCollection<string> BonusFeatFilterList { get; set; } = new();
    public ObservableCollection<string> ConditionFeatFilterList { get; set; } = new();
    public ObservableCollection<string> SourceFeatFilterList { get; set; } = new();

    public Feat? SelectedFeat
    {
        get => _selectedFeat;
        set
        {
            if (value != null)
            {
                SelectedFeat = null;
            }

            _selectedFeat = value;
            CurrentFeat.Clear();
            if (value != null)
            {
                CurrentFeat.Add(value);
            }

            OnPropertyChanged("SelectedFeat");
        }
    }

    public string? SelectedFeatReq
    {
        get => _selectedFeatReq;
        set
        {
            _selectedFeatReq = value ?? "";
            string sanitizedSelection = _selectedFeatReq.Sanitize();
            foreach (Feat possibleFeat in MasterFeatList)
            {
                if (possibleFeat.Name.Sanitize() == sanitizedSelection)
                {
                    SelectedFeat = possibleFeat;
                    _selectedFeatReq = "";
                }
            }

            OnPropertyChanged("SelectedFeatReq");
        }
    }

    public string FeatSearchText
    {
        get => _featSearchText;
        set
        {
            _featSearchText = value;
            ApplyFeatFilters();

            OnPropertyChanged("FeatSearchText");
        }
    }

    public string FeatMinLevel
    {
        get => _featMinLevel;
        set
        {
            if (value == "")
            {
                _featMinLevel = value;
                OnPropertyChanged("FeatMinLevel");
                return;
            }

            if (!RegexHandler.NumberFilter.IsMatch(value))
            {
                int input = int.Parse(value);
                if (value == _featMinLevel)
                {
                    return;
                }

                _featMinLevel = input > int.Parse(_featMaxLevel) ? _featMaxLevel : value;
                ApplyFeatFilters();
                OnPropertyChanged("FeatMinLevel");
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

    public string FeatMaxLevel
    {
        get => _featMaxLevel;
        set
        {
            if (value == "")
            {
                _featMaxLevel = value;
                OnPropertyChanged("FeatMaxLevel");
                return;
            }

            if (!RegexHandler.NumberFilter.IsMatch(value) && int.Parse(value) <= 20)
            {
                int input = int.Parse(value);
                if (value == _featMaxLevel)
                {
                    return;
                }

                _featMaxLevel = input < int.Parse(_featMinLevel) ? _featMinLevel : value;
                ApplyFeatFilters();
                OnPropertyChanged("FeatMaxLevel");
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

    public List<Feat> MasterFeatList { get; } = new();
    public ObservableCollection<Feat> CurrentFeat { get; set; } = new();

    // Feat Checkbox Commands
    public DelegateCommand CoreFeatCheckboxCommand { get; }
    public DelegateCommand SkillFeatCheckboxCommand { get; }
    public DelegateCommand ClassFeatCheckboxCommand { get; }
    public DelegateCommand CombatFeatCheckboxCommand { get; }
    public DelegateCommand RoleFeatCheckboxCommand { get; }
    public DelegateCommand MagicFeatCheckboxCommand { get; }
    public DelegateCommand BonusFeatCheckboxCommand { get; }
    public DelegateCommand ConditionFeatCheckboxCommand { get; }
    public DelegateCommand SourceFeatCheckboxCommand { get; }
    public DelegateCommand CustomFeatCheckboxCommand { get; }
    public DelegateCommand FeatMinLevelRemovePlaceholderTextCommand { get; }
    public DelegateCommand FeatMinLevelAddPlaceholderTextCommand { get; }
    public DelegateCommand FeatMaxLevelRemovePlaceholderTextCommand { get; }
    public DelegateCommand FeatMaxLevelAddPlaceholderTextCommand { get; }

    // Feat Control Bar Commands
    public DelegateCommand FeatSearchRemovePlaceholderTextCommand { get; }
    public DelegateCommand FeatSearchAddPlaceholderTextCommand { get; }
    public DelegateCommand AddFavoriteFeatCommand { get; }
    public DelegateCommand AddHiddenFeatCommand { get; }
    public DelegateCommand EditFeatCommand { get; }
    public DelegateCommand NewFeatCommand { get; }
    public DelegateCommand RemoveFeatCommand { get; }
    public DelegateCommand RemoveFavoriteFeatCommand { get; }
    public DelegateCommand RemoveHiddenFeatCommand { get; }

    private bool ActiveFeatFilters => _coreFeatFilters.Any() ||
                                      _skillFeatFilters.Any() ||
                                      _classFeatFilters.Any() ||
                                      _combatFeatFilters.Any() ||
                                      _roleFeatFilters.Any() ||
                                      _conditionFeatFilters.Any() ||
                                      _sourceFeatFilters.Any() ||
                                      _customFeatFilters.Any() ||
                                      _magicFeatFilters.Any() ||
                                      _bonusFeatFilters.Any();

    public event PropertyChangedEventHandler? PropertyChanged;

    internal void Clear()
    {
        CoreFeatFilterList.Clear();
        SourceFeatFilterList.Clear();
        SkillFeatFilterList.Clear();
        CombatFeatFilterList.Clear();
        RoleFeatFilterList.Clear();
        MagicFeatFilterList.Clear();
        BonusFeatFilterList.Clear();
        ConditionFeatFilterList.Clear();
        ClassFeatFilterList.Clear();
    }

    internal void InitializePathfinder()
    {
    }

    internal void InitializeHavenheim()
    {
        CoreFeatFilterList.Fill<Core>(typeof(Core));
        SourceFeatFilterList.Fill<Source>(typeof(Source));
        SkillFeatFilterList.Fill<Skill>(typeof(Skill));
        CombatFeatFilterList.Fill<Combat>(typeof(Combat));
        RoleFeatFilterList.Fill<Role>(typeof(Role));
        MagicFeatFilterList.Fill<Magic>(typeof(Magic));
        BonusFeatFilterList.Fill<Bonus>(typeof(Bonus));
        ConditionFeatFilterList.Fill<Condition>(typeof(Condition));
        ClassFeatFilterList.Fill<Class>(typeof(Class));
    }

    // Feat Checkbox Actions
    internal void FeatCoreFilterAction(object arg)
    {
        if (arg is string filter)
        {
            Core toggleCore = filter.StringToEnum<Core>();

            if (_coreFeatFilters.Contains(toggleCore))
            {
                _coreFeatFilters.Remove(toggleCore);
            }
            else
            {
                _coreFeatFilters.Add(toggleCore);
            }

            ApplyFeatFilters();
        }
    }

    internal void FeatSkillFilterAction(object arg)
    {
        if (arg is string filter)
        {
            Skill toggleSkill = filter.StringToEnum<Skill>();

            if (_skillFeatFilters.Contains(toggleSkill))
            {
                _skillFeatFilters.Remove(toggleSkill);
            }
            else
            {
                _skillFeatFilters.Add(toggleSkill);
            }

            ApplyFeatFilters();
        }
    }

    internal void FeatClassFilterAction(object arg)
    {
        if (arg is string filter)
        {
            Class toggleClass = filter.StringToEnum<Class>();

            if (_classFeatFilters.Contains(toggleClass))
            {
                _classFeatFilters.Remove(toggleClass);
            }
            else
            {
                _classFeatFilters.Add(toggleClass);
            }

            ApplyFeatFilters();
        }
    }

    internal void FeatCombatFilterAction(object arg)
    {
        if (arg is string filter)
        {
            Combat toggleCombat = filter.StringToEnum<Combat>();

            if (_combatFeatFilters.Contains(toggleCombat))
            {
                _combatFeatFilters.Remove(toggleCombat);
            }
            else
            {
                _combatFeatFilters.Add(toggleCombat);
            }

            ApplyFeatFilters();
        }
    }

    internal void FeatRoleFilterAction(object arg)
    {
        if (arg is string filter)
        {
            Role toggleRole = filter.StringToEnum<Role>();

            if (_roleFeatFilters.Contains(toggleRole))
            {
                _roleFeatFilters.Remove(toggleRole);
            }
            else
            {
                _roleFeatFilters.Add(toggleRole);
            }

            ApplyFeatFilters();
        }
    }

    internal void FeatMagicFilterAction(object arg)
    {
        if (arg is string filter)
        {
            Magic toggleMagic = filter.StringToEnum<Magic>();

            if (_magicFeatFilters.Contains(toggleMagic))
            {
                _magicFeatFilters.Remove(toggleMagic);
            }
            else
            {
                _magicFeatFilters.Add(toggleMagic);
            }

            ApplyFeatFilters();
        }
    }

    internal void FeatBonusFilterAction(object arg)
    {
        if (arg is string filter)
        {
            Bonus toggleBonus = filter.StringToEnum<Bonus>();

            if (_bonusFeatFilters.Contains(toggleBonus))
            {
                _bonusFeatFilters.Remove(toggleBonus);
            }
            else
            {
                _bonusFeatFilters.Add(toggleBonus);
            }

            ApplyFeatFilters();
        }
    }

    internal void FeatConditionFilterAction(object arg)
    {
        if (arg is string filter)
        {
            Condition toggleCondition = filter.StringToEnum<Condition>();

            if (_conditionFeatFilters.Contains(toggleCondition))
            {
                _conditionFeatFilters.Remove(toggleCondition);
            }
            else
            {
                _conditionFeatFilters.Add(toggleCondition);
            }

            ApplyFeatFilters();
        }
    }

    internal void FeatSourceFilterAction(object arg)
    {
        if (arg is string filter)
        {
            Source toggleSource = filter.StringToEnum<Source>();

            if (_sourceFeatFilters.Contains(toggleSource))
            {
                _sourceFeatFilters.Remove(toggleSource);
            }
            else
            {
                _sourceFeatFilters.Add(toggleSource);
            }

            ApplyFeatFilters();
        }
    }

    internal void FeatCustomFilterAction(object arg)
    {
        if (arg is string filter)
        {
            if (_customFeatFilters.Contains(filter))
            {
                _customFeatFilters.Remove(filter);
            }
            else
            {
                _customFeatFilters.Add(filter);
            }

            ApplyFeatFilters();
        }
    }

    internal void ApplyFeatFilters()
    {
        FilteredFeatList.Clear();
        List<Feat> possibleFeats =
            (FeatSearchText != RegexHandler.SearchPlaceholderText && FeatSearchText != ""
                ? MasterFeatList.Where(x => x.Name.Sanitize().Contains(FeatSearchText.Sanitize())).ToList()
                : MasterFeatList).Where(x => !FavoriteFeatList.Contains(x) && !HiddenFeatList.Contains(x) &&
                                             x.Level <= int.Parse(FeatMaxLevel) &&
                                             x.Level >= int.Parse(FeatMinLevel)).ToList();

        foreach (Source filter in _sourceFeatFilters)
        {
            possibleFeats = possibleFeats.Where(x => x.Source == filter).ToList();
        }

        foreach (Feat feat in BroadFeatFilter(possibleFeats))
        {
            FilteredFeatList.Add(feat);
        }
    }

    private IEnumerable<Feat> BroadFeatFilter(List<Feat> possibleFeats)
    {
        if (!ActiveFeatFilters)
        {
            foreach (Feat feat in possibleFeats)
            {
                yield return feat;
            }

            yield break;
        }

        foreach (Feat feat in possibleFeats)
        {
            if (_coreFeatFilters.Any(filter => feat.CoreTags.Contains(filter)))
            {
                yield return feat;
                continue;
            }

            if (_skillFeatFilters.Any(filter => feat.SkillTags.Contains(filter)))
            {
                yield return feat;
                continue;
            }

            if (_classFeatFilters.Any(filter => feat.ClassTags.Contains(filter)))
            {
                yield return feat;
                continue;
            }

            if (_combatFeatFilters.Any(filter => feat.CombatTags.Contains(filter)))
            {
                yield return feat;
                continue;
            }

            if (_roleFeatFilters.Any(filter => feat.RoleTags.Contains(filter)))
            {
                yield return feat;
                continue;
            }

            if (_magicFeatFilters.Any(filter => feat.MagicTags.Contains(filter)))
            {
                yield return feat;
                continue;
            }

            if (_bonusFeatFilters.Any(filter => feat.BonusTags.Contains(filter)))
            {
                yield return feat;
                continue;
            }

            if (_conditionFeatFilters.Any(filter => feat.ConditionTags.Contains(filter)))
            {
                yield return feat;
                continue;
            }

            if (_customFeatFilters.Any(filter => feat.CustomTags.Contains(filter)))
            {
                yield return feat;
            }
        }
    }

    private IEnumerable<Feat> StrictFeatFilter(List<Feat> possibleFeats)
    {
        foreach (Core filter in _coreFeatFilters)
        {
            possibleFeats = possibleFeats.Where(x => x.CoreTags.Contains(filter)).ToList();
        }

        foreach (Skill filter in _skillFeatFilters)
        {
            possibleFeats = possibleFeats.Where(x => x.SkillTags.Contains(filter)).ToList();
        }

        foreach (Class filter in _classFeatFilters)
        {
            possibleFeats = possibleFeats.Where(x => x.ClassTags.Contains(filter)).ToList();
        }

        foreach (Combat filter in _combatFeatFilters)
        {
            possibleFeats = possibleFeats.Where(x => x.CombatTags.Contains(filter)).ToList();
        }

        foreach (Role filter in _roleFeatFilters)
        {
            possibleFeats = possibleFeats.Where(x => x.RoleTags.Contains(filter)).ToList();
        }

        foreach (Magic filter in _magicFeatFilters)
        {
            possibleFeats = possibleFeats.Where(x => x.MagicTags.Contains(filter)).ToList();
        }

        foreach (Bonus filter in _bonusFeatFilters)
        {
            possibleFeats = possibleFeats.Where(x => x.BonusTags.Contains(filter)).ToList();
        }

        foreach (Condition filter in _conditionFeatFilters)
        {
            possibleFeats = possibleFeats.Where(x => x.ConditionTags.Contains(filter)).ToList();
        }

        foreach (string filter in _customFeatFilters)
        {
            possibleFeats = possibleFeats.Where(x => x.CustomTags.Contains(filter)).ToList();
        }

        foreach (Feat feat in possibleFeats)
        {
            yield return feat;
        }
    }

    internal void UpdateFeatCustomTags()
    {
        CustomFeatFilterList.Clear();
        foreach (Feat feat in MasterFeatList)
        {
            if (FavoriteFeatList.Contains(feat) || HiddenFeatList.Contains(feat))
            {
                continue;
            }

            foreach (string tag in feat.CustomTags)
            {
                if (!CustomFeatFilterList.Contains(tag))
                {
                    CustomFeatFilterList.Add(tag);
                }
            }
        }
    }

    internal void UpdateFeatReqs()
    {
        foreach (Feat feat in MasterFeatList)
        {
            string sanitizedName = feat.Name.Sanitize();
            foreach (Feat possibleReq in MasterFeatList)
            {
                string possibleFeatSanitizedName = possibleReq.Name.Sanitize();
                if (sanitizedName == possibleFeatSanitizedName)
                {
                    continue;
                }

                // Antireqs
                if (possibleReq.Antireqs.Select(x => x.Sanitize()).Contains(sanitizedName) &&
                    !feat.Antireqs.Select(x => x.Sanitize()).Contains(possibleFeatSanitizedName))
                {
                    feat.Antireqs.Add(possibleReq.Name);
                }

                // Postreqs
                if (possibleReq.Prereqs.Select(x => x.Sanitize()).Contains(sanitizedName))
                {
                    feat.Postreqs.Add(possibleReq.Name);
                }
            }
        }
    }

    internal void AddFavoriteFeatAction(object arg)
    {
        if (SelectedFeat != null && !FavoriteFeatList.Contains(SelectedFeat))
        {
            FavoriteFeatList.Add(SelectedFeat);
            HiddenFeatList.Remove(SelectedFeat);

            UpdateFeatCustomTags();
            ApplyFeatFilters();
            SelectedFeat = null;
        }
    }

    internal void AddHiddenFeatAction(object arg)
    {
        if (SelectedFeat != null && !HiddenFeatList.Contains(SelectedFeat))
        {
            HiddenFeatList.Add(SelectedFeat);
            FavoriteFeatList.Remove(SelectedFeat);

            UpdateFeatCustomTags();
            ApplyFeatFilters();
            SelectedFeat = null;
        }
    }

    internal void EditFeatAction(object arg)
    {
        try
        {
            if (SelectedFeat != null)
            {
                FeatViewModel vm = new(SelectedFeat);
                FeatView configWindow = new(vm);

                if (configWindow.ShowDialog() == true)
                {
                    Feat newFeat = vm.GetFeat();

                    if (MasterFeatList.Contains(SelectedFeat))
                    {
                        MasterFeatList.Remove(SelectedFeat);
                        MasterFeatList.Add(newFeat);

                        if (FavoriteFeatList.Contains(SelectedFeat))
                        {
                            FavoriteFeatList.Remove(SelectedFeat);
                            FavoriteFeatList.Add(newFeat);
                        }

                        if (HiddenFeatList.Contains(SelectedFeat))
                        {
                            HiddenFeatList.Remove(SelectedFeat);
                            HiddenFeatList.Add(newFeat);
                        }

                        UpdateFeatReqs();
                        UpdateFeatCustomTags();
                        ApplyFeatFilters();

                        SelectedFeat = newFeat;
                    }
                }
            }
            else
            {
                string messageBoxText = "No Feat selected to edit";
                string caption = "Select Feat";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Exclamation;
                MessageBox.Show(messageBoxText, caption, button, icon);
            }
        }
        catch (ArgumentOutOfRangeException)
        {
            string messageBoxText = "Exception when adding Feat";
            string caption = "Exception";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Error;
            MessageBox.Show(messageBoxText, caption, button, icon);
        }

        RefreshButtonState();
    }

    public void RefreshButtonState()
    {
        CoreFeatCheckboxCommand.RaiseCanExecuteChanged();
    }

    internal void NewFeatAction(object arg)
    {
        try
        {
            FeatViewModel vm = new(new Feat());
            FeatView configWindow = new(vm);

            if (configWindow.ShowDialog() == true)
            {
                Feat newFeat = vm.GetFeat();
                if (MasterFeatList.Select(x => x.Name).Contains(newFeat.Name))
                {
                    string messageBoxText = "Feat with same name already exists";
                    string caption = "Duplicate";
                    MessageBoxButton button = MessageBoxButton.OK;
                    MessageBoxImage icon = MessageBoxImage.Exclamation;
                    MessageBox.Show(messageBoxText, caption, button, icon);
                }
                else
                {
                    MasterFeatList.Add(newFeat);

                    UpdateFeatReqs();
                    UpdateFeatCustomTags();
                    ApplyFeatFilters();
                    if (FilteredFeatList.Contains(newFeat))
                    {
                        SelectedFeat = newFeat;
                    }
                }
            }
        }
        catch (ArgumentOutOfRangeException)
        {
            string messageBoxText = "Exception when adding Feat";
            string caption = "Exception";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Error;
            MessageBox.Show(messageBoxText, caption, button, icon);
        }

        RefreshButtonState();
    }

    internal void FeatSearchRemovePlaceholderTextAction(object arg)
    {
        if (FeatSearchText == RegexHandler.SearchPlaceholderText)
        {
            FeatSearchText = "";
        }
    }

    internal void FeatSearchAddPlaceholderTextAction(object arg)
    {
        if (string.IsNullOrWhiteSpace(FeatSearchText))
        {
            FeatSearchText = RegexHandler.SearchPlaceholderText;
        }
    }

    internal void FeatMinLevelRemovePlaceholderTextAction(object arg)
    {
        if (FeatMinLevel == RegexHandler.FeatMinLevelPlaceholder.ToString())
        {
            FeatMinLevel = "";
        }
    }

    internal void FeatMinLevelAddPlaceholderTextAction(object arg)
    {
        if (string.IsNullOrWhiteSpace(FeatMinLevel))
        {
            FeatMinLevel = RegexHandler.FeatMinLevelPlaceholder.ToString();
        }
    }

    internal void FeatMaxLevelRemovePlaceholderTextAction(object arg)
    {
        if (FeatMaxLevel == RegexHandler.FeatMaxLevelPlaceholder.ToString())
        {
            FeatMaxLevel = "";
        }
    }

    internal void FeatMaxLevelAddPlaceholderTextAction(object arg)
    {
        if (string.IsNullOrWhiteSpace(FeatMaxLevel))
        {
            FeatMaxLevel = RegexHandler.FeatMaxLevelPlaceholder.ToString();
        }
    }

    internal void RemoveFeatAction(object arg)
    {
        if (SelectedFeat != null)
        {
            string messageBoxText = "Feat will be removed. Are you sure?";
            string caption = "Warning";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            if (result == MessageBoxResult.Yes)
            {
                MasterFeatList.Remove(SelectedFeat);
                HiddenFeatList.Remove(SelectedFeat);
                FavoriteFeatList.Remove(SelectedFeat);
                SelectedFeat = null;
                UpdateFeatCustomTags();
                ApplyFeatFilters();
            }
        }
    }

    internal void RemoveFavoriteFeatAction(object arg)
    {
        if (SelectedFeat != null && FavoriteFeatList.Contains(SelectedFeat))
        {
            FavoriteFeatList.Remove(SelectedFeat);

            SelectedFeat = null;
            UpdateFeatCustomTags();
            ApplyFeatFilters();
        }
    }

    internal void RemoveHiddenFeatAction(object arg)
    {
        if (SelectedFeat != null && HiddenFeatList.Contains(SelectedFeat))
        {
            HiddenFeatList.Remove(SelectedFeat);

            SelectedFeat = null;
            UpdateFeatCustomTags();
            ApplyFeatFilters();
        }
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}