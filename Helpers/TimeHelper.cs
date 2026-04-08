namespace Web_Novel.Helpers
{
    public static class TimeHelper
    {
        public static DateTime ToThaiTime(DateTime utcDateTime)
        {
            var tz = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, tz);
        }

        public static string ToThaiString(DateTime utcDateTime)
        {
            var tz = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            var thaiTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, tz);
            return thaiTime.ToString("dd/MM/yyyy HH:mm");
        }
    }
}