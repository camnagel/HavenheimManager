using System.Collections.Generic;
using HavenheimManager.Descriptors;
using HavenheimManager.Enums;

namespace HavenheimManager.Containers;

public class Item : ISummary, ITaggable
{
    public HashSet<string> Postreqs { get; set; } = new();

    public HashSet<string> Antireqs { get; set; } = new();
    public string Name { get; set; }

    public string Description { get; set; }

    public HashSet<string> Prereqs { get; set; } = new();

    public string Url { get; set; }

    public string Notes { get; set; }

    public Source Source { get; set; } = Source.Unknown;

    public IList<string> CustomTags { get; set; } = new List<string>();

    public HashSet<Core> CoreTags { get; set; } = new();

    public HashSet<Skill> SkillTags { get; set; } = new();

    public HashSet<Class> ClassTags { get; set; } = new();

    public HashSet<Combat> CombatTags { get; set; } = new();

    public HashSet<Role> RoleTags { get; set; } = new();

    public HashSet<Magic> MagicTags { get; set; } = new();

    public HashSet<Bonus> BonusTags { get; set; } = new();

    public HashSet<Condition> ConditionTags { get; set; } = new();
    public HashSet<DescContent> ContentDescriptors { get; set; }
    public HashSet<Creature> CreatureDescriptors { get; set; }
    public HashSet<CreatureType> CreatureTypeDescriptors { get; set; }
    public HashSet<CreatureSubType> CreatureSubTypeDescriptors { get; set; }
    public HashSet<CraftSkill> CraftSkillDescriptors { get; set; }
    public HashSet<DescAbility> AbilityDescriptors { get; set; }
    public HashSet<AbilityType> AbilityTypeDescriptors { get; set; }
    public HashSet<DescBuff> BuffDescriptors { get; set; }
    public HashSet<DescFeat> FeatDescriptors { get; set; }
    public HashSet<DescMagic> MagicDescriptors { get; set; }
    public HashSet<DescSave> SaveDescriptors { get; set; }
    public HashSet<DescSkill> SkillDescriptors { get; set; }
    public HashSet<DescSystem> SystemDescriptors { get; set; }
    public HashSet<DescTrait> TraitDescriptors { get; set; }
    public HashSet<DescUsage> UsageDescriptors { get; set; }
    public HashSet<DescDuration> DurationDescriptors { get; set; }
    public HashSet<Knowledge> KnowledgeDescriptors { get; set; }
    public HashSet<MagicAura> MagicAuraDescriptors { get; set; }
    public HashSet<PerformSkill> PerformSkillDescriptors { get; set; }
    public HashSet<SpellSchool> SpellSchoolDescriptors { get; set; }
    public HashSet<Stimulus> StimulusDescriptors { get; set; }
    public HashSet<Terrain> TerrainDescriptors { get; set; }
    public IList<string> CustomDescriptors { get; set; }
}