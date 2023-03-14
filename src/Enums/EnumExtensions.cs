using System;

namespace AssetManager.Enums
{
    public static class EnumExtensions
    {
        public static Skill StringToSkill(this string skillName)
        {
            switch (skillName.ToLower())
            {
                case "acrobatics":
                    return Skill.Acrobatics;
                case "appraise":
                    return Skill.Appraise;
                case "bluff":
                    return Skill.Bluff;
                case "climb":
                    return Skill.Climb;
                case "craft":
                    return Skill.Craft;
                case "diplomacy":
                    return Skill.Diplomacy;
                case "disable device":
                    return Skill.DisableDevice;
                case "disguise":
                    return Skill.Disguise;
                case "escape artist":
                    return Skill.EscapeArtist;
                case "fly":
                    return Skill.Fly;
                case "handle animal":
                    return Skill.HandleAnimal;
                case "heal":
                    return Skill.Heal;
                case "intimidate":
                    return Skill.Intimidate;
                case "knowledge":
                    return Skill.Knowledge;
                case "linguistics":
                    return Skill.Linguistics;
                case "perception":
                    return Skill.Perception;
                case "perform":
                    return Skill.Perform;
                case "profession":
                    return Skill.Profession;
                case "ride":
                    return Skill.Ride;
                case "sense motive":
                    return Skill.SenseMotive;
                case "sleight of hand":
                    return Skill.SleightOfHand;
                case "spellcraft":
                    return Skill.Spellcraft;
                case "stealth":
                    return Skill.Stealth;
                case "survival":
                    return Skill.Survival;
                case "swim":
                    return Skill.Swim;
                case "umd":
                    return Skill.UMD;
                case "alternate":
                    return Skill.Alternate;
                case "all":
                    return Skill.All;
            }

            throw new ArgumentOutOfRangeException(nameof(skillName), "Could not determine requested skill: " + skillName);
        }

        public static Class StringToClass(this string className)
        {
            switch (className.ToLower())
            {
                case "alchemist":
                    return Class.Alchemist;
                case "arcanist":
                    return Class.Arcanist;
                case "aegis":
                    return Class.Aegis;
                case "barbarian":
                    return Class.Barbarian;
                case "bard":
                    return Class.Bard;
                case "bloodrager":
                    return Class.Bloodrager;
                case "brawler":
                    return Class.Brawler;
                case "cavalier":
                    return Class.Cavalier;
                case "cleric":
                    return Class.Cleric;
                case "cryptic":
                    return Class.Cryptic;
                case "dread":
                    return Class.Dread;
                case "druid":
                    return Class.Druid;
                case "fighter":
                    return Class.Fighter;
                case "hunter":
                    return Class.Hunter;
                case "inquisitor":
                    return Class.Inquisitor;
                case "investigator":
                    return Class.Investigator;
                case "kineticist":
                    return Class.Kineticist;
                case "magus":
                    return Class.Magus;
                case "marksman":
                    return Class.Marksman;
                case "medium":
                    return Class.Medium;
                case "mesmerist":
                    return Class.Mesmerist;
                case "monk":
                    return Class.Monk;
                case "ninja":
                    return Class.Ninja;
                case "occultist":
                    return Class.Occultist;
                case "oracle":
                    return Class.Oracle;
                case "paladin":
                    return Class.Paladin;
                case "psion":
                    return Class.Psion;
                case "psychic":
                    return Class.Psychic;
                case "psychic warrior":
                    return Class.PsychicWarrior;
                case "ranger":
                    return Class.Ranger;
                case "rogue":
                    return Class.Rogue;
                case "samurai":
                    return Class.Samurai;
                case "shaman":
                    return Class.Shaman;
                case "shifter":
                    return Class.Shifter;
                case "skald":
                    return Class.Skald;
                case "slayer":
                    return Class.Slayer;
                case "sorcerer":
                    return Class.Sorcerer;
                case "soul knife":
                    return Class.SoulKnife;
                case "spiritualist":
                    return Class.Spiritualist;
                case "summoner":
                    return Class.Summoner;
                case "swashbuckler":
                    return Class.Swashbuckler;
                case "tactician":
                    return Class.Tactician;
                case "warpriest":
                    return Class.Warpriest;
                case "wilder":
                    return Class.Wilder;
                case "witch":
                    return Class.Witch;
                case "wizard":
                    return Class.Wizard;
            }

            throw new ArgumentOutOfRangeException(nameof(className), "Could not determine requested class: " + className);
        }

