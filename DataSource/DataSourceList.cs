using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DataSource
{
    public static class DataSourceList
    {
        public static List<Host> Hosts = new List<Host>();
        public static List<HostingUnit> HostingUnits = new List<HostingUnit>();
        public static List<Order> Orders = new List<Order>();
        public static List<GuestRequest> GuestRequests = new List<GuestRequest>();
    }
}
