using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using HavenheimManager.Containers;
using HavenheimManager.Enums;
using HavenheimManager.Extensions;
using HavenheimManager.Handlers;
using Condition = HavenheimManager.Enums.Condition;

namespace HavenheimManager.Editors;

public class FeatViewModel : INotifyPropertyChanged
{
    private string _featDescription;

    private string _featLevel;
    private string _featName = "";

    private string _featNotes;

    private string _featSource;

    private string _featUrl;

    private string _selectedAntireq;

    private string _selectedCustomTag;

    private string _selectedPostreq;

    private string _selectedPrereq;

    public FeatViewModel(Feat feat)
    {
        AcceptFeatCommand = new DelegateCommand(AcceptFeatAction);
        CancelCommand = new DelegateCommand(CancelAction);
        AddPrereqCommand = new DelegateCommand(AddPrereqAction);
        RemovePrereqCommand = new DelegateCommand(RemovePrereqAction);
        AddPostreqCommand = new DelegateCommand(AddPostreqAction);
        RemovePostreqCommand = new DelegateCommand(RemovePostreqAction);
        AddAntireqCommand = new DelegateCommand(AddAntireqAction);
        RemoveAntireqCommand = new DelegateCommand(RemoveAntireqAction);
        AddCustomTagCommand = new DelegateCommand(AddCustomTagAction);
        RemoveCustomTagCommand = new DelegateCommand(RemoveCustomTagAction);

        Feat = feat;

        FeatName = Feat.Name;
        FeatDescription = Feat.Description;
        FeatUrl = Feat.Url;
        FeatLevel = Feat.Level.ToString();
        FeatNotes = Feat.Notes;
        FeatSource = Feat.Source != Source.Unknown ? Feat.Source.GetEnumDescription() : "";

        foreach (string prereq in Feat.Prereqs)
        {
            Prereqs.Add(prereq);
        }

        foreach (string postreq in Feat.Postreqs)
        {
            Postreqs.Add(postreq);
        }

        foreach (string antireq in Feat.Antireqs)
        {
            Antireqs.Add(antireq);
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

        foreach (Magic item in Enum.GetValues(typeof(Magic)))
        {
            string tag = item.GetEnumDescription();
            MagicTags.Add(new CheckboxKvp(tag, Feat.MagicTags.Contains(item)));
        }

        foreach (Bonus item in Enum.GetValues(typeof(Bonus)))
        {
            string tag = item.GetEnumDescription();
            BonusTags.Add(new CheckboxKvp(tag, Feat.BonusTags.Contains(item)));
        }

        foreach (Condition item in Enum.GetValues(typeof(Condition)))
        {
            string tag = item.GetEnumDescription();
            ConditionTags.Add(new CheckboxKvp(tag, Feat.ConditionTags.Contains(item)));
        }

        foreach (Class item in Enum.GetValues(typeof(Class)))
        {
            string tag = item.GetEnumDescription();
            ClassTags.Add(new CheckboxKvp(tag, Feat.ClassTags.Contains(item)));
        }
    }

    public string FeatName
    {
        get => _featName;
        set
        {
            _featName = value;
            OnPropertyChanged("FeatName");
        }
    }

    public string FeatDescription
    {
        get => _featDescription;
        set
        {
            _featDescription = value;

            OnPropertyChanged("FeatDescription");
        }
    }

    public string FeatUrl
    {
        get => _featUrl;
        set
        {
            _featUrl = value;

            OnPropertyChanged("FeatUrl");
        }
    }

    public string FeatLevel
    {
        get => _featLevel;
        set
        {
            if (value == "")
            {
                _featLevel = "0";
                OnPropertyChanged("FeatLevel");
            }

            if (!RegexHandler.NumberFilter.IsMatch(value) && int.Parse(value) is <= 20 and >= 0)
            {
                _featLevel = value;
                OnPropertyChanged("FeatLevel");
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

    public string FeatNotes
    {
        get => _featNotes;
        set
        {
            _featNotes = value;

            OnPropertyChanged("FeatNotes");
        }
    }

    public string FeatSource
    {
        get => _featSource;
        set
        {
            _featSource = value;
            OnPropertyChanged("FeatSource");
        }
    }

    public string? SelectedPrereq
    {
        get => _selectedPrereq;
        set
        {
            _selectedPrereq = value ?? string.Empty;
            OnPropertyChanged("SelectedPrereq");
        }
    }

    public string? SelectedPostreq
    {
        get => _selectedPostreq;
        set
        {
            _selectedPostreq = value ?? string.Empty;
            OnPropertyChanged("SelectedPostreq");
        }
    }

    public string? SelectedAntireq
    {
        get => _selectedAntireq;
        set
        {
            _selectedAntireq = value ?? string.Empty;
            OnPropertyChanged("SelectedAntireq");
        }
    }

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

    public ObservableCollection<string> CustomTags { get; set; } = new();

    public ObservableCollection<string> Prereqs { get; set; } = new();

    public ObservableCollection<string> Postreqs { get; set; } = new();

    public ObservableCollection<string> Antireqs { get; set; } = new();

    public IList<CheckboxKvp> CoreTags { get; set; } = new List<CheckboxKvp>();

    public IList<CheckboxKvp> SkillTags { get; set; } = new List<CheckboxKvp>();

    public IList<CheckboxKvp> ClassTags { get; set; } = new List<CheckboxKvp>();

    public IList<CheckboxKvp> CombatTags { get; set; } = new List<CheckboxKvp>();

    public IList<CheckboxKvp> RoleTags { get; set; } = new List<CheckboxKvp>();

    public IList<CheckboxKvp> MagicTags { get; set; } = new List<CheckboxKvp>();

    public IList<CheckboxKvp> BonusTags { get; set; } = new List<CheckboxKvp>();

    public IList<CheckboxKvp> ConditionTags { get; set; } = new List<CheckboxKvp>();

    public DelegateCommand AcceptFeatCommand { get; }
    public DelegateCommand CancelCommand { get; }
    public DelegateCommand AddCustomTagCommand { get; }
    public DelegateCommand RemoveCustomTagCommand { get; }
    public DelegateCommand AddPrereqCommand { get; }
    public DelegateCommand RemovePrereqCommand { get; }
    public DelegateCommand AddPostreqCommand { get; }
    public DelegateCommand RemovePostreqCommand { get; }
    public DelegateCommand AddAntireqCommand { get; }
    public DelegateCommand RemoveAntireqCommand { get; }

    public event PropertyChangedEventHandler? PropertyChanged;

    public Feat GetFeat()
    {
        Feat.Name = FeatName;
        Feat.Description = FeatDescription;
        Feat.Url = FeatUrl;
        Feat.Level = int.Parse(FeatLevel);
        Feat.Notes = FeatNotes;
        Feat.Source = FeatSource != "Select Source" ? FeatSource.StringToEnum<Source>() : Source.Standard;

        Feat.Prereqs.Clear();
        foreach (string prereq in Prereqs)
        {
            Feat.Prereqs.Add(prereq);
        }

        Feat.Postreqs.Clear();
        foreach (string postreq in Postreqs)
        {
            Feat.Postreqs.Add(postreq);
        }

        Feat.Antireqs.Clear();
        foreach (string antireq in Antireqs)
        {
            Feat.Antireqs.Add(antireq);
        }

        Feat.CustomTags.Clear();
        foreach (string tag in CustomTags)
        {
            Feat.CustomTags.Add(tag);
        }

        Feat.CoreTags.Clear();
        foreach (CheckboxKvp tag in CoreTags)
        {
            if (tag.Value)
            {
                Feat.CoreTags.Add(tag.Key.StringToEnum<Core>());
            }
        }

        Feat.SkillTags.Clear();
        foreach (CheckboxKvp tag in SkillTags)
        {
            if (tag.Value)
            {
                Feat.SkillTags.Add(tag.Key.StringToEnum<Skill>());
            }
        }

        Feat.ClassTags.Clear();
        foreach (CheckboxKvp tag in ClassTags)
        {
            if (tag.Value)
            {
                Feat.ClassTags.Add(tag.Key.StringToEnum<Class>());
            }
        }

        Feat.CombatTags.Clear();
        foreach (CheckboxKvp tag in CombatTags)
        {
            if (tag.Value)
            {
                Feat.CombatTags.Add(tag.Key.StringToEnum<Combat>());
            }
        }

        Feat.RoleTags.Clear();
        foreach (CheckboxKvp tag in RoleTags)
        {
            if (tag.Value)
            {
                Feat.RoleTags.Add(tag.Key.StringToEnum<Role>());
            }
        }

        Feat.MagicTags.Clear();
        foreach (CheckboxKvp tag in MagicTags)
        {
            if (tag.Value)
            {
                Feat.MagicTags.Add(tag.Key.StringToEnum<Magic>());
            }
        }

        Feat.BonusTags.Clear();
        foreach (CheckboxKvp tag in BonusTags)
        {
            if (tag.Value)
            {
                Feat.BonusTags.Add(tag.Key.StringToEnum<Bonus>());
            }
        }

        Feat.ConditionTags.Clear();
        foreach (CheckboxKvp tag in ConditionTags)
        {
            if (tag.Value)
            {
                Feat.ConditionTags.Add(tag.Key.StringToEnum<Condition>());
            }
        }

        return Feat;
    }

    private void AddPrereqAction(object arg)
    {
        InputBox box = new("Prereq:");
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

    private void AddPostreqAction(object arg)
    {
        InputBox box = new("Postreq:");
        box.ShowDialog();

        if (box.DialogResult == true)
        {
            Postreqs.Add(box.GetInput());
        }
    }

    private void RemovePostreqAction(object arg)
    {
        if (SelectedPostreq != null)
        {
            Prereqs.Remove(SelectedPostreq);
            SelectedPostreq = null;
        }
    }

    private void AddAntireqAction(object arg)
    {
        InputBox box = new("Antireq:");
        box.ShowDialog();

        if (box.DialogResult == true)
        {
            Antireqs.Add(box.GetInput());
        }
    }

    private void RemoveAntireqAction(object arg)
    {
        if (SelectedAntireq != null)
        {
            Antireqs.Remove(SelectedAntireq);
            SelectedAntireq = null;
        }
    }

    private void AddCustomTagAction(object arg)
    {
        InputBox box = new("Custom Tag:");
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
        if (FeatName == null || string.IsNullOrEmpty(RegexHandler.SanitizationFilter.Replace(FeatName, "")))
        {
            string messageBoxText = "Feat must have a name";
            string caption = "Error";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Error;

            MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.OK);
            return;
        }

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

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}