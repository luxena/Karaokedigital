using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class SubCustomers
    {
        public int SubCustID { get; set; }
        public int CustomerID { get; set; }
        public string Customer { get; set; }
        public int SubCustomerID { get; set; }
        public string SubCustomer { get; set; }
        public bool IsActive { get; set; }
    }
}