        public static Source StringToSource(this string sourceName)
        {
            switch (sourceName.ToLower())
            {
                case "standard":
                    return Source.Standard;
                case "rework":
                    return Source.Rework;
                case "homebrew":
                    return Source.Homebrew;
            }

            throw new ArgumentOutOfRangeException(nameof(sourceName), "Could not determine requested source: " + sourceName);
        }

        public static School StringToSchool(this string schoolName)
        {
            switch (schoolName.ToLower())
            {
                case "abjuration":
                    return School.Abjuration;
                case "conjuration":
                    return School.Conjuration;
                case "divination":
                    return School.Divination;
                case "enchantment":
                    return School.Enchantment;
                case "evocation":
                    return School.Evocation;
                case "illusion":
                    return School.Illusion;
                case "necromancy":
                    return School.Necromancy;
                case "transmutation":
                    return School.Transmutation;
                case "universal":
                    return School.Universal;
                case "offensive":
                    return School.Offensive;
                case "defensive":
                    return School.Defensive;
            }

            throw new ArgumentOutOfRangeException(nameof(schoolName), "Could not determine requested school: " + schoolName);
        }

        public static Role StringToRole(this string roleName)
        {
            switch (roleName.ToLower())
            {
                case "melee":
                    return Role.Melee;
                case "ranged":
                    return Role.Ranged;
                case "switch attacker":
                    return Role.SwitchAttacker;
                case "tank":
                    return Role.Tank;
                case "leader":
                    return Role.Leader;
                case "blaster":
                    return Role.Blaster;
                case "debuffer":
                    return Role.Debuffer;
                case "buffer":
                    return Role.Buffer;
                case "healer":
                    return Role.Healer;
                case "battlefield control":
                    return Role.BattlefieldControl;
                case "shapeshifter":
                    return Role.Shapeshifter;
                case "minions":
                    return Role.Minions;
                case "skill monkey":
                    return Role.SkillMonkey;
                case "social":
                    return Role.Social;
                case "information":
                    return Role.Information;
                case "crafter":
                    return Role.Crafter;
            }

            throw new ArgumentOutOfRangeException(nameof(roleName), "Could not determine requested role: " + roleName);
        }

        public static Core StringToCore(this string coreName)
        {
            switch (coreName.ToLower())
            {
                case "strength":
                    return Core.Strength;
                case "dexterity":
                    return Core.Dexterity;
                case "constitution":
                    return Core.Constitution;
                case "intelligence":
                    return Core.Intelligence;
                case "wisdom":
                    return Core.Wisdom;
                case "charisma":
                    return Core.Charisma;
                case "hit points":
                    return Core.HitPoints;
                case "wealth":
                    return Core.Wealth;
            }

            throw new ArgumentOutOfRangeException(nameof(coreName), "Could not determine requested core: " + coreName);
        }

        public static Combat StringToCombat(this string combatName)
        {
            switch (combatName.ToLower())
            {
                case "initiative":
                    return Combat.Initiative;
                case "ac":
                    return Combat.AC;
                case "attack":
                    return Combat.Attack;
                case "damage":
                    return Combat.Damage;
                case "critical":
                    return Combat.Critical;
                case "nonlethal":
                    return Combat.Nonlethal;
                case "combat manuver":
                    return Combat.CombatManeuver;
                case "unarmed":
                    return Combat.Unarmed;
                case "weapon":
                    return Combat.Weapon;
                case "two weapon":
                    return Combat.TwoWeapon;
                case "two handed":
                    return Combat.TwoHanded;
                case "bow":
                    return Combat.Bow;
                case "thrown":
                    return Combat.Thrown;
                case "attacks of opportunity":
                    return Combat.AttacksOfOpportunity;
                case "simple weapons":
                    return Combat.SimpleWeapons;
                case "martial weapons":
                    return Combat.MartialWeapons;
                case "exotic weapons":
                    return Combat.ExoticWeapons;
            }

            throw new ArgumentOutOfRangeException(nameof(combatName), "Could not determine requested combat: " + combatName);
        }
    }
}
