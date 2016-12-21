using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenweatherApps
{
    class ForecastKota
    {
        public string name { get; set; }
        public string id { get; set; }
        public string cod { get; set; }

        public string dt { get; set; }

        public SysSun sys { get; set; }

        public TempHumid main { get; set; }

        public List<CuacaKota> weather { get; set; }
    }
}
