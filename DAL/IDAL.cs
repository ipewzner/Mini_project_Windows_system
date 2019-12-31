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
        bool AddHostingUnitToList(HostingUnit hostingUnit);
        bool deleteHostingUnit(HostingUnit hostingUnit);           
        bool updateHostingUnit(HostingUnit hostingUnit);           

        IEnumerable<HostingUnit> returnHostingUnitList(Func<HostingUnit, bool> predicate = null);  
        IEnumerable<GuestRequest> returnGuestRequestList(Func<GuestRequest, bool> predicate = null);    
        IEnumerable<Order> reurenAllOrders(Func<Order, bool> predicate = null);                     
        IEnumerable<string> returnAllLocelBank();                   



    }
}