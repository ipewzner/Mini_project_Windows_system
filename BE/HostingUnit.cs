using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Utilities;

namespace BE
{
    [Serializable]
    public class HostingUnit   : requirement
    {
        requirement requirement = new requirement();
        public int HostingUnitKey { get; set; }
        public Host Owner { get; set; }
        public String HostingUnitName { get; set; }
        public List<DateTime> Diary { get; set; }
  
        public HostingUnit()
        {
            Diary = new List<DateTime>();
            HostingUnitKey = Configuration.serialHostingUnit++;
        }

        public override string ToString()
        {
            return this.TostringProperties();
        }

    }
}
