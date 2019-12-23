using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DataSource;
using System.Reflection;

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
       
        //may be need to be externel becuse it used in the PL as wall ***************
        //****************************************************************************
 void autoInfoUpdate<T>(T info)
            {
                Console.Clear();
                Console.WriteLine("Please enter info:");
                foreach (PropertyInfo p in info.GetType().GetProperties())
                {
                    Console.WriteLine("{0,5} , {1}\n", p.Name, p.GetValue(info, null));
                    try
                    {
                        string input = Console.ReadLine();
                        p.SetValue(info, Convert.ChangeType(input, p.PropertyType));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
            }
        //****************************************************************************
      
  public Order getOrder(int id)
        {
            Order result = (from o in DataSourceList.Orders
                            where o.OrderKey == id
                            select o).FirstOrDefault();

            return result.Clone();
        }

            //find the unit hom we want to update and update
       public bool updateHostingUnit(HostingUnit hostingUnit)  
        {
            foreach (var currentHostingUnit in DataSourceList.HostingUnits)
            {
                if (currentHostingUnit.Equals(hostingUnit))
{
  autoInfoUpdate(currentHostingUnit);
return true;
           }
}
            return false;
                }

   /*
     public bool updateHostingUnit(HostingUnit hostingUnit)            // עדכון יחידת אירוח
        {
            foreach (var currentHostingUnit in DataSourceList.HostingUnits)
            {
                if (currentHostingUnit.Equals(hostingUnit)) return false;
            }
            DataSourceList.HostingUnits.Add(hostingUnit);
            return true;
        }
        */
      
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
       

        //------------------------------------
        //maybe use obect insntse of T?
//           public bool addToList<T>(T newObject)
          // public bool addToList(Object newObject)
            //{                                      
            /*
        //   newObject= Convert.ChangeType(newObject.newObject.GetType()) ;
           // newObject.GetType() ;
           // ver type=newObject.GetType()  ;
              //if (DataSourceList.type.Any(current => current.Key == newObject.Key))
              if (DataSourceList.(newObject.GetType()).Any(current => current.Key == newObject.Key))
                    return false;              
                    *-/

               foreach (PropertyInfo list in DataSourceList.GetType().GetProperties())
	{
                if(list.GetType()==newObject.GetType())  {
                    if(list.any(current => current.Key == newObject.Key))return false;
                                  
	list.Add(newObject.Clone());           
}                                                         
}
            return true;
             }


        //------------------------------------
         */

       public bool addHostingUnit(HostingUnit newHostingUnit) {
            if (DataSourceList.HostingUnits.Any(hostingUnit => hostingUnit.HostingUnitKey == newHostingUnit.HostingUnitKey))
            {
                return false;
            }
            DataSourceList.HostingUnits.Add(newHostingUnit);
            return true;
        }
           
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


