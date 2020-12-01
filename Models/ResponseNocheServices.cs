using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace noche.Models
{
    public class ResponseNocheServices
    {
        public dynamic Data { get; set; }
        public int Flag { get; set; }
        public string Message { get; set; }
        public string Href { get; set; }
        //public string FunctionName { get; set; }

        public ResponseNocheServices()
        {
            this.Flag = 0;
            this.Message = string.Empty;
        }
        public void SetResponse(int r, string m = "")
        {
            this.Flag = r;
            this.Message = m;
            if (r<=0 && m == "") this.Message = "unexpected error";
        }
    }
}
