using System;
using System.Collections.Generic;
using HavenheimManager.Enums;

namespace HavenheimManager.Descriptors;

internal static class DescriptorUtils
{
    public static List<DescContent> GetContent(AppMode mode)
    {
        switch (mode)
        {
            case AppMode.Havenheim:
                return new List<DescContent>
                {
                    DescContent.Direct,
                    DescContent.Homebrew,
                    DescContent.Modified,
                    DescContent.Reworded
                };

            case AppMode.Pathfinder:
                return new List<DescContent>();

            default:
                throw new ArgumentOutOfRangeException(nameof(mode), "How did you get here?");
        }
    }

    public static List<DescSystem> GetSystem(AppMode mode)
    {
        switch (mode)
        {
            case AppMode.Havenheim:
                return new List<DescSystem>
                {
                    DescSystem.Environment,
                    DescSystem.Creature,
                    DescSystem.Class,
                    DescSystem.Companion,
                    DescSystem.Item,
                    DescSystem.Crafting,
                    DescSystem.Skill,
                    DescSystem.Faith,
                    DescSystem.Spells,
                    DescSystem.Traits,
                    DescSystem.Feats
                };

            case AppMode.Pathfinder:
                return new List<DescSystem>();

            default:
                throw new ArgumentOutOfRangeException(nameof(mode), "How did you get here?");
        }
    }

    public static List<CreatureType> GetCreatureType(AppMode mode)
    {
        switch (mode)
        {
            case AppMode.Havenheim:
                return new List<CreatureType>
                {
                    CreatureType.Aberration,
                    CreatureType.Animal,
                    CreatureType.Celestial,
                    CreatureType.Construct,
                    CreatureType.Dragons,
                    CreatureType.Devil,
                    CreatureType.Elemental,
                    CreatureType.Fiend,
                    CreatureType.Fey,
                    CreatureType.Humanoid,
                    CreatureType.MagicalBeast,
                    CreatureType.Ooze,
                    CreatureType.Plant,
                    CreatureType.Spirit,
                    CreatureType.Undead,
                    CreatureType.Vermin
                };

            case AppMode.Pathfinder:
                return new List<CreatureType>();

            default:
                throw new ArgumentOutOfRangeException(nameof(mode), "How did you get here?");
        }
    }

    public static List<Creature> GetCreature(AppMode mode)
    {
        switch (mode)
        {
            case AppMode.Havenheim:
                return new List<Creature>
                {
                    Creature.Creature,
                    Creature.Progress,
                    Creature.Rating
                };

            case AppMode.Pathfinder:
                return new List<Creature>();

            default:
                throw new ArgumentOutOfRangeException(nameof(mode), "How did you get here?");
        }
    }

    public static List<AbilityScore> GetAbilityScore(AppMode mode)
    {
        switch (mode)
        {
            case AppMode.Havenheim:
                return new List<AbilityScore>
                {
                    AbilityScore.Strength,
                    AbilityScore.Dexterity,
                    AbilityScore.Constitution,
                    AbilityScore.Intelligence,
                    AbilityScore.Wisdom,
                    AbilityScore.Charisma,
                    AbilityScore.Replacement
                };

            case AppMode.Pathfinder:
                return new List<AbilityScore>();

            default:
                throw new ArgumentOutOfRangeException(nameof(mode), "How did you get here?");
        }
    }

    public static List<CreatureSubType> GetCreatureSubType(AppMode mode)
    {
        switch (mode)
        {
            case AppMode.Havenheim:
                return new List<CreatureSubType>
                {
                    CreatureSubType.Human,
                    CreatureSubType.Elf,
                    CreatureSubType.Dwarf,
                    CreatureSubType.Wildfolk
                };

            case AppMode.Pathfinder:
                return new List<CreatureSubType>();

            default:
                throw new ArgumentOutOfRangeException(nameof(mode), "How did you get here?");
        }
    }

    public static List<AbilityType> GetAbilityType(AppMode mode)
    {
        switch (mode)
        {
            case AppMode.Havenheim:
                return new List<AbilityType>
                {
                    AbilityType.Ordinary,
                    AbilityType.Extraordinary,
                    AbilityType.Supernatural,
                    AbilityType.Supernatural
                };

            case AppMode.Pathfinder:
                return new List<AbilityType>();

            default:
                throw new ArgumentOutOfRangeException(nameof(mode), "How did you get here?");
        }
    }

    public static List<DescUsage> GetUsage(AppMode mode)
    {
        switch (mode)
        {
            case AppMode.Havenheim:
                return new List<DescUsage>
                {
                    DescUsage.One,
                    DescUsage.Three,
                    DescUsage.Charge,
                    DescUsage.Unlimited
                };

            case AppMode.Pathfinder:
                return new List<DescUsage>();

            default:
                throw new ArgumentOutOfRangeException(nameof(mode), "How did you get here?");
        }
    }

