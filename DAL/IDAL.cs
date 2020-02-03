﻿using BE;
using System;
using System.Collections.Generic;

namespace DAL
{
    public interface IDAL
    {
        bool AddOrderToList(Order order);
        bool AddHostingUnitToList(HostingUnit hostingUnit);

        void UpdateOrder(int OrderKey, OrderStatus status);             
        bool UpdateHostingUnit(HostingUnit hostingUnit);

        bool DeleteHostingUnit(HostingUnit hostingUnit);
        bool DeleteHost(Host host);


        IEnumerable<HostingUnit> ReturnHostingUnitList(Func<HostingUnit, bool> predicate = null);  
        IEnumerable<GuestRequest> ReturnGuestRequestList(Func<GuestRequest, bool> predicate = null);    
        IEnumerable<Order> ReturenAllOrders(Func<Order, bool> predicate = null);
        IEnumerable<BankDetails> ReturnAllLocelBank(Func<BankDetails, bool> predicate = null);
                   
        IEnumerable<string> ReturnAllLocelBank();                   


    }
}