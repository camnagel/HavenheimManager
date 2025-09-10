using System.ComponentModel;

namespace HavenheimManager.Enums.Descriptors;

public enum Creature
{
    [Description("Creature")] Creature,
    [Description("Rating")] Rating,
    [Description("Progression")] Progress,
    [Description("Crafting")] Crafting,
    [Description("Skill")] Skill,
    [Description("Class")] Class,
    [Description("Companion")] Companion,
    [Description("Faith")] Faith,
    [Description("Spells")] Spells,
    [Description("Traits")] Traits,
    [Description("Feats")] Feats
}