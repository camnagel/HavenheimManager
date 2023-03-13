using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssetManager.Enums;

namespace AssetManager.Containers
{
    public class Trait : ISummary, ITaggable
    {
        public string Name { get; }

        public string Description { get; }

        public string Effects { get; }

        public Source Source { get; }

        public HashSet<Combat> CombatTags { get; } = new HashSet<Combat>();

        public HashSet<School> SchoolTags { get; } = new HashSet<School>();

        public HashSet<Skill> SkillTags { get; } = new HashSet<Skill>();

        public HashSet<Core> CoreTags { get; } = new HashSet<Core>();

        public HashSet<Role> RoleTags { get; } = new HashSet<Role>();

        public Trait(string name, string description, string effects, 
            Source source, IList<Combat> combatTags, IList<School> schoolTags, 
            IList<Skill> skillTags, IList<Core> coreTags, IList<Role> roleTags)
        {
            Name = name;
            Description = description;
            Effects = effects;
            Source = source;

            foreach (var tag in combatTags)
            {
                CombatTags.Add(tag);
            }

            foreach (var tag in schoolTags)
            {
                SchoolTags.Add(tag);
            }

            foreach (var tag in skillTags)
            {
                SkillTags.Add(tag);
            }

            foreach (var tag in coreTags)
            {
                CoreTags.Add(tag);
            }

            foreach (var tag in roleTags)
            {
                RoleTags.Add(tag);
            }
        }
    }
}
