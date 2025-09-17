namespace HavenheimManager.Descriptors;

/// <summary>
/// Settings for showing descriptors in the filter list
/// </summary>
/// <remarks>
/// When a new Descriptor set is added, update the following locations: <br/>
/// - DescriptorViewModel.ExtractDescriptorSettings() <br/> 
/// - DescriptorViewModel.AcceptChangesAction() <br/>
/// - Add a ShowDescriptorCheckboxAction/Command in DescriptorViewModel <br/>
/// - Add a public bool ShowDescriptor get/set in DescriptorViewModel <br/>
/// - Add a private bool _showDescriptor backing variable in DescriptorViewModel <br/>
/// - Add an Expander in the FilterControl.xaml for the new Descriptor <br/>
/// - Add a new HashSet for the new Descriptor in FilterHandler <br/>
/// - Add a ShowDescriptorCheckboxAction/Command in FilterHandler <br/>
/// - Add a Visibility field for the Descriptor in FilterHandler
/// - Add a ShowDescriptor ObservableCollection in FilterHandler <br/>
/// - Add the ObservableCollection to Strict, Broad, and Active trait filter methods in FilterHandler <br/>
/// - Add the ObservableCollection to FilterHandler.Clear() <br/>
/// - Fill the ObservableCollection with Havenheim/Pathfinder as needed
/// </remarks>
public record DescriptorSettings
{
    public bool ShowContent { get; set; }
    public bool ShowCreature { get; set; }
    public bool ShowCreatureType { get; set; }
    public bool ShowCreatureSubType { get; set; }
    public bool ShowCraftSkill { get; set; }
    public bool ShowAbilityScore { get; set; }
    public bool ShowAbilityType { get; set; }
    public bool ShowBuff { get; set; }
    public bool ShowFeat { get; set; }
    public bool ShowMagic { get; set; }
    public bool ShowSave { get; set; }
    public bool ShowSkill { get; set; }
    public bool ShowSystem { get; set; }
    public bool ShowTrait { get; set; }
    public bool ShowUsage { get; set; }
    public bool ShowDuration { get; set; }
    public bool ShowKnowledge { get; set; }
    public bool ShowMagicAura { get; set; }
    public bool ShowPerform { get; set; }
    public bool ShowSpellSchool { get; set; }
    public bool ShowStimulus { get; set; }
    public bool ShowTerrain { get; set; }
    public bool ShowCustom { get; set; }
}