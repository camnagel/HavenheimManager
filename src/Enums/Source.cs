using System.ComponentModel;
using HavenheimManager.Containers;

namespace HavenheimManager.Enums
{
    /// <summary>
    /// Describes the source of an <see cref="ISummary"/>
    /// </summary>
    public enum Source
    {
        [Description("Standard")]
        Standard,
        [Description("Rework")]
        Rework,
        [Description("Homebrew")]
        Homebrew,
        [Description("Unknown")]
        Unknown
    }
}
