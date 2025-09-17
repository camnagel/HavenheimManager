using System.ComponentModel;

namespace HavenheimManager.Descriptors;

public enum CreatureSubType
{
    [Description("Aberration")] Human,
    [Description("Animal")] Dwarf,
    [Description("Construct")] Elf,
    [Description("Celestial")] Wildfolk
}