using System.ComponentModel;

namespace HavenheimManager.Descriptors;

public enum DescSystem
{
    [Description("Environment")] Environment,
    [Description("Creature")] Creature,
    [Description("Item")] Item,
    [Description("Crafting")] Crafting,
    [Description("Skill")] Skill,
    [Description("Class")] Class,
    [Description("Companion")] Companion,
    [Description("Faith")] Faith,
    [Description("Spells")] Spells,
    [Description("Traits")] Traits,
    [Description("Feats")] Feats
}