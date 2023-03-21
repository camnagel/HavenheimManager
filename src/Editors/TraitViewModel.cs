using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using AssetManager.Containers;
using AssetManager.Enums;

namespace AssetManager.Editors
{
    public class TraitViewModel : INotifyPropertyChanged
    {
        private string _traitName;
        public string TraitName
        {
            get => _traitName;
            set
            {
                _traitName = value;

                OnPropertyChanged("TraitName");
            }
        }

        private string _traitDescription;
        public string TraitDescription
        {
            get => _traitDescription;
            set
            {
                _traitDescription = value;

                OnPropertyChanged("TraitDescription");
            }
        }

        private string _traitUrl;
        public string TraitUrl
        {
            get => _traitUrl;
            set
            {
                _traitUrl = value;

                OnPropertyChanged("TraitUrl");
            }
        }

        private string _traitNotes;
        public string TraitNotes
        {
            get => _traitNotes;
            set
            {
                _traitNotes = value;

                OnPropertyChanged("TraitNotes");
            }
        }

        private string _traitSource;
        public string TraitSource
        {
            get => _traitSource;
            set
            {
                _traitSource = value;
                OnPropertyChanged("TraitSource");
            }
        }

        private string _selectedPrereq;
        public string? SelectedPrereq
        {
            get => _selectedPrereq;
            set
            {
                _selectedPrereq = value ?? string.Empty;
                OnPropertyChanged("SelectedPrereq");
            }
        }

        private string _selectedCustomTag;
        public string? SelectedCustomTag
        {
            get => _selectedCustomTag;
            set
            {
                _selectedCustomTag = value ?? string.Empty;
                OnPropertyChanged("SelectedCustomTag");
            }
        }

        public Trait Trait { get; }

        public ObservableCollection<string> CustomTags { get; set; } = new ObservableCollection<string>();

        public ObservableCollection<string> Prereqs { get; set; } = new ObservableCollection<string>();

        public IList<CheckboxKvp> CoreTags { get; set; } = new List<CheckboxKvp>();

        public IList<CheckboxKvp> SkillTags { get; set; } = new List<CheckboxKvp>();

        public IList<CheckboxKvp> ClassTags { get; set; } = new List<CheckboxKvp>();

        public IList<CheckboxKvp> CombatTags { get; set; } = new List<CheckboxKvp>();

        public IList<CheckboxKvp> RoleTags { get; set; } = new List<CheckboxKvp>();

        public IList<CheckboxKvp> MagicTags { get; set; } = new List<CheckboxKvp>();

        public IList<CheckboxKvp> BonusTags { get; set; } = new List<CheckboxKvp>();

        public DelegateCommand AcceptTraitCommand { get; }
        public DelegateCommand CancelCommand { get; }
        public DelegateCommand AddCustomTagCommand { get; }
        public DelegateCommand RemoveCustomTagCommand { get; }
        public DelegateCommand AddPrereqCommand { get; }
        public DelegateCommand RemovePrereqCommand { get; }

        public TraitViewModel(Trait trait)
        {
            AcceptTraitCommand = new DelegateCommand(AcceptTraitAction);
            CancelCommand = new DelegateCommand(CancelAction);
            AddPrereqCommand = new DelegateCommand(AddPrereqAction);
            RemovePrereqCommand = new DelegateCommand(RemovePrereqAction);
            AddCustomTagCommand = new DelegateCommand(AddCustomTagAction);
            RemoveCustomTagCommand = new DelegateCommand(RemoveCustomTagAction);

            Trait = trait;

            TraitName = Trait.Name;
            TraitDescription = Trait.Description;
            TraitUrl = Trait.Url;
            TraitNotes = Trait.Notes;
            TraitSource = (Trait.Source != Source.Unknown) ? Trait.Source.GetEnumDescription() : "";

            foreach (string prereq in Trait.Prereqs)
            {
                Prereqs.Add(prereq);
            }

            foreach (string tag in Trait.CustomTags)
            {
                CustomTags.Add(tag);
            }

            foreach (Core item in Enum.GetValues(typeof(Core)))
            {
                string tag = item.GetEnumDescription();
                CoreTags.Add(new CheckboxKvp(tag, trait.CoreTags.Contains(item)));
            }

            foreach (Skill item in Enum.GetValues(typeof(Skill)))
            {
                string tag = item.GetEnumDescription();
                SkillTags.Add(new CheckboxKvp(tag, trait.SkillTags.Contains(item)));
            }

            foreach (Combat item in Enum.GetValues(typeof(Combat)))
            {
                string tag = item.GetEnumDescription();
                CombatTags.Add(new CheckboxKvp(tag, trait.CombatTags.Contains(item)));
            }

            foreach (Role item in Enum.GetValues(typeof(Role)))
            {
                string tag = item.GetEnumDescription();
                RoleTags.Add(new CheckboxKvp(tag, trait.RoleTags.Contains(item)));
            }

            foreach (Magic item in Enum.GetValues(typeof(Magic)))
            {
                string tag = item.GetEnumDescription();
                MagicTags.Add(new CheckboxKvp(tag, trait.MagicTags.Contains(item)));
            }

            foreach (Bonus item in Enum.GetValues(typeof(Bonus)))
            {
                string tag = item.GetEnumDescription();
                BonusTags.Add(new CheckboxKvp(tag, trait.BonusTags.Contains(item)));
            }

            foreach (Class item in Enum.GetValues(typeof(Class)))
            {
                string tag = item.GetEnumDescription();
                ClassTags.Add(new CheckboxKvp(tag, trait.ClassTags.Contains(item)));
            }
        }

