using HavenheimManager.Containers;
using HavenheimManager.Descriptors;
using HavenheimManager.Enums;
using HavenheimManager.Extensions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace HavenheimManager.Editors;

public class DescriptorViewModel : INotifyPropertyChanged
{
    /// <summary>
    ///     The <see cref="DescriptorSettings" /> from the caller
    /// </summary>
    /// <remarks>
    ///     These should be used to populate the show variables on this view model <br />
    ///     These should only be changed when the user accepts the changes
    /// </remarks>
    private readonly DescriptorSettings _settings;

    private DescWrapper? _selectedCustomDesc;

    private string _searchText = AppSettings.SearchPlaceholderText;

    internal DescriptorViewModel(DescriptorSettings settings)
    {
        CancelCommand = new DelegateCommand(CancelAction);
        AcceptChangesCommand = new DelegateCommand(AcceptChangesAction);
        SearchRemovePlaceholderTextCommand = new DelegateCommand(SearchRemovePlaceholderTextAction);
        SearchAddPlaceholderTextCommand = new DelegateCommand(SearchAddPlaceholderTextAction);
        AddCustomDescCommand = new DelegateCommand(AddCustomDescAction);
        RemoveCustomDescCommand = new DelegateCommand(RemoveCustomDescAction);

        // We do this to avoid messing with the settings until the user confirms their changes
        ExtractDescriptorSettings(settings);
        _settings = settings;

        // Get descriptors based off the current app mode
        GetDescriptors(AppSettings.Mode);
    }

    public DelegateCommand AddCustomDescCommand { get; }

    public DelegateCommand RemoveCustomDescCommand { get; }

    public DescWrapper? SelectedCustomDesc
    {
        get => _selectedCustomDesc;
        set
        {
            _selectedCustomDesc = value;
            OnPropertyChanged("SelectedCustomDesc");
        }
    }

    private void AddCustomDescAction(object arg)
    {
        CustomDescriptorList.Add(new DescWrapper("Custom Descriptor"));
    }

    private void RemoveCustomDescAction(object arg)
    {
        if (SelectedCustomDesc != null)
        {
            CustomDescriptorList.Remove(SelectedCustomDesc);
            SelectedCustomDesc = null;
        }
    }

    public DelegateCommand AcceptChangesCommand { get; }

    public DelegateCommand CancelCommand { get; }

    public ObservableCollection<string> ContentDescriptorList { get; } = new();
    public ObservableCollection<string> CreatureDescriptorList { get; } = new();
    public ObservableCollection<string> CreatureTypeDescriptorList { get; } = new();
    public ObservableCollection<string> CreatureSubTypeDescriptorList { get; } = new();
    public ObservableCollection<string> CraftSkillDescriptorList { get; } = new();
    public ObservableCollection<string> AbilityDescriptorList { get; } = new();
    public ObservableCollection<string> AbilityTypeDescriptorList { get; } = new();
    public ObservableCollection<string> BuffDescriptorList { get; } = new();
    public ObservableCollection<string> FeatDescriptorList { get; } = new();
    public ObservableCollection<string> MagicDescriptorList { get; } = new();
    public ObservableCollection<string> SaveDescriptorList { get; } = new();
    public ObservableCollection<string> SkillDescriptorList { get; } = new();
    public ObservableCollection<string> SystemDescriptorList { get; } = new();
    public ObservableCollection<string> TraitDescriptorList { get; } = new();
    public ObservableCollection<string> UsageDescriptorList { get; } = new();
    public ObservableCollection<string> DurationDescriptorList { get; } = new();
    public ObservableCollection<string> KnowledgeDescriptorList { get; } = new();
    public ObservableCollection<string> MagicAuraDescriptorList { get; } = new();
    public ObservableCollection<string> PerformDescriptorList { get; } = new();
    public ObservableCollection<string> SpellSchoolDescriptorList { get; } = new();
    public ObservableCollection<string> StimulusDescriptorList { get; } = new();
    public ObservableCollection<string> TerrainDescriptorList { get; } = new();
    public ObservableCollection<DescWrapper> CustomDescriptorList { get; } = new();

