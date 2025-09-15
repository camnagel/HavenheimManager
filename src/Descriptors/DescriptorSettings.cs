using HavenheimManager.Editors;

namespace HavenheimManager.Descriptors;

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

    internal void UpdateSettings(DescriptorViewModel vm)
    {
        ShowContent = vm.ShowContent;
        ShowSkill = vm.ShowSkill;
        ShowMagic = vm.ShowMagic;
        ShowSystem = vm.ShowSystem;
        ShowTrait = vm.ShowTrait;
        ShowUsage = vm.ShowUsage;
        ShowDuration = vm.ShowDuration;
        ShowKnowledge = vm.ShowKnowledge;
        ShowMagicAura = vm.ShowMagicAura;
        ShowPerform = vm.ShowPerform;
        ShowSpellSchool = vm.ShowSpellSchool;
        ShowStimulus = vm.ShowStimulus;
        ShowTerrain = vm.ShowTerrain;
        ShowCreature = vm.ShowCreature;
        ShowCreatureType = vm.ShowCreatureType;
        ShowCreatureSubType = vm.ShowCreatureSubType;
        ShowCraftSkill = vm.ShowCraftSkill;
        ShowSave = vm.ShowSave;
        ShowAbilityScore = vm.ShowAbilityScore;
        ShowAbilityType = vm.ShowAbilityType;
        ShowBuff = vm.ShowBuff;
        ShowFeat = vm.ShowFeat;
    }
}