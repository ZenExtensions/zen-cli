using System;

namespace Zen.Core.Extensions
{
    public static class ConversionExtensions
    {
        public static DateTime? GetDateTime(this string val)
        {
            if(DateTime.TryParse(val, out var date))
            {
                return date;
            }
            return null;
        }

        public static string GetPossibleYesNoValue(this string val)
        {
            if(val.Equals("true", StringComparison.InvariantCultureIgnoreCase))
                return "Yes";
            if(val.Equals("false", StringComparison.InvariantCultureIgnoreCase))
                return "No";
            return val;
        }
        public static string GetDateText(this DateTime? date)
        {
            if(date is null)
                return null;
            return date.Value.ToString("dd MMMM yyyy");
        }

        public static string GetBooleanText(this bool boolean)
        {
            return boolean ? "Yes": "No";
        }
        public static string GetBooleanText(this bool? boolean)
        {
            if(boolean is null)
                return "No";
            return boolean.Value ? "Yes": "No";
        }
    }
}