    public DelegateCommand SearchRemovePlaceholderTextCommand { get; }
    public DelegateCommand SearchAddPlaceholderTextCommand { get; }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void ExtractDescriptorSettings(DescriptorSettings settings)
    {
        ShowLevel = settings.ShowLevel;
        ShowContent = settings.ShowContent;
        ShowSkill = settings.ShowSkill;
        ShowMagic = settings.ShowMagic;
        ShowSystem = settings.ShowSystem;
        ShowTrait = settings.ShowTrait;
        ShowUsage = settings.ShowUsage;
        ShowDuration = settings.ShowDuration;
        ShowKnowledge = settings.ShowKnowledge;
        ShowMagicAura = settings.ShowMagicAura;
        ShowPerform = settings.ShowPerform;
        ShowSpellSchool = settings.ShowSpellSchool;
        ShowStimulus = settings.ShowStimulus;
        ShowTerrain = settings.ShowTerrain;
        ShowCreature = settings.ShowCreature;
        ShowCreatureType = settings.ShowCreatureType;
        ShowCreatureSubType = settings.ShowCreatureSubType;
        ShowCraftSkill = settings.ShowCraftSkill;
        ShowSave = settings.ShowSave;
        ShowAbility = settings.ShowAbility;
        ShowAbilityType = settings.ShowAbilityType;
        ShowBuff = settings.ShowBuff;
        ShowFeat = settings.ShowFeat;
        ShowCustom = settings.ShowCustom;
        
        foreach (string desc in settings.CustomDescList)
        {
            CustomDescriptorList.Add(new DescWrapper(desc));
        }
    }

    private void GetDescriptors(AppMode mode)
    {
        ContentDescriptorList.Fill(DescriptorUtils.GetContent(mode));
        CreatureDescriptorList.Fill(DescriptorUtils.GetCreature(mode));
        CreatureTypeDescriptorList.Fill(DescriptorUtils.GetCreatureType(mode));
        CreatureSubTypeDescriptorList.Fill(DescriptorUtils.GetCreatureSubType(mode));
        CraftSkillDescriptorList.Fill(DescriptorUtils.GetCraftSkill(mode));
        AbilityDescriptorList.Fill(DescriptorUtils.GetAbilityScore(mode));
        AbilityTypeDescriptorList.Fill(DescriptorUtils.GetAbilityType(mode));
        BuffDescriptorList.Fill(DescriptorUtils.GetBuff(mode));
        FeatDescriptorList.Fill(DescriptorUtils.GetDescFeat(mode));
        MagicDescriptorList.Fill(DescriptorUtils.GetDescMagic(mode));
        SaveDescriptorList.Fill(DescriptorUtils.GetSave(mode));
        SkillDescriptorList.Fill(DescriptorUtils.GetDescSkill(mode));
        SystemDescriptorList.Fill(DescriptorUtils.GetSystem(mode));
        TraitDescriptorList.Fill(DescriptorUtils.GetDescTrait(mode));
        UsageDescriptorList.Fill(DescriptorUtils.GetUsage(mode));
        DurationDescriptorList.Fill(DescriptorUtils.GetDuration(mode));
        KnowledgeDescriptorList.Fill(DescriptorUtils.GetKnowledge(mode));
        MagicAuraDescriptorList.Fill(DescriptorUtils.GetMagicAura(mode));
        PerformDescriptorList.Fill(DescriptorUtils.GetPerformSkill(mode));
        SpellSchoolDescriptorList.Fill(DescriptorUtils.GetSpellSchool(mode));
        StimulusDescriptorList.Fill(DescriptorUtils.GetStimulus(mode));
        TerrainDescriptorList.Fill(DescriptorUtils.GetTerrain(mode));
    }

