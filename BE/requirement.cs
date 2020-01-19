using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace BE
{
    public class requirement
    {
       
        public Area Area { get; set; }
        public String SubArea { get; set; }
        public HostingType HostingType { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public Requirements Pool { get; set; }
        public Requirements Jacuzzi { get; set; }
        public Requirements Garden { get; set; }
        public Requirements ChildrensAttractions { get; set; }
        //********************
        public Requirements SpredBads { get; set; }
        public Requirements AirCondsner { get; set; }
        public Requirements frisider { get; set; }
        public Requirements SingogNaerBy { get; set; }
        public Requirements NaerPublicTrensportion { get; set; }

        /// <summary>
        /// to-string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.TostringProperties();
        }

    }
}
