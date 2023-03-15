using AssetManager.Enums;

namespace AssetManager.Containers
{
    internal interface ISummary
    {
        string Name { get; set; }

        string Description { get; set; }

        string Prereqs { get; set; }

        string Url { get; set; }

        string Effects { get; set; }

        Source Source { get; set; }
    }
}