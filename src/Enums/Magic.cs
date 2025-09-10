using System.ComponentModel;

namespace HavenheimManager.Enums;

public enum Magic
{
    [Description("Abjuration")] Abjuration,
    [Description("Conjuration")] Conjuration,
    [Description("Divination")] Divination,
    [Description("Enchantment")] Enchantment,
    [Description("Evocation")] Evocation,
    [Description("Illusion")] Illusion,
    [Description("Necromancy")] Necromancy,
    [Description("Transmutation")] Transmutation,
    [Description("Universal")] Universal,
    [Description("Offensive")] Offensive,
    [Description("Defensive")] Defensive,
    [Description("Caster Level")] CasterLevel,
    [Description("Concentration")] Concentration,
    [Description("Metamagic")] Metamagic,
    [Description("Saves")] Saves
}