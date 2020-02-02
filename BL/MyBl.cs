using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
<<<<<<< Updated upstream
using DAL;
=======
using System.Reflection;
using DAL;
using System.Net.Mail;
using System.Net;
using System.Windows;
using System.Threading.Tasks;

>>>>>>> Stashed changes
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
                for (int day = y.EntryDate.Day; day < y.ReleaseDate.Day||y.ReleaseDate.Month!=month; day++)
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
                if(x.Diary[y.EntryDate.Month, day] = true)
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
        public bool IsAccountCharged(Host host)j
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
<<<<<<< Updated upstream
    }
}
=======


        public Dictionary<Area, int> GuestRequestPerArea()
        {
            return new Dictionary<Area, int>
            {
                { Area.Center , GuestRequestBy().Count(p => p.Area == Area.Center) },
                { Area.Jerusalem , GuestRequestBy().Count(p => p.Area == Area.Jerusalem) },
                { Area.North , GuestRequestBy().Count(p => p.Area == Area.North) } ,
                { Area.South , GuestRequestBy().Count(p => p.Area == Area.South) }
            };
        }
        public Dictionary<String, int> GuestRequestPerRquirement(Requirements requirements)
        {
            return new Dictionary<String, int>
            {
                { "Pool"                   , GuestRequestBy().Count(p => p.Pool == requirements) },
                { "Jacuzzi"                , GuestRequestBy().Count(p => p.Jacuzzi              == requirements) },
                { "Garden"                 , GuestRequestBy().Count(p => p.Garden               == requirements) },
                { "ChildrensAttractions"   , GuestRequestBy().Count(p => p.ChildrensAttractions == requirements) },
                { "SpredBads"              , GuestRequestBy().Count(p => p.SpredBads            == requirements) },
                { "AirCondsner"            , GuestRequestBy().Count(p => p.AirCondsner          == requirements) },
                { "frisider"               , GuestRequestBy().Count(p => p.frisider             == requirements) },
                { "SingogNaerBy"           , GuestRequestBy().Count(p => p.SingogNaerBy         == requirements) },
                { "NaerPublicTrensportion" , GuestRequestBy().Count(p => p.NaerPublicTrensportion==requirements) }
               };
        }


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
        /// Get Banks by predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<BankDetails> GetBanks(Func<BankDetails, bool> predicate = null)
        {
            if (predicate == null)
                return myDAL.ReturnAllLocelBank().AsEnumerable();
            return myDAL.ReturnAllLocelBank().Where(predicate);
        }


        /// <summary>
        /// return list of all the husting uint key for spasific host 
        /// </summary>
        /// <param name="hostKey"></param>
        /// <returns></returns>
        public IEnumerable<int> GetHostingUnitsKeysList(int hostKey)
        {
            List<int> result = new List<int>();

            foreach (var hostingUnit in myDAL.ReturnHostingUnitList(x => x.Owner.HostKey == hostKey))
            { result.Add(hostingUnit.HostingUnitKey); }
            return result;
        }

        /// <summary>
        /// Return host list
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Host> GetHosts()
        {
            //return myDAL.returnHostList();
            return myDAL.getAllHosts();
        }

        //need to replace the one in idal with this
        public void UpdateOrder(Order order)
        {
            try
            {
                myDAL.updateOrder(order);
            }
            catch (Exception ex)
            {
                throw new Exception("Can't update order" + ex);
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
    


        /// <summary>
        /// Gruping Order Hosts By Number Of Hosting Unit
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
        /// Number of Hosting Unit per area
        /// </summary>
        /// <returns>Dictionary<Area, int></returns>
        public Dictionary<Area, int> HostingUnitPerArea()
        {
            return new Dictionary<Area, int>
            {
                { Area.Center , HustingUnitsBy().Count(p => p.Area == Area.Center) },
                { Area.Jerusalem , HustingUnitsBy().Count(p => p.Area == Area.Jerusalem) },
                { Area.North , HustingUnitsBy().Count(p => p.Area == Area.North) } ,
                { Area.South , HustingUnitsBy().Count(p => p.Area == Area.South) }
            };
        }


        /// <summary>
        /// Average Orders per hosting unit
        /// </summary>
        /// <returns></returns>
        public double averageOrdersPerHostingUnit()
        {
            double sum = 0;
            foreach (var hostingUnit in HustingUnitsBy())
            {
                sum += OrdersPerUnit(hostingUnit);
            }
            return sum;
        }


        ///// <summary>
        ///// BootingUp the progrem.  
        ///// 1) Receive bank data from the web
        ///// 2)
        ///// </summary>
        //public void bootingUp()
        //{
        //    try {
        //        GetBankInfoFromTheWeb();
        //    } catch (Exception ex) {
        //        // throw new Exception("Can't get bank info from the web " + ex);
        //        MessageBox.Show("Can't get bank info from the web\n " + ex);
        //    }

        //}




        /// <summary>
        /// Sending Email to client 
        /// </summary>
        public void SendMail(Order order)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(GuestRequestBy(x => x.GuestRequestKey == order.GuestRequestKey).First().MailAddress);
            mail.From = new MailAddress(GetHostingUnit(order.HostingUnitKey).Owner.MailAddress);
            mail.Subject = "Resort offeras as you request";
            mail.Body = "<p>You'r request: </p>" +
                GuestRequestBy(x => x.GuestRequestKey == order.GuestRequestKey).First().ToString() +
                "<p>------------------</p>" +
                "<p>Our offer is: </p>" +
                GetHostingUnit(order.HostingUnitKey).ToString() +
                "<p>Please notify us with return mail to this address: </p>" +
                GetHostingUnit(order.HostingUnitKey).Owner.MailAddress;
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Credentials = new System.Net.NetworkCredential("ipewzner@g.jct.ac.il", "Reptor17");
            smtp.EnableSsl = true;

            try
            {
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                throw new Exception("Email error " + ex);
            }


        }

        /// <summary>
        /// Send email with new password
        /// </summary>
        /// <param name="host"></param>
        public void SendMailWithNewPassword(Host host)
        {
            // Manager manager = new Manager();
            // manager.MailAddress = "dotnetproject2020@gmail.com";
            var rand = new Random();
            int password = rand.Next(10000000, 99999999);
            host.PasswordKey = KeyForPassword(password);

            MailMessage mail = new MailMessage();
            mail.To.Add(host.MailAddress);
            // mail.From = new MailAddress("ipewzner@g.jct.ac.il");
            mail.From = new MailAddress(" dotnetproject2020 @gmail.com");

            mail.Subject = "New password - do not replay!";
            mail.Body = "You'r new password is: " + password +
                "<p>Please change your password after the next login</p>" +
                "<p>You can  change your password in the Host updata section</p>";
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Credentials = new System.Net.NetworkCredential("dotnetproject2020@gmail.com", "kuku4ever");
            smtp.EnableSsl = true;

            try
            {
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                throw new Exception("Email error " + ex);
            }

        }

        /// <summary>
        /// Checke if password is curect
        /// </summary>
        /// <param name="key"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool CheckePassword(double key, int password)
        {
            // return Math.Sin(password) * Math.Sqrt(password) == key;
            return true;
        }

        /// <summary>
        /// Make key for password
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public double KeyForPassword(int password)
        {
            return Math.Sin(password) * Math.Sqrt(password);
        }

        

        public double averageOrdersPerClient()
        {
            double sum = 0;
            foreach (var guestRequest in GuestRequestBy())
            {
                sum += OrdersPerClient(guestRequest);
            }
            return sum;
        }
    }
}

>>>>>>> Stashed changes
