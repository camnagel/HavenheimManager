using AssetManager.Enums;
using System.Collections.Generic;

namespace AssetManager.Containers
{
    internal interface ISummary
    {
        string Name { get; set; }

        string Description { get; set; }

        HashSet<string> Prereqs { get; set; }

        string Url { get; set; }

        string Notes { get; set; }

        Source Source { get; set; }
    }
}