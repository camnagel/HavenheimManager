using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using HavenheimManager.Containers;
using HavenheimManager.Enums;
using HavenheimManager.Extensions;

namespace HavenheimManager.Editors;

public class ItemViewModel : INotifyPropertyChanged
{
    private string _ItemDescription;
    private string _ItemName;

    private string _ItemNotes;

    private string _ItemSource;

    private string _ItemUrl;

    private string _selectedAntireq;

    private string _selectedCustomTag;

    private string _selectedPostreq;

    private string _selectedPrereq;

    public ItemViewModel(Item selectedItem)
    {
        AcceptItemCommand = new DelegateCommand(AcceptItemAction);
        CancelCommand = new DelegateCommand(CancelAction);
        AddPrereqCommand = new DelegateCommand(AddPrereqAction);
        RemovePrereqCommand = new DelegateCommand(RemovePrereqAction);
        AddPostreqCommand = new DelegateCommand(AddPostreqAction);
        RemovePostreqCommand = new DelegateCommand(RemovePostreqAction);
        AddAntireqCommand = new DelegateCommand(AddAntireqAction);
        RemoveAntireqCommand = new DelegateCommand(RemoveAntireqAction);
        AddCustomTagCommand = new DelegateCommand(AddCustomTagAction);
        RemoveCustomTagCommand = new DelegateCommand(RemoveCustomTagAction);

        Item = selectedItem;

        ItemName = Item.Name;
        ItemDescription = Item.Description;
        ItemUrl = Item.Url;
        ItemNotes = Item.Notes;
        ItemSource = Item.Source != Source.Unknown ? Item.Source.GetEnumDescription() : "";

        foreach (string prereq in Item.Prereqs)
        {
            Prereqs.Add(prereq);
        }

        foreach (string postreq in Item.Postreqs)
        {
            Postreqs.Add(postreq);
        }

        foreach (string antireq in Item.Antireqs)
        {
            Antireqs.Add(antireq);
        }

        foreach (string tag in Item.CustomTags)
        {
            CustomTags.Add(tag);
        }

        foreach (Core item in Enum.GetValues(typeof(Core)))
        {
            string tag = item.GetEnumDescription();
            CoreTags.Add(new CheckboxKvp(tag, Item.CoreTags.Contains(item)));
        }

        foreach (Skill item in Enum.GetValues(typeof(Skill)))
        {
            string tag = item.GetEnumDescription();
            SkillTags.Add(new CheckboxKvp(tag, Item.SkillTags.Contains(item)));
        }

        foreach (Combat item in Enum.GetValues(typeof(Combat)))
        {
            string tag = item.GetEnumDescription();
            CombatTags.Add(new CheckboxKvp(tag, Item.CombatTags.Contains(item)));
        }

        foreach (Role item in Enum.GetValues(typeof(Role)))
        {
            string tag = item.GetEnumDescription();
            RoleTags.Add(new CheckboxKvp(tag, Item.RoleTags.Contains(item)));
        }

        foreach (Magic item in Enum.GetValues(typeof(Magic)))
        {
            string tag = item.GetEnumDescription();
            MagicTags.Add(new CheckboxKvp(tag, Item.MagicTags.Contains(item)));
        }

        foreach (Bonus item in Enum.GetValues(typeof(Bonus)))
        {
            string tag = item.GetEnumDescription();
            BonusTags.Add(new CheckboxKvp(tag, Item.BonusTags.Contains(item)));
        }

        foreach (Class item in Enum.GetValues(typeof(Class)))
        {
            string tag = item.GetEnumDescription();
            ClassTags.Add(new CheckboxKvp(tag, Item.ClassTags.Contains(item)));
        }
    }

    public string ItemName
    {
        get => _ItemName;
        set
        {
            _ItemName = value;

            OnPropertyChanged("ItemName");
        }
    }

    public string ItemDescription
    {
        get => _ItemDescription;
        set
        {
            _ItemDescription = value;

            OnPropertyChanged("ItemDescription");
        }
    }

    public string ItemUrl
    {
        get => _ItemUrl;
        set
        {
            _ItemUrl = value;

            OnPropertyChanged("ItemUrl");
        }
    }

    public string ItemNotes
    {
        get => _ItemNotes;
        set
        {
            _ItemNotes = value;

            OnPropertyChanged("ItemNotes");
        }
    }

    public string ItemSource
    {
        get => _ItemSource;
        set
        {
            _ItemSource = value;
            OnPropertyChanged("ItemSource");
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

    public Item Item { get; }

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

    public DelegateCommand AcceptItemCommand { get; }
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

    public Item GetItem()
    {
        Item.Name = ItemName;
        Item.Description = ItemDescription;
        Item.Url = ItemUrl;
        Item.Notes = ItemNotes;
        Item.Source = ItemSource.StringToSource();

        Item.Prereqs.Clear();
        foreach (string prereq in Prereqs)
        {
            Item.Prereqs.Add(prereq);
        }

        Item.Postreqs.Clear();
        foreach (string postreq in Postreqs)
        {
            Item.Postreqs.Add(postreq);
        }

        Item.Antireqs.Clear();
        foreach (string antireq in Antireqs)
        {
            Item.Antireqs.Add(antireq);
        }

        Item.CustomTags.Clear();
        foreach (string tag in CustomTags)
        {
            Item.CustomTags.Add(tag);
        }

        Item.CoreTags.Clear();
        foreach (CheckboxKvp tag in CoreTags)
        {
            if (tag.Value)
            {
                Item.CoreTags.Add(tag.Key.StringToCore());
            }
        }

        Item.SkillTags.Clear();
        foreach (CheckboxKvp tag in SkillTags)
        {
            if (tag.Value)
            {
                Item.SkillTags.Add(tag.Key.StringToSkill());
            }
        }

        Item.ClassTags.Clear();
        foreach (CheckboxKvp tag in ClassTags)
        {
            if (tag.Value)
            {
                Item.ClassTags.Add(tag.Key.StringToClass());
            }
        }

        Item.CombatTags.Clear();
        foreach (CheckboxKvp tag in CombatTags)
        {
            if (tag.Value)
            {
                Item.CombatTags.Add(tag.Key.StringToCombat());
            }
        }

        Item.RoleTags.Clear();
        foreach (CheckboxKvp tag in RoleTags)
        {
            if (tag.Value)
            {
                Item.RoleTags.Add(tag.Key.StringToRole());
            }
        }

        Item.MagicTags.Clear();
        foreach (CheckboxKvp tag in MagicTags)
        {
            if (tag.Value)
            {
                Item.MagicTags.Add(tag.Key.StringToMagic());
            }
        }

        Item.BonusTags.Clear();
        foreach (CheckboxKvp tag in BonusTags)
        {
            if (tag.Value)
            {
                Item.BonusTags.Add(tag.Key.StringToBonus());
            }
        }

        return Item;
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

    private void AcceptItemAction(object arg)
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

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}