﻿using System;
using System.Collections.Generic;
using System.Linq;
using BE;
using System.Reflection;

using DAL;

namespace BL
{
    public class MyBl : IBL
    {
        DALImp myDAL = new DALImp();

        /// <summary>
        /// Add Guest Request
        /// </summary>
        public bool AddGuestRequest(GuestRequest req)
        {
            //TODO
            //check id and name and move to DAL
            myDAL.AddGuestRequestToList(req);
            return true;
        }

        /// <summary>
        /// Add Hosting Unit
        /// </summary>
        public void AddHostingUnit(HostingUnit unit)
        {
            myDAL.AddHostingUnitToList(unit);
        }

        /// <summary>
        /// create a new order
        /// </summary>
        public bool AddOrder(Order neworder)
        {
            IDAL instance = FactorySingletonDal.Instance;

            Order order = instance.ReturenAllOrders((x)=> x.OrderKey == neworder.OrderKey).First();
            if (order.Status == OrderStatus.CloseByClient || order.Status == OrderStatus.CloseByClientTimeOut)
            {
                return false;
            }
            else
            {
                instance.AddOrderToList(neworder);
            }
            return true;
        }

        /// <summary>
        /// Close the order and handle the Implications
        /// </summary>
        public void CloseOrder(Order order)
        {
            if (order.Status == OrderStatus.CloseByClient)
            {
                //TODO:
                //close status for changes

                HostingUnit x = GetHostingUnit(Convert.ToInt32(order.HostingUnitKey));
                GuestRequest y = GetGusetRequest(Convert.ToInt32(order.GuestRequestKey));
                for (DateTime i = y.EntryDate; i <= y.ReleaseDate; i.AddDays(1))
                {
                    x.Diary.Add(i);
                }


                //TODO add Commision
                //To Where??

                //Change client STATUS 
                y.Status = ClientStatus.CloseByApp;

            }
        }

        /// <summary>
        /// Is date available
        /// </summary>
        public bool IsDateAvailable(DateTime start, DateTime end, int unitKey)
        {

            HostingUnit x = GetHostingUnit(unitKey);
            start.AddDays(1);
            for (DateTime i = start; i < end; i.AddDays(1))
            {
               if(x.Diary.FindIndex((z) => z == i) != -1 )
                {
                    return false;
                }
            }

            return true;

        }

