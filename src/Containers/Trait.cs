using AssetManager.Enums;
using System.Collections.Generic;

namespace AssetManager.Containers
{
    public class Trait : ISummary, ITaggable
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Prereqs { get; set; }

        public string Url { get; set; }

        public string Effects { get; set; }

        public Source Source { get; set; }

        public HashSet<Core> CoreTags { get; set; } = new HashSet<Core>();

        public HashSet<Skill> SkillTags { get; set; } = new HashSet<Skill>();

        public HashSet<Class> ClassTags { get; set; } = new HashSet<Class>();

        public HashSet<Combat> CombatTags { get; set; } = new HashSet<Combat>();

        public HashSet<Role> RoleTags { get; set; } = new HashSet<Role>();

        public HashSet<School> SchoolTags { get; set; } = new HashSet<School>();

        /*public Trait(string name, string description, string effects, string url, string prereqs,
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
        }*/
    }
}
