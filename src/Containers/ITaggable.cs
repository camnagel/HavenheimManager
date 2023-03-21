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

        HashSet<Magic> MagicTags { get; set; }

        HashSet<Bonus> BonusTags { get; set; }

        IList<string> CustomTags { get; set; }
    }
}
