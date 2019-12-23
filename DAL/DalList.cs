using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DataSource;

namespace DAL
{
    class DalList : IDal
    {
        public bool addOrder(Order order)
        {
            if (DataSourceList.Orders.Any(guest => guest.OrderKey == order.OrderKey))
            {
                return false;
            }
            DataSourceList.Orders.Add(order.Clone());
            return true;
        }

        public Order getOrder(int id)
        {
            Order result = (from o in DataSourceList.Orders
                            where o.OrderKey == id
                            select o).FirstOrDefault();

            return result.Clone();
        }


       public bool updateHostingUnit(HostingUnit hostingUnit)            // עדכון יחידת אירוח
        {
            foreach (var currentHostingUnit in DataSourceList.HostingUnits)
            {
                if (currentHostingUnit.Equals(hostingUnit)) return false;
            }
            DataSourceList.HostingUnits.Add(hostingUnit);
            return true;
        }

      public  List<string> returnAllCastumer()                     // קבלת רשימת כל הלקוחות.
        {
            /*
            {
            Order result = (from o in DataSourceList.GuestRequests
                            select o.FamilyName +o.PrivateName);

            return result.Clone();
             }
            */


            List<string> result=new List<string>();
            foreach (var order in DataSourceList.GuestRequests)
            {
                result.Add( order.PrivateName+" "+order.FamilyName );
            }
            return result;
        }
      
      public  List<string> returnAllLocelBank()                          // קבלת רשימת כל סניפי הבנק הקיימים בארץ 
        {
            return new List<string> { "poelim","marcntil","laomi","disceunt","pagi" };
        }
      
      public   bool updateOrder(GuestRequest newInfo)
 {
            foreach (var currentGuestRequest in DataSourceList.GuestRequests)
            {
                if (currentGuestRequest.Equals(newInfo)) return false;
            }
            DataSourceList.GuestRequests.Add(newInfo);
            return true;
        }
       
      public  bool addHostinUnit(HostingUnit hostingUnit)                 {return true;}
           
      public  bool deleteHostingUnit(HostingUnit hostingUnit)              {return true;}
          
        

      public  List<HostingUnit> returnHostingUnitList(Host host)  
{
             return (from o in DataSourceList.HostingUnits
                            where o.Owner==host
                            select o).ToList();
                   }
       
      public  List<Order> reurenAllOrder()  
      {
return null;
 }
        
     


}
}


