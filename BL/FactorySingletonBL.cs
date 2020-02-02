using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class FactorySingletonBL
    {
        private static MyBl instance = null;

        static FactorySingletonBL() { }

        public static MyBl Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MyBl();
                }
                return instance;
            }
        }

    }
}
