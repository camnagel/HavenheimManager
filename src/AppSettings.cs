using HavenheimManager.Descriptors;
using HavenheimManager.Enums;

namespace HavenheimManager;

/// <summary>
///     Static class to hold app-wide settings
/// </summary>
internal static class AppSettings
{
    internal static AppMode Mode { get; set; }

    internal static readonly string SearchPlaceholderText = "Search...";

    internal static readonly int MinLevelPlaceholder = 0;

    internal static readonly int MaxLevelPlaceholder = 20;
}