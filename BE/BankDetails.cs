using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BankDetails
    {
        public int BankNumber { get; set; }
        public String BankName { get; set; }
        public List<BankBranch> Branches;
    }

    public class BankBranch
    {
        public int BranchNumber { get; set; }
        public String BranchCity { get; set; }
    }
}
