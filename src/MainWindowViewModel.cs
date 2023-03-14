using AssetManager.Containers;
using AssetManager.Enums;
using Microsoft.VisualBasic;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;

namespace AssetManager
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        // Primary Object Collections
        public ObservableCollection<Trait> TraitList { get; set; } = new();

        // Selected Object Backing Collections
        public ObservableCollection<Trait> CurrentTrait { get; set; } = new();
        public ObservableCollection<Feat> CurrentFeat { get; set; } = new();
        public ObservableCollection<Spell> CurrentSpell { get; set; } = new();
        public ObservableCollection<Item> CurrentItem { get; set; } = new();

        // Trait Filter Lists
        private List<Core> CoreTraitFilters = new List<Core>();
        private List<Skill> SkillTraitFilters = new List<Skill>();
        private List<Class> ClassTraitFilters = new List<Class>();
        private List<Combat> CombatTraitFilters = new List<Combat>();
        private List<Role> RoleTraitFilters = new List<Role>();
        private List<School> SchoolTraitFilters = new List<School>();
        private List<Source> SourceTraitFilters = new List<Source>();

        private string _saveFileName = "";

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
        public DelegateCommand LoadCommand { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand SaveAsCommand { get; set; }

        // Trait Checkbox Commands
        public DelegateCommand SkillTraitCheckboxCommand { get; set; }
        public DelegateCommand CoreTraitCheckboxCommand { get; set; }
        public DelegateCommand ClassTraitCheckboxCommand { get; set; }
        public DelegateCommand CombatTraitCheckboxCommand { get; set; }
        public DelegateCommand RoleTraitCheckboxCommand { get; set; }
        public DelegateCommand SchoolTraitCheckboxCommand { get; set; }

        public MainWindowViewModel()
        {
            LoadCommand = new DelegateCommand(LoadAction);
            SaveCommand = new DelegateCommand(SaveAction);
            SaveAsCommand = new DelegateCommand(SaveAsAction);
            SkillTraitCheckboxCommand = new DelegateCommand(TraitSkillFilterAction);
            CoreTraitCheckboxCommand = new DelegateCommand(TraitCoreFilterAction);
            /*CheckedClassTraitCheckboxCommand = new DelegateCommand();
            CheckedCombatTraitCheckboxCommand = new DelegateCommand();
            CheckedRoleTraitCheckboxCommand = new DelegateCommand();
            CheckedSchoolTraitCheckboxCommand = new DelegateCommand();
            UncheckedSkillTraitCheckboxCommand = new DelegateCommand();
            UncheckedCoreTraitCheckboxCommand = new DelegateCommand();
            UncheckedClassTraitCheckboxCommand = new DelegateCommand();
            UncheckedCombatTraitCheckboxCommand = new DelegateCommand();
            UncheckedRoleTraitCheckboxCommand = new DelegateCommand();
            UncheckedSchoolTraitCheckboxCommand = new DelegateCommand();*/
        }

        // Trait Checkbox Actions
        private void TraitSkillFilterAction(object arg)
        {
            if (arg is string filter)
            {
                Skill toggleSkill = filter.StringToSkill();
            }
        }
        private void TraitCoreFilterAction(object arg)
        {
            if (arg is string filter)
            {
                int a = 0;
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

                foreach (Clip clip in bin.ClipList)
                {
                    //ClipList.Add(clip);
                }
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

        private void RefreshButtonState()
        {
            LoadCommand.RaiseCanExecuteChanged();
            SaveCommand.RaiseCanExecuteChanged();
            LoadCommand.RaiseCanExecuteChanged();
            SaveCommand.RaiseCanExecuteChanged();
            SaveAsCommand.RaiseCanExecuteChanged();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
