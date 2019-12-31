using BE;
using System;
using System.Collections.Generic;

namespace DAL
{
    public interface IDal
    {
        bool addOrder(Order order);
        Order getOrder(int id);

        void updateOrder(int OrderKey, Status status);
        //bool updateOrder(GuestRequest newInfo);                     // עדכון דרישת לקוח.
        bool addHostinUnit(HostingUnit hostingUnit);                // הוספת יחידת אירוח
        bool deleteHostingUnit(HostingUnit hostingUnit);            // מחיקת יחידת אירוח
        bool updateHostingUnit(HostingUnit hostingUnit);            // עדכון יחידת אירוח

        /*
        List<HostingUnit> returnHostingUnitList(Host host);         // קבלת רשימת כל יחידות האירוח
        List<string> returnAllCastumer();                           // קבלת רשימת כל הלקוחות.
        List<Order> reurenAllOrder();                               // קבלת רשימת כל ההזמנות
        List<string> returnAllLocelBank();                          // קבלת רשימת כל סניפי הבנק הקיימים בארץ 
        */
        IEnumerable<HostingUnit> returnHostingUnitList(Func<HostingUnit, bool> predicate = null);  // קבלת רשימת כל יחידות האירוח
        IEnumerable<GuestRequest> returnAllCastumer(Func<GuestRequest, bool> predicate = null);                    // קבלת רשימת כל הלקוחות.
        IEnumerable<Order> reurenAllOrder(Func<Order, bool> predicate = null);                        // קבלת רשימת כל ההזמנות
        IEnumerable<string> returnAllLocelBank();                   // קבלת רשימת כל סניפי הבנק הקיימים בארץ 



    }
}