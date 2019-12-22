using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DataSource;

namespace DAL
{
    class DalList : IDal
    {
        public bool addOrder(Order order)
        {
            if (DataSourceList.Orders.Any(guest => guest.OrderKey == order.OrderKey))
            {
                return false;
            }
            DataSourceList.Orders.Add(order.Clone());
            return true;
        }

        public Order getOrder(int id)
        {
            Order result = (from o in DataSourceList.Orders
                            where o.OrderKey == id
                            select o).FirstOrDefault();

            return result.Clone();
        }


        bool updateHostingUnit(HostingUnit hostingUnit)            // עדכון יחידת אירוח
        {
            foreach (var currentHostingUnit in DataSourceList.HostingUnits)
            {
                if (currentHostingUnit.Equals(hostingUnit)) return false;
            }
            DataSourceList.HostingUnits.Add(hostingUnit);
            return true;
        }

        List<string> returnAllCastumer()                     // קבלת רשימת כל הלקוחות.
        {
            List<string> result=new List<string>();
            foreach (var order in DataSourceList.GuestRequests)
            {
                result.Add( order.PrivateName+" "+order.FamilyName );
            }
            return result;
        }

        List<string> returnAllLocelBank()                          // קבלת רשימת כל סניפי הבנק הקיימים בארץ 
        {
            return new List<string> { "poelim","marcntil","laomi","disceunt","pagi" };
        }
    }
}
