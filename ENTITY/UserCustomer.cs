using System;
using System.Collections.Generic;
using System.Text;

namespace ENTITY
{
    public class UserCustomer
    {
        public int UserCustomerID { get; set; }
        public int UserID { get; set; }
        public int CustomerID { get; set; }
        public string Username { get; set; }
        public string Img { get; set; }
        public string Society { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Date { get; set; }
    }
}
