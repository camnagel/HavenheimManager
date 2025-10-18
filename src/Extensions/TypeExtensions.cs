using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HavenheimManager.Containers;

namespace HavenheimManager.Extensions;

internal static class TypeExtensions
{
    internal static int DefaultMax(this List<int> list, int defaultValue = 0)
    {
        return list.Any() ? list.Max() : defaultValue;
    }

    internal static int DefaultMax(this Dictionary<string, int> list, int defaultValue = 0)
    {
        return list.Any() ? list.Values.Max() : defaultValue;
    }

    internal static BonusSvp? Max(this ObservableCollection<BonusSvp> list)
    {
        if (!list.Any())
        {
            return null;
        }

        if (list.Count == 1)
        {
            return list[0];
        }

        BonusSvp ret = list[0];
        foreach (BonusSvp bonusSvp in list)
        {
            if (bonusSvp.Value > ret.Value)
            {
                ret = bonusSvp;
            }
        }

        return ret;
    }

    /// <summary>
    ///     Populates the collection with the descriptions off all values in the provided enum
    /// </summary>
    internal static void Fill<T>(this ICollection<string> collection, Type enumValue) where T : Enum
    {
        foreach (T value in Enum.GetValues(enumValue))
        {
            collection.Add(value.GetEnumDescription());
        }
    }

    /// <summary>
    ///     Populates the collection with the descriptions of all values in the provided list
    /// </summary>
    internal static void Fill<T>(this ICollection<string> collection, ICollection<T> values) where T : Enum
    {
        foreach (T value in values)
        {
            collection.Add(value.GetEnumDescription());
        }
    }
}