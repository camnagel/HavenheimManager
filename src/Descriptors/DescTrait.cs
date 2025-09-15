using System.ComponentModel;

namespace HavenheimManager.Descriptors;

public enum DescTrait
{
    [Description("Abjuration")] Combat,
    [Description("Conjuration")] Magic,
    [Description("Divination")] Faction,
    [Description("Enchantment")] Story,
    [Description("Illusion")] Social
}