using System.ComponentModel;

namespace AssetManager.Enums
{
    public enum Role
    {
        [Description("Melee")]
        Melee,
        [Description("Ranged")]
        Ranged,
        [Description("Switch Attacker")]
        SwitchAttacker,
        [Description("Tank")]
        Tank,
        [Description("Leader")]
        Leader,
        [Description("Blaster")]
        Blaster,
        [Description("Debuffer")]
        Debuffer,
        [Description("Buffer")]
        Buffer,
        [Description("Healer")]
        Healer,
        [Description("Battlefield Control")]
        BattlefieldControl,
        [Description("Shapeshifter")]
        Shapeshifter,
        [Description("Minions")]
        Minions,
        [Description("Skill Monkey")]
        SkillMonkey,
        [Description("Social")]
        Social,
        [Description("Information")]
        Information,
        [Description("Crafter")]
        Crafter
    }
}
