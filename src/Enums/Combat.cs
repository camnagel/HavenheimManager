using System.ComponentModel;

namespace AssetManager.Enums
{
    public enum Combat
    {
        [Description("Initiative")]
        Initiative,
        [Description("AC")]
        AC,
        [Description("Attack")]
        Attack,
        [Description("Damage")]
        Damage,
        [Description("Critical")]
        Critical,
        [Description("Nonlethal")]
        Nonlethal,
        [Description("Combat Maneuver")]
        CombatManeuver,
        [Description("Unarmed")]
        Unarmed,
        [Description("Weapon")]
        Weapon,
        [Description("Two Weapon")]
        TwoWeapon,
        [Description("Two Handed")]
        TwoHanded,
        [Description("Bow")]
        Bow,
        [Description("Thrown")]
        Thrown,
        [Description("Attacks 0f Opportunity")]
        AttacksOfOpportunity,
        [Description("Simple Weapons")]
        SimpleWeapons,
        [Description("Martial Weapons")]
        MartialWeapons,
        [Description("ExoticWeapons")]
        ExoticWeapons
    }
}
