using System.ComponentModel;

namespace HavenheimManager.Enums;

public enum Bonus
{
    [Description("Alchemical")] Alchemical,
    [Description("Armor")] Armor,
    [Description("BAB")] Bab,
    [Description("Circumstance")] Circumstance,
    [Description("Competence")] Competence,
    [Description("Deflection")] Deflection,
    [Description("Dodge")] Dodge,
    [Description("Enhancement")] Enhancement,
    [Description("Inherent")] Inherent,
    [Description("Insight")] Insight,
    [Description("Luck")] Luck,
    [Description("Morale")] Morale,
    [Description("Natural Armor")] NaturalArmor,
    [Description("Profane")] Profane,
    [Description("Racial")] Racial,
    [Description("Resistance")] Resistance,
    [Description("Sacred")] Sacred,
    [Description("Shield")] Shield,
    [Description("Size")] Size,
    [Description("Trait")] Trait,
    [Description("Untyped")] Untyped,
    [Description("None")] None
}