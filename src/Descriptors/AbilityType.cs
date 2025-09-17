using System.ComponentModel;

namespace HavenheimManager.Descriptors;

public enum AbilityType
{
    [Description("Ordinary (Or)")] Ordinary,
    [Description("Extraordinary (Ex)")] Extraordinary,
    [Description("Supernatural (Su)")] Supernatural,
    [Description("Magical Ability (Ma)")] MagicalAbility
}