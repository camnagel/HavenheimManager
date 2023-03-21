﻿using AssetManager.Containers;
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
using File = System.IO.File;

namespace AssetManager
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        // Primary Object Collections
        public List<Trait> MasterTraitList { get; } = new();
        public List<Feat> MasterFeatList { get; } = new();

        // Selected Object Backing Collections
        public ObservableCollection<Trait> CurrentTrait { get; set; } = new();
        public ObservableCollection<Feat> CurrentFeat { get; set; } = new();
        public ObservableCollection<Spell> CurrentSpell { get; set; } = new();
        public ObservableCollection<Item> CurrentItem { get; set; } = new();

        private string _saveFileName = "";

        private static readonly Regex _whitespaceFilter = new Regex(@"[\s',-]+");

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
            CoreTraitCheckboxCommand = new DelegateCommand(TraitCoreFilterAction);
            SkillTraitCheckboxCommand = new DelegateCommand(TraitSkillFilterAction);
            ClassTraitCheckboxCommand = new DelegateCommand(TraitClassFilterAction);
            CombatTraitCheckboxCommand = new DelegateCommand(TraitCombatFilterAction);
            RoleTraitCheckboxCommand = new DelegateCommand(TraitRoleFilterAction);
            SchoolTraitCheckboxCommand = new DelegateCommand(TraitSchoolFilterAction);
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
            CoreFeatCheckboxCommand = new DelegateCommand(FeatCoreFilterAction);
            SkillFeatCheckboxCommand = new DelegateCommand(FeatSkillFilterAction);
            ClassFeatCheckboxCommand = new DelegateCommand(FeatClassFilterAction);
            CombatFeatCheckboxCommand = new DelegateCommand(FeatCombatFilterAction);
            RoleFeatCheckboxCommand = new DelegateCommand(FeatRoleFilterAction);
            SchoolFeatCheckboxCommand = new DelegateCommand(FeatSchoolFilterAction);
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
        public ObservableCollection<string> SchoolTraitFilterList { get; set; } = new();
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
        private HashSet<School> SchoolTraitFilters = new HashSet<School>();
        private HashSet<Source> SourceTraitFilters = new HashSet<Source>();
        private HashSet<string> CustomTraitFilters = new HashSet<string>();

        // Trait Checkbox Commands
        public DelegateCommand CoreTraitCheckboxCommand { get; }
        public DelegateCommand SkillTraitCheckboxCommand { get; }
        public DelegateCommand ClassTraitCheckboxCommand { get; }
        public DelegateCommand CombatTraitCheckboxCommand { get; }
        public DelegateCommand RoleTraitCheckboxCommand { get; }
        public DelegateCommand SchoolTraitCheckboxCommand { get; }
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

        private void TraitSchoolFilterAction(object arg)
        {
            if (arg is string filter)
            {
                School toggleSchool = filter.StringToSchool();

                if (SchoolTraitFilters.Contains(toggleSchool))
                {
                    SchoolTraitFilters.Remove(toggleSchool);
                }
                else
                {
                    SchoolTraitFilters.Add(toggleSchool);
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
                MasterTraitList.Where(
                    x => _whitespaceFilter.Replace(
                        x.Name.ToLower(), "").Contains(
                        _whitespaceFilter.Replace(_traitSearchText.ToLower(), "")))
                               .ToList() : MasterTraitList;

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

            foreach (School filter in SchoolTraitFilters)
            {
                possibleTraits = possibleTraits.Where(x => x.SchoolTags.Contains(filter)).ToList();
            }

            foreach (Source filter in SourceTraitFilters)
            {
                possibleTraits = possibleTraits.Where(x => x.Source == filter).ToList();
            }

            foreach (string filter in CustomTraitFilters)
            {
                possibleTraits = possibleTraits.Where(x => x.CustomTags.Contains(filter)).ToList();
            }

            foreach (Trait trait in possibleTraits)
            {
                FilteredTraitList.Add(trait);
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
        public ObservableCollection<string> SchoolFeatFilterList { get; set; } = new();
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
                string sanitizedSelection = _whitespaceFilter.Replace(_selectedFeatReq.ToLower(), "");
                foreach (Feat possibleFeat in MasterFeatList)
                {
                    if (_whitespaceFilter.Replace(possibleFeat.Name.ToLower(), "") == sanitizedSelection)
                    {
                        SelectedFeat = possibleFeat;
                        _selectedFeatReq = "";
                    }
                }

                OnPropertyChanged("SelectedFeatReq");
            }
        }

        private string _FeatSearchText = _searchPlaceholderText;
        public string FeatSearchText
        {
            get => _FeatSearchText;
            set
            {
                _FeatSearchText = value;
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
        private HashSet<School> SchoolFeatFilters = new HashSet<School>();
        private HashSet<Source> SourceFeatFilters = new HashSet<Source>();
        private HashSet<string> CustomFeatFilters = new HashSet<string>();

        // Feat Checkbox Commands
        public DelegateCommand CoreFeatCheckboxCommand { get; }
        public DelegateCommand SkillFeatCheckboxCommand { get; }
        public DelegateCommand ClassFeatCheckboxCommand { get; }
        public DelegateCommand CombatFeatCheckboxCommand { get; }
        public DelegateCommand RoleFeatCheckboxCommand { get; }
        public DelegateCommand SchoolFeatCheckboxCommand { get; }
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

        private void FeatSchoolFilterAction(object arg)
        {
            if (arg is string filter)
            {
                School toggleSchool = filter.StringToSchool();

                if (SchoolFeatFilters.Contains(toggleSchool))
                {
                    SchoolFeatFilters.Remove(toggleSchool);
                }
                else
                {
                    SchoolFeatFilters.Add(toggleSchool);
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
                MasterFeatList.Where(
                    x => _whitespaceFilter.Replace(
                        x.Name.ToLower(), "").Contains(
                        _whitespaceFilter.Replace(_FeatSearchText.ToLower(), "")))
                               .ToList() : MasterFeatList;

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

            foreach (School filter in SchoolFeatFilters)
            {
                possibleFeats = possibleFeats.Where(x => x.SchoolTags.Contains(filter)).ToList();
            }

            foreach (Source filter in SourceFeatFilters)
            {
                possibleFeats = possibleFeats.Where(x => x.Source == filter).ToList();
            }

            foreach (string filter in CustomFeatFilters)
            {
                possibleFeats = possibleFeats.Where(x => x.CustomTags.Contains(filter)).ToList();
            }

            foreach (Feat Feat in possibleFeats)
            {
                FilteredFeatList.Add(Feat);
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
                UpdateFeatCustomTags();
                ApplyFeatFilters();
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
            SchoolTraitCheckboxCommand.RaiseCanExecuteChanged();
            SourceTraitCheckboxCommand.RaiseCanExecuteChanged();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
