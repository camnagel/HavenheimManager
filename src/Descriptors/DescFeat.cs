using System.ComponentModel;

namespace HavenheimManager.Descriptors;

public enum DescFeat
{
    [Description("Abjuration")] Combat,
    [Description("Conjuration")] Magic,
    [Description("Divination")] Utility,
    [Description("Enchantment")] Armor,
    [Description("Evocation")] Weapon,
    [Description("Illusion")] Maneuver,
    [Description("Necromancy")] Critical,
    [Description("Spell")] Spell,
    [Description("Metamagic")] Metamagic,
    [Description("Abjuration")] Crafting,
    [Description("Conjuration")] Skill,
    [Description("Divination")] Mythic,
    [Description("Enchantment")] Story,
    [Description("Evocation")] Style,
    [Description("Illusion")] Social
}