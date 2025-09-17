using System;
using System.ComponentModel;
using System.Reflection;
using HavenheimManager.Enums;
using HavenheimManager.Handlers;

namespace HavenheimManager.Extensions;

public static class EnumExtensions
{
    public static string GetEnumDescription<T>(this T enumValue) where T : Enum
    {
        FieldInfo? fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

        if (fieldInfo == null)
        {
            return string.Empty;
        }

        DescriptionAttribute[] descriptionAttributes =
            (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

        return descriptionAttributes.Length > 0 ? descriptionAttributes[0].Description : enumValue.ToString();
    }

    public static string Sanitize(this string input)
    {
        return RegexHandler.SanitizationFilter.Replace(input.ToLower(), "");
    }

    public static T StringToEnum<T>(this string input) where T : Enum
    {
        string sanitizedInput = input.Sanitize();

        // I know this is innefficient and a bit of me dies writing it, but I want to be lazy
        foreach (T value in Enum.GetValues(typeof(T)))
        {
            if (value.GetEnumDescription<T>().Sanitize().Equals(sanitizedInput))
            {
                return value;
            }
        }

        throw new ArgumentOutOfRangeException(nameof(input),
            "Could not determine requested enum: " + input);
    }

    public static int ToolToBonus(this Tool tool)
    {
        switch (tool)
        {
            case Tool.Improvised:
                return -4;
            case Tool.Masterwork:
                return 2;
            case Tool.Amazing:
                return 4;
            case Tool.None:
            case Tool.Basic:
            default:
                return 0;
        }
    }

    public static int WorkshopToBonus(this Workshop workshop)
    {
        switch (workshop)
        {
            case Workshop.None:
                return -10;
            case Workshop.Improvised:
                return -4;
            case Workshop.Masterwork:
                return 2;
            case Workshop.Guild:
                return 4;
            case Workshop.Basic:
            default:
                return 0;
        }
    }
}