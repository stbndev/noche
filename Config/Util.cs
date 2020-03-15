using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace noche
{
    public class Util
    {
        public static string ConvertToTimestamp(DateTime value = default)
        {
            if (value == default)
            {
                value = DateTime.Now;
            }

            long epoch = (value.Ticks - 621355968000000000) / 10000000;
            // epoch epoca

            //int tmpepoch = int.Parse(epoch.ToString());
            return epoch.ToString();
        }

        public static decimal Rounding2digits(decimal values)
        {
            return Math.Round(values, 2);
        }
    }
}
