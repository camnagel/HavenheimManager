using System.Collections.Generic;
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

    public class CoreConverter<T> : DefaultTypeConverter
    {
        public override object ConvertFromString(string? tags, IReaderRow row, MemberMapData data)
        {
            HashSet<Core> tagSet= new HashSet<Core>();
            if (tags != null) tagSet.Add(tags.StringToCore());
            return tagSet;
        } 
    }

    public class SkillConverter<T> : DefaultTypeConverter
    {
        public override object ConvertFromString(string? tags, IReaderRow row, MemberMapData data)
        {
            HashSet<Skill> tagSet = new HashSet<Skill>();
            if (tags != null) tagSet.Add(tags.StringToSkill());
            return tagSet;
        }
    }

    public class ClassConverter<T> : DefaultTypeConverter
    {
        public override object ConvertFromString(string? tags, IReaderRow row, MemberMapData data)
        {
            HashSet<Class> tagSet = new HashSet<Class>();
            if (tags != null) tagSet.Add(tags.StringToClass());
            return tagSet;
        }
    }

    public class CombatConverter<T> : DefaultTypeConverter
    {
        public override object ConvertFromString(string? tags, IReaderRow row, MemberMapData data)
        {
            HashSet<Combat> tagSet = new HashSet<Combat>();
            if (tags != null) tagSet.Add(tags.StringToCombat());
            return tagSet;
        }
    }

    public class RoleConverter<T> : DefaultTypeConverter
    {
        public override object ConvertFromString(string? tags, IReaderRow row, MemberMapData data)
        {
            HashSet<Role> tagSet = new HashSet<Role>();
            if (tags != null) tagSet.Add(tags.StringToRole());
            return tagSet;
        }
    }

    public class SchoolConverter<T> : DefaultTypeConverter
    {
        public override object ConvertFromString(string? tags, IReaderRow row, MemberMapData data)
        {
            HashSet<School> tagSet = new HashSet<School>();
            if (tags != null) tagSet.Add(tags.StringToSchool());
            return tagSet;
        }
    }
}
