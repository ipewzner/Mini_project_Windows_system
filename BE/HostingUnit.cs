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
        public int HostingUnitKey { get; private set; }
        public Host Owner { get; set; }
        public String HostingUnitName { get; set; }


        [XmlIgnore]
        //public bool[,] Diary { get; private set; }
        public List<DateTime> Diary { get; set; }

        //[XmlArray("Diary")]
        //public bool[] DiaryDto
        //{
        //    get { return Diary.Flatten(); }
        //    set { Diary = value.Expand(12); }
        //}


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
