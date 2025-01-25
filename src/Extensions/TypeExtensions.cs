using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AssetManager.Containers;

namespace AssetManager.Extensions;

internal static class TypeExtensions
{
    internal static int DefaultMax(this List<int> list, int defaultValue = 0)
    {
        return list.Any() ? list.Max() : defaultValue;
    }

    internal static int DefaultMax(this Dictionary<string,int> list, int defaultValue = 0)
    {
        return list.Any() ? list.Values.Max() : defaultValue;
    }

    internal static BonusSvp? Max(this ObservableCollection<BonusSvp> list)
    {
        if (!list.Any()) return null;
        if (list.Count == 1) return list[0];

        BonusSvp ret = list[0];
        foreach (BonusSvp bonusSvp in list)
        {
            if (bonusSvp.Value > ret.Value) ret = bonusSvp;
        }

        return ret;
    }
}