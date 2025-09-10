using CsvHelper.Configuration;
using HavenheimManager.Containers;
using HavenheimManager.Enums;

namespace HavenheimManager.Import
{
    public class TraitMap : ClassMap<Trait>
    {
        public TraitMap()
        {
            Map(x => x.Name).Index(0);
            Map(x => x.Prereqs).TypeConverter<PrereqsConverter<string>>().Index(1);
            Map(x => x.Description).Index(2);
            Map(x => x.CustomTags).TypeConverter<CustomTagsConverter<string>>().Index(3);
            Map(x => x.Url).Index(4);
            Map(x => x.Source).TypeConverter<SourceConverter<Source>>().Index(5);
            Map(x => x.RoleTags).TypeConverter<RoleConverter<Role>>().Index(6);
            Map(x => x.ClassTags).TypeConverter<ClassConverter<Class>>().Index(7);
            Map(x => x.CoreTags).TypeConverter<CoreConverter<Core>>().Index(8);
            Map(x => x.SkillTags).TypeConverter<SkillConverter<Skill>>().Index(9);
            Map(x => x.CombatTags).TypeConverter<CombatConverter<Combat>>().Index(10);
            Map(x => x.MagicTags).TypeConverter<MagicConverter<Magic>>().Index(11);
            Map(x => x.BonusTags).TypeConverter<BonusConverter<Bonus>>().Index(12);
            Map(x => x.ConditionTags).TypeConverter<ConditionConverter<Condition>>().Index(13);
        }
    }
}
