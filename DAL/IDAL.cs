using BE;
using System;
using System.Collections.Generic;

namespace DAL
{
    public interface IDAL
    {
        bool addOrder(Order order);
        Order getOrder(int id);

        void updateOrder(int OrderKey, OrderStatus status);
        //bool updateOrder(GuestRequest newInfo);                  
        bool AddHostingUnitToList(HostingUnit hostingUnit);
        bool deleteHostingUnit(HostingUnit hostingUnit);           
        //bool deleteHostingUnit(int hostingUnitKey);           
        bool updateHostingUnit(HostingUnit hostingUnit);           

        /*
        List<HostingUnit> returnHostingUnitList(Host host);        
        List<string> returnAllCastumer();                          
        List<Order> reurenAllOrder();                              
        List<string> returnAllLocelBank();                         
        */
        IEnumerable<HostingUnit> returnHostingUnitList(Func<HostingUnit, bool> predicate = null);  
        IEnumerable<GuestRequest> returnGuestRequestList(Func<GuestRequest, bool> predicate = null);    
        IEnumerable<Order> reurenAllOrders(Func<Order, bool> predicate = null);                     
        IEnumerable<string> returnAllLocelBank();                   



    }
}