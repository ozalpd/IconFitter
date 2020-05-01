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
    }
}
