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

        /// <summary>
        /// Add Order To List
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public bool AddOrderToList(Order order)
        {
          //  DataSourceList.Orders.Add(order.Clone());
            DataSourceList.Orders.Add(order);
            return true;
        }

        /// <summary>
        ///  Update Order
        /// </summary>
        /// <param name="OrderKey"></param>
        /// <param name="status"></param>
        public void UpdateOrder(int OrderKey,OrderStatus status)
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

        /// <summary>
        /// Returen Orders from list using predicate for filtering
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<Order> ReturenAllOrders(Func<Order,bool> predicate=null)
        {
            if (predicate == null)
                return DataSourceList.Orders.AsEnumerable();
            return DataSourceList.Orders.Where(predicate);
        }

        #endregion

        #region ///// HosingUnit /////

        /// <summary>
        /// Add Hosting Unit To List
        /// </summary>
        /// <param name="hostingUnit"></param>
        /// <returns></returns>
        public bool AddHostingUnitToList(HostingUnit hostingUnit)
        {
            foreach (var currentHostingUnit in DataSourceList.HostingUnits)
            {
                if (currentHostingUnit.Equals(hostingUnit)) return false;
            }
            DataSourceList.HostingUnits.Add(hostingUnit);
            return true;
        }

        /// <summary>
        /// Delete Hosting Unit from list
        /// </summary>
        /// <param name="hu"></param>
        /// <returns></returns>
        public bool DeleteHostingUnit(HostingUnit hu)
        {
            return DataSourceList.HostingUnits.Remove(hu);
        }

        /// <summary>
        /// Update Hosting Unit
        /// </summary>
        /// <param name="hu"></param>
        /// <returns></returns>
        public bool UpdateHostingUnit(HostingUnit hu)
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

        /// <summary>
        /// Returen Hosting Unit from list using predicate for filtering
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<HostingUnit> ReturnHostingUnitList(Func<HostingUnit, bool> predicate = null)
        {
            if (predicate == null)
                return DataSourceList.HostingUnits.AsEnumerable();
            return DataSourceList.HostingUnits.Where(predicate);
        }

        #endregion

        #region ///// Host /////

        /// <summary>
        /// Add Host To List
        /// </summary>
        /// <param name="host"></param>
        public void AddHostToList(Host host)
        {
            DataSourceList.Hosts.Add(host);
        }

        /// <summary>
        /// Returen Hosts from list using predicate for filtering
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<Host> returnHostList(Func<Host, bool> predicate = null)
        {
            if (predicate == null)
                return DataSourceList.Hosts.AsEnumerable();
            return DataSourceList.Hosts.Where(predicate);
        }
        #endregion Host

        #region ///// GuestRequest /////

        /// <summary>
        /// Returen Guest Request from list using predicate for filtering
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<GuestRequest> ReturnGuestRequestList(Func<GuestRequest, bool> predicate = null)
        {
            if (predicate == null)
                return DataSourceList.GuestRequests.AsEnumerable();
            return DataSourceList.GuestRequests.Where(predicate);
        }

        /// <summary>
        ///Add Guest Request To List
        /// </summary>
        /// <param name="gr"></param>
        public void AddGuestRequestToList(GuestRequest gr)
        {
           // DataSourceList.GuestRequests.Add(Cloning.Copy(gr));
            DataSourceList.GuestRequests.Add(gr);
        }
        #endregion GuestRequest

        #region ///// Bank /////

        /// <summary>
        /// Return All Locel Bank
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> ReturnAllLocelBank()
        {
            return new List<string> { "poelim", "marcntil", "laomi", "disceunt", "pagi" };
        }

        #endregion

        #region ///// In progres /////

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
        public static IEnumerable<T> GetResultByCondtion<T>(IEnumerable<T> src, Func<T, bool> predicate)
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

        #endregion

    }
}