        public Trait GetTrait()
        {
            Trait.Name = TraitName;
            Trait.Description = TraitDescription;
            Trait.Url = TraitUrl;
            Trait.Notes = TraitNotes;
            Trait.Source = TraitSource.StringToSource();

            Trait.Prereqs.Clear();
            foreach (string prereq in Prereqs)
            {
                Trait.Prereqs.Add(prereq);
            }

            Trait.CustomTags.Clear();
            foreach (string tag in CustomTags)
            {
                Trait.CustomTags.Add(tag);
            }

            Trait.CoreTags.Clear();
            foreach (CheckboxKvp tag in CoreTags)
            {
                if (tag.Value)
                {
                    Trait.CoreTags.Add(tag.Key.StringToCore());
                }
            }

            Trait.SkillTags.Clear();
            foreach (CheckboxKvp tag in SkillTags)
            {
                if (tag.Value)
                {
                    Trait.SkillTags.Add(tag.Key.StringToSkill());
                }
            }

            Trait.ClassTags.Clear();
            foreach (CheckboxKvp tag in ClassTags)
            {
                if (tag.Value)
                {
                    Trait.ClassTags.Add(tag.Key.StringToClass());
                }
            }

            Trait.CombatTags.Clear();
            foreach (CheckboxKvp tag in CombatTags)
            {
                if (tag.Value)
                {
                    Trait.CombatTags.Add(tag.Key.StringToCombat());
                }
            }

            Trait.RoleTags.Clear();
            foreach (CheckboxKvp tag in RoleTags)
            {
                if (tag.Value)
                {
                    Trait.RoleTags.Add(tag.Key.StringToRole());
                }
            }

            Trait.MagicTags.Clear();
            foreach (CheckboxKvp tag in MagicTags)
            {
                if (tag.Value)
                {
                    Trait.MagicTags.Add(tag.Key.StringToMagic());
                }
            }

            Trait.BonusTags.Clear();
            foreach (CheckboxKvp tag in BonusTags)
            {
                if (tag.Value)
                {
                    Trait.BonusTags.Add(tag.Key.StringToBonus());
                }
            }

            return Trait;
        }

        private void AddPrereqAction(object arg)
        {
            InputBox box = new InputBox("Prereq:");
            box.ShowDialog();

            if (box.DialogResult == true)
            {
                Prereqs.Add(box.GetInput());
            }
        }

        private void RemovePrereqAction(object arg)
        {
            if (SelectedPrereq != null)
            {
                Prereqs.Remove(SelectedPrereq);
                SelectedPrereq = null;
            }
        }

        private void AddCustomTagAction(object arg)
        {
            InputBox box = new InputBox("Custom Tag:");
            box.ShowDialog();

            if (box.DialogResult == true)
            {
                CustomTags.Add(box.GetInput());
            }
        }

        private void RemoveCustomTagAction(object arg)
        {
            if (SelectedCustomTag != null)
            {
                CustomTags.Remove(SelectedCustomTag);
                SelectedCustomTag = null;
            }
        }

        private void AcceptTraitAction(object arg)
        {
            if (arg is Window window)
            {
                window.DialogResult = true;
            }
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

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
