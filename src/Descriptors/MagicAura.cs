using System.ComponentModel;

namespace HavenheimManager.Descriptors;

internal enum MagicAura
{
    [Description("Faint")] Faint,
    [Description("Moderate")] Moderate,
    [Description("Strong")] Strong,
    [Description("Overwhelming")] Overwhelming
}