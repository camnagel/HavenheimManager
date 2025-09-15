using System.ComponentModel;

namespace HavenheimManager.Descriptors;

internal enum Duration
{
    [Description("Any")] Any,
    [Description("Instant")] Instant,
    [Description("End of Turn")] EndTurn,
    [Description("1 Round")] Round,
    [Description("1 Round/Increment")] RoundPer,
    [Description("3 Rounds/Increment")] ThreeRoundPer,
    [Description("1 Minute/Increment")] MinutePer,
    [Description("10 Minutes/Increment")] TenMinutePer,
    [Description("1 Hour/Increment")] HourPer,
    [Description("1 Day/Increment")] DayPer,
    [Description("10 Days/Increment")] TenDayPer,
    [Description("1 Month/Increment")] MonthPer,
    [Description("1 Year/Increment")] YearPer,
}