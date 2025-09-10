using System.ComponentModel;

namespace HavenheimManager.Enums;

public enum Role
{
    [Description("Melee")] Melee,
    [Description("Ranged")] Ranged,
    [Description("Switch Attacker")] SwitchAttacker,
    [Description("Area Attacker")] AreaAttacker,
    [Description("Tank")] Tank,
    [Description("Guard")] Guard,
    [Description("Scout")] Scout,
    [Description("Ambusher")] Ambusher,
    [Description("Leader")] Leader,
    [Description("Blaster")] Blaster,
    [Description("Debuffer")] Debuffer,
    [Description("Buffer")] Buffer,
    [Description("Healer")] Healer,
    [Description("Battlefield Control")] BattlefieldControl,
    [Description("Shapeshifter")] Shapeshifter,
    [Description("Minions")] Minions,
    [Description("Skill Monkey")] SkillMonkey,
    [Description("Social")] Social,
    [Description("Information")] Information,
    [Description("Crafter")] Crafter,
    [Description("Mounted")] Mounted
}