using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    [Serializable]
    public class Offer
    {
        public static List<Offer> ListOfOffers = new List<Offer>();
        public int UnitKey { get; set; }
        public int GuestKey { get; set; }
    }
}
