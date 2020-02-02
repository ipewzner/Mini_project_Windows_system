using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BE;
using DataSource;


namespace DAL
{
    public class DALImp 
    {

        #region ///// Order /////

        /// <summary>
        /// Add Order To List
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public bool AddOrderToList(Order order)
        {
            try
            {
                DataSourceList.Orders.Add(Cloning.Copy(order));
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Fail to add Order to the list " + ex);
            }
        }

        /// <summary>
        ///  Update Order
        /// </summary>
        /// <param name="OrderKey"></param>
        /// <param name="status"></param>
        public void UpdateOrder(int OrderKey, OrderStatus status)
        {
            try
            {
                //var temp = from order in DataSourceList.Orders
                //           where order.OrderKey == OrderKey
                //           select order.Status = status;

                int index = DataSourceList.Orders.FindIndex(x => x.OrderKey == OrderKey);
                if (index != -1)
                {
                    DataSourceList.Orders[index].Status = status;
                }
            }
            catch (Exception ex)
            {
                throw new KeyNotFoundException("OrderKey not feund" + ex);
            }
        }

        /// <summary>
        /// Returen Orders from list using predicate for filtering
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<Order> ReturenAllOrders(Func<Order, bool> predicate = null)
        {
            try
            {
                if (predicate == null)
                    return Cloning.Clone(DataSourceList.Orders.AsEnumerable());
                return Cloning.Clone(DataSourceList.Orders.Where(predicate));
            }
            catch (Exception ex)
            {
                throw new Exception("Fail to retrieve the Orders from the list " + ex);
            }
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
            try
            {
                foreach (var currentHostingUnit in DataSourceList.HostingUnits)
                {
                    if (currentHostingUnit.Equals(hostingUnit)) return false;
                }
                DataSourceList.HostingUnits.Add(Cloning.Copy(hostingUnit));
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Fail to add the Hosting-Unit to the list " + ex);
            }
        }

        /// <summary>
        /// Delete Hosting Unit from list
        /// </summary>
        /// <param name="hu"></param>
        /// <returns></returns>
        public bool DeleteHostingUnit(HostingUnit hu)
        {
            try
            {
                return DataSourceList.HostingUnits.Remove(hu);
            }
            catch (Exception ex)
            {
                throw new Exception("Fail to delete the Hosting-Unit " + ex);
            }
        }

        /// <summary>
        /// Update Hosting Unit
        /// </summary>
        /// <param name="hu"></param>
        /// <returns></returns>
        public bool UpdateHostingUnit(HostingUnit hostingUnit)
        {
            //Remove old
            try
            {
                try
                {
                    DataSourceList.HostingUnits.Remove(DataSourceList.HostingUnits.Find(x => x.HostingUnitKey == hostingUnit.HostingUnitKey));
                }
                catch (Exception ex)
                {
                    throw new Exception("can't remove the old Hosting-Unit " + ex);
                }

                //insert new
                try
                {
                    DataSourceList.HostingUnits.Add(Cloning.Copy(hostingUnit));
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception("can't add the new Hosting-Unit " + ex);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Fail to update Hosting-Unit becuse it " + ex);
            }
        }

        /// <summary>
        /// Returen Hosting Unit from list using predicate for filtering
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<HostingUnit> ReturnHostingUnitList(Func<HostingUnit, bool> predicate = null)
        {
            try
            {
                if (predicate == null)
                    return Cloning.Clone(DataSourceList.HostingUnits.AsEnumerable());
                return Cloning.Clone(DataSourceList.HostingUnits.Where(predicate));
            }
            catch (Exception ex)
            {
                throw new Exception("Fail to retrieve the Hosting-Units from the list " + ex);
            }
        }

        #endregion

        #region ///// Host /////

        /// <summary>
        /// Add Host To List
        /// </summary>
        /// <param name="host"></param>
        public void AddHostToList(Host host)
        {
            try
            {
                DataSourceList.Hosts.Add(Cloning.Copy(host));
            }
            catch (Exception ex)
            {
                throw new Exception("Fail to add the Host to the list " + ex);
            }
        }

        /// <summary>
        /// Returen Hosts from list using predicate for filtering
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<Host> returnHostList(Func<Host, bool> predicate = null)
        {
            try
            {
                if (predicate == null)
                    return Cloning.Clone(DataSourceList.Hosts.AsEnumerable());
                return Cloning.Clone(DataSourceList.Hosts.Where(predicate));
            }
            catch (Exception ex)
            {
                throw new Exception("Fail to retrieve the Hosts from the list " + ex);
            }
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
            try
            {
                if (predicate == null)
                    return Cloning.Clone(DataSourceList.GuestRequests.AsEnumerable());
                return Cloning.Clone(DataSourceList.GuestRequests.Where(predicate));
            }
            catch (Exception ex)
            {
                throw new Exception("Fail to retrieve the Guest-Request from the list " + ex);
            }
        }

        /// <summary>
        ///Add Guest Request To List
        /// </summary>
        /// <param name="gr"></param>
        public void AddGuestRequestToList(GuestRequest gr)
        {
            try
            {
                DataSourceList.GuestRequests.Add(Cloning.Copy(gr));
            }
            catch (Exception ex)
            {
                throw new Exception("Fail to add the Guest Request to the list " + ex);
            }
        }
        #endregion GuestRequest

        #region ///// Bank /////

        /// <summary>
        /// Return All Locel Bank
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> ReturnAllLocelBank()
        {
            try
            {
                return new List<string> { "poelim", "marcntil", "laomi", "disceunt", "pagi" };
            }
            catch (Exception ex)
            {
                throw new Exception("Fail to return the Locel-Bank list " + ex);
            }
        }

        #endregion





    }
}


