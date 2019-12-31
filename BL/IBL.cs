using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    interface IBL
    {
        bool IsDateCorrect(DateTime start, DateTime end);
        bool IsAccountCharged(Host host);
        bool IsDateAvailable(DateTime start, DateTime end);
        void CloseOrder(Order order);
        void SendMail(Order order);

        List<HostingUnit> UintsAvailable(DateTime start, int numOfDays);
        int NumOfDays(DateTime date);
        int NumOfDays(DateTime firstDate, DateTime SecondDate);
        List<Order> OrdersUntilDate(int days);
        int OrdersPerClient(GuestRequest req);
        int OrdersPerUnit(HostingUnit unit);



    }
}