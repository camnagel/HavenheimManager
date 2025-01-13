using System;
using System.Collections.Generic;
using System.Linq;
using AssetManager.Enums;
using AssetManager.Extensions;
using AssetManager.Handlers;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace AssetManager.Import
{
    public class SourceConverter<T> : DefaultTypeConverter
    {
        public override object ConvertFromString(string? source, IReaderRow row, MemberMapData data) => source.StringToSource();
    }

    public class CustomTagsConverter<T> : DefaultTypeConverter
    {
        public override object ConvertFromString(string? tags, IReaderRow row, MemberMapData data)
        {
            List<string> tagList = new List<string>();
            if (tags is { Length: > 0 })
            {
                List<string> splitTags = tags.Split(',').ToList();
                foreach (string tag in splitTags)
                {
                    tagList.Add(tag.Trim());
                }
            }
            return tagList;
        }
    }

    public class PrereqsConverter<T> : DefaultTypeConverter
    {
        public override object ConvertFromString(string? prereqs, IReaderRow row, MemberMapData data)
        {
            HashSet<string> prereqList = new HashSet<string>();
            if (prereqs is { Length: > 0 })
            {
                List<string> splitTags = prereqs.Split(',').ToList();
                foreach (string prereq in splitTags)
                {
                    prereqList.Add(prereq.Trim());
                }
            }
            return prereqList;
        }
    }

    public class CoreConverter<T> : DefaultTypeConverter
    {
        public override object ConvertFromString(string? tags, IReaderRow row, MemberMapData data)
        {
            HashSet<Core> tagSet= new HashSet<Core>();
            if (tags is { Length: > 0 })
            {
                List<string> splitTags = tags.Split(',').ToList();
                foreach (string tag in splitTags)
                {
                    tagSet.Add(tag.StringToCore());
                }
            }
            return tagSet;
        } 
    }

    public class SkillConverter<T> : DefaultTypeConverter
    {
        public override object ConvertFromString(string? tags, IReaderRow row, MemberMapData data)
        {
            HashSet<Skill> tagSet = new HashSet<Skill>();
            if (tags is { Length: > 0 })
            {
                List<string> splitTags = tags.Split(',').ToList();
                foreach (string tag in splitTags)
                {
                    tagSet.Add(tag.StringToSkill());
                }
            }
            return tagSet;
        }
    }

    public class ClassConverter<T> : DefaultTypeConverter
    {
        public override object ConvertFromString(string? tags, IReaderRow row, MemberMapData data)
        {
            HashSet<Class> tagSet = new HashSet<Class>();
            if (tags is { Length: > 0 })
            {
                List<string> splitTags = tags.Split(',').ToList();
                foreach (string tag in splitTags)
                {
                    tagSet.Add(tag.StringToClass());
                }
            }
            return tagSet;
        }
    }

    public class CombatConverter<T> : DefaultTypeConverter
    {
        public override object ConvertFromString(string? tags, IReaderRow row, MemberMapData data)
        {
            HashSet<Combat> tagSet = new HashSet<Combat>();
            if (tags is { Length: > 0 })
            {
                List<string> splitTags = tags.Split(',').ToList();
                foreach (string tag in splitTags)
                {
                    tagSet.Add(tag.StringToCombat());
                }
            }
            return tagSet;
        }
    }

    public class RoleConverter<T> : DefaultTypeConverter
    {
        public override object ConvertFromString(string? tags, IReaderRow row, MemberMapData data)
        {
            HashSet<Role> tagSet = new HashSet<Role>();
            if (tags is { Length: > 0 })
            {
                List<string> splitTags = tags.Split(',').ToList();
                foreach (string tag in splitTags)
                {
                    tagSet.Add(tag.StringToRole());
                }
            }
            return tagSet;
        }
    }

    public class MagicConverter<T> : DefaultTypeConverter
    {
        public override object ConvertFromString(string? tags, IReaderRow row, MemberMapData data)
        {
            HashSet<Magic> tagSet = new HashSet<Magic>();
            if (tags is { Length: > 0 })
            {
                List<string> splitTags = tags.Split(',').ToList();
                foreach (string tag in splitTags)
                {
                    tagSet.Add(tag.StringToMagic());
                }
            }
            return tagSet;
        }
    }

    public class BonusConverter<T> : DefaultTypeConverter
    {
        public override object ConvertFromString(string? tags, IReaderRow row, MemberMapData data)
        {
            HashSet<Bonus> tagSet = new HashSet<Bonus>();
            if (tags is { Length: > 0 })
            {
                List<string> splitTags = tags.Split(',').ToList();
                foreach (string tag in splitTags)
                {
                    tagSet.Add(tag.StringToBonus());
                }
            }
            return tagSet;
        }
    }

    public class ConditionConverter<T> : DefaultTypeConverter
    {
        public override object ConvertFromString(string? tags, IReaderRow row, MemberMapData data)
        {
            HashSet<Condition> tagSet = new HashSet<Condition>();
            if (tags is { Length: > 0 })
            {
                List<string> splitTags = tags.Split(',').ToList();
                foreach (string tag in splitTags)
                {
                    tagSet.Add(tag.StringToCondition());
                }
            }
            return tagSet;
        }
    }

    public class LevelConverter<T> : DefaultTypeConverter
    {
        public override object ConvertFromString(string? level, IReaderRow row, MemberMapData data)
        {
            if (string.IsNullOrEmpty(level))
            {
                return 0;
            }

            if (!RegexHandler.NumberFilter.IsMatch(level) && int.Parse(level) is <= 20 and >= 0)
            {
                return int.Parse(level);
            }
            
            throw new ArgumentOutOfRangeException(nameof(level), "Level must be an int between 0 and 20");
        }
    }
}
