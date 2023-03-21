using AssetManager.Containers;
using AssetManager.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace AssetManager.Editors
{
    public class FeatViewModel : INotifyPropertyChanged
    {
        private string _FeatName;
        public string FeatName
        {
            get => _FeatName;
            set
            {
                _FeatName = value;

                OnPropertyChanged("FeatName");
            }
        }

        private string _FeatDescription;
        public string FeatDescription
        {
            get => _FeatDescription;
            set
            {
                _FeatDescription = value;

                OnPropertyChanged("FeatDescription");
            }
        }

        private string _FeatUrl;
        public string FeatUrl
        {
            get => _FeatUrl;
            set
            {
                _FeatUrl = value;

                OnPropertyChanged("FeatUrl");
            }
        }

        private string _FeatNotes;
        public string FeatNotes
        {
            get => _FeatNotes;
            set
            {
                _FeatNotes = value;

                OnPropertyChanged("FeatNotes");
            }
        }

        private string _FeatSource;
        public string FeatSource
        {
            get => _FeatSource;
            set
            {
                _FeatSource = value;
                OnPropertyChanged("FeatSource");
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

        public Feat Feat { get; }

        public ObservableCollection<string> CustomTags { get; set; } = new ObservableCollection<string>();

        public ObservableCollection<string> Prereqs { get; set; } = new ObservableCollection<string>();

        public IList<CheckboxKvp> CoreTags { get; set; } = new List<CheckboxKvp>();

        public IList<CheckboxKvp> SkillTags { get; set; } = new List<CheckboxKvp>();

        public IList<CheckboxKvp> ClassTags { get; set; } = new List<CheckboxKvp>();

        public IList<CheckboxKvp> CombatTags { get; set; } = new List<CheckboxKvp>();

        public IList<CheckboxKvp> RoleTags { get; set; } = new List<CheckboxKvp>();

        public IList<CheckboxKvp> SchoolTags { get; set; } = new List<CheckboxKvp>();

        public DelegateCommand AcceptFeatCommand { get; }
        public DelegateCommand CancelCommand { get; }
        public DelegateCommand AddCustomTagCommand { get; }
        public DelegateCommand RemoveCustomTagCommand { get; }
        public DelegateCommand AddPrereqCommand { get; }
        public DelegateCommand RemovePrereqCommand { get; }

        public FeatViewModel(Feat feat)
        {
            AcceptFeatCommand = new DelegateCommand(AcceptFeatAction);
            CancelCommand = new DelegateCommand(CancelAction);
            AddPrereqCommand = new DelegateCommand(AddPrereqAction);
            RemovePrereqCommand = new DelegateCommand(RemovePrereqAction);
            AddCustomTagCommand = new DelegateCommand(AddCustomTagAction);
            RemoveCustomTagCommand = new DelegateCommand(RemoveCustomTagAction);

            Feat = feat;

            FeatName = Feat.Name;
            FeatDescription = Feat.Description;
            FeatUrl = Feat.Url;
            FeatNotes = Feat.Notes;
            FeatSource = (Feat.Source != Source.Unknown) ? Feat.Source.GetEnumDescription() : "";

            foreach (string prereq in Feat.Prereqs)
            {
                Prereqs.Add(prereq);
            }

            foreach (string tag in Feat.CustomTags)
            {
                CustomTags.Add(tag);
            }

            foreach (Core item in Enum.GetValues(typeof(Core)))
            {
                string tag = item.GetEnumDescription();
                CoreTags.Add(new CheckboxKvp(tag, Feat.CoreTags.Contains(item)));
            }

            foreach (Skill item in Enum.GetValues(typeof(Skill)))
            {
                string tag = item.GetEnumDescription();
                SkillTags.Add(new CheckboxKvp(tag, Feat.SkillTags.Contains(item)));
            }

            foreach (Combat item in Enum.GetValues(typeof(Combat)))
            {
                string tag = item.GetEnumDescription();
                CombatTags.Add(new CheckboxKvp(tag, Feat.CombatTags.Contains(item)));
            }

            foreach (Role item in Enum.GetValues(typeof(Role)))
            {
                string tag = item.GetEnumDescription();
                RoleTags.Add(new CheckboxKvp(tag, Feat.RoleTags.Contains(item)));
            }

            foreach (School item in Enum.GetValues(typeof(School)))
            {
                string tag = item.GetEnumDescription();
                SchoolTags.Add(new CheckboxKvp(tag, Feat.SchoolTags.Contains(item)));
            }

            foreach (Class item in Enum.GetValues(typeof(Class)))
            {
                string tag = item.GetEnumDescription();
                ClassTags.Add(new CheckboxKvp(tag, Feat.ClassTags.Contains(item)));
            }
        }

        public Feat GetFeat()
        {
            Feat.Name = FeatName;
            Feat.Description = FeatDescription;
            Feat.Url = FeatUrl;
            Feat.Notes = FeatNotes;
            Feat.Source = FeatSource.StringToSource();
            foreach (string prereq in Prereqs)
            {
                if (!Feat.Prereqs.Contains(prereq))
                {
                    Feat.Prereqs.Add(prereq);
                }
            }

            foreach (string tag in CustomTags)
            {
                if (!Feat.CustomTags.Contains(tag))
                {
                    Feat.CustomTags.Add(tag);
                }
            }

            Feat.CoreTags.Clear();
            foreach (CheckboxKvp tag in CoreTags)
            {
                if (tag.Value)
                {
                    Feat.CoreTags.Add(tag.Key.StringToCore());
                }
            }

            Feat.SkillTags.Clear();
            foreach (CheckboxKvp tag in SkillTags)
            {
                if (tag.Value)
                {
                    Feat.SkillTags.Add(tag.Key.StringToSkill());
                }
            }

            Feat.ClassTags.Clear();
            foreach (CheckboxKvp tag in ClassTags)
            {
                if (tag.Value)
                {
                    Feat.ClassTags.Add(tag.Key.StringToClass());
                }
            }

            Feat.CombatTags.Clear();
            foreach (CheckboxKvp tag in CombatTags)
            {
                if (tag.Value)
                {
                    Feat.CombatTags.Add(tag.Key.StringToCombat());
                }
            }

            Feat.RoleTags.Clear();
            foreach (CheckboxKvp tag in RoleTags)
            {
                if (tag.Value)
                {
                    Feat.RoleTags.Add(tag.Key.StringToRole());
                }
            }

            Feat.SchoolTags.Clear();
            foreach (CheckboxKvp tag in SchoolTags)
            {
                if (tag.Value)
                {
                    Feat.SchoolTags.Add(tag.Key.StringToSchool());
                }
            }

            return Feat;
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

        private void AcceptFeatAction(object arg)
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
