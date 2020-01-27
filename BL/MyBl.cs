using System;
using System.Collections.Generic;
using System.Linq;
using BE;
using System.Reflection;

using DAL;
using System.Net.Mail;
using System.Net;
using System.Windows;

namespace BL
{
    public class MyBl : IBL
    {
        DALImp myDAL = new DALImp();
      
        #region GuestRequest
      
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
        /// Return Guest Request By any requirment
        /// </summary>
        public IEnumerable<GuestRequest> GuestRequestBy(Func<GuestRequest, bool> predicate = null)
        {
            if (predicate == null)
                return myDAL.ReturnGuestRequestList().AsEnumerable();
            return myDAL.ReturnGuestRequestList().Where(predicate);
        }


        /// <summary>
        /// Number of Guest Request per area
        /// </summary>
        /// <returns>Dictionary<Area, int></returns>
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

        /// <summary>
        /// Number of Guest-request per rquirement
        /// </summary>
        /// <param name="requirements"></param>
        /// <returns></returns>
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
        /// Gruping Order Guest Request By Location
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
        /// Gruping Order Guest Request By Number Of Vacationers
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IGrouping<int, GuestRequest>> GuestRequest_OrderBy_NumberOfVacationers()
        {
            IEnumerable<IGrouping<int, GuestRequest>> result =
                  from gr in myDAL.ReturnGuestRequestList()
                  group gr by (gr.Adults + gr.Children);
            return result;
        }

        #endregion GuestRequest
        
        #region Host

        /// <summary>
        /// Add Host
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
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
        /// Return host list
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Host> GetHosts()
        {
            return myDAL.returnHostList();
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

        #endregion Host

        //update
        #region HostingUnit

        /// <summary>
        /// Add Hosting Unit
        /// </summary>
        public void AddHostingUnit(HostingUnit unit)
        {
            myDAL.AddHostingUnitToList(unit);
        }

        /// <summary>
        /// Check if Unit can be remove
        /// </summary>
        public bool UnitRemove(int unit)
        {
            int x = 0;
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
            HostingUnit y = GetHostingUnit(Convert.ToInt32(unit));
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

                if (IsDateAvailable(start, end, item.HostingUnitKey))
                {
                    listOfUnits.Add(item);
                }

            }

            return listOfUnits;
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
        /// Find Hosting Unit by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public HostingUnit GetHostingUnit(int key)
        {
            return (myDAL.ReturnHostingUnitList(x => x.HostingUnitKey == key)).First();
        }

        /// <summary>
        /// Return Husting uinits By any requirment
        /// </summary>
        public IEnumerable<HostingUnit> HustingUnitsBy(Func<HostingUnit, bool> predicate = null)
        {
            return myDAL.ReturnHostingUnitList(predicate);
        }

        /// <summary>
        /// Gruping Order Hosting Unit By Location
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

        #endregion HostingUnit
        //new husting unit
        #region Order

        /// <summary>
        /// create a new order
        /// </summary>
        public bool AddOrder(Order neworder)
        {
            IDAL instance = FactorySingletonDal.Instance;

            //Order order = instance.ReturenAllOrders((x)=> x.OrderKey == neworder.OrderKey).First();
            //if (order.Status == OrderStatus.CloseByClient || order.Status == OrderStatus.CloseByClientTimeOut)
            //{
            //    return false;
            //}
            //else
            //{
                instance.AddOrderToList(neworder);
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

                HostingUnit hostingUnit = GetHostingUnit(Convert.ToInt32(order.HostingUnitKey));
                GuestRequest guestRequest = GuestRequestBy(x=>x.GuestRequestKey== order.GuestRequestKey).First();
                for (DateTime dateTime = guestRequest.EntryDate; dateTime <= guestRequest.ReleaseDate; dateTime.AddDays(1))
                {
                    hostingUnit.Diary.Add(dateTime);
                }
                //TODO add Commision
                //To Where??

                //Change client STATUS 
                guestRequest.Status = ClientStatus.CloseByApp;

            }
        }

        //need to replace the one in idal with this
        public void UpdateOrder(Order order)
        {
            try
            {
                myDAL.UpdateOrder(order.OrderKey, order.Status);
            }
            catch (Exception ex)
            {
                throw new Exception("Can't update order" + ex);
            }
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
        /// Return all Orders that created X dayes before 
        /// </summary>
        public List<Order> OrdersUntilDate(int days)
        {
            return myDAL.ReturenAllOrders(x => NumOfDays(x.CreateDate) == days).ToList(); ;
        }

        /// <summary>
        /// Average Orders per client
        /// </summary>
        /// <returns></returns>
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
       
        #endregion Order

        #region Date

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

        #endregion Date

        /// <summary>
        /// BootingUp the progrem.  
        /// 1) Receive bank data from the web
        /// 2)
        /// </summary>
        public void bootingUp()
        {
            try{
                GetBankInfoFromTheWeb();
            } catch (Exception ex){
             // throw new Exception("Can't get bank info from the web " + ex);
                MessageBox.Show("Can't get bank info from the web\n "+ex );
            }

        }




        /// <summary>
        /// Sending Email to client 
        /// </summary>
        public void SendMail(Order order)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(GuestRequestBy(x => x.GuestRequestKey == order.GuestRequestKey).First().MailAddress);
            mail.From = new MailAddress(GetHostingUnit(order.HostingUnitKey).Owner.MailAddress);
            mail.Subject = "Resort offeras as you request";
            mail.Body = "<p>You'r request: </p>"+
                GuestRequestBy(x => x.GuestRequestKey == order.GuestRequestKey).First().ToString()+
                "<p>------------------</p>"+
                "<p>Our offer is: </p>"+
                GetHostingUnit(order.HostingUnitKey).ToString()+
                "<p>Please notify us with return mail to this address: </p>"+
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
            int password = rand.Next(10000000,99999999);
            host.PasswordKey = KeyForPassword(password);

            MailMessage mail = new MailMessage();
            mail.To.Add(host.MailAddress);
           // mail.From = new MailAddress("ipewzner@g.jct.ac.il");
            mail.From = new MailAddress(" dotnetproject2020 @gmail.com");

            mail.Subject = "New password - do not replay!";
            mail.Body = "You'r new password is: "+ password +
                "<p>Please change your password after the next login</p>"+
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
        public bool CheckePassword(double key,int password)
        {
            return Math.Sin( password )*Math.Sqrt(password)==key;
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

        public bool IsAccountCharged(Host host)
        {
            //HOW TO CHECK IF ACCOUNT CHARGED??
            throw new NotImplementedException();
        }

        /// <summary>
        /// GetBankInfoFromTheWeb
        /// </summary>
        public void GetBankInfoFromTheWeb()
        {
            //C:\Users\ipewz\Documents\GitHub\Mini_project_Windows_system
            //const string xmlLocalPath = @"atm.xml";
            const string xmlLocalPath = @"C:\Users\ipewz\Documents\GitHub\Mini_project_Windows_system\ATM.xml";
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
    }
}