    public static List<Duration> GetDuration(AppMode mode)
    {
        switch (mode)
        {
            case AppMode.Havenheim:
                return new List<Duration>
                {
                    Duration.Any,
                    Duration.Instant,
                    Duration.EndTurn,
                    Duration.Round,
                    Duration.RoundPer,
                    Duration.ThreeRoundPer,
                    Duration.MinutePer,
                    Duration.TenMinutePer,
                    Duration.HourPer,
                    Duration.DayPer,
                    Duration.TenDayPer,
                    Duration.MonthPer,
                    Duration.YearPer
                };

            case AppMode.Pathfinder:
                return new List<Duration>();

            default:
                throw new ArgumentOutOfRangeException(nameof(mode), "How did you get here?");
        }
    }

    public static List<DescBuff> GetBuff(AppMode mode)
    {
        switch (mode)
        {
            case AppMode.Havenheim:
                return new List<DescBuff>
                {
                    DescBuff.Buff,
                    DescBuff.Debuff,
                    DescBuff.SelfBuff,
                    DescBuff.SelfDebuff,
                    DescBuff.AllyBuff,
                    DescBuff.Bonus,
                    DescBuff.Penalty
                };

            case AppMode.Pathfinder:
                return new List<DescBuff>();

            default:
                throw new ArgumentOutOfRangeException(nameof(mode), "How did you get here?");
        }
    }

    public static List<DescSave> GetSave(AppMode mode)
    {
        switch (mode)
        {
            case AppMode.Havenheim:
                return new List<DescSave>
                {
                    DescSave.Reflex,
                    DescSave.Will,
                    DescSave.Fortitude,
                    DescSave.ReflexPartial,
                    DescSave.WillPartial,
                    DescSave.FortitudePartial,
                    DescSave.Multiple
                };

            case AppMode.Pathfinder:
                return new List<DescSave>();

            default:
                throw new ArgumentOutOfRangeException(nameof(mode), "How did you get here?");
        }
    }

    public static List<MagicAura> GetMagicAura(AppMode mode)
    {
        switch (mode)
        {
            case AppMode.Havenheim:
                return new List<MagicAura>
                {
                    MagicAura.Faint,
                    MagicAura.Moderate,
                    MagicAura.Strong,
                    MagicAura.Overwhelming
                };

            case AppMode.Pathfinder:
                return new List<MagicAura>();

            default:
                throw new ArgumentOutOfRangeException(nameof(mode), "How did you get here?");
        }
    }

    public static List<SpellSchool> GetSpellSchool(AppMode mode)
    {
        switch (mode)
        {
            case AppMode.Havenheim:
                return new List<SpellSchool>
                {
                    SpellSchool.Abjuration,
                    SpellSchool.Conjuration,
                    SpellSchool.Divination,
                    SpellSchool.Evocation,
                    SpellSchool.Enchantment,
                    SpellSchool.Illusion,
                    SpellSchool.Necromancy,
                    SpellSchool.Transmutation,
                    SpellSchool.Universal
                };

            case AppMode.Pathfinder:
                return new List<SpellSchool>();

            default:
                throw new ArgumentOutOfRangeException(nameof(mode), "How did you get here?");
        }
    }

    public static List<DescMagic> GetDescMagic(AppMode mode)
    {
        switch (mode)
        {
            case AppMode.Havenheim:
                return new List<DescMagic>
                {
                    DescMagic.Spell,
                    DescMagic.SpellLike,
                    DescMagic.Ritual,
                    DescMagic.Arcane,
                    DescMagic.Divine,
                    DescMagic.Occult,
                    DescMagic.Primal,
                    DescMagic.SpellResist,
                    DescMagic.SpellAccept
                };

            case AppMode.Pathfinder:
                return new List<DescMagic>();

            default:
                throw new ArgumentOutOfRangeException(nameof(mode), "How did you get here?");
        }
    }

    public static List<DescFeat> GetDescFeat(AppMode mode)
    {
        switch (mode)
        {
            case AppMode.Havenheim:
                return new List<DescFeat>
                {
                    DescFeat.Combat,
                    DescFeat.Magic,
                    DescFeat.Utility,
                    DescFeat.Armor,
                    DescFeat.Weapon,
                    DescFeat.Maneuver,
                    DescFeat.Critical,
                    DescFeat.Spell,
                    DescFeat.Metamagic,
                    DescFeat.Crafting,
                    DescFeat.Skill,
                    DescFeat.Mythic,
                    DescFeat.Story,
                    DescFeat.Style,
                    DescFeat.Social
                };

            case AppMode.Pathfinder:
                return new List<DescFeat>();

            default:
                throw new ArgumentOutOfRangeException(nameof(mode), "How did you get here?");
        }
    }

