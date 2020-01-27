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
    
        
        public int GuestRequestKey { get; set; }
        public String PrivateName { get; set; }
        public String FamilyName { get; set; }
        public String MailAddress { get; set; }
        public ClientStatus Status { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime ReleaseDate { get; set; }
       
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
