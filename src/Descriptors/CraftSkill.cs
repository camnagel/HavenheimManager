using System.ComponentModel;

namespace HavenheimManager.Descriptors;

internal enum CraftSkill
{
    [Description("Alchemy")] Alchemy,
    [Description("Artistry")] Artistry,
    [Description("Carpentry")] Carpentry,
    [Description("Essence")] Essence,
    [Description("Flesh")] Flesh,
    [Description("Smithing")] Smithing,
    [Description("Stonemasonry")] Stonemasonry,
    [Description("Tinkering")] Tinkering,
    [Description("Weaving")] Weaving,
    [Description("Jewler")] Jewler
}