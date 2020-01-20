using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace BE
{
    [Serializable]
    public class GuestRequest : requirement
    {
        requirement requirement = new requirement();
    
        
        public int GuestRequestKey { get; private set; }
        public String PrivateName { get; set; }
        public String FamilyName { get; set; }
        public String MailAddress { get; set; }
        public ClientStatus Status { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime ReleaseDate { get; set; }
          /*
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
                */
        /// <summary>
        /// c-tor
        /// </summary>
        public GuestRequest()
        {
            GuestRequestKey = Configuration.serialGuestRequest++;
        }
       
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
