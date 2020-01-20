using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace BE
{
    [Serializable]
    public class Order
    {
        public int OrderKey { get; set; }

        public int HostingUnitKey { get; set; }
        public int GuestRequestKey { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }

        public override string ToString()
        {
            return this.TostringProperties();
        }
    }
}
