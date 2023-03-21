using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using AssetManager.Import;

namespace AssetManager.Enums
{
    public static class EnumExtensions
    {
        private static readonly Regex _whitespaceFilter = new Regex(@"[\s-',()]+");

        public static Skill StringToSkill(this string skillName)
        {
            switch (_whitespaceFilter.Replace(skillName.ToLower(), ""))
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
                case "disabledevice":
                    return Skill.DisableDevice;
                case "disguise":
                    return Skill.Disguise;
                case "escapeartist":
                    return Skill.EscapeArtist;
                case "fly":
                    return Skill.Fly;
                case "handleanimal":
                    return Skill.HandleAnimal;
                case "heal":
                    return Skill.Heal;
                case "intimidate":
                    return Skill.Intimidate;
                case "knowledgeall":
                    return Skill.KnowledgeAll;
                case "knowledgearcane":
                    return Skill.KnowledgeArcane;
                case "knowledgedungeoneering":
                    return Skill.KnowledgeDungeoneering;
                case "knowledgeengineering":
                    return Skill.KnowledgeEngineering;
                case "knowledgegeography":
                    return Skill.KnowledgeGeography;
                case "knowledgehistory":
                    return Skill.KnowledgeHistory;
                case "knowledgelocal":
                    return Skill.KnowledgeLocal;
                case "knowledgenature":
                    return Skill.KnowledgeNature;
                case "knowledgenobility":
                    return Skill.KnowledgeNobility;
                case "knowledgeplanes":
                    return Skill.KnowledgePlanes;
                case "knowledgepsionic":
                    return Skill.KnowledgePsionic;
                case "knowledgereligion":
                    return Skill.KnowledgeReligion;
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
                case "sensemotive":
                    return Skill.SenseMotive;
                case "sleightofhand":
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
                case "classskill":
                    return Skill.Class;
            }

            throw new ArgumentOutOfRangeException(nameof(skillName), "Could not determine requested skill: " + skillName);
        }

        public static Class StringToClass(this string className)
        {
            switch (_whitespaceFilter.Replace(className.ToLower(), ""))
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
                case "psychicwarrior":
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
                case "soulknife":
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
            switch (_whitespaceFilter.Replace(sourceName.ToLower(), ""))
            {
                case "standard":
                case "":
                    return Source.Standard;
                case "rework":
                    return Source.Rework;
                case "homebrew":
                    return Source.Homebrew;
                case "unknown":
                    return Source.Unknown;
            }

            throw new ArgumentOutOfRangeException(nameof(sourceName), "Could not determine requested source: " + sourceName);
        }

        public static Bonus StringToBonus(this string bonusName)
        {
            switch (_whitespaceFilter.Replace(bonusName.ToLower(), ""))
            {
                case "alchemical":
                    return Bonus.Alchemical;
                case "armor":
                    return Bonus.Armor;
                case "bab":
                    return Bonus.BAB;
                case "circumstance":
                    return Bonus.Circumstance;
                case "competence":
                    return Bonus.Competence;
                case "deflection":
                    return Bonus.Deflection;
                case "dodge":
                    return Bonus.Dodge;
                case "enhancement":
                    return Bonus.Enhancement;
                case "inherent":
                    return Bonus.Inherent;
                case "insight":
                    return Bonus.Insight;
                case "luck":
                    return Bonus.Luck;
                case "morale":
                    return Bonus.Morale;
                case "naturalarmor":
                    return Bonus.NaturalArmor;
                case "profane":
                    return Bonus.Profane;
                case "racial":
                    return Bonus.Racial;
                case "resistance":
                    return Bonus.Resistance;
                case "sacred":
                    return Bonus.Sacred;
                case "shield":
                    return Bonus.Shield;
                case "size":
                    return Bonus.Size;
                case "trait":
                    return Bonus.Trait;
                case "untyped":
                    return Bonus.Untyped;
                case "none":
                case "":
                    return Bonus.None;
            }

            throw new ArgumentOutOfRangeException(nameof(bonusName), "Could not determine requested bonus: " + bonusName);
        }

        public static Magic StringToMagic(this string magicName)
        {
            switch (_whitespaceFilter.Replace(magicName.ToLower(), ""))
            {
                case "abjuration":
                    return Magic.Abjuration;
                case "conjuration":
                    return Magic.Conjuration;
                case "divination":
                    return Magic.Divination;
                case "enchantment":
                    return Magic.Enchantment;
                case "evocation":
                    return Magic.Evocation;
                case "illusion":
                    return Magic.Illusion;
                case "necromancy":
                    return Magic.Necromancy;
                case "transmutation":
                    return Magic.Transmutation;
                case "universal":
                    return Magic.Universal;
                case "offensive":
                    return Magic.Offensive;
                case "defensive":
                    return Magic.Defensive;
                case "casterlevel":
                    return Magic.CasterLevel;
                case "concentration":
                    return Magic.Concentration;
            }

            throw new ArgumentOutOfRangeException(nameof(magicName), "Could not determine requested magic: " + magicName);
        }

