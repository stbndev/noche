using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace noche.Config
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
    }
}
