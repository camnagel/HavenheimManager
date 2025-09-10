using System.Collections.Generic;
using HavenheimManager.Enums;

namespace HavenheimManager.Containers;

public class Item : ISummary, ITaggable
{
    public HashSet<string> Postreqs { get; set; } = new();

    public HashSet<string> Antireqs { get; set; } = new();
    public string Name { get; set; }

    public string Description { get; set; }

    public HashSet<string> Prereqs { get; set; } = new();

    public string Url { get; set; }

    public string Notes { get; set; }

    public Source Source { get; set; } = Source.Unknown;

    public IList<string> CustomTags { get; set; } = new List<string>();

    public HashSet<Core> CoreTags { get; set; } = new();

    public HashSet<Skill> SkillTags { get; set; } = new();

    public HashSet<Class> ClassTags { get; set; } = new();

    public HashSet<Combat> CombatTags { get; set; } = new();

    public HashSet<Role> RoleTags { get; set; } = new();

    public HashSet<Magic> MagicTags { get; set; } = new();

    public HashSet<Bonus> BonusTags { get; set; } = new();

    public HashSet<Condition> ConditionTags { get; set; } = new();
}