using AssetManager.Containers;
using AssetManager.Enums;
using CsvHelper.Configuration;

namespace AssetManager.Import
{
    public class FeatMap : ClassMap<Feat>
    {
        public FeatMap()
        {
            Map(x => x.Name).Index(0);
            Map(x => x.Prereqs).TypeConverter<PrereqsConverter<string>>().Index(1);
            Map(x => x.Postreqs).TypeConverter<PrereqsConverter<string>>().Index(2);
            Map(x => x.Antireqs).TypeConverter<PrereqsConverter<string>>().Index(3);
            Map(x => x.Description).Index(4);
            Map(x => x.CustomTags).TypeConverter<CustomTagsConverter<string>>().Index(5);
            Map(x => x.Url).Index(6);
            Map(x => x.Source).TypeConverter<SourceConverter<Source>>().Index(7);
            Map(x => x.RoleTags).TypeConverter<RoleConverter<Role>>().Index(8);
            Map(x => x.ClassTags).TypeConverter<ClassConverter<Class>>().Index(9);
            Map(x => x.CoreTags).TypeConverter<CoreConverter<Core>>().Index(10);
            Map(x => x.SkillTags).TypeConverter<SkillConverter<Skill>>().Index(11);
            Map(x => x.CombatTags).TypeConverter<CombatConverter<Combat>>().Index(12);
            Map(x => x.MagicTags).TypeConverter<MagicConverter<Magic>>().Index(13);
        }
    }
}
