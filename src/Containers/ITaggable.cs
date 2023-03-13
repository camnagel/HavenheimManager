using AssetManager.Enums;
using System.Collections.Generic;

namespace AssetManager.Containers
{
    internal interface ITaggable
    {
        HashSet<Combat> CombatTags { get; }

        HashSet<School> SchoolTags { get; }

        HashSet<Skill> SkillTags { get; }

        HashSet<Core> CoreTags { get; }

        HashSet<Role> RoleTags { get; }
    }
}
