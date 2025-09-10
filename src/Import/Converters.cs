using System;
using System.Collections.Generic;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using HavenheimManager.Enums;
using HavenheimManager.Extensions;
using HavenheimManager.Handlers;

namespace HavenheimManager.Import;

public class SourceConverter<T> : DefaultTypeConverter
{
    public override object ConvertFromString(string? source, IReaderRow row, MemberMapData data)
    {
        return source.StringToSource();
    }
}

public class CustomTagsConverter<T> : DefaultTypeConverter
{
    public override object ConvertFromString(string? tags, IReaderRow row, MemberMapData data)
    {
        List<string> tagList = new();
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
        HashSet<string> prereqList = new();
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
        HashSet<Core> tagSet = new();
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
        HashSet<Skill> tagSet = new();
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
        HashSet<Class> tagSet = new();
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
        HashSet<Combat> tagSet = new();
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
        HashSet<Role> tagSet = new();
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
        HashSet<Magic> tagSet = new();
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
        HashSet<Bonus> tagSet = new();
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
        HashSet<Condition> tagSet = new();
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