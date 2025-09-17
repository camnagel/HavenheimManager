using System.ComponentModel;

namespace HavenheimManager.Descriptors;

public enum DescSave
{
    [Description("Reflex")] Reflex,
    [Description("Will")] Will,
    [Description("Fortitude")] Fortitude,
    [Description("ReflexPartial")] ReflexPartial,
    [Description("WillPartial")] WillPartial,
    [Description("FortitudePartial")] FortitudePartial,
    [Description("Multiple")] Multiple
}