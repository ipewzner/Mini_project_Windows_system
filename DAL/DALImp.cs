using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BE;
using DataSource;


namespace DAL
{
    public class DALImp : IDAL
    {
        
        #region ///// Order /////

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

        public void updateOrder(int OrderKey,OrderStatus status)
        {
            try
            {
                var g = from w in DataSourceList.Orders
                        where w.OrderKey == OrderKey
                        select w.Status = status;
            }
            catch(Exception)
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

        #region ///// HosingUnit /////
                
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

        public IEnumerable<HostingUnit> returnHostingUnitList(Func<HostingUnit, bool> predicate = null)
        {
            if (predicate == null)
                return DataSourceList.HostingUnits.AsEnumerable();
            return DataSourceList.HostingUnits.Where(predicate);
        }

        #endregion

        public void AddHostToList(Host host)
        {
            DataSourceList.Hosts.Add(host);
        }
        public IEnumerable<GuestRequest> returnGuestRequestList(Func<GuestRequest, bool> predicate = null)
        {
            if (predicate == null)
                return DataSourceList.GuestRequests.AsEnumerable();
            return DataSourceList.GuestRequests.Where(predicate);
        }
       
        public IEnumerable<string> returnAllLocelBank()
        { 
            return new List<string> { "poelim", "marcntil", "laomi", "disceunt", "pagi" };
        }

        public IEnumerable<Host> returnHostList(Func<Host, bool> predicate = null)
        {
            if (predicate == null)
                return DataSourceList.Hosts.AsEnumerable();
            return DataSourceList.Hosts.Where(predicate);
        }

        public void AddGuestRequestToList(GuestRequest gr)
        {
            DataSourceList.GuestRequests.Add(gr);
        }


        /*
         public IEnumerable<Object> returnWishList(Func<Object, bool> predicate = null)
         {

             foreach (PropertyInfo p in DataSourceList.GetType().GetProperties())
             {
                 result += String.Format("{0,-25} , {1}\n", p.Name, p.GetValue(t, null));
             }

             if (predicate == null)
                 return DataSourceList.(predicate.Target).AsEnumerable();
             return DataSourceList.B.Where(predicate);
         }
           */
        public static IEnumerable<T> GetResultByCondtion<T>( IEnumerable<T> src, Func<T, bool> predicate)
        {
            if (predicate == null)
            {
                return src;
            }
            return src.Where(predicate);
           // var result = GetResultByCondtion<GuestRequest>(DataSourceList.GuestRequests, c => (c.Adults+c.Children) > 20);
        }

    /*
            public IEnumerable<Object> returnQueryList(Func<Object, bool> predicate = null)
        {
            if (this.Equals( DataSourceList.GuestRequests.GetType()))
            {
                if (predicate == null)
                    return DataSourceList.GuestRequests.AsEnumerable();
                return DataSourceList.GuestRequests.Where(predicate);
            }
            if (this.Equals( DataSourceList.HostingUnits.GetType()))
            {
                if (predicate == null)
                    return DataSourceList.HostingUnits.AsEnumerable();
                return DataSourceList.HostingUnits.Where(predicate);
            }
            if (this.Equals( DataSourceList.Hosts.GetType()))
            {
                if (predicate == null)
                    return DataSourceList.Hosts.AsEnumerable();
                return DataSourceList.Hosts.Where(predicate);
            }
            
                if (predicate == null)
                    return DataSourceList.Orders.AsEnumerable();
                return DataSourceList.Orders.Where(predicate);
            
        }
               
      */



    }
}


