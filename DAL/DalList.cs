using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DataSource;
//using IDAL;



namespace DAL
{
    public class DalList : IDAL
    {
        
        #region Order
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
        /*
         bool IDAL.updateOrder(GuestRequest newInfo)
         {
             throw new NotImplementedException();
         }
         */
        public void updateOrder(int OrderKey,OrderStatus status)
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
        public IEnumerable<Order> reurenAllOrders(Func<Order,bool> predicate=null)
        {
            if (predicate == null)
                return DataSourceList.Orders.AsEnumerable();
            return DataSourceList.Orders.Where(predicate);
        }
        #endregion

        #region HosingUnit
                
        public bool AddHostingUnitToList(HostingUnit hostingUnit)
        {
            foreach (var currentHostingUnit in DataSourceList.HostingUnits)
            {
                if (currentHostingUnit.Equals(hostingUnit)) return false;
            }
            DataSourceList.HostingUnits.Add(hostingUnit);
            return true;
        }
            
        public bool deleteHostingUnit(HostingUnit hu)
        {
         /*
         bool IDAL.deleteHostingUnit(int huKey)
         {
             HostingUnit hu = DataSourceList.HostingUnits.Find(x => x.HostingUnitKey == huKey);
             if (hu == null) throw new Exception("hosting Unit with the same Hosting Unit Key not found...");

                 //shude be in the BL layer
                 if (!hu.Diary.empty()) throw new Exception("this husting unit is still in use wwe can't delete it");
        */
            return DataSourceList.HostingUnits.Remove(hu);
        }
        public bool updateHostingUnit(HostingUnit hu)
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
        public IEnumerable<HostingUnit> returnHostingUnitList(Func<HostingUnit, bool> predicate = null)
        {
            if (predicate == null)
                return DataSourceList.HostingUnits.AsEnumerable();
            return DataSourceList.HostingUnits.Where(predicate);
        }
        #endregion
        public IEnumerable<GuestRequest> returnGuestRequestList(Func<GuestRequest, bool> predicate = null)
        {
            if (predicate == null)
                return DataSourceList.GuestRequests.AsEnumerable();
            return DataSourceList.GuestRequests.Where(predicate);
        }
        /*
        IEnumerable<string> IDAL.returnAllCastumer()                     // קבלת רשימת כל הלקוחות.
        {  
            List<string> result = new List<string>();
            foreach (var order in DataSourceList.GuestRequests)
            {
                result.Add(order.PrivateName + " " + order.FamilyName);
            }
            return result;
           
        }    
        */
        public IEnumerable<string> returnAllLocelBank()                          // קבלת רשימת כל סניפי הבנק הקיימים בארץ 
        {
            return new List<string> { "poelim", "marcntil", "laomi", "disceunt", "pagi" };
        }

        public void AddGuestRequestToList(GuestRequest gr)
        {
           DataSourceList.GuestRequests.Add(gr);
        }
        

    }
}


