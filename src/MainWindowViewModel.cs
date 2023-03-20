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
using File = System.IO.File;

namespace AssetManager
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        // Primary Object Collections
        public List<Trait> MasterTraitList { get; } = new();

        // Filtered Object Collections
        public ObservableCollection<Trait> FilteredTraitList { get; set; } = new();
        public ObservableCollection<Trait> FavoriteTraitList { get; set; } = new();
        public ObservableCollection<Trait> HiddenTraitList { get; set; } = new();

        // Selected Object Backing Collections
        public ObservableCollection<Trait> CurrentTrait { get; set; } = new();
        public ObservableCollection<Feat> CurrentFeat { get; set; } = new();
        public ObservableCollection<Spell> CurrentSpell { get; set; } = new();
        public ObservableCollection<Item> CurrentItem { get; set; } = new();

        // Tag collections
        public ObservableCollection<string> CustomTraitFilterList { get; set; } = new();
        public ObservableCollection<string> CoreTraitFilterList { get; set; } = new();
        public ObservableCollection<string> SkillTraitFilterList { get; set; } = new();
        public ObservableCollection<string> ClassTraitFilterList { get; set; } = new();
        public ObservableCollection<string> CombatTraitFilterList { get; set; } = new();
        public ObservableCollection<string> RoleTraitFilterList { get; set; } = new();
        public ObservableCollection<string> SchoolTraitFilterList { get; set; } = new();
        public ObservableCollection<string> SourceTraitFilterList { get; set; } = new();

        private string _saveFileName = "";

        private static readonly Regex _whitespaceFilter = new Regex(@"\s+");

        private static readonly string _searchPlaceholderText = "Search...";

        /// <summary>
        /// The selected <see cref="Trait"/>
        /// </summary>
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
        }

        #region Traits
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
                            ImportReader.ReadFeatCsv(importPath);
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
