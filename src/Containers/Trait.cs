using AssetManager.Enums;
using System.Collections.Generic;

namespace AssetManager.Containers
{
    public class Trait : ISummary, ITaggable
    {
        public string Name { get; }

        public string Description { get; }

        public string Prereqs { get; }

        public string Url { get; }

        public string Effects { get; }

        public Source Source { get; }

        public HashSet<Core> CoreTags { get; } = new HashSet<Core>();

        public HashSet<Skill> SkillTags { get; } = new HashSet<Skill>();

        public HashSet<Class> ClassTags { get; } = new HashSet<Class>();

        public HashSet<Combat> CombatTags { get; } = new HashSet<Combat>();

        public HashSet<Role> RoleTags { get; } = new HashSet<Role>();

        public HashSet<School> SchoolTags { get; } = new HashSet<School>();

        public Trait(string name, string description, string effects, string url, string prereqs,
            Source source, IList<Core> coreTags, IList<Skill> skillTags, IList<Class> classTags, 
            IList<Combat> combatTags, IList<Role> roleTags, IList<School> schoolTags)
        {
            Name = name;
            Description = description;
            Prereqs = prereqs;
            Url = url;
            Effects = effects;
            Source = source;

            foreach (var tag in coreTags)
            {
                CoreTags.Add(tag);
            }

            foreach (var tag in skillTags)
            {
                SkillTags.Add(tag);
            }

            foreach (var tag in classTags)
            {
                ClassTags.Add(tag);
            }

            foreach (var tag in combatTags)
            {
                CombatTags.Add(tag);
            }

            foreach (var tag in roleTags)
            {
                RoleTags.Add(tag);
            }

            foreach (var tag in schoolTags)
            {
                SchoolTags.Add(tag);
            }
        }
    }
}
