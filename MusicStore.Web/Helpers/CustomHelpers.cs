using System;

namespace MusicStore.Web.Helpers
{
    public static class CustomHelpers
    {
        public static string Truncate(string? value, int maxLength)
        {
            if (string.IsNullOrEmpty(value) || maxLength <= 0)
                return string.Empty;

            if (value.Length <= maxLength)
                return value;

            return value.Substring(0, maxLength) + "...";
        }
    }
}