    public static List<DescTrait> GetDescTrait(AppMode mode)
    {
        switch (mode)
        {
            case AppMode.Havenheim:
                return new List<DescTrait>
                {
                    DescTrait.Combat,
                    DescTrait.Magic,
                    DescTrait.Faction,
                    DescTrait.Story,
                    DescTrait.Social
                };

            case AppMode.Pathfinder:
                return new List<DescTrait>();

            default:
                throw new ArgumentOutOfRangeException(nameof(mode), "How did you get here?");
        }
    }

    public static List<DescSkill> GetDescSkill(AppMode mode)
    {
        switch (mode)
        {
            case AppMode.Havenheim:
                return new List<DescSkill>
                {
                    DescSkill.Acrobatics,
                    DescSkill.Bluff,
                    DescSkill.Climb,
                    DescSkill.Craft,
                    DescSkill.Diplomacy,
                    DescSkill.DisableDevice,
                    DescSkill.Disguise,
                    DescSkill.Fly,
                    DescSkill.HandleCreature,
                    DescSkill.Heal,
                    DescSkill.Intimidate,
                    DescSkill.Knowledge,
                    DescSkill.Linguistics,
                    DescSkill.Perception,
                    DescSkill.Perform,
                    DescSkill.Profession,
                    DescSkill.Ride,
                    DescSkill.SenseMotive,
                    DescSkill.SleightOfHand,
                    DescSkill.Stealth,
                    DescSkill.Swim,
                    DescSkill.Umd,
                    DescSkill.Signature,
                    DescSkill.All
                };

            case AppMode.Pathfinder:
                return new List<DescSkill>();

            default:
                throw new ArgumentOutOfRangeException(nameof(mode), "How did you get here?");
        }
    }

    public static List<CraftSkill> GetCraftSkill(AppMode mode)
    {
        switch (mode)
        {
            case AppMode.Havenheim:
                return new List<CraftSkill>
                {
                    CraftSkill.Alchemy,
                    CraftSkill.Artistry,
                    CraftSkill.Carpentry,
                    CraftSkill.Essence,
                    CraftSkill.Flesh,
                    CraftSkill.Smithing,
                    CraftSkill.Stonemasonry,
                    CraftSkill.Tinkering,
                    CraftSkill.Weaving,
                    CraftSkill.Jewler
                };

            case AppMode.Pathfinder:
                return new List<CraftSkill>();

            default:
                throw new ArgumentOutOfRangeException(nameof(mode), "How did you get here?");
        }
    }

    public static List<Knowledge> GetKnowledge(AppMode mode)
    {
        switch (mode)
        {
            case AppMode.Havenheim:
                return new List<Knowledge>
                {
                    Knowledge.Arcana,
                    Knowledge.Dungeoneering,
                    Knowledge.Engineering,
                    Knowledge.Geography,
                    Knowledge.History,
                    Knowledge.Local,
                    Knowledge.Nature,
                    Knowledge.Occult,
                    Knowledge.Planes,
                    Knowledge.Religion
                };

            case AppMode.Pathfinder:
                return new List<Knowledge>();

            default:
                throw new ArgumentOutOfRangeException(nameof(mode), "How did you get here?");
        }
    }

    public static List<PerformSkill> GetPerformSkill(AppMode mode)
    {
        switch (mode)
        {
            case AppMode.Havenheim:
                return new List<PerformSkill>
                {
                    PerformSkill.Act,
                    PerformSkill.Choreography,
                    PerformSkill.Manipulate,
                    PerformSkill.Oratory,
                    PerformSkill.Percussion,
                    PerformSkill.Wind
                };

            case AppMode.Pathfinder:
                return new List<PerformSkill>();

            default:
                throw new ArgumentOutOfRangeException(nameof(mode), "How did you get here?");
        }
    }

    public static List<Terrain> GetTerrain(AppMode mode)
    {
        switch (mode)
        {
            case AppMode.Havenheim:
                return new List<Terrain>
                {
                    Terrain.Any,
                    Terrain.Urban,
                    Terrain.Plains,
                    Terrain.Forest,
                    Terrain.Swamp,
                    Terrain.Coastal,
                    Terrain.River,
                    Terrain.Ocean
                };

            case AppMode.Pathfinder:
                return new List<Terrain>();

            default:
                throw new ArgumentOutOfRangeException(nameof(mode), "How did you get here?");
        }
    }

    public static List<Stimulus> GetStimulus(AppMode mode)
    {
        switch (mode)
        {
            case AppMode.Havenheim:
                return new List<Stimulus>
                {
                    Stimulus.Visual,
                    Stimulus.Auditory,
                    Stimulus.Chemical,
                    Stimulus.Tactile,
                    Stimulus.Thermal
                };

            case AppMode.Pathfinder:
                return new List<Stimulus>();

            default:
                throw new ArgumentOutOfRangeException(nameof(mode), "How did you get here?");
        }
    }
}