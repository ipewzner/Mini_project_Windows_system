using BE;
using System.Collections.Generic;

namespace DAL
{
    public interface IDal
    {
        bool addOrder(Order order);
        Order getOrder(int id);
 
        bool updateOrder(GuestRequest newInfo);                     
        bool addHostinUnit(HostingUnit hostingUnit);                
        bool deleteHostingUnit(HostingUnit hostingUnit);            
        bool updateHostingUnit(HostingUnit hostingUnit);            
        List<HostingUnit> returnHostingUnitList(Host host);         
        List<string> returnAllCastumer();                           
        List<Order> reurenAllOrder();                               
        List<string> returnAllLocelBank();                          
        }
}