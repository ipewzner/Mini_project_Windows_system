using System;
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
        /// Add new Host
        /// </summary>
        public bool AddHost(Host host)
        {
            if(myDAL.returnHostList((x) => (x == host)) != null)
            {
                //myDAL.AddHostToList(host);
                return true;
            }
            else
            {
                return false;
            }
    
        }

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

            Order order = instance.getOrder(neworder.OrderKey);
            if (order.Status == OrderStatus.CloseByClient || order.Status == OrderStatus.CloseByClientTimeOut)
            {
                return false;
            }
            else
            {
                instance.addOrder(neworder);
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

                //TODO bring the orginals and remove x and y
                HostingUnit x = GetHostingUnit(Convert.ToInt32(order.HostingUnitKey));
                GuestRequest y = GetGusetRequest(Convert.ToInt32(order.GuestRequestKey));
                int month = y.EntryDate.Month;
                for (int day = y.EntryDate.Day; day < y.ReleaseDate.Day || y.ReleaseDate.Month != month; day++)
                {
                    x.Diary[y.EntryDate.Month, day] = true;

                    if (day == 31)
                    {
                        day = 0;
                        month++;
                    }

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
            int month = start.Month;
            for (int day = start.Day; day < end.Day || end.Month != month; day++)
            {
                if (x.Diary[start.Month, day] == true)
                {
                    return false;
                }

                if (day == 31)
                {
                    day = 0;
                    month++;
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
            foreach (var item in myDAL.reurenAllOrders())
            {
                if (item.GuestRequestKey == req.GuestRequestKey.ToString())
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
            foreach (var item in myDAL.reurenAllOrders())
            {
                if (item.HostingUnitKey == unit.HostingUnitKey.ToString())
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
            string x ="";
            foreach (var item in myDAL.reurenAllOrders())
            {
                if (item.HostingUnitKey == unit.ToString())
                {
                    if (item.Status == OrderStatus.UntreatedYet)
                    {
                        return false;
                    }
                    x = item.HostingUnitKey;
                }
            }
            HostingUnit y = GetHostingUnit(Convert.ToInt32(x));
            myDAL.deleteHostingUnit(y);
            return true;
        }

        /// <summary>
        /// Return all units available in given date range
        /// </summary>
        public List<HostingUnit> UintsAvailable(DateTime start, int numOfDays)
        {
            List<HostingUnit> listOfUnits = new List<HostingUnit>();

            DateTime end = start.AddDays(numOfDays);
            var x = myDAL.returnHostingUnitList(null);
            foreach (var item in x)
            {
                int month = start.Month;
                for (int day = start.Day; day < end.Day || end.Month != month; day++)
                {

                    if (!item.Diary[start.Month, day] == true)
                    {
                        break;
                    }

                    if (day == 31)
                    {
                        day = 0;
                        month++;
                    }
                }

                listOfUnits.Add(item);

            }

            return listOfUnits;
        }

        /// <summary>
        /// Return Guest Request By any requirment
        /// </summary>
        public IEnumerable<GuestRequest> GuestRequestBy(Func<GuestRequest, bool> predicate = null)
        {
            if (predicate == null)
                return myDAL.returnGuestRequestList().AsEnumerable();
            return myDAL.returnGuestRequestList().Where(predicate);
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

                foreach (var guest in GuestRequestBy((x => x.Area == item.UnitArea)))
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
            return myDAL.reurenAllOrders(x => NumOfDays(x.CreateDate) == days).ToList(); ;
        }


        #region ///// Helpers /////

        GuestRequest GetGusetRequest(int key)
        {
            return (myDAL.returnGuestRequestList(x => x.GuestRequestKey == key)).First();
        }
            
        public HostingUnit GetHostingUnit(int key)
        {
            return (myDAL.returnHostingUnitList(x => x.HostingUnitKey == key)).First();
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

        public IEnumerable<IGrouping<Area, GuestRequest>> GuestRequestOrderBy_Location()
        {
            IEnumerable<IGrouping<Area, GuestRequest>> result =
                from gr in myDAL.returnGuestRequestList()
                group gr by gr.Area;
            return result;
        }
         
        public IEnumerable<IGrouping<int, GuestRequest>> GuestRequest_OrderBy_NumberOfVacationers()
        {
            IEnumerable<IGrouping<int, GuestRequest>> result =
                  from gr in myDAL.returnGuestRequestList()
                  group gr by (gr.Adults + gr.Children);
            return result;
        }
         
        public IEnumerable<IGrouping<int, Host>> Hosts_OrderBy_NumberOfHostingUnit()
        {
            IEnumerable<IGrouping<int, Host>> result =
                     from hosts in myDAL.returnHostList()
                     group hosts by NumOfHostingUnitsInHost(hosts);
            return result;
        }
         
        public int NumOfHostingUnitsInHost(Host host)
        {
            int sum = 0;
            foreach (var hu in myDAL.returnHostingUnitList())
            {
                if (hu.HostKey == host.HostKey)
                    sum++;
            }
            return sum;
        }

        public IEnumerable<IGrouping<Area, HostingUnit>> HostingUnit_OrderBy_Location()
        {
            IEnumerable<IGrouping<Area, HostingUnit>> result =
                    from hu in myDAL.returnHostingUnitList()
                    group hu by hu.UnitArea;
            return result;
        }
        #endregion

    }
}