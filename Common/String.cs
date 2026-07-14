using System;

namespace CommandsExtended.Common
{
    public static class String
    {
        public static bool EqualsIgnoreCase(this string one, string two)
        {
            return one.Equals(two, StringComparison.OrdinalIgnoreCase);
        }

        public static bool StartsWithIgnoreCase(this string one, string two)
        {
            return one.StartsWith(two, StringComparison.OrdinalIgnoreCase);
        }

        public static bool ContainsIgnoreCase(this string one, string two)
        {
            return one.Contains(two, StringComparison.OrdinalIgnoreCase);
        }
    }
}
