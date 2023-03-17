using System;
using System.Collections.Generic;
using System.Linq;
using AssetManager.Enums;
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
            List<string> prereqList = new List<string>();
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

    public class SchoolConverter<T> : DefaultTypeConverter
    {
        public override object ConvertFromString(string? tags, IReaderRow row, MemberMapData data)
        {
            HashSet<School> tagSet = new HashSet<School>();
            if (tags is { Length: > 0 })
            {
                List<string> splitTags = tags.Split(',').ToList();
                foreach (string tag in splitTags)
                {
                    tagSet.Add(tag.StringToSchool());
                }
            }
            return tagSet;
        }
    }
}
