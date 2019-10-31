namespace RealEstate.Infrastructure
{
    using System;
    using System.Linq;

    public static class RandomExtension
    {
        private const string DATA_COLLECTION = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public static string RandomCapcha(this Random rd, int length)
            => new string(Enumerable.Repeat(DATA_COLLECTION, length)
                .Select(s => s[rd.Next(s.Length)]).ToArray());

        public static int RandomInRange(this Random rd, int minValue, int maxValue)
            => rd.Next(minValue, maxValue);

        public static double RandomInRange(this Random rd, double minValue, double maxValue)
            => minValue + (rd.NextDouble() * (maxValue - minValue));

        public static DateTime RandomInRange(this Random rd, DateTime minDate, DateTime maxDate)
            => minDate.AddDays(rd.Next((maxDate - minDate).Days));

        public static DateTimeOffset RandomInRange(this Random rd, DateTimeOffset minDate, DateTimeOffset maxDate)
            => minDate.AddDays(rd.Next((maxDate - minDate).Days));
    }
}