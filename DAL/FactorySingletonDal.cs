using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class FactorySingletonDal
    {
        private static IDAL instance = null;

        static FactorySingletonDal() { }

        public static IDAL Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DALImp();
                }
                return instance;
            }
        }

    }
}
