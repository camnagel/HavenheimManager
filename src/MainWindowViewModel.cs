using System;
using AssetManager.Containers;
using AssetManager.Enums;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text.Json;
using System.Threading;
using AssetManager.Import;
using File = System.IO.File;
using System.Windows;

namespace AssetManager
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        // Primary Object Collections
        public List<Trait> MasterTraitList { get; } = new();

        // Filtered Object Collections
        public ObservableCollection<Trait> FilteredTraitList { get; set; } = new();

        // Selected Object Backing Collections
        public ObservableCollection<Trait> CurrentTrait { get; set; } = new();
        public ObservableCollection<Feat> CurrentFeat { get; set; } = new();
        public ObservableCollection<Spell> CurrentSpell { get; set; } = new();
        public ObservableCollection<Item> CurrentItem { get; set; } = new();

        // Custom tag collections
        public ObservableCollection<string> CustomTraitFilterList { get; set; } = new();

        // Trait Filter Lists
        private HashSet<Core> CoreTraitFilters = new HashSet<Core>();
        private HashSet<Skill> SkillTraitFilters = new HashSet<Skill>();
        private HashSet<Class> ClassTraitFilters = new HashSet<Class>();
        private HashSet<Combat> CombatTraitFilters = new HashSet<Combat>();
        private HashSet<Role> RoleTraitFilters = new HashSet<Role>();
        private HashSet<School> SchoolTraitFilters = new HashSet<School>();
        private HashSet<Source> SourceTraitFilters = new HashSet<Source>();
        private HashSet<string> CustomTraitFilters = new HashSet<string>();

        private string _saveFileName = "";

        private bool _activeFilters => CoreTraitFilters.Any() || 
                                      SkillTraitFilters.Any() || 
                                      ClassTraitFilters.Any() || 
                                      CombatTraitFilters.Any() || 
                                      RoleTraitFilters.Any() || 
                                      SchoolTraitFilters.Any() ||
                                      SourceTraitFilters.Any() ||
                                      CustomTraitFilters.Any();

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

        // Asset Commands
        public DelegateCommand LoadCommand { get; }
        public DelegateCommand SaveCommand { get; }
        public DelegateCommand SaveAsCommand { get; }
        public DelegateCommand ImportCommand { get; }

        // Trait Checkbox Commands
        public DelegateCommand CoreTraitCheckboxCommand { get; }
        public DelegateCommand SkillTraitCheckboxCommand { get; }
        public DelegateCommand ClassTraitCheckboxCommand { get; }
        public DelegateCommand CombatTraitCheckboxCommand { get; }
        public DelegateCommand RoleTraitCheckboxCommand { get; }
        public DelegateCommand SchoolTraitCheckboxCommand { get; }
        public DelegateCommand SourceTraitCheckboxCommand { get; }
        public DelegateCommand CustomTraitCheckboxCommand { get; }

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
        }

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
            if (!_activeFilters)
            {
                foreach (Trait trait in MasterTraitList)
                {
                    FilteredTraitList.Add(trait);
                }

                return;
            }

            List<Trait> possibleTraits = new List<Trait>(MasterTraitList);

            if (CoreTraitFilters.Any())
            {
                foreach (Core filter in CoreTraitFilters)
                {
                    possibleTraits = possibleTraits.Where(x => x.CoreTags.Contains(filter)).ToList();
                }
            }

            if (SkillTraitFilters.Any())
            {
                foreach (Skill filter in SkillTraitFilters)
                {
                    possibleTraits = possibleTraits.Where(x => x.SkillTags.Contains(filter)).ToList();
                }
            }

            if (ClassTraitFilters.Any())
            {
                foreach (Class filter in ClassTraitFilters)
                {
                    possibleTraits = possibleTraits.Where(x => x.ClassTags.Contains(filter)).ToList();
                }
            }

            if (CombatTraitFilters.Any())
            {
                foreach (Combat filter in CombatTraitFilters)
                {
                    possibleTraits = possibleTraits.Where(x => x.CombatTags.Contains(filter)).ToList();
                }
            }

            if (RoleTraitFilters.Any())
            {
                foreach (Role filter in RoleTraitFilters)
                {
                    possibleTraits = possibleTraits.Where(x => x.RoleTags.Contains(filter)).ToList();
                }
            }

            if (SchoolTraitFilters.Any())
            {
                foreach (School filter in SchoolTraitFilters)
                {
                    possibleTraits = possibleTraits.Where(x => x.SchoolTags.Contains(filter)).ToList();
                }
            }

            if (SourceTraitFilters.Any())
            {
                foreach (Source filter in SourceTraitFilters)
                {
                    possibleTraits = possibleTraits.Where(x => x.Source == filter).ToList();
                }
            }

            if (CustomTraitFilters.Any())
            {
                foreach (string filter in CustomTraitFilters)
                {
                    possibleTraits = possibleTraits.Where(x => x.CustomTags.Contains(filter)).ToList();
                }
            }

            foreach (Trait trait in possibleTraits)
            {
                FilteredTraitList.Add(trait);
            }
        }

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
                                foreach (string tag in trait.CustomTags)
                                {
                                    if (!CustomTraitFilterList.Contains(tag))
                                    {
                                        CustomTraitFilterList.Add(tag);
                                    }
                                }
                            }
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