        /// <summary>
        /// Check if end day later than the start date
        /// </summary>
        public bool IsDateCorrect(DateTime start, DateTime end)
        {
            if (end > start)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Return number of days from date until now
        /// </summary>
        public int NumOfDays(DateTime date)
        {
            return (int)(DateTime.Now - date).TotalDays;
        }

        /// <summary>
        /// Return number of days between 2 Date Times OBJ
        /// </summary>
        public int NumOfDays(DateTime firstDate, DateTime SecondDate)
        {
            return (int)(SecondDate - firstDate).TotalDays;
        }

        /// <summary>
        /// Sending Email to client 
        /// </summary>
        public void SendMail(Order order)
        {
            if (order.Status == OrderStatus.MailSent)
            {
                Console.WriteLine(
                    $"You order:\n" +
                    $"Create Date: {order.CreateDate}\n" +
                    $"Order Date: {order.OrderDate}\n" +
                    $"Hosting Unit Key: {order.HostingUnitKey}\n"
                    );
            }
        }

        /// <summary>
        /// Return number of orders that sent to client
        /// </summary>
        public int OrdersPerClient(GuestRequest req)
        {
            int counter = 0;
            foreach (var item in myDAL.ReturenAllOrders())
            {
                if (item.GuestRequestKey == req.GuestRequestKey)
                {
                    if (item.Status == OrderStatus.MailSent)
                        counter++;
                }
            }
            return counter;
        }

        /// <summary>
        /// Return number of orders that seccessfully close
        /// </summary>
        public int OrdersPerUnit(HostingUnit unit)
        {
            int counter = 0;
            foreach (var item in myDAL.ReturenAllOrders())
            {
                if (item.HostingUnitKey == unit.HostingUnitKey)
                {
                    if (item.Status == OrderStatus.CloseByClient)
                        counter++;
                }
            }
            return counter;
        }

        /// <summary>
        /// Check if Unit can be remove
        /// </summary>
        public bool UnitRemove(int unit)
        {
            int x =0;
            foreach (var item in myDAL.ReturenAllOrders())
            {
                if (item.HostingUnitKey == unit)
                {
                    if (item.Status == OrderStatus.UntreatedYet)
                    {
                        return false;
                    }
                    x = item.HostingUnitKey;
                }
            }
            HostingUnit y = GetHostingUnit(Convert.ToInt32(x));
            myDAL.DeleteHostingUnit(y);
            return true;
        }

        /// <summary>
        /// Return all units available in given date range
        /// </summary>
        public List<HostingUnit> UintsAvailable(DateTime start, int numOfDays)
        {
            List<HostingUnit> listOfUnits = new List<HostingUnit>();

            DateTime end = start.AddDays(numOfDays);
            var x = myDAL.ReturnHostingUnitList(null);
            foreach (var item in x)
            {

                if(IsDateAvailable(start, end, item.HostingUnitKey))
                {
                    listOfUnits.Add(item);
                }

            }

            return listOfUnits;
        }

        /// <summary>
        /// Add new Host
        /// </summary>
        public bool AddHost(Host host)
        {
            if (myDAL.returnHostList((x) => (x == host)) != null)
            {
                myDAL.AddHostToList(host);
                return true;
            }
            else
            {
                return false;
            }

        }
       
        /// <summary>
        /// Return Guest Request By any requirment
        /// </summary>
        public IEnumerable<GuestRequest> GuestRequestBy(Func<GuestRequest, bool> predicate = null)
        {
            if (predicate == null)
                return myDAL.ReturnGuestRequestList().AsEnumerable();
            return myDAL.ReturnGuestRequestList().Where(predicate);
        }

        /// <summary>
        /// Return Husting uinits By any requirment
        /// </summary>
        public IEnumerable<HostingUnit> HustingUnitsBy(Func<HostingUnit, bool> predicate = null)
        {
              return myDAL.ReturnHostingUnitList(predicate);
        }


        /// <summary>
        /// Creates Offers by area and available dates
        /// </summary>
        public void CreateOffer(GuestRequest req)
        {
            List<HostingUnit> hostingUnits = UintsAvailable(req.EntryDate, NumOfDays(req.EntryDate, req.EntryDate));
            foreach (var item in hostingUnits)
            {
                Offer y = new Offer();

                foreach (var guest in GuestRequestBy((x => x.Area == item.Area)))
                {
                    y.GuestKey = guest.GuestRequestKey;
                    y.UnitKey = item.HostingUnitKey;
                }

                if (y != null)
                    Offer.ListOfOffers.Add(y);
            }

        }

        /// <summary>
        /// Return all Orders that created X dayes before 
        /// </summary>
        public List<Order> OrdersUntilDate(int days)
        {
            return myDAL.ReturenAllOrders(x => NumOfDays(x.CreateDate) == days).ToList(); ;
        }

        #region ///// Helpers /////

        /// <summary>
        /// Find Guset Request by ID
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        GuestRequest GetGusetRequest(int key)
        {
            return (myDAL.ReturnGuestRequestList(x => x.GuestRequestKey == key)).First();
        }

        /// <summary>
        /// Find Hosting Unit by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public HostingUnit GetHostingUnit(int key)
        {
            return (myDAL.ReturnHostingUnitList(x => x.HostingUnitKey == key)).First();
        }

        #endregion

        #region ///// NOT IMPLAMENTED /////

        public bool IsAccountCharged(Host host)
        {
            //HOW TO CHECK IF ACCOUNT CHARGED??
            throw new NotImplementedException();
        }

        #endregion

        #region ///// Gruping /////

        /// <summary>
        /// Order Guest Request By Location
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IGrouping<Area, GuestRequest>> GuestRequestOrderBy_Location()
        {
            IEnumerable<IGrouping<Area, GuestRequest>> result =
                from gr in myDAL.ReturnGuestRequestList()
                group gr by gr.Area;
            return result;
        }

        /// <summary>
        /// Order Guest Request By Number Of Vacationers
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IGrouping<int, GuestRequest>> GuestRequest_OrderBy_NumberOfVacationers()
        {
            IEnumerable<IGrouping<int, GuestRequest>> result =
                  from gr in myDAL.ReturnGuestRequestList()
                  group gr by (gr.Adults + gr.Children);
            return result;
        }

        /// <summary>
        /// Order Hosts By Number Of Hosting Unit
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IGrouping<int, Host>> Hosts_OrderBy_NumberOfHostingUnit()
        {
            IEnumerable<IGrouping<int, Host>> result =
                     from hosts in myDAL.returnHostList()
                     group hosts by NumOfHostingUnitsInHost(hosts);
            return result;
        }

        /// <summary>
        /// Order Hosting Unit By Location
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IGrouping<Area, HostingUnit>> HostingUnit_OrderBy_Location()
        {
            IEnumerable<IGrouping<Area, HostingUnit>> result =
                    from hu in myDAL.ReturnHostingUnitList()
                    group hu by hu.Area;
            return result;
        }

        /// <summary>
        /// Return the number Of Hosting Units In Host
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        public int NumOfHostingUnitsInHost(Host host)
        {
            int sum = 0;
            foreach (var hu in myDAL.ReturnHostingUnitList())
            {
                if (hu.Owner.HostKey == host.HostKey)
                    sum++;
            }
            return sum;
        }

        #endregion

        

        /// <summary>
        /// Get Orders by predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<Order> GetOrders(Func<Order, bool> predicate = null)
        {
            if (predicate == null)
                return myDAL.ReturenAllOrders().AsEnumerable();
            return myDAL.ReturenAllOrders().Where(predicate);
        }

        /// <summary>
        /// return list of all the husting uint key for spasific host 
        /// </summary>
        /// <param name="hostKey"></param>
        /// <returns></returns>
        public IEnumerable<int> GetHostingUnitsKeysList(int hostKey)
        {
            List<int> result= new List <int>();
            
            foreach (var hostingUnit in myDAL.ReturnHostingUnitList(x => x.Owner.HostKey == hostKey) )
                { result.Add(hostingUnit.HostingUnitKey); }
            return result;
        }

        /// <summary>
        /// Return host list
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Host> GetHosts()
        {
            return myDAL.returnHostList();
        }

        //need to replace the one in idal with this
        public void UpdateOrder(Order order)
        {
            try
            {
                UpdateOrder(order);
            }
            catch(Exception ex) 
            {
                throw new Exception("Can't update order"+ex);
            }
        }

      
        /*
        public IEnumerable<int> GetGuestRequestKeysList<T>(IEnumerable<GuestRequest> list,PropertyInfo propertyInfo)
        {
            List<int> result = new List<int>();

            foreach (var i in list)
            { result.Add(i.GuestRequestKey); }
            { result.Add(i.g; }
            return result;
        }
         */
    }
}