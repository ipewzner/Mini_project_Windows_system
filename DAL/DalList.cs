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
        #region Order
        bool IDal.addOrder(Order order)
        {
            if (DataSourceList.Orders.Any(guest => guest.OrderKey == order.OrderKey))
            {
                return false;
            }
            DataSourceList.Orders.Add(order.Clone());
            return true;
        }
        Order IDal.getOrder(int id)
        {
            Order result = (from o in DataSourceList.Orders
                            where o.OrderKey == id
                            select o).FirstOrDefault();

            return result.Clone();
        }
        bool IDal.updateOrder(GuestRequest newInfo)
        {
            throw new NotImplementedException();
        }
        IEnumerable<Order> IDal.reurenAllOrder(Func<Order,bool> predicate=null)
        {
            if (predicate == null)
                return DataSourceList.Orders.AsEnumerable();
            return DataSourceList.Orders.Where(predicate);
        }
        #endregion

        #region HosingUnit
        bool IDal.addHostinUnit(HostingUnit hostingUnit)
        {
            foreach (var currentHostingUnit in DataSourceList.HostingUnits)
            {
                if (currentHostingUnit.Equals(hostingUnit)) return false;
            }
            DataSourceList.HostingUnits.Add(hostingUnit);
            return true;
        }
        bool IDal.deleteHostingUnit(HostingUnit hostingUnit)
        {
            throw new NotImplementedException();
        }
        bool IDal.updateHostingUnit(HostingUnit hostingUnit)            // עדכון יחידת אירוח
        {
            foreach (var currentHostingUnit in DataSourceList.HostingUnits)
            {
                if (currentHostingUnit.Equals(hostingUnit)) return false;
            }
            DataSourceList.HostingUnits.Add(hostingUnit);
            return true;
        }

        bool updateHostingUnitStatus(HostingUnit hostingUnit)            // עדכון יחידת אירוח
        {
            //To-Do
            return true;
        }
        IEnumerable<HostingUnit> IDal.returnHostingUnitList(Func<HostingUnit, bool> predicate = null)
        {
            if (predicate == null)
                return DataSourceList.HostingUnits.AsEnumerable();
            return DataSourceList.HostingUnits.Where(predicate);
        }
        #endregion
        IEnumerable<GuestRequest> IDal.returnAllCastumer(Func<GuestRequest, bool> predicate = null)
        {
            if (predicate == null)
                return DataSourceList.GuestRequests.AsEnumerable();
            return DataSourceList.GuestRequests.Where(predicate);
        }
        /*
        IEnumerable<string> IDal.returnAllCastumer()                     // קבלת רשימת כל הלקוחות.
        {  
            List<string> result = new List<string>();
            foreach (var order in DataSourceList.GuestRequests)
            {
                result.Add(order.PrivateName + " " + order.FamilyName);
            }
            return result;
           
        }    
        */
        IEnumerable<string> IDal.returnAllLocelBank()                          // קבלת רשימת כל סניפי הבנק הקיימים בארץ 
        {
            return new List<string> { "poelim", "marcntil", "laomi", "disceunt", "pagi" };
        }


        /*
        public List<HostingUnit> returnHostingUnitList(Host host)
        {
            throw new NotImplementedException();
        }

        List<string> IDal.returnAllCastumer()
        {
            throw new NotImplementedException();
        }

        public List<Order> reurenAllOrder()
        {
            throw new NotImplementedException();
        }

        List<string> IDal.returnAllLocelBank()
        {
            throw new NotImplementedException();
        }
          */

    }
}


