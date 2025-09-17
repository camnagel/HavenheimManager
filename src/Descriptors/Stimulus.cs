using System.ComponentModel;

namespace HavenheimManager.Descriptors;

public enum Stimulus
{
    [Description("Visual")] Visual,
    [Description("Auditory")] Auditory,
    [Description("Chemical")] Chemical,
    [Description("Tactile")] Tactile,
    [Description("Thermal")] Thermal
}