using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IconLib
{
    public static class Extensions
    {
        public static double GetZoom(this string zoomText)
        {
            double z = 0;
            if (!string.IsNullOrEmpty(zoomText))
            {
                string s = zoomText.Replace('%', ' ').Trim();
                double.TryParse(s, out z);
            }
            return z;
        }

        public static string ToElapsedString(this TimeSpan timeSpan)
        {
            string elapsed = "Elapsed Time:";
            if (timeSpan.TotalMinutes > 60)
                return string.Format("{0} {1:hh\\:mm\\:ss}", elapsed, timeSpan);

            if (timeSpan.TotalSeconds > 60)
                return string.Format("{0} {1:mm\\:ss}", elapsed, timeSpan);

            if (timeSpan.TotalSeconds > 1)
                return string.Format("{0} {1:ss\\:fff}", elapsed, timeSpan);

            if (timeSpan.TotalMilliseconds > 99)
                return string.Format("{0} {1:###} ms", elapsed, timeSpan.TotalMilliseconds);

            return string.Format("{0} {1:###.##} ms", elapsed, timeSpan.TotalMilliseconds);
        }

        public static string ToElapsedString(this TimeSpan? timeSpan)
        {
            if (timeSpan == null)
                return string.Empty;

            return timeSpan.Value.ToElapsedString();
        }
    }
}
