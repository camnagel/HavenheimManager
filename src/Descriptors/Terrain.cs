using System.ComponentModel;

namespace HavenheimManager.Descriptors;

internal enum Terrain
{
    [Description("Any")] Any,
    [Description("Urban")] Urban,
    [Description("Plains")] Plains,
    [Description("Forest")] Forest,
    [Description("Swamp")] Swamp,
    [Description("Coastal")] Coastal,
    [Description("River/Lake")] River,
    [Description("Ceanic")] Ocean
}