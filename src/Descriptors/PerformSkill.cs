using System.ComponentModel;

namespace HavenheimManager.Descriptors;

public enum PerformSkill
{
    [Description("Act")] Act,
    [Description("Choreography")] Choreography,
    [Description("Manipulate")] Manipulate,
    [Description("Oratory")] Oratory,
    [Description("Percussion")] Percussion,
    [Description("Wind")] Wind
}