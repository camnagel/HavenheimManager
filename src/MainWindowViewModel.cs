using AssetManager.Containers;
using AssetManager.Enums;
using AssetManager.Import;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows;
using AssetManager.Editors;
using Condition = AssetManager.Enums.Condition;
using File = System.IO.File;

namespace AssetManager
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        // Primary Object Collections
        public List<Trait> MasterTraitList { get; } = new();
        public List<Feat> MasterFeatList { get; } = new();
        public List<Item> MasterItemList { get; } = new();

        // Selected Object Backing Collections
        public ObservableCollection<Trait> CurrentTrait { get; set; } = new();
        public ObservableCollection<Feat> CurrentFeat { get; set; } = new();
        public ObservableCollection<Spell> CurrentSpell { get; set; } = new();
        public ObservableCollection<Item> CurrentItem { get; set; } = new();

        private string _saveFileName = "";

        private static readonly string _searchPlaceholderText = "Search...";

        // Asset Commands
        public DelegateCommand LoadCommand { get; }
        public DelegateCommand SaveCommand { get; }
        public DelegateCommand SaveAsCommand { get; }
        public DelegateCommand ImportCommand { get; }

        public MainWindowViewModel()
        {
            LoadCommand = new DelegateCommand(LoadAction);
            SaveCommand = new DelegateCommand(SaveAction);
            SaveAsCommand = new DelegateCommand(SaveAsAction);
            ImportCommand = new DelegateCommand(ImportAction);
            
            // Traits
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
            TraitSearchRemovePlaceholderTextCommand = new DelegateCommand(TraitSearchRemovePlaceholderTextAction);
            TraitSearchAddPlaceholderTextCommand = new DelegateCommand(TraitSearchAddPlaceholderTextAction);
            
            // Feats
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
        }

        #region Traits
        // Filtered Trait Collections
        public ObservableCollection<Trait> FilteredTraitList { get; set; } = new();
        public ObservableCollection<Trait> FavoriteTraitList { get; set; } = new();
        public ObservableCollection<Trait> HiddenTraitList { get; set; } = new();

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
        
        private Trait? _selectedTrait;
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

        private string _traitSearchText = _searchPlaceholderText;
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

        // Trait Filter Lists
        private HashSet<Core> CoreTraitFilters = new HashSet<Core>();
        private HashSet<Skill> SkillTraitFilters = new HashSet<Skill>();
        private HashSet<Class> ClassTraitFilters = new HashSet<Class>();
        private HashSet<Combat> CombatTraitFilters = new HashSet<Combat>();
        private HashSet<Role> RoleTraitFilters = new HashSet<Role>();
        private HashSet<Magic> MagicTraitFilters = new HashSet<Magic>();
        private HashSet<Bonus> BonusTraitFilters = new HashSet<Bonus>();
        private HashSet<Condition> ConditionTraitFilters = new HashSet<Condition>();
        private HashSet<Source> SourceTraitFilters = new HashSet<Source>();
        private HashSet<string> CustomTraitFilters = new HashSet<string>();

        private bool _activeTraitFilters => CoreTraitFilters.Any() ||
                                            SkillTraitFilters.Any() ||
                                            ClassTraitFilters.Any() ||
                                            CombatTraitFilters.Any() ||
                                            RoleTraitFilters.Any() ||
                                            ConditionTraitFilters.Any() ||
                                            SourceTraitFilters.Any() ||
                                            CustomTraitFilters.Any() ||
                                            MagicTraitFilters.Any() ||
                                            BonusTraitFilters.Any();

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

        // Trait Checkbox Actions
        private void TraitCoreFilterAction(object arg)
        {
            if (arg is string filter)
            {
                Core toggleCore = filter.StringToCore();

                if (CoreTraitFilters.Contains(toggleCore))
                {
                    CoreTraitFilters.Remove(toggleCore);
                }
                else
                {
                    CoreTraitFilters.Add(toggleCore);
                }

                ApplyTraitFilters();
            }
        }

        private void TraitSkillFilterAction(object arg)
        {
            if (arg is string filter)
            {
                Skill toggleSkill = filter.StringToSkill();

                if (SkillTraitFilters.Contains(toggleSkill))
                {
                    SkillTraitFilters.Remove(toggleSkill);
                }
                else
                {
                    SkillTraitFilters.Add(toggleSkill);
                }

                ApplyTraitFilters();
            }
        }

        private void TraitClassFilterAction(object arg)
        {
            if (arg is string filter)
            {
                Class toggleClass = filter.StringToClass();

                if (ClassTraitFilters.Contains(toggleClass))
                {
                    ClassTraitFilters.Remove(toggleClass);
                }
                else
                {
                    ClassTraitFilters.Add(toggleClass);
                }

                ApplyTraitFilters();
            }
        }

        private void TraitCombatFilterAction(object arg)
        {
            if (arg is string filter)
            {
                Combat toggleCombat = filter.StringToCombat();

                if (CombatTraitFilters.Contains(toggleCombat))
                {
                    CombatTraitFilters.Remove(toggleCombat);
                }
                else
                {
                    CombatTraitFilters.Add(toggleCombat);
                }

                ApplyTraitFilters();
            }
        }

        private void TraitRoleFilterAction(object arg)
        {
            if (arg is string filter)
            {
                Role toggleRole = filter.StringToRole();

                if (RoleTraitFilters.Contains(toggleRole))
                {
                    RoleTraitFilters.Remove(toggleRole);
                }
                else
                {
                    RoleTraitFilters.Add(toggleRole);
                }

                ApplyTraitFilters();
            }
        }

        private void TraitMagicFilterAction(object arg)
        {
            if (arg is string filter)
            {
                Magic toggleMagic = filter.StringToMagic();

                if (MagicTraitFilters.Contains(toggleMagic))
                {
                    MagicTraitFilters.Remove(toggleMagic);
                }
                else
                {
                    MagicTraitFilters.Add(toggleMagic);
                }

                ApplyTraitFilters();
            }
        }

        private void TraitBonusFilterAction(object arg)
        {
            if (arg is string filter)
            {
                Bonus toggleBonus = filter.StringToBonus();

                if (BonusTraitFilters.Contains(toggleBonus))
                {
                    BonusTraitFilters.Remove(toggleBonus);
                }
                else
                {
                    BonusTraitFilters.Add(toggleBonus);
                }

                ApplyTraitFilters();
            }
        }

        private void TraitConditionFilterAction(object arg)
        {
            if (arg is string filter)
            {
                Condition toggleCondition = filter.StringToCondition();

                if (ConditionTraitFilters.Contains(toggleCondition))
                {
                    ConditionTraitFilters.Remove(toggleCondition);
                }
                else
                {
                    ConditionTraitFilters.Add(toggleCondition);
                }

                ApplyTraitFilters();
            }
        }

        private void TraitSourceFilterAction(object arg)
        {
            if (arg is string filter)
            {
                Source toggleSource = filter.StringToSource();

                if (SourceTraitFilters.Contains(toggleSource))
                {
                    SourceTraitFilters.Remove(toggleSource);
                }
                else
                {
                    SourceTraitFilters.Add(toggleSource);
                }

                ApplyTraitFilters();
            }
        }

        private void TraitCustomFilterAction(object arg)
        {
            if (arg is string filter)
            {
                if (CustomTraitFilters.Contains(filter))
                {
                    CustomTraitFilters.Remove(filter);
                }
                else
                {
                    CustomTraitFilters.Add(filter);
                }

                ApplyTraitFilters();
            }
        }

        private void ApplyTraitFilters()
        {
            FilteredTraitList.Clear();
            List<Trait> possibleTraits = TraitSearchText != _searchPlaceholderText && TraitSearchText != "" ?
                MasterTraitList.Where(x => x.Name.Sanitize()
                                           .Contains(_traitSearchText.Sanitize())).ToList() : MasterTraitList;

            foreach (Source filter in SourceTraitFilters)
            {
                possibleTraits = possibleTraits.Where(x => x.Source == filter).ToList();
            }

            foreach (Trait trait in BroadTraitFilter(possibleTraits))
            {
                FilteredTraitList.Add(trait);
            }
        }

        private IEnumerable<Trait> BroadTraitFilter(List<Trait> possibleTraits)
        {
            if (!_activeTraitFilters)
            {
                foreach (Trait trait in possibleTraits)
                {
                    yield return trait;
                }
                yield break;
            }

            foreach (Trait trait in possibleTraits)
            {
                if (CoreTraitFilters.Any(filter => trait.CoreTags.Contains(filter)))
                {
                    yield return trait;
                    continue;
                }

                if (SkillTraitFilters.Any(filter => trait.SkillTags.Contains(filter)))
                {
                    yield return trait;
                    continue;
                }

                if (ClassTraitFilters.Any(filter => trait.ClassTags.Contains(filter)))
                {
                    yield return trait;
                    continue;
                }

                if (CombatTraitFilters.Any(filter => trait.CombatTags.Contains(filter)))
                {
                    yield return trait;
                    continue;
                }

                if (RoleTraitFilters.Any(filter => trait.RoleTags.Contains(filter)))
                {
                    yield return trait;
                    continue;
                }

                if (MagicTraitFilters.Any(filter => trait.MagicTags.Contains(filter)))
                {
                    yield return trait;
                    continue;
                }

                if (BonusTraitFilters.Any(filter => trait.BonusTags.Contains(filter)))
                {
                    yield return trait;
                    continue;
                }

                if (ConditionTraitFilters.Any(filter => trait.ConditionTags.Contains(filter)))
                {
                    yield return trait;
                    continue;
                }

                if (CustomTraitFilters.Any(filter => trait.CustomTags.Contains(filter)))
                {
                    yield return trait;
                }
            }
        }

        private IEnumerable<Trait> StrictTraitFilter(List<Trait> possibleTraits)
        {
            foreach (Core filter in CoreTraitFilters)
            {
                possibleTraits = possibleTraits.Where(x => x.CoreTags.Contains(filter)).ToList();
            }

            foreach (Skill filter in SkillTraitFilters)
            {
                possibleTraits = possibleTraits.Where(x => x.SkillTags.Contains(filter)).ToList();
            }

            foreach (Class filter in ClassTraitFilters)
            {
                possibleTraits = possibleTraits.Where(x => x.ClassTags.Contains(filter)).ToList();
            }

            foreach (Combat filter in CombatTraitFilters)
            {
                possibleTraits = possibleTraits.Where(x => x.CombatTags.Contains(filter)).ToList();
            }

            foreach (Role filter in RoleTraitFilters)
            {
                possibleTraits = possibleTraits.Where(x => x.RoleTags.Contains(filter)).ToList();
            }

            foreach (Magic filter in MagicTraitFilters)
            {
                possibleTraits = possibleTraits.Where(x => x.MagicTags.Contains(filter)).ToList();
            }

            foreach (Bonus filter in BonusTraitFilters)
            {
                possibleTraits = possibleTraits.Where(x => x.BonusTags.Contains(filter)).ToList();
            }

            foreach (Condition filter in ConditionTraitFilters)
            {
                possibleTraits = possibleTraits.Where(x => x.ConditionTags.Contains(filter)).ToList();
            }

            foreach (string filter in CustomTraitFilters)
            {
                possibleTraits = possibleTraits.Where(x => x.CustomTags.Contains(filter)).ToList();
            }

            foreach (Trait trait in possibleTraits)
            {
                yield return trait;
            }
        }

        private void UpdateTraitCustomTags()
        {
            CustomTraitFilterList.Clear();
            foreach (Trait trait in MasterTraitList)
            {
                foreach (string tag in trait.CustomTags)
                {
                    if (!CustomTraitFilterList.Contains(tag))
                    {
                        CustomTraitFilterList.Add(tag);
                    }
                }
            }
        }

        private void AddFavoriteTraitAction(object arg)
        {
            if (SelectedTrait != null && !FavoriteTraitList.Contains(SelectedTrait))
            {
                FavoriteTraitList.Add(SelectedTrait);
                MasterTraitList.Remove(SelectedTrait);
                HiddenTraitList.Remove(SelectedTrait);

                UpdateTraitCustomTags();
                ApplyTraitFilters();
                SelectedTrait = null;
            }
        }

        private void AddHiddenTraitAction(object arg)
        {
            if (SelectedTrait != null && !HiddenTraitList.Contains(SelectedTrait))
            {
                HiddenTraitList.Add(SelectedTrait);
                MasterTraitList.Remove(SelectedTrait);
                FavoriteTraitList.Remove(SelectedTrait);

                UpdateTraitCustomTags();
                ApplyTraitFilters();
                SelectedTrait = null;
            }
        }

        private void EditTraitAction(object arg)
        {
            try
            {
                if (SelectedTrait != null)
                {
                    var vm = new TraitViewModel(SelectedTrait);
                    var configWindow = new TraitView(vm);

                    if (configWindow.ShowDialog() == true)
                    {
                        Trait newTrait = vm.GetTrait();

                        if (MasterTraitList.Contains(SelectedTrait))
                        {
                            MasterTraitList.Remove(SelectedTrait);
                            MasterTraitList.Add(newTrait);

                            UpdateTraitCustomTags();
                            ApplyTraitFilters();
                            if (FilteredTraitList.Contains(newTrait))
                            {
                                SelectedTrait = newTrait;
                            }
                        }

                        if (FavoriteTraitList.Contains(SelectedTrait))
                        {
                            FavoriteTraitList.Remove(SelectedTrait);
                            FavoriteTraitList.Add(newTrait);
                            SelectedTrait = newTrait;
                        }

                        else if (HiddenTraitList.Contains(SelectedTrait))
                        {
                            HiddenTraitList.Remove(SelectedTrait);
                            HiddenTraitList.Add(newTrait);
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

        private void NewTraitAction(object arg)
        {
            try
            {
                var vm = new TraitViewModel(new Trait());
                var configWindow = new TraitView(vm);

                if (configWindow.ShowDialog() == true)
                {
                    Trait newTrait = vm.GetTrait();
                    if (MasterTraitList.Select(x => x.Name).Contains(newTrait.Name) || 
                        FavoriteTraitList.Select(x => x.Name).Contains(newTrait.Name) ||
                        HiddenTraitList.Select(x => x.Name).Contains(newTrait.Name)) 
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

        private void TraitSearchRemovePlaceholderTextAction(object arg)
        {
            if (TraitSearchText == _searchPlaceholderText)
            {
                TraitSearchText = "";
            }
        }

        private void TraitSearchAddPlaceholderTextAction(object arg)
        {
            if (string.IsNullOrWhiteSpace(TraitSearchText))
            {
                TraitSearchText = _searchPlaceholderText;
            }
        }
        
        private void RemoveTraitAction(object arg)
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

        private void RemoveFavoriteTraitAction(object arg)
        {
            if (SelectedTrait != null && FavoriteTraitList.Contains(SelectedTrait))
            {
                MasterTraitList.Add(SelectedTrait);
                FavoriteTraitList.Remove(SelectedTrait);

                SelectedTrait = null;
                UpdateTraitCustomTags();
                ApplyTraitFilters();
            }
        }

        private void RemoveHiddenTraitAction(object arg)
        {
            if (SelectedTrait != null && HiddenTraitList.Contains(SelectedTrait))
            {
                MasterTraitList.Add(SelectedTrait);
                HiddenTraitList.Remove(SelectedTrait);

                SelectedTrait = null;
                UpdateTraitCustomTags();
                ApplyTraitFilters();
            }
        }
        #endregion

        #region Feats
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

        private Feat? _selectedFeat;
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

        private string _selectedFeatReq;
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

        private string _featSearchText = _searchPlaceholderText;
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

        // Feat Filter Lists
        private HashSet<Core> CoreFeatFilters = new HashSet<Core>();
        private HashSet<Skill> SkillFeatFilters = new HashSet<Skill>();
        private HashSet<Class> ClassFeatFilters = new HashSet<Class>();
        private HashSet<Combat> CombatFeatFilters = new HashSet<Combat>();
        private HashSet<Role> RoleFeatFilters = new HashSet<Role>();
        private HashSet<Magic> MagicFeatFilters = new HashSet<Magic>();
        private HashSet<Bonus> BonusFeatFilters = new HashSet<Bonus>();
        private HashSet<Condition> ConditionFeatFilters = new HashSet<Condition>();
        private HashSet<Source> SourceFeatFilters = new HashSet<Source>();
        private HashSet<string> CustomFeatFilters = new HashSet<string>();

        private bool _activeFeatFilters => CoreFeatFilters.Any() ||
                                           SkillFeatFilters.Any() ||
                                           ClassFeatFilters.Any() ||
                                           CombatFeatFilters.Any() ||
                                           RoleFeatFilters.Any() ||
                                           ConditionFeatFilters.Any() ||
                                           SourceFeatFilters.Any() ||
                                           CustomFeatFilters.Any() ||
                                           MagicFeatFilters.Any() ||
                                           BonusFeatFilters.Any();

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

        // Feat Checkbox Actions
        private void FeatCoreFilterAction(object arg)
        {
            if (arg is string filter)
            {
                Core toggleCore = filter.StringToCore();

                if (CoreFeatFilters.Contains(toggleCore))
                {
                    CoreFeatFilters.Remove(toggleCore);
                }
                else
                {
                    CoreFeatFilters.Add(toggleCore);
                }

                ApplyFeatFilters();
            }
        }

        private void FeatSkillFilterAction(object arg)
        {
            if (arg is string filter)
            {
                Skill toggleSkill = filter.StringToSkill();

                if (SkillFeatFilters.Contains(toggleSkill))
                {
                    SkillFeatFilters.Remove(toggleSkill);
                }
                else
                {
                    SkillFeatFilters.Add(toggleSkill);
                }

                ApplyFeatFilters();
            }
        }

        private void FeatClassFilterAction(object arg)
        {
            if (arg is string filter)
            {
                Class toggleClass = filter.StringToClass();

                if (ClassFeatFilters.Contains(toggleClass))
                {
                    ClassFeatFilters.Remove(toggleClass);
                }
                else
                {
                    ClassFeatFilters.Add(toggleClass);
                }

                ApplyFeatFilters();
            }
        }

        private void FeatCombatFilterAction(object arg)
        {
            if (arg is string filter)
            {
                Combat toggleCombat = filter.StringToCombat();

                if (CombatFeatFilters.Contains(toggleCombat))
                {
                    CombatFeatFilters.Remove(toggleCombat);
                }
                else
                {
                    CombatFeatFilters.Add(toggleCombat);
                }

                ApplyFeatFilters();
            }
        }

        private void FeatRoleFilterAction(object arg)
        {
            if (arg is string filter)
            {
                Role toggleRole = filter.StringToRole();

                if (RoleFeatFilters.Contains(toggleRole))
                {
                    RoleFeatFilters.Remove(toggleRole);
                }
                else
                {
                    RoleFeatFilters.Add(toggleRole);
                }

                ApplyFeatFilters();
            }
        }

        private void FeatMagicFilterAction(object arg)
        {
            if (arg is string filter)
            {
                Magic toggleMagic = filter.StringToMagic();

                if (MagicFeatFilters.Contains(toggleMagic))
                {
                    MagicFeatFilters.Remove(toggleMagic);
                }
                else
                {
                    MagicFeatFilters.Add(toggleMagic);
                }

                ApplyFeatFilters();
            }
        }

        private void FeatBonusFilterAction(object arg)
        {
            if (arg is string filter)
            {
                Bonus toggleBonus = filter.StringToBonus();

                if (BonusFeatFilters.Contains(toggleBonus))
                {
                    BonusFeatFilters.Remove(toggleBonus);
                }
                else
                {
                    BonusFeatFilters.Add(toggleBonus);
                }

                ApplyFeatFilters();
            }
        }

        private void FeatConditionFilterAction(object arg)
        {
            if (arg is string filter)
            {
                Condition toggleCondition = filter.StringToCondition();

                if (ConditionFeatFilters.Contains(toggleCondition))
                {
                    ConditionFeatFilters.Remove(toggleCondition);
                }
                else
                {
                    ConditionFeatFilters.Add(toggleCondition);
                }

                ApplyFeatFilters();
            }
        }

        private void FeatSourceFilterAction(object arg)
        {
            if (arg is string filter)
            {
                Source toggleSource = filter.StringToSource();

                if (SourceFeatFilters.Contains(toggleSource))
                {
                    SourceFeatFilters.Remove(toggleSource);
                }
                else
                {
                    SourceFeatFilters.Add(toggleSource);
                }

                ApplyFeatFilters();
            }
        }

        private void FeatCustomFilterAction(object arg)
        {
            if (arg is string filter)
            {
                if (CustomFeatFilters.Contains(filter))
                {
                    CustomFeatFilters.Remove(filter);
                }
                else
                {
                    CustomFeatFilters.Add(filter);
                }

                ApplyFeatFilters();
            }
        }

        private void ApplyFeatFilters()
        {
            FilteredFeatList.Clear();
            List<Feat> possibleFeats = FeatSearchText != _searchPlaceholderText && FeatSearchText != "" ?
                MasterFeatList.Where(x => x.Name.Sanitize()
                                           .Contains(_featSearchText.Sanitize())).ToList() : MasterFeatList;

            foreach (Source filter in SourceFeatFilters)
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
            if (!_activeFeatFilters)
            {
                foreach (Feat feat in possibleFeats)
                {
                    yield return feat;
                }
                yield break;
            }

            foreach (Feat feat in possibleFeats)
            {
                if (CoreFeatFilters.Any(filter => feat.CoreTags.Contains(filter)))
                {
                    yield return feat;
                    continue;
                }

                if (SkillFeatFilters.Any(filter => feat.SkillTags.Contains(filter)))
                {
                    yield return feat;
                    continue;
                }

                if (ClassFeatFilters.Any(filter => feat.ClassTags.Contains(filter)))
                {
                    yield return feat;
                    continue;
                }

                if (CombatFeatFilters.Any(filter => feat.CombatTags.Contains(filter)))
                {
                    yield return feat;
                    continue;
                }

                if (RoleFeatFilters.Any(filter => feat.RoleTags.Contains(filter)))
                {
                    yield return feat;
                    continue;
                }

                if (MagicFeatFilters.Any(filter => feat.MagicTags.Contains(filter)))
                {
                    yield return feat;
                    continue;
                }

                if (BonusFeatFilters.Any(filter => feat.BonusTags.Contains(filter)))
                {
                    yield return feat;
                    continue;
                }

                if (ConditionFeatFilters.Any(filter => feat.ConditionTags.Contains(filter)))
                {
                    yield return feat;
                    continue;
                }

                if (CustomFeatFilters.Any(filter => feat.CustomTags.Contains(filter)))
                {
                    yield return feat;
                }
            }
        }

        private IEnumerable<Feat> StrictFeatFilter(List<Feat> possibleFeats)
        {
            foreach (Core filter in CoreFeatFilters)
            {
                possibleFeats = possibleFeats.Where(x => x.CoreTags.Contains(filter)).ToList();
            }

            foreach (Skill filter in SkillFeatFilters)
            {
                possibleFeats = possibleFeats.Where(x => x.SkillTags.Contains(filter)).ToList();
            }

            foreach (Class filter in ClassFeatFilters)
            {
                possibleFeats = possibleFeats.Where(x => x.ClassTags.Contains(filter)).ToList();
            }

            foreach (Combat filter in CombatFeatFilters)
            {
                possibleFeats = possibleFeats.Where(x => x.CombatTags.Contains(filter)).ToList();
            }

            foreach (Role filter in RoleFeatFilters)
            {
                possibleFeats = possibleFeats.Where(x => x.RoleTags.Contains(filter)).ToList();
            }

            foreach (Magic filter in MagicFeatFilters)
            {
                possibleFeats = possibleFeats.Where(x => x.MagicTags.Contains(filter)).ToList();
            }

            foreach (Bonus filter in BonusFeatFilters)
            {
                possibleFeats = possibleFeats.Where(x => x.BonusTags.Contains(filter)).ToList();
            }

            foreach (Condition filter in ConditionFeatFilters)
            {
                possibleFeats = possibleFeats.Where(x => x.ConditionTags.Contains(filter)).ToList();
            }

            foreach (string filter in CustomFeatFilters)
            {
                possibleFeats = possibleFeats.Where(x => x.CustomTags.Contains(filter)).ToList();
            }

            foreach (Feat feat in possibleFeats)
            {
                yield return feat;
            }
        }

        private void UpdateFeatCustomTags()
        {
            CustomFeatFilterList.Clear();
            foreach (Feat Feat in MasterFeatList)
            {
                foreach (string tag in Feat.CustomTags)
                {
                    if (!CustomFeatFilterList.Contains(tag))
                    {
                        CustomFeatFilterList.Add(tag);
                    }
                }
            }
        }

        private void UpdateFeatReqs()
        {
            foreach (Feat feat in MasterFeatList)
            {
                string sanitizedName = feat.Name.Sanitize();
                foreach (Feat possibleReq in MasterFeatList)
                {
                    string possibleFeatSanitizedName = possibleReq.Name.Sanitize();
                    if (sanitizedName == possibleFeatSanitizedName) continue;

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

        private void AddFavoriteFeatAction(object arg)
        {
            if (SelectedFeat != null && !FavoriteFeatList.Contains(SelectedFeat))
            {
                FavoriteFeatList.Add(SelectedFeat);
                MasterFeatList.Remove(SelectedFeat);
                HiddenFeatList.Remove(SelectedFeat);

                UpdateFeatCustomTags();
                ApplyFeatFilters();
                SelectedFeat = null;
            }
        }

        private void AddHiddenFeatAction(object arg)
        {
            if (SelectedFeat != null && !HiddenFeatList.Contains(SelectedFeat))
            {
                HiddenFeatList.Add(SelectedFeat);
                MasterFeatList.Remove(SelectedFeat);
                FavoriteFeatList.Remove(SelectedFeat);

                UpdateFeatCustomTags();
                ApplyFeatFilters();
                SelectedFeat = null;
            }
        }

        private void EditFeatAction(object arg)
        {
            try
            {
                if (SelectedFeat != null)
                {
                    var vm = new FeatViewModel(SelectedFeat);
                    var configWindow = new FeatView(vm);

                    if (configWindow.ShowDialog() == true)
                    {
                        Feat newFeat = vm.GetFeat();

                        if (MasterFeatList.Contains(SelectedFeat))
                        {
                            MasterFeatList.Remove(SelectedFeat);
                            MasterFeatList.Add(newFeat);

                            UpdateFeatReqs();
                            UpdateFeatCustomTags();
                            ApplyFeatFilters();
                            if (FilteredFeatList.Contains(newFeat))
                            {
                                SelectedFeat = newFeat;
                            }
                        }

                        if (FavoriteFeatList.Contains(SelectedFeat))
                        {
                            FavoriteFeatList.Remove(SelectedFeat);
                            FavoriteFeatList.Add(newFeat);
                            SelectedFeat = newFeat;
                        }

                        else if (HiddenFeatList.Contains(SelectedFeat))
                        {
                            HiddenFeatList.Remove(SelectedFeat);
                            HiddenFeatList.Add(newFeat);
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

        private void NewFeatAction(object arg)
        {
            try
            {
                var vm = new FeatViewModel(new Feat());
                var configWindow = new FeatView(vm);

                if (configWindow.ShowDialog() == true)
                {
                    Feat newFeat = vm.GetFeat();
                    if (MasterFeatList.Select(x => x.Name).Contains(newFeat.Name) ||
                        FavoriteFeatList.Select(x => x.Name).Contains(newFeat.Name) ||
                        HiddenFeatList.Select(x => x.Name).Contains(newFeat.Name))
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

        private void FeatSearchRemovePlaceholderTextAction(object arg)
        {
            if (FeatSearchText == _searchPlaceholderText)
            {
                FeatSearchText = "";
            }
        }

        private void FeatSearchAddPlaceholderTextAction(object arg)
        {
            if (string.IsNullOrWhiteSpace(FeatSearchText))
            {
                FeatSearchText = _searchPlaceholderText;
            }
        }

        private void RemoveFeatAction(object arg)
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

        private void RemoveFavoriteFeatAction(object arg)
        {
            if (SelectedFeat != null && FavoriteFeatList.Contains(SelectedFeat))
            {
                MasterFeatList.Add(SelectedFeat);
                FavoriteFeatList.Remove(SelectedFeat);

                SelectedFeat = null;
                UpdateFeatReqs();
                UpdateFeatCustomTags();
                ApplyFeatFilters();
            }
        }

        private void RemoveHiddenFeatAction(object arg)
        {
            if (SelectedFeat != null && HiddenFeatList.Contains(SelectedFeat))
            {
                MasterFeatList.Add(SelectedFeat);
                HiddenFeatList.Remove(SelectedFeat);

                SelectedFeat = null;
                UpdateFeatReqs();
                UpdateFeatCustomTags();
                ApplyFeatFilters();
            }
        }
        #endregion

        #region Items
        // Filtered Item Collections
        public ObservableCollection<Item> FilteredItemList { get; set; } = new();
        public ObservableCollection<Item> FavoriteItemList { get; set; } = new();
        public ObservableCollection<Item> HiddenItemList { get; set; } = new();

        // Item Tag Collections
        public ObservableCollection<string> CustomItemFilterList { get; set; } = new();
        public ObservableCollection<string> CoreItemFilterList { get; set; } = new();
        public ObservableCollection<string> SkillItemFilterList { get; set; } = new();
        public ObservableCollection<string> ClassItemFilterList { get; set; } = new();
        public ObservableCollection<string> CombatItemFilterList { get; set; } = new();
        public ObservableCollection<string> RoleItemFilterList { get; set; } = new();
        public ObservableCollection<string> MagicItemFilterList { get; set; } = new();
        public ObservableCollection<string> BonusItemFilterList { get; set; } = new();
        public ObservableCollection<string> SourceItemFilterList { get; set; } = new();

        private Item? _selectedItem;
        public Item? SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (value != null)
                {
                    SelectedItem = null;
                }
                _selectedItem = value;
                CurrentItem.Clear();
                if (value != null)
                {
                    CurrentItem.Add(value);
                }

                OnPropertyChanged("SelectedItem");
            }
        }

        private string _selectedItemReq;
        public string? SelectedItemReq
        {
            get => _selectedItemReq;
            set
            {
                _selectedItemReq = value ?? "";
                string sanitizedSelection = _selectedItemReq.Sanitize();
                foreach (Item possibleItem in MasterItemList)
                {
                    if (possibleItem.Name.Sanitize() == sanitizedSelection)
                    {
                        SelectedItem = possibleItem;
                        _selectedItemReq = "";
                    }
                }

                OnPropertyChanged("SelectedItemReq");
            }
        }

        private string _itemSearchText = _searchPlaceholderText;
        public string ItemSearchText
        {
            get => _itemSearchText;
            set
            {
                _itemSearchText = value;
                ApplyItemFilters();

                OnPropertyChanged("ItemSearchText");
            }
        }

        // Item Filter Lists
        private HashSet<Core> CoreItemFilters = new HashSet<Core>();
        private HashSet<Skill> SkillItemFilters = new HashSet<Skill>();
        private HashSet<Class> ClassItemFilters = new HashSet<Class>();
        private HashSet<Combat> CombatItemFilters = new HashSet<Combat>();
        private HashSet<Role> RoleItemFilters = new HashSet<Role>();
        private HashSet<Magic> MagicItemFilters = new HashSet<Magic>();
        private HashSet<Bonus> BonusItemFilters = new HashSet<Bonus>();
        private HashSet<Source> SourceItemFilters = new HashSet<Source>();
        private HashSet<string> CustomItemFilters = new HashSet<string>();

        // Item Checkbox Commands
        public DelegateCommand CoreItemCheckboxCommand { get; }
        public DelegateCommand SkillItemCheckboxCommand { get; }
        public DelegateCommand ClassItemCheckboxCommand { get; }
        public DelegateCommand CombatItemCheckboxCommand { get; }
        public DelegateCommand RoleItemCheckboxCommand { get; }
        public DelegateCommand MagicItemCheckboxCommand { get; }
        public DelegateCommand BonusItemCheckboxCommand { get; }
        public DelegateCommand SourceItemCheckboxCommand { get; }
        public DelegateCommand CustomItemCheckboxCommand { get; }

        // Item Control Bar Commands
        public DelegateCommand ItemSearchRemovePlaceholderTextCommand { get; }
        public DelegateCommand ItemSearchAddPlaceholderTextCommand { get; }
        public DelegateCommand AddFavoriteItemCommand { get; }
        public DelegateCommand AddHiddenItemCommand { get; }
        public DelegateCommand EditItemCommand { get; }
        public DelegateCommand NewItemCommand { get; }
        public DelegateCommand RemoveItemCommand { get; }
        public DelegateCommand RemoveFavoriteItemCommand { get; }
        public DelegateCommand RemoveHiddenItemCommand { get; }

        // Item Checkbox Actions
        private void ItemCoreFilterAction(object arg)
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

        private void ItemSkillFilterAction(object arg)
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

        private void ItemClassFilterAction(object arg)
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

        private void ItemCombatFilterAction(object arg)
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

        private void ItemRoleFilterAction(object arg)
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

        private void ItemMagicFilterAction(object arg)
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

        private void ItemBonusFilterAction(object arg)
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

        private void ItemSourceFilterAction(object arg)
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

        private void ItemCustomFilterAction(object arg)
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

        private void ApplyItemFilters()
        {
            FilteredItemList.Clear();
            List<Item> possibleItems = ItemSearchText != _searchPlaceholderText && ItemSearchText != "" ?
                MasterItemList.Where(x => x.Name.Sanitize()
                                           .Contains(_itemSearchText.Sanitize())).ToList() : MasterItemList;

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
                FilteredItemList.Add(Item);
            }
        }

        private void UpdateItemCustomTags()
        {
            CustomItemFilterList.Clear();
            foreach (Item Item in MasterItemList)
            {
                foreach (string tag in Item.CustomTags)
                {
                    if (!CustomItemFilterList.Contains(tag))
                    {
                        CustomItemFilterList.Add(tag);
                    }
                }
            }
        }

        private void AddFavoriteItemAction(object arg)
        {
            if (SelectedItem != null && !FavoriteItemList.Contains(SelectedItem))
            {
                FavoriteItemList.Add(SelectedItem);
                MasterItemList.Remove(SelectedItem);
                HiddenItemList.Remove(SelectedItem);

                UpdateItemCustomTags();
                ApplyItemFilters();
                SelectedItem = null;
            }
        }

        private void AddHiddenItemAction(object arg)
        {
            if (SelectedItem != null && !HiddenItemList.Contains(SelectedItem))
            {
                HiddenItemList.Add(SelectedItem);
                MasterItemList.Remove(SelectedItem);
                FavoriteItemList.Remove(SelectedItem);

                UpdateItemCustomTags();
                ApplyItemFilters();
                SelectedItem = null;
            }
        }

        private void EditItemAction(object arg)
        {
            try
            {
                if (SelectedItem != null)
                {
                    var vm = new ItemViewModel(SelectedItem);
                    var configWindow = new ItemView(vm);

                    if (configWindow.ShowDialog() == true)
                    {
                        Item newItem = vm.GetItem();

                        if (MasterItemList.Contains(SelectedItem))
                        {
                            MasterItemList.Remove(SelectedItem);
                            MasterItemList.Add(newItem);

                            UpdateItemCustomTags();
                            ApplyItemFilters();
                            if (FilteredItemList.Contains(newItem))
                            {
                                SelectedItem = newItem;
                            }
                        }

                        if (FavoriteItemList.Contains(SelectedItem))
                        {
                            FavoriteItemList.Remove(SelectedItem);
                            FavoriteItemList.Add(newItem);
                            SelectedItem = newItem;
                        }

                        else if (HiddenItemList.Contains(SelectedItem))
                        {
                            HiddenItemList.Remove(SelectedItem);
                            HiddenItemList.Add(newItem);
                            SelectedItem = newItem;
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

            RefreshButtonState();
        }

        private void NewItemAction(object arg)
        {
            try
            {
                var vm = new ItemViewModel(new Item());
                var configWindow = new ItemView(vm);

                if (configWindow.ShowDialog() == true)
                {
                    Item newItem = vm.GetItem();
                    if (MasterItemList.Select(x => x.Name).Contains(newItem.Name) ||
                        FavoriteItemList.Select(x => x.Name).Contains(newItem.Name) ||
                        HiddenItemList.Select(x => x.Name).Contains(newItem.Name))
                    {
                        string messageBoxText = "Item with same name already exists";
                        string caption = "Duplicate";
                        MessageBoxButton button = MessageBoxButton.OK;
                        MessageBoxImage icon = MessageBoxImage.Exclamation;
                        MessageBox.Show(messageBoxText, caption, button, icon);
                    }
                    else
                    {
                        MasterItemList.Add(newItem);

                        UpdateItemCustomTags();
                        ApplyItemFilters();
                        if (FilteredItemList.Contains(newItem))
                        {
                            SelectedItem = newItem;
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

            RefreshButtonState();
        }

        private void ItemSearchRemovePlaceholderTextAction(object arg)
        {
            if (ItemSearchText == _searchPlaceholderText)
            {
                ItemSearchText = "";
            }
        }

        private void ItemSearchAddPlaceholderTextAction(object arg)
        {
            if (string.IsNullOrWhiteSpace(ItemSearchText))
            {
                ItemSearchText = _searchPlaceholderText;
            }
        }

        private void RemoveItemAction(object arg)
        {
            if (SelectedItem != null)
            {
                string messageBoxText = "Item will be removed. Are you sure?";
                string caption = "Warning";
                MessageBoxButton button = MessageBoxButton.YesNo;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

                if (result == MessageBoxResult.Yes)
                {
                    MasterItemList.Remove(SelectedItem);
                    HiddenItemList.Remove(SelectedItem);
                    FavoriteItemList.Remove(SelectedItem);
                    SelectedItem = null;
                    UpdateItemCustomTags();
                    ApplyItemFilters();
                }
            }
        }

        private void RemoveFavoriteItemAction(object arg)
        {
            if (SelectedItem != null && FavoriteItemList.Contains(SelectedItem))
            {
                MasterItemList.Add(SelectedItem);
                FavoriteItemList.Remove(SelectedItem);

                SelectedItem = null;
                UpdateItemCustomTags();
                ApplyItemFilters();
            }
        }

        private void RemoveHiddenItemAction(object arg)
        {
            if (SelectedItem != null && HiddenItemList.Contains(SelectedItem))
            {
                MasterItemList.Add(SelectedItem);
                HiddenItemList.Remove(SelectedItem);

                SelectedItem = null;
                UpdateItemCustomTags();
                ApplyItemFilters();
            }
        }
        #endregion

        private void LoadAction(object arg)
        {
            OpenFileDialog dialog = new()
            {
                Title = "Select File",
                Multiselect = false
            };

            if (dialog.ShowDialog() == true)
            {
                SerialBin bin = JsonSerializer.Deserialize<SerialBin>(File.ReadAllText(dialog.FileName))!;

                /*foreach (Clip clip in bin.ClipList)
                {
                    ClipList.Add(clip);
                }*/
            }
        }

        private void SaveAction(object arg)
        {
            if (_saveFileName.Length == 0)
            {
                SaveAsAction(arg);
                return;
            }
            SerializeAndSave(_saveFileName);
        }

        private void SerializeAndSave(string filename)
        {
            /*_saveFileName = filename;
            var jsonOpts = new JsonSerializerOptions { WriteIndented = true };
            var bin = new SerialBin(ClipList, ReadyList, ExportedList, HeadList);
            string serialBin = JsonSerializer.Serialize(bin, jsonOpts);

            File.WriteAllText(filename, serialBin);*/
        }

        private void SaveAsAction(object arg)
        {
            SaveFileDialog dialog = new()
            {
                Title = "Enter Filename"
            };

            if (dialog.ShowDialog() == true)
            {
                SerializeAndSave(dialog.FileName);
            }
        }

        private void ImportAction(object arg)
        {
            try
            {
                var vm = new ImportViewModel();
                var configWindow = new ImportView(vm);

                if (configWindow.ShowDialog() == true)
                {
                    string importPath = vm.GetSourcePath();
                    SourceType type = vm.GetSourceType();

                    switch (type)
                    {
                        case SourceType.Trait:
                            foreach (Trait trait in ImportReader.ReadTraitCsv(importPath))
                            {
                                MasterTraitList.Add(trait);
                            }
                            UpdateTraitCustomTags();
                            ApplyTraitFilters();
                            break;
                        case SourceType.Feat:
                            foreach (Feat feat in ImportReader.ReadFeatCsv(importPath))
                            {
                                MasterFeatList.Add(feat);
                            }
                            UpdateFeatReqs();
                            UpdateFeatCustomTags();
                            ApplyFeatFilters();
                            break;
                        case SourceType.Item:
                            ImportReader.ReadItemCsv(importPath);
                            break;
                        case SourceType.Spell:
                            ImportReader.ReadSpellCsv(importPath);
                            break;
                    }
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                string messageBoxText = "Source type was not selected";
                string caption = "Error";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                
                MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.OK);
            }

            RefreshButtonState();
        }

        private void RefreshButtonState()
        {
            LoadCommand.RaiseCanExecuteChanged();
            SaveCommand.RaiseCanExecuteChanged();
            LoadCommand.RaiseCanExecuteChanged();
            SaveCommand.RaiseCanExecuteChanged();
            SaveAsCommand.RaiseCanExecuteChanged();
            ImportCommand.RaiseCanExecuteChanged();
            CoreTraitCheckboxCommand.RaiseCanExecuteChanged();
            SkillTraitCheckboxCommand.RaiseCanExecuteChanged();
            ClassTraitCheckboxCommand.RaiseCanExecuteChanged();
            CombatTraitCheckboxCommand.RaiseCanExecuteChanged();
            RoleTraitCheckboxCommand.RaiseCanExecuteChanged();
            MagicTraitCheckboxCommand.RaiseCanExecuteChanged();
            SourceTraitCheckboxCommand.RaiseCanExecuteChanged();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
