using System.Collections.Generic;
using System.Linq;

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
}