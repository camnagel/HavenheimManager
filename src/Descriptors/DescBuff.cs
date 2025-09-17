using System.ComponentModel;

namespace HavenheimManager.Descriptors;

public enum DescBuff
{
    [Description("Buff")] Buff,
    [Description("Debuff")] Debuff,
    [Description("SelfBuff")] SelfBuff,
    [Description("AllyBuff")] AllyBuff,
    [Description("SelfDebuff")] SelfDebuff,
    [Description("Bonus")] Bonus,
    [Description("Penalty")] Penalty
}