    private void ClearDescriptors()
    {
        ContentDescriptorList.Clear();
        CreatureDescriptorList.Clear();
        CreatureTypeDescriptorList.Clear();
        CreatureSubTypeDescriptorList.Clear();
        CraftSkillDescriptorList.Clear();
        AbilityDescriptorList.Clear();
        AbilityTypeDescriptorList.Clear();
        BuffDescriptorList.Clear();
        FeatDescriptorList.Clear();
        MagicDescriptorList.Clear();
        SaveDescriptorList.Clear();
        SkillDescriptorList.Clear();
        SystemDescriptorList.Clear();
        TraitDescriptorList.Clear();
        UsageDescriptorList.Clear();
        DurationDescriptorList.Clear();
        KnowledgeDescriptorList.Clear();
        MagicAuraDescriptorList.Clear();
        PerformDescriptorList.Clear();
        SpellSchoolDescriptorList.Clear();
        StimulusDescriptorList.Clear();
        TerrainDescriptorList.Clear();
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

    private void AcceptChangesAction(object arg)
    {
        _settings.ShowLevel = ShowLevel;
        _settings.ShowContent = ShowContent;
        _settings.ShowSkill = ShowSkill;
        _settings.ShowMagic = ShowMagic;
        _settings.ShowSystem = ShowSystem;
        _settings.ShowTrait = ShowTrait;
        _settings.ShowUsage = ShowUsage;
        _settings.ShowDuration = ShowDuration;
        _settings.ShowKnowledge = ShowKnowledge;
        _settings.ShowMagicAura = ShowMagicAura;
        _settings.ShowPerform = ShowPerform;
        _settings.ShowSpellSchool = ShowSpellSchool;
        _settings.ShowStimulus = ShowStimulus;
        _settings.ShowTerrain = ShowTerrain;
        _settings.ShowCreature = ShowCreature;
        _settings.ShowCreatureType = ShowCreatureType;
        _settings.ShowCreatureSubType = ShowCreatureSubType;
        _settings.ShowCraftSkill = ShowCraftSkill;
        _settings.ShowSave = ShowSave;
        _settings.ShowAbility = ShowAbility;
        _settings.ShowAbilityType = ShowAbilityType;
        _settings.ShowBuff = ShowBuff;
        _settings.ShowFeat = ShowFeat;
        _settings.ShowCustom = ShowCustom;

        _settings.CustomDescList.Clear();
        _settings.CustomDescList.AddRange(CustomDescriptorList.Select(x => x.Descriptor));

        if (arg is Window window)
        {
            window.DialogResult = true;
        }
    }

    private void SearchRemovePlaceholderTextAction(object arg)
    {
        if (SearchText == AppSettings.SearchPlaceholderText)
        {
            SearchText = "";
        }
    }

    private void SearchAddPlaceholderTextAction(object arg)
    {
        if (string.IsNullOrWhiteSpace(SearchText))
        {
            SearchText = AppSettings.SearchPlaceholderText;
        }
    }

    public string SearchText
    {
        get => _searchText;
        set
        {
            _searchText = value;
            SearchTextAction(_searchText.Sanitize());

            OnPropertyChanged("SearchText");
        }
    }

    private void SearchTextAction(string text)
    {
        // Refresh descriptor lists, inefficient but should be fast enough
        ClearDescriptors();
        GetDescriptors(AppSettings.Mode);

        if (text == AppSettings.SearchPlaceholderText.Sanitize() || text.Length == 0)
            return;

        // Filter descriptor lists for match to search string
        KeepSearchMatch(ContentDescriptorList, text);
        KeepSearchMatch(CreatureDescriptorList, text);
        KeepSearchMatch(CreatureTypeDescriptorList, text);
        KeepSearchMatch(CreatureSubTypeDescriptorList, text);
        KeepSearchMatch(CraftSkillDescriptorList, text);
        KeepSearchMatch(AbilityDescriptorList, text);
        KeepSearchMatch(AbilityTypeDescriptorList, text);
        KeepSearchMatch(BuffDescriptorList, text);
        KeepSearchMatch(FeatDescriptorList, text);
        KeepSearchMatch(MagicDescriptorList, text);
        KeepSearchMatch(SaveDescriptorList, text);
        KeepSearchMatch(SkillDescriptorList, text);
        KeepSearchMatch(SystemDescriptorList, text);
        KeepSearchMatch(TraitDescriptorList, text);
        KeepSearchMatch(UsageDescriptorList, text);
        KeepSearchMatch(DurationDescriptorList, text);
        KeepSearchMatch(KnowledgeDescriptorList, text);
        KeepSearchMatch(MagicAuraDescriptorList, text);
        KeepSearchMatch(PerformDescriptorList, text);
        KeepSearchMatch(SpellSchoolDescriptorList, text);
        KeepSearchMatch(StimulusDescriptorList, text);
        KeepSearchMatch(TerrainDescriptorList, text);
    }

    /// <summary>
    /// Check if a collection contains a specific string
    /// </summary>
    /// <remarks>
    /// Also matches on substring rather than just exact match
    /// </remarks>
    private void KeepSearchMatch(ICollection<string> list, string text)
    {
        if (!list.Any(entry => entry.Sanitize().Contains(text)))
            list.Clear();
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    #region ShowVariables
    private bool _showLevel;

    public bool ShowLevel
    {
        get => _showLevel;
        set
        {
            if (value == _showLevel)
            {
                return;
            }

            _showLevel = value;
            OnPropertyChanged("ShowLevel");
        }
    }

    private bool _showContent;

    public bool ShowContent
    {
        get => _showContent;
        set
        {
            if (value == _showContent)
            {
                return;
            }

            _showContent = value;
            OnPropertyChanged("ShowContent");
        }
    }

    private bool _showCreature;

    public bool ShowCreature
    {
        get => _showCreature;
        set
        {
            if (value == _showCreature)
            {
                return;
            }

            _showCreature = value;
            OnPropertyChanged("ShowCreature");
        }
    }

    private bool _showCreatureType;

    public bool ShowCreatureType
    {
        get => _showCreatureType;
        set
        {
            if (value == _showCreatureType)
            {
                return;
            }

            _showCreatureType = value;
            OnPropertyChanged("ShowCreatureType");
        }
    }

    private bool _showCreatureSubType;

    public bool ShowCreatureSubType
    {
        get => _showCreatureSubType;
        set
        {
            if (value == _showCreatureSubType)
            {
                return;
            }

            _showCreatureSubType = value;
            OnPropertyChanged("ShowCreatureSubType");
        }
    }

    private bool _showCraftSkill;

    public bool ShowCraftSkill
    {
        get => _showCraftSkill;
        set
        {
            if (value == _showCraftSkill)
            {
                return;
            }

            _showCraftSkill = value;
            OnPropertyChanged("ShowCraftSkill");
        }
    }

    private bool _showAbility;

    public bool ShowAbility
    {
        get => _showAbility;
        set
        {
            if (value == _showAbility)
            {
                return;
            }

            _showAbility = value;
            OnPropertyChanged("ShowAbility");
        }
    }

    private bool _showAbilityType;

    public bool ShowAbilityType
    {
        get => _showAbilityType;
        set
        {
            if (value == _showAbilityType)
            {
                return;
            }

            _showAbilityType = value;
            OnPropertyChanged("ShowAbilityType");
        }
    }

    private bool _showBuff;

    public bool ShowBuff
    {
        get => _showBuff;
        set
        {
            if (value == _showBuff)
            {
                return;
            }

            _showBuff = value;
            OnPropertyChanged("ShowBuff");
        }
    }

    private bool _showFeat;

    public bool ShowFeat
    {
        get => _showFeat;
        set
        {
            if (value == _showFeat)
            {
                return;
            }

            _showFeat = value;
            OnPropertyChanged("ShowFeat");
        }
    }

    private bool _showMagic;

    public bool ShowMagic
    {
        get => _showMagic;
        set
        {
            if (value == _showMagic)
            {
                return;
            }

            _showMagic = value;
            OnPropertyChanged("ShowMagic");
        }
    }

    private bool _showSave;

    public bool ShowSave
    {
        get => _showSave;
        set
        {
            if (value == _showSave)
            {
                return;
            }

            _showSave = value;
            OnPropertyChanged("ShowSave");
        }
    }

    private bool _showSkill;

    public bool ShowSkill
    {
        get => _showSkill;
        set
        {
            if (value == _showSkill)
            {
                return;
            }

            _showSkill = value;
            OnPropertyChanged("ShowSkill");
        }
    }

    private bool _showSystem;

    public bool ShowSystem
    {
        get => _showSystem;
        set
        {
            if (value == _showSystem)
            {
                return;
            }

            _showSystem = value;
            OnPropertyChanged("ShowSystem");
        }
    }

    private bool _showTrait;

    public bool ShowTrait
    {
        get => _showTrait;
        set
        {
            if (value == _showTrait)
            {
                return;
            }

            _showTrait = value;
            OnPropertyChanged("ShowTrait");
        }
    }

    private bool _showUsage;

    public bool ShowUsage
    {
        get => _showUsage;
        set
        {
            if (value == _showUsage)
            {
                return;
            }

            _showUsage = value;
            OnPropertyChanged("ShowUsage");
        }
    }

    private bool _showDuration;

    public bool ShowDuration
    {
        get => _showDuration;
        set
        {
            if (value == _showDuration)
            {
                return;
            }

            _showDuration = value;
            OnPropertyChanged("ShowDuration");
        }
    }

    private bool _showKnowledge;

    public bool ShowKnowledge
    {
        get => _showKnowledge;
        set
        {
            if (value == _showKnowledge)
            {
                return;
            }

            _showKnowledge = value;
            OnPropertyChanged("ShowKnowledge");
        }
    }

    private bool _showMagicAura;

    public bool ShowMagicAura
    {
        get => _showMagicAura;
        set
        {
            if (value == _showMagicAura)
            {
                return;
            }

            _showMagicAura = value;
            OnPropertyChanged("ShowMagicAura");
        }
    }

    private bool _showPerform;

    public bool ShowPerform
    {
        get => _showPerform;
        set
        {
            if (value == _showPerform)
            {
                return;
            }

            _showPerform = value;
            OnPropertyChanged("ShowPerform");
        }
    }

    private bool _showSpellSchool;

    public bool ShowSpellSchool
    {
        get => _showSpellSchool;
        set
        {
            if (value == _showSpellSchool)
            {
                return;
            }

            _showSpellSchool = value;
            OnPropertyChanged("ShowSpellSchool");
        }
    }

    private bool _showStimulus;

    public bool ShowStimulus
    {
        get => _showStimulus;
        set
        {
            if (value == _showStimulus)
            {
                return;
            }

            _showStimulus = value;
            OnPropertyChanged("ShowStimulus");
        }
    }

    private bool _showTerrain;

    public bool ShowTerrain
    {
        get => _showTerrain;
        set
        {
            if (value == _showTerrain)
            {
                return;
            }

            _showTerrain = value;
            OnPropertyChanged("ShowTerrain");
        }
    }

    private bool _showCustom;

    public bool ShowCustom
    {
        get => _showCustom;
        set
        {
            if (value == _showCustom)
            {
                return;
            }

            _showCustom = value;
            OnPropertyChanged("ShowCustom");
        }
    }

    #endregion
}