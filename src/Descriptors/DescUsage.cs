using System.ComponentModel;

namespace HavenheimManager.Descriptors;

public enum DescUsage
{
    [Description("1/Day")] One,
    [Description("3/Day")] Three,
    [Description("Charge")] Charge,
    [Description("Unlimited")] Unlimited
}