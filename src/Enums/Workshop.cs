using System.ComponentModel;

namespace HavenheimManager.Enums
{
    public enum Workshop
    {
        [Description("None")]
        None,
        [Description("Improvised")]
        Improvised,
        [Description("Basic")]
        Basic,
        [Description("Masterwork")]
        Masterwork,
        [Description("Guild")]
        Guild
    }
}
