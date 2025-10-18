using System.Text.RegularExpressions;

namespace HavenheimManager.Handlers;

public static class RegexHandler
{
    public static readonly Regex NumberFilter = new(@"[^0-9]+");

    public static readonly Regex SanitizationFilter = new(@"[\s-',()]+");
}