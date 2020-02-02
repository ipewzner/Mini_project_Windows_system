using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using BE;
using DataSource;
using System.Net;

namespace DAL
{
    public class DalXML : IDAL
    {
        private static int serialGuestRequest;
        private static int serialOrder;
       // private static double commision;  //10 shekels -- zol meod public DalXML()
       // private static string serialHostingUnit;

        public DalXML()
        {
            GetAndStoreBankInfo();

            serialOrder = Int32.Parse(DataSource.DataSourceXML.Orders.Element("lastSerial").Value);
            serialGuestRequest = Int32.Parse(DataSource.DataSourceXML.GuestRequests.Element("lastSerial").Value);
       

        }

        public bool addGuestRequest(GuestRequest gr)
        {
            XElement guestRequestElement = XElement.Parse(gr.ToXMLstring());
            DataSource.DataSourceXML.GuestRequests.Element("lastSerial").Value = guestRequestElement.Element("GuestRequestKey").Value;
            DataSource.DataSourceXML.SaveGuestRequests();
            DataSource.DataSourceXML.GuestRequests.Add(guestRequestElement);
            DataSource.DataSourceXML.SaveGuestRequests();
            return true;
        }

        public bool addHost(Host host)
        {
            DataSource.DataSourceXML.Hosts.Add(XElement.Parse(host.ToXMLstring()));
             DataSource.DataSourceXML.SaveHosts();
            //DataSource.DataSourceXML.Hosts.Element("lastSerial").Value = host.HostKey.ToXMLstring();
            DataSource.DataSourceXML.SaveHosts();
            return true;
        }

        public bool addHostingUnit(HostingUnit HostingUnit)
        {
            DataSource.DataSourceXML.HostingUnits.Add(XElement.Parse(HostingUnit.ToXMLstring()));
            DataSource.DataSourceXML.SaveHosts();
            //DataSource.DataSourceXML.HostingUnits.Element("lastSerial").Value = HostingUnit.HostingUnitKey.ToXMLstring();
            DataSource.DataSourceXML.SaveHostingUnits();
            return true;
        }

        public bool addOrder(Order neworder)
        {
            // XElement findOrder = (from o in DataSource.DataSourceXML.Orders.Elements("Order")
            //                      where Int32.Parse(o.Element("OrderKey").Value) == neworder.OrderKey
            //                      select o).FirstOrDefault();
            //if(findOrder!=null)
            //{
            //    //throw new Exception("order alraedy exist");
            //    return false;
            //}
            serialOrder = Int32.Parse(DataSource.DataSourceXML.Orders.Element("lastSerial").Value);
            neworder.OrderKey = ++serialOrder;
            DataSource.DataSourceXML.Orders.Add(neworder.ToXML());
            DataSource.DataSourceXML.SaveOrders();
            DataSource.DataSourceXML.Orders.Element("lastSerial").Value = neworder.OrderKey.ToString();
            DataSource.DataSourceXML.SaveOrders();
            return true;
        }

        public List<GuestRequest> getAllGuestRequests()
        {
            return (from o in DataSource.DataSourceXML.GuestRequests.Elements("GuestRequest")
                    select o.ToString().ToObject<GuestRequest>()).ToList();
        }

        public List<HostingUnit> getAllHostingUnits()
        {
            return (from o in DataSource.DataSourceXML.HostingUnits.Elements("HostingUnit")
                    select o.ToString().ToObject<HostingUnit>()).ToList();
        }

        public List<Host> getAllHosts()
        {
            return (from o in DataSource.DataSourceXML.Hosts.Elements("Host")
                    select o.ToString().ToObject<Host>()).ToList();
        }

        public List<Order> getAllorders()
        {
            return (from o in DataSource.DataSourceXML.Orders.Elements("Order")
                          select o.ToString().ToObject<Order>()).ToList();
        }

        public GuestRequest getGuestRequest(int id)
        {
            throw new NotImplementedException();
        }

        public Host getHost(int id)
        {
            throw new NotImplementedException();
        }

        public HostingUnit getHostingUnit(int id)
        {
            throw new NotImplementedException();
        }

        public Order getOrder(int id)
        {
            Order result = null;
            XElement findOrder = (from o in DataSource.DataSourceXML.Orders.Elements("Order")
                                  where Int32.Parse(o.Element("OrderKey").Value) == id
                                  select o).FirstOrDefault();
            if (findOrder != null)
            {
                 result = findOrder.ToString().ToObject<Order>();
            }

            return result;

        }

