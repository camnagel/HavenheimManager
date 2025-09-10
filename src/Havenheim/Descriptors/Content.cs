using System.ComponentModel;

namespace HavenheimManager.Havenheim.Descriptors;

public enum Content
{
    [Description("Pathfinder 1e")] Direct,

    [Description("Pathfinder 1e reworded")]
    Reworded,

    [Description("Pathfinder 1e modified")]
    Modified,

    [Description("Pathfinder 1e homebrew")]
    Homebrew
}