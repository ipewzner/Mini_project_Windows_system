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
        /*
         bool IDal.updateOrder(GuestRequest newInfo)
         {
             throw new NotImplementedException();
         }
         */
        void IDal.updateOrder(int OrderKey,OrderStatus status)
        {
            try
            {
                var g = from w in DataSourceList.Orders
                        where w.OrderKey == OrderKey
                        select w.Status = status;
            }
            catch(Exception ex)
            {
                throw new KeyNotFoundException("OrderKey not feund");
            }
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

        bool IDal.deleteHostingUnit(HostingUnit hu)
        {
            /*
        bool IDal.deleteHostingUnit(int HostingUnitKey)
        {
            HostingUnit hu = DataSourceList.HostingUnits.find(HostingUnitKey);
                if (hu == null) throw new Exception("hosting Unit with the same Hosting Unit Key not found...");
                
                //shude be in the BL layer
                if (!hu.Diary.empty()) throw new Exception("this husting unit is still in use wwe can't delete it");
           */
            return DataSourceList.HostingUnits.Remove(hu);
        }
        bool IDal.updateHostingUnit(HostingUnit hu)            // עדכון יחידת אירוח
        {
            //Remove old
            try
            {
                DataSourceList.HostingUnits.Remove(DataSourceList.HostingUnits.Find(x => x.HostingUnitKey == hu.HostingUnitKey));
            }
            catch(Exception ex)
            {
                throw ex;
            }
         
            //insert new
            DataSourceList.HostingUnits.Add(hu);
            return true;
        }
        /*  isn't status to update
        bool updateHostingUnitStatus(HostingUnit hostingUnit)            
        {
            DataSourceList.HostingUnits.Find(x => x.HostingUnitKey == hu.HostingUnitKey);
            return true;
        } 
        */
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


