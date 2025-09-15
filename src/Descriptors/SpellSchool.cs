using System.ComponentModel;

namespace HavenheimManager.Descriptors;

public enum SpellSchool
{
    [Description("Abjuration")] Abjuration,
    [Description("Conjuration")] Conjuration,
    [Description("Divination")] Divination,
    [Description("Enchantment")] Enchantment,
    [Description("Evocation")] Evocation,
    [Description("Illusion")] Illusion,
    [Description("Necromancy")] Necromancy,
    [Description("Transmutation")] Transmutation,
    [Description("Universal")] Universal
}