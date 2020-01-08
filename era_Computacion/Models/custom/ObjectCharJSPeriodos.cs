using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace era_Computacion.Models.custom
{
    public class ObjectCharJSPeriodos
    {
        public List<double?> data { get; set; }
        public List<string> backgroundColor { get; set; }
        public string label { get; set; }
        public string borderColor { get; set; }
        public List<string> pointBackgroundColor { get; set; }


        public string fill { get; set; }
        public double borderWidth { get; set; }
    }
}
