using AssetManager.Enums;
using System.Collections.Generic;

namespace AssetManager.Containers
{
    public class Feat : ISummary, ITaggable
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int Level { get; set; }

        public HashSet<string> Prereqs { get; set; } = new HashSet<string>();

        public HashSet<string> Postreqs { get; set; } = new HashSet<string>();

        public HashSet<string> Antireqs { get; set; } = new HashSet<string>();

        public string Url { get; set; }

        public string Notes { get; set; }

        public IList<string> CustomTags { get; set; } = new List<string>();

        public Source Source { get; set; } = Source.Unknown;

        public HashSet<Core> CoreTags { get; set; } = new HashSet<Core>();

        public HashSet<Skill> SkillTags { get; set; } = new HashSet<Skill>();

        public HashSet<Class> ClassTags { get; set; } = new HashSet<Class>();

        public HashSet<Combat> CombatTags { get; set; } = new HashSet<Combat>();

        public HashSet<Role> RoleTags { get; set; } = new HashSet<Role>();

        public HashSet<Magic> MagicTags { get; set; } = new HashSet<Magic>();

        public HashSet<Bonus> BonusTags { get; set; } = new HashSet<Bonus>();

        public HashSet<Condition> ConditionTags { get; set; } = new HashSet<Condition>();
    }
}
