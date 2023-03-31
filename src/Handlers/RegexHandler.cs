using System.Text.RegularExpressions;

namespace AssetManager.Handlers
{
    public static class RegexHandler
    {
        public static readonly Regex NumberFilter = new Regex(@"[^0-9]+");

        public static readonly Regex SanitizationFilter = new Regex(@"[\s-',()]+");

        public static readonly string SearchPlaceholderText = "Search...";

        public static readonly int FeatMinLevelPlaceholder = 0;

        public static readonly int FeatMaxLevelPlaceholder = 20;
    }
}
