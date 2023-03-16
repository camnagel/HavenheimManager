using AssetManager.Enums;
using System.Collections.Generic;

namespace AssetManager.Containers
{
    internal interface ISummary
    {
        string Name { get; set; }

        string Description { get; set; }

        IList<string> Prereqs { get; set; }

        string Url { get; set; }

        string Effects { get; set; }

        Source Source { get; set; }
    }
}