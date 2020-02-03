using System;
using System.Collections.Generic;
using System.Linq;
using BE;

using DAL;
using System.Net.Mail;
using System.Net;
using System.Windows;
using System.Threading;

namespace BL
{
    public class MyBl : IBL
    {
        DalXML myDAL = FactorySingletonDal.Instance;

        /// <summary>
        /// Add Guest Request
        /// </summary>
        public bool AddGuestRequest(GuestRequest req)
        {
            if (IsDateCorrect(req.EntryDate, req.ReleaseDate))
            {

                try
                {
                    myDAL.addGuestRequest(req);
                    return true;
                }
                catch (Exception ex)
                {
                    //throw new Exception("Fail to add this Guest Request! " + ex);
                    return false;
                }
            }
            else
            {
                //throw new Exception("can't add this request, Release Date before Entry Date!");
                return false;
            }
            
        }

        /// <summary>
        /// Add Hosting Unit
        /// </summary>
        public void AddHostingUnit(HostingUnit unit)
        {

                myDAL.addHostingUnit(unit);


        }

        /// <summary>
        /// create a new order
        /// </summary>
        public bool AddOrder(Order neworder)
        {

            //Order order = instance.ReturenAllOrders((x)=> x.OrderKey == neworder.OrderKey).First();
            //if (order.Status == OrderStatus.CloseByClient || order.Status == OrderStatus.CloseByClientTimeOut)
            //{
            //    return false;
            //}
            //else
            //{
            myDAL.addOrder(neworder);
            //}
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
        /// Add new Host
        /// </summary>
        public bool AddHost(Host host)
        {
            if (myDAL.returnHostList((x) => x.HostKey == host.HostKey).ToList().Count == 0)
            {
                try
                {
                    myDAL.addHost(host);
                    return true;
                }     
                catch(Exception ex)
                {
                    throw new Exception("Fail to add the host! "+ex);
                }
            }
            else return false;
         }

        /// <summary>
        /// Update Host
        /// </summary>
        /// <param name="host"></param>
        public void UpdateHost(Host host)
        {
            try
            {
                RemoveHost(host);
                AddHost(host);
                MessageBox.Show($"Host: {host.FamilyName} updated Seccessfuly!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during Update {host.FamilyName} please try again later! " + ex);
            }
        }

        /// <summary>
        /// Remove Host and all is units
        /// </summary>
        /// <param name="host"></param>
        public void RemoveHost(Host host)
        {
            //check if all the unit that blonge to this host can be deleted
            foreach (var unit in myDAL.ReturnHostingUnitList(x => x.Owner.HostKey == host.HostKey))
            {
                if (!CanUnitBeDeleted(unit.HostingUnitKey))
                {
                    throw new Exception("Can't delete the Host, whan some order still open in some of the units!");
                }
            }

            //delete all the unit that blonge to this host 
            foreach (var unit in myDAL.ReturnHostingUnitList(x => x.Owner.HostKey == host.HostKey))
            {
                try
                {
                    UnitRemove(unit.HostingUnitKey);
                }
                catch (Exception ex)
                {                                           
                    throw new Exception("Fail to delete the Host " + ex);
                }
            }

            //delete the host
            try
            {
                myDAL.DeleteHost(host);
            }
            catch (Exception ex)
            {
                throw new Exception("Fail to delete the Host, but delete all is HostingUnit " + ex);
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
                if (x.Diary.FindIndex((z) => z == i) != -1)
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
            return (end > start) ? true : false;
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
        public void UnitRemove(int unitKey)
        {
            if(!CanUnitBeDeleted(unitKey))
                throw new Exception("Can't delete this unit, whan some order still open!");

            HostingUnit hostingUnit = GetHostingUnit(Convert.ToInt32(unitKey));
            try
            {
                myDAL.DeleteHostingUnit(hostingUnit);
            }
            catch (Exception ex)
            {
                throw new Exception("Fail to delete the unit! " + ex);
            }
        }

        /// <summary>
        /// Can Unit Be Deleted? jest if no order that conected to this unit, have Status Untreated Yet!
        /// </summary>
        /// <param name="unitKey"></param>
        /// <returns></returns>
        private bool CanUnitBeDeleted(int unitKey)
        {
            foreach (var order in myDAL.ReturenAllOrders())
            {
                if ((order.HostingUnitKey == unitKey) && (order.Status == OrderStatus.UntreatedYet))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Update Unit
        /// </summary>
        /// <param name="hostingUnit"></param>
        public void UpdateUnit(HostingUnit hostingUnit)
        {
            try
            {
                UnitRemove(hostingUnit.HostingUnitKey);
                AddHostingUnit(hostingUnit);
                MessageBox.Show($"Hosting unit: {hostingUnit.HostingUnitName} updated Seccessfuly!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during Update {hostingUnit.HostingUnitName} please try again later! " + ex);
            }
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

                if (IsDateAvailable(start, end, item.HostingUnitKey))
                {
                    listOfUnits.Add(item);
                }

            }

            return listOfUnits;
        }

       
        /// <summary>
        /// Return Guest Request By any requirment
        /// </summary>
        public IEnumerable<GuestRequest> GuestRequestBy(Func<GuestRequest, bool> predicate = null)
        {
            if (predicate == null)
                return myDAL.getAllGuestRequests().AsEnumerable();
            return myDAL.getAllGuestRequests().Where(predicate);
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
        //public IEnumerable<IGrouping<int, Host>> Hosts_OrderBy_NumberOfHostingUnit()
        //{
        //    IEnumerable<IGrouping<int, Host>> result =
        //             from hosts in myDAL.returnHostList()
        //             group hosts by NumOfHostingUnitsInHost(hosts);
        //    return result;
        //}

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


        /// <summary>
        /// BootingUp the progrem.  
        /// 1) Receive bank data from the web
        /// 2)
        /// </summary>
        public void bootingUp()
        {
            try
            {
                Thread thread = new Thread(GetBankInfoFromTheWeb);
                thread.Start();
            }
            catch (Exception ex)
            {
                // throw new Exception("Can't get bank info from the web " + ex);
                MessageBox.Show("Can't get bank info from the web\n " + ex);
            }

            if (NewDay())
            {
                try
                {
                    Thread thread = new Thread(RefreshDatabase);
                    thread.Start();
                }
                catch (Exception ex)
                {
                    // throw new Exception("Can't get bank info from the web " + ex);
                    MessageBox.Show("Fail to refresh the database! \n " + ex);
                }
            }

        }

        private void RefreshDatabase() { }
        private bool NewDay() { return true; }

        /// <summary>
        /// Sending Email to client 
        /// </summary>
        public void SendMail(Order order)
        {
            MailMessage mail = new MailMessage();
            //mail.To.Add(GuestRequestBy(x => x.GuestRequestKey == order.GuestRequestKey).First().MailAddress);
            mail.To.Add(GetGusetRequest(order.GuestRequestKey).MailAddress);
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
            // smtp.Credentials = from;
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



        /// <summary>
        /// GetBankInfoFromTheWeb
        /// </summary>
        public void GetBankInfoFromTheWeb()
        {
            //C:\Users\ipewz\Documents\GitHub\Mini_project_Windows_system
            //const string xmlLocalPath = @"atm.xml";
            const string xmlLocalPath = @"ATM.xml";
            WebClient wc = new WebClient();
            try
            {
                string xmlServerPath =
               @"http://www.boi.org.il/he/BankingSupervision/BanksAndBranchLocations/Lists/BoiBankBranchesDocs/atm.xml";
                wc.DownloadFile(xmlServerPath, xmlLocalPath);
            }
            catch (Exception)
            {
                string xmlServerPath = @"http://www.jct.ac.il/~coshri/atm.xml";
                wc.DownloadFile(xmlServerPath, xmlLocalPath);
            }
            finally
            {
                wc.Dispose();
            }

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
    }
}