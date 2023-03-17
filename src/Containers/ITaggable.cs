using AssetManager.Enums;
using System.Collections.Generic;

namespace AssetManager.Containers
{
    internal interface ITaggable
    {
        HashSet<Core> CoreTags { get; set; }

        HashSet<Skill> SkillTags { get; set; }

        HashSet<Class> ClassTags { get; set; }

        HashSet<Combat> CombatTags { get; set; }

        HashSet<Role> RoleTags { get; set; }

        HashSet<School> SchoolTags { get; set; }

        IList<string> CustomTags { get; set; }
    }
}
