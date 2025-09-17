using System.ComponentModel;

namespace HavenheimManager.Descriptors;

public enum DescTrait
{
    [Description("Combat")] Combat,
    [Description("Magic")] Magic,
    [Description("Faction")] Faction,
    [Description("Story")] Story,
    [Description("Social")] Social
}