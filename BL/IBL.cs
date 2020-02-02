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
        bool IsDateAvailable(DateTime start, DateTime end, int unitKey);
        void CloseOrder(Order order);
        void SendMail(Order order);
        void UnitRemove(int unit);

        List<HostingUnit> UintsAvailable(DateTime start, int numOfDays);
        int NumOfDays(DateTime date);
        int NumOfDays(DateTime firstDate, DateTime SecondDate);
        List<Order> OrdersUntilDate(int days);
        int OrdersPerClient(GuestRequest req);
        int OrdersPerUnit(HostingUnit unit);
        bool AddGuestRequest(GuestRequest req);
        void AddHostingUnit(HostingUnit unit);
        IEnumerable<GuestRequest> GuestRequestBy(Func<GuestRequest, bool> predicate = null);
        double averageOrdersPerClient();
        double averageOrdersPerHostingUnit();

    }
}