        public string getserialGuestRequest()
        {
            String result = DataSource.DataSourceXML.GuestRequests.Element("lastSerial").Value;
            return result;
        }

        public bool updateGuestRequest(GuestRequest guestRequest)
        {
            throw new NotImplementedException();
        }

        public bool updateHost(Host host)
        {
            throw new NotImplementedException();
        }

        public bool updateOrder(Order updateorder)
        {
            XElement findOrder = (from o in DataSource.DataSourceXML.Orders.Elements("Order")
                                  where Int32.Parse(o.Element("OrderKey").Value) == updateorder.OrderKey
                                  select o).FirstOrDefault();

            if (findOrder == null)
            {
                return false;
            }
  
            return true;
        }

        public void UpdateOrder(int OrderKey, OrderStatus status)
        {
            throw new NotImplementedException();
        }


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
        /// Returen Orders from list using predicate for filtering
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<Order> ReturenAllOrders(Func<Order, bool> predicate = null)
        {
            try
            {
                if (predicate == null)
                    return Cloning.Clone(getAllorders().AsEnumerable());
                return Cloning.Clone(getAllorders().Where(predicate));
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
                    return Cloning.Clone(getAllHostingUnits().AsEnumerable());
                return Cloning.Clone(getAllHostingUnits().Where(predicate));
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
                    return Cloning.Clone(getAllHosts().AsEnumerable());
                return Cloning.Clone(getAllHosts().Where(predicate));
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
                    return Cloning.Clone(getAllGuestRequests().AsEnumerable());
                return Cloning.Clone(getAllGuestRequests().Where(predicate));
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
        public IEnumerable<BankDetails> ReturnAllLocelBank(Func<BankDetails, bool> predicate = null)
        {
            try
            {
                if (predicate == null)
                    return DataSourceList.banks.AsEnumerable();
                return DataSourceList.banks.Where(predicate);
            }
            catch (Exception ex)
            {
                throw new Exception("Fail to retrieve the Guest-Request from the list " + ex);
            }
        }

        #endregion


        /// <summary>
        /// GetBankInfoFromTheWeb
        /// </summary>
        public void GetAndStoreBankInfo()
        {
            //C:\Users\ipewz\Documents\GitHub\Mini_project_Windows_system
            //const string xmlLocalPath = @"atm.xml";
            const string xmlLocalPath = @"ATM.xml";
            WebClient wc = new WebClient();
            Task.Run(() =>
            {
                try
                {
                    string xmlServerPath = @"http://www.jct.ac.il/~coshri/atm.xml";
                    wc.DownloadFile(xmlServerPath, xmlLocalPath);
                }
                catch (Exception)
                {
                    string xmlServerPath =
                    @"http://www.boi.org.il/he/BankingSupervision/BanksAndBranchLocations/Lists/BoiBankBranchesDocs/atm.xml";
                    wc.DownloadFile(xmlServerPath, xmlLocalPath);
                }
                finally
                {
                    wc.Dispose();
                }

                try
                {

                    XmlRootAttribute xRoot = new XmlRootAttribute();
                    xRoot.ElementName = "ATMs";
                    xRoot.IsNullable = true;

                    XmlSerializer serializer = new XmlSerializer(typeof(List<ATM>), xRoot);

                    using (FileStream stream = File.OpenRead("ATM.xml"))
                    {
                        List<ATM> dezerializedList = (List<ATM>)serializer.Deserialize(stream);
    
                        foreach (var item in dezerializedList)
                        {
                            int findBank = DataSourceList.banks.FindIndex((x) => x.BankNumber == Convert.ToInt32(item.קוד_בנק));
                            if (findBank == -1)
                            {
                                DataSourceList.banks.Add(new BankDetails { 
                                    BankName = item.שם_בנק,
                                    BankNumber = Convert.ToInt32(item.קוד_בנק),
                                    Branches = new List<BankBranch>()
                                    {
                                        new BankBranch
                                        {
                                            BranchCity = item.ישוב,
                                            BranchNumber =  Convert.ToInt32(item.קוד_סניף)

                                        }
                                    }
                                
                                });
                            }
                            else
                            {
                                int findBranch = DataSourceList.banks[findBank].Branches.FindIndex((x) => x.BranchNumber == Convert.ToInt32(item.קוד_סניף));
                                if (findBranch == -1)
                                {
                                    DataSourceList.banks[findBank].Branches.Add(new BankBranch
                                    {
                                        BranchNumber = Convert.ToInt32( item.קוד_סניף),
                                        BranchCity = item.ישוב
                                    });
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Fail to return the Locel-Bank list " + ex);
                }
            });


        }



    }
}



