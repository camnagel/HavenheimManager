using System.ComponentModel;

namespace HavenheimManager.Descriptors;

public enum DescAbility
{
    [Description("Strength")] Strength,
    [Description("Dexterity")] Dexterity,
    [Description("Constitution")] Constitution,
    [Description("Intelligence")] Intelligence,
    [Description("Wisdom")] Wisdom,
    [Description("Charisma")] Charisma,
    [Description("Replacement")] Replacement
}