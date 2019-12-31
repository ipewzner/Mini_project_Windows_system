using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;
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
                HostingUnit x = new HostingUnit();
                GuestRequest y = new GuestRequest();
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
                //duffrante between close by app to close by client??
                y.Status = ClientStatus.CloseByApp;


            }
        }

        /// <summary>
        /// Is date available
        /// </summary>
        public bool IsDateAvailable(DateTime start, DateTime end)
        {
            HostingUnit x = new HostingUnit();
            GuestRequest y = new GuestRequest();
            int month = y.EntryDate.Month;
            for (int day = y.EntryDate.Day; day < y.ReleaseDate.Day || y.ReleaseDate.Month != month; day++)
            {
                if (x.Diary[y.EntryDate.Month, day] = true)
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


        #region ///// NOT IMPLAMENT /////
        public bool IsAccountCharged(Host host)
        {
            //HOW TO CHECK IF ACCOUNT CHARGED??
            throw new NotImplementedException();
    }

    public int OrdersPerClient(GuestRequest req)
    {
        //COLACTION??
        throw new NotImplementedException();
    }

    public int OrdersPerUnit(HostingUnit unit)
    {
        //COLACTION??
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