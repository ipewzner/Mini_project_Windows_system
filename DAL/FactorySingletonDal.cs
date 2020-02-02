using DataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class FactorySingletonDal
    {
       
        private static DalXML instance = null;

        static FactorySingletonDal() { }

        public static DalXML Instance
        {

            get
            {

                if (instance == null)
                {
                    instance = new DalXML();
                    
                }
                return instance;
            }
        }

    }
}
