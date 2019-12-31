﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;
using DataSource;

namespace BL
{
    public class MyBl : IBL
    {
        /// <summary>
        /// create a new order
        /// </summary>
        public bool AddOrder(Order neworder)
        {
            IDal instance = FactorySingletonDal.Instance;

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
                HostingUnit x = FindHostUnit(Convert.ToInt32(order.HostingUnitKey));
                GuestRequest y = FindGuset(Convert.ToInt32(order.GuestRequestKey));
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

            HostingUnit x = FindHostUnit(unitKey);
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
            foreach (var item in DataSourceList.Orders)
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
            foreach (var item in DataSourceList.Orders)
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
        public bool UnitCanBeRemove(HostingUnit unit)
        {
            foreach (var item in DataSourceList.Orders)
            {
                if (item.HostingUnitKey == unit.HostingUnitKey.ToString())
                {
                    if (item.Status == OrderStatus.UntreatedYet)
                    {
                        return false;
                    }

                }
            }
            return true;
        }


        #region ///// Helpers /////

        GuestRequest FindGuset(int key)
        {
            return DataSourceList.GuestRequests.Find(x => x.GuestRequestKey == key);
        }

        HostingUnit FindHostUnit(int key)
        {
            return DataSourceList.HostingUnits.Find(x => x.HostingUnitKey == key);
        }

        #endregion

        #region ///// NOT IMPLAMENT /////
        public bool IsAccountCharged(Host host)
        {
            //HOW TO CHECK IF ACCOUNT CHARGED??
            throw new NotImplementedException();
        }

        public List<Order> OrdersUntilDate(int days)
        {
            throw new NotImplementedException();
        }

        public List<HostingUnit> UintsAvailable(DateTime start, int numOfDays)
        {
            throw new NotImplementedException();
        }

        #endregion


    }
}