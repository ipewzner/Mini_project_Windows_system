using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;
namespace BL
{
    public class MyBl
    {
        public bool AddOrder(Order neworder)
        {
            IDal instance = FactorySingletonDal.Instance;

            Order order = instance.getOrder(neworder.OrderKey);
            if (order.Status == Status.CloseByApp || order.Status == Status.CloseByClient)
            {
                return false;
            }
            else
            {
                instance.addOrder(neworder);
            }
            return true;
        }
    }
}
