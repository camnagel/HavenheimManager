using AssetManager.Enums;
using System.Collections.Generic;

namespace AssetManager.Containers
{
    internal interface ITaggable
    {
        HashSet<Core> CoreTags { get; }

        HashSet<Skill> SkillTags { get; }

        HashSet<Class> ClassTags { get; }

        HashSet<Combat> CombatTags { get; }

        HashSet<Role> RoleTags { get; }

        HashSet<School> SchoolTags { get; }
    }
}
