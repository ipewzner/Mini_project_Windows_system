using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace BE
{
     [Serializable]
        public class Manager
    {
        

            public int ManagerKey { get; set; }
            public String PrivateName { get; set; }
            public String FamilyName { get; set; }
            public String PhoneNumber { get; set; }
            public String MailAddress { get; set; }
            public double PasswordKey { get; set; }
            public BankAccount BankAccount { get; set; }
            public String CollectionClearance { get; set; }

            public override string ToString()
            {
                return this.TostringProperties();
            }

       
    }

}

