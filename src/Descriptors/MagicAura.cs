using System.ComponentModel;

namespace HavenheimManager.Descriptors;

public enum MagicAura
{
    [Description("Faint")] Faint,
    [Description("Moderate")] Moderate,
    [Description("Strong")] Strong,
    [Description("Overwhelming")] Overwhelming
}