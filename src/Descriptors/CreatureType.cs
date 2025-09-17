using System.ComponentModel;

namespace HavenheimManager.Descriptors;

public enum CreatureType
{
    [Description("Aberration")] Aberration,
    [Description("Animal")] Animal,
    [Description("Construct")] Construct,
    [Description("Celestial")] Celestial,
    [Description("Dragons")] Dragons,
    [Description("Devil")] Devil,
    [Description("Elemental")] Elemental,
    [Description("Fiend")] Fiend,
    [Description("Fey")] Fey,
    [Description("Humanoid")] Humanoid,
    [Description("Magical Beast")] MagicalBeast,
    [Description("Ooze")] Ooze,
    [Description("Plant")] Plant,
    [Description("Spirit")] Spirit,
    [Description("Undead")] Undead,
    [Description("Vermin")] Vermin
}