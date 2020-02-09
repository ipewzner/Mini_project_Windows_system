using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace BE
{
    [Serializable]
    public class requirement
    {
        public Area Area { get; set; }
        public String SubArea { get; set; }
        public HostingType HostingType { get; set; }
        public int Adults   { get; set; }
        public int Children { get; set; }
        public GestRequirements Pool { get; set; }
        public GestRequirements Jacuzzi { get; set; }
        public GestRequirements Garden { get; set; }
        public GestRequirements ChildrensAttractions { get; set; }
        public GestRequirements SpredBads { get; set; }
        public GestRequirements AirCondsner { get; set; }
        public GestRequirements frisider { get; set; }
        public GestRequirements SingogNaerBy { get; set; }
        public GestRequirements NaerPublicTrensportion { get; set; }

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
