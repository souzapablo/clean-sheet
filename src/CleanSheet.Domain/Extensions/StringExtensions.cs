using System.Text.RegularExpressions;

namespace CleanSheet.Domain.Extensions
{
    public static partial class StringExtensions
    {
        public static string CreateSlug(this string input)
        {
            var slug = input.ToLower();
            slug = MyRegex().Replace(slug, "-");
            slug = slug.Trim('-');
            return slug;
        }

        [GeneratedRegex("[^a-z0-9]+")]
        private static partial Regex MyRegex();
    }
}