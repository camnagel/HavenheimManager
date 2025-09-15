using System.ComponentModel;

namespace HavenheimManager.Descriptors;

public enum DescMagic
{
    [Description("Abjuration")] Spell,
    [Description("Conjuration")] SpellLike,
    [Description("Divination")] Ritual,
    [Description("Enchantment")] Arcane,
    [Description("Evocation")] Divine,
    [Description("Illusion")] Occult,
    [Description("Necromancy")] Primal,
    [Description("Spell Resistance (Yes)")] SpellResist,
    [Description("Spell Resistance (No)")] SpellAccept
}