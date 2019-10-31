namespace RealEstate.Infrastructure
{
    using System;

    public static class DateTimeExtenstion
    {
        private const string DATE_FORMAT = "dd/MM/yyyy";

        public static string TimeAgo(this DateTimeOffset dateTime)
        {
            return dateTime.ToString(DATE_FORMAT);
            //TimeSpan timeSpan = DateTimeOffset.Now.Subtract(dateTime);

            //if (timeSpan <= TimeSpan.FromSeconds(60))
            //    return string.Format("{0} giây trước", timeSpan.Seconds);
            //else if (timeSpan <= TimeSpan.FromMinutes(60))
            //    return timeSpan.Minutes > 1 ?
            //        string.Format("khoảng {0} phút trước", timeSpan.Minutes) :
            //        "khoảng một phút trước";
            //else if (timeSpan <= TimeSpan.FromHours(24))
            //    return timeSpan.Hours > 1 ?
            //        string.Format("khoảng {0} giờ trước", timeSpan.Hours) :
            //        "khoảng một giờ trước";
            //else if (timeSpan <= TimeSpan.FromDays(30))
            //    return timeSpan.Days > 1 ?
            //        string.Format("khoảng {0} ngày trước", timeSpan.Days) :
            //        "hôm qua";
            //else
            //    return dateTime.ToString(DATE_FORMAT);
        }

        public static string TimeAgo(this DateTime dateTime)
        {
            TimeSpan timeSpan = DateTime.Now.Subtract(dateTime);

            if (timeSpan <= TimeSpan.FromSeconds(60))
                return string.Format("{0} giây trước", timeSpan.Seconds);
            else if (timeSpan <= TimeSpan.FromMinutes(60))
                return timeSpan.Minutes > 1 ?
                    string.Format("khoảng {0} phút trước", timeSpan.Minutes) :
                    "khoảng một phút trước";
            else if (timeSpan <= TimeSpan.FromHours(24))
                return timeSpan.Hours > 1 ?
                    string.Format("khoảng {0} giờ trước", timeSpan.Hours) :
                    "khoảng một giờ trước";
            else if (timeSpan <= TimeSpan.FromDays(30))
                return timeSpan.Days > 1 ?
                    string.Format("khoảng {0} ngày trước", timeSpan.Days) :
                    "hôm qua";
            else
                return dateTime.ToString(DATE_FORMAT);
        }
    }
}