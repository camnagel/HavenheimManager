using System.Collections.Generic;
using HavenheimManager.Descriptors;

namespace HavenheimManager.Containers;

public interface ITaggable
{
    HashSet<DescContent> ContentDescriptors { get; set; }
    HashSet<Creature> CreatureDescriptors { get; set; }
    HashSet<CreatureType> CreatureTypeDescriptors { get; set; }
    HashSet<CreatureSubType> CreatureSubTypeDescriptors { get; set; }
    HashSet<CraftSkill> CraftSkillDescriptors { get; set; }
    HashSet<DescAbility> AbilityDescriptors { get; set; }
    HashSet<AbilityType> AbilityTypeDescriptors { get; set; }
    HashSet<DescBuff> BuffDescriptors { get; set; }
    HashSet<DescFeat> FeatDescriptors { get; set; }
    HashSet<DescMagic> MagicDescriptors { get; set; }
    HashSet<DescSave> SaveDescriptors { get; set; }
    HashSet<DescSkill> SkillDescriptors { get; set; }
    HashSet<DescSystem> SystemDescriptors { get; set; }
    HashSet<DescTrait> TraitDescriptors { get; set; }
    HashSet<DescUsage> UsageDescriptors { get; set; }
    HashSet<DescDuration> DurationDescriptors { get; set; }
    HashSet<Knowledge> KnowledgeDescriptors { get; set; }
    HashSet<MagicAura> MagicAuraDescriptors { get; set; }
    HashSet<PerformSkill> PerformSkillDescriptors { get; set; }
    HashSet<SpellSchool> SpellSchoolDescriptors { get; set; }
    HashSet<Stimulus> StimulusDescriptors { get; set; }
    HashSet<Terrain> TerrainDescriptors { get; set; }

    IList<string> CustomDescriptors { get; set; }
}