        public static Role StringToRole(this string roleName)
        {
            switch (_whitespaceFilter.Replace(roleName.ToLower(), ""))
            {
                case "melee":
                    return Role.Melee;
                case "ranged":
                    return Role.Ranged;
                case "switchattacker":
                    return Role.SwitchAttacker;
                case "areaattacker":
                    return Role.AreaAttacker;
                case "tank":
                    return Role.Tank;
                case "guard":
                    return Role.Guard;
                case "scout":
                    return Role.Scout;
                case "ambusher":
                    return Role.Ambusher;
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
                case "battlefieldcontrol":
                    return Role.BattlefieldControl;
                case "shapeshifter":
                    return Role.Shapeshifter;
                case "minions":
                    return Role.Minions;
                case "skillmonkey":
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
            switch (_whitespaceFilter.Replace(coreName.ToLower(), ""))
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
                case "hitpoints":
                    return Core.HitPoints;
                case "wealth":
                    return Core.Wealth;
                case "social":
                    return Core.Social;
                case "sustenance":
                    return Core.Sustenance;
            }

            throw new ArgumentOutOfRangeException(nameof(coreName), "Could not determine requested core: " + coreName);
        }

        public static Combat StringToCombat(this string combatName)
        {
            switch (_whitespaceFilter.Replace(combatName.ToLower(), ""))
            {
                case "initiative":
                    return Combat.Initiative;
                case "ac":
                    return Combat.AC;
                case "saves":
                    return Combat.Saves;
                case "spellcasting":
                    return Combat.Spellcasting;
                case "attack":
                    return Combat.Attack;
                case "damage":
                    return Combat.Damage;
                case "range":
                    return Combat.Range;
                case "critical":
                    return Combat.Critical;
                case "nonlethal":
                    return Combat.Nonlethal;
                case "combatmaneuver":
                    return Combat.CombatManeuver;
                case "bullrush":
                    return Combat.BullRush;
                case "dirtytrick":
                    return Combat.DirtyTrick;
                case "disarm":
                    return Combat.Disarm;
                case "drag":
                    return Combat.Drag;
                case "grapple":
                    return Combat.Grapple;
                case "overrun":
                    return Combat.Overrun;
                case "reposition":
                    return Combat.Reposition;
                case "steal":
                    return Combat.Steal;
                case "sunder":
                    return Combat.Sunder;
                case "trip":
                    return Combat.Trip;
                case "unarmed":
                    return Combat.Unarmed;
                case "weapon":
                    return Combat.Weapon;
                case "twoweapon":
                    return Combat.TwoWeapon;
                case "twohanded":
                    return Combat.TwoHanded;
                case "bow":
                    return Combat.Bow;
                case "thrown":
                    return Combat.Thrown;
                case "attacksofopportunity":
                    return Combat.AttacksOfOpportunity;
                case "feint":
                    return Combat.Feint;
                case "simpleweapons":
                    return Combat.SimpleWeapons;
                case "martialweapons":
                    return Combat.MartialWeapons;
                case "exoticweapons":
                    return Combat.ExoticWeapons;
                case "bludgeoning":
                    return Combat.Bludgeoning;
                case "piercing":
                    return Combat.Piercing;
                case "slashing":
                    return Combat.Slashing;
                case "creaturetype":
                    return Combat.Creature;
            }

            throw new ArgumentOutOfRangeException(nameof(combatName), "Could not determine requested combat: " + combatName);
        }

        public static SourceType StringToSourceType(this string sourceTypeName)
        {
            switch (_whitespaceFilter.Replace(sourceTypeName.ToLower(), ""))
            {
                case "feat":
                    return SourceType.Feat;
                case "item":
                    return SourceType.Item;
                case "trait":
                    return SourceType.Trait;
                case "spell":
                    return SourceType.Spell;
            }

            throw new ArgumentOutOfRangeException(nameof(sourceTypeName), "Could not determine requested source type: " + sourceTypeName);
        }

        public static string GetEnumDescription(this Core enumValue)
        {
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            var descriptionAttributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return descriptionAttributes.Length > 0 ? descriptionAttributes[0].Description : enumValue.ToString();
        }

        public static string GetEnumDescription(this Skill enumValue)
        {
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            var descriptionAttributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return descriptionAttributes.Length > 0 ? descriptionAttributes[0].Description : enumValue.ToString();
        }

        public static string GetEnumDescription(this Combat enumValue)
        {
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            var descriptionAttributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return descriptionAttributes.Length > 0 ? descriptionAttributes[0].Description : enumValue.ToString();
        }

        public static string GetEnumDescription(this Class enumValue)
        {
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            var descriptionAttributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return descriptionAttributes.Length > 0 ? descriptionAttributes[0].Description : enumValue.ToString();
        }

        public static string GetEnumDescription(this Magic enumValue)
        {
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            var descriptionAttributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return descriptionAttributes.Length > 0 ? descriptionAttributes[0].Description : enumValue.ToString();
        }

        public static string GetEnumDescription(this Role enumValue)
        {
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            var descriptionAttributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return descriptionAttributes.Length > 0 ? descriptionAttributes[0].Description : enumValue.ToString();
        }

        public static string GetEnumDescription(this Source enumValue)
        {
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            var descriptionAttributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return descriptionAttributes.Length > 0 ? descriptionAttributes[0].Description : enumValue.ToString();
        }

        public static string GetEnumDescription(this Bonus enumValue)
        {
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            var descriptionAttributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return descriptionAttributes.Length > 0 ? descriptionAttributes[0].Description : enumValue.ToString();
        }
    }
}
