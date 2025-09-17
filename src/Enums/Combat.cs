using System.ComponentModel;

namespace HavenheimManager.Enums;

public enum Combat
{
    [Description("Movement")] Movement,
    [Description("Initiative")] Initiative,
    [Description("AC")] AC,
    [Description("Saves")] Saves,
    [Description("Spellcasting")] Spellcasting,
    [Description("Attack")] Attack,
    [Description("Damage")] Damage,
    [Description("Range")] Range,
    [Description("Critical")] Critical,
    [Description("Nonlethal")] Nonlethal,
    [Description("Combat Maneuver")] CombatManeuver,
    [Description("Bull Rush")] BullRush,
    [Description("Dirty Trick")] DirtyTrick,
    [Description("Disarm")] Disarm,
    [Description("Drag")] Drag,
    [Description("Grapple")] Grapple,
    [Description("Overrun")] Overrun,
    [Description("Reposition")] Reposition,
    [Description("Steal")] Steal,
    [Description("Sunder")] Sunder,
    [Description("Trip")] Trip,
    [Description("Unarmed")] Unarmed,
    [Description("Weapon")] Weapon,
    [Description("Performance")] Performance,
    [Description("Mounted Combat")] MountedCombat,
    [Description("Two-Weapon")] TwoWeapon,
    [Description("Two-Handed")] TwoHanded,
    [Description("Bow")] Bow,
    [Description("Thrown")] Thrown,

    [Description("Attacks of Opportunity")]
    AttacksOfOpportunity,
    [Description("Feint")] Feint,
    [Description("Simple Weapons")] SimpleWeapons,
    [Description("Martial Weapons")] MartialWeapons,
    [Description("ExoticWeapons")] ExoticWeapons,
    [Description("Bludgeoning")] Bludgeoning,
    [Description("Piercing")] Piercing,
    [Description("Slashing")] Slashing,
    [Description("Creature Type")] Creature
}