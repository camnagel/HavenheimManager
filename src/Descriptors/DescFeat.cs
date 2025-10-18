using System.ComponentModel;

namespace HavenheimManager.Descriptors;

public enum DescFeat
{
    [Description("Combat")] Combat,
    [Description("Magic")] Magic,
    [Description("Utility")] Utility,
    [Description("Armor")] Armor,
    [Description("Weapon")] Weapon,
    [Description("Maneuver")] Maneuver,
    [Description("Critical")] Critical,
    [Description("Spell")] Spell,
    [Description("Metamagic")] Metamagic,
    [Description("Crafting")] Crafting,
    [Description("Skill")] Skill,
    [Description("Mythic")] Mythic,
    [Description("Story")] Story,
    [Description("Style")] Style,
    [Description("Social")] Social
}