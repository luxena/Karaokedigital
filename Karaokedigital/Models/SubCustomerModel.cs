using ENTITY;
using System.Security.AccessControl;

namespace Karaokedigital.Models
{
    public class SubCustomerModel
    {
        public int SubCustID { get; set; }
        public int CustomerID { get; set; }
        public string Customer { get; set; }
        public int SubCustomerID { get; set; }
        public string SubCustomer { get; set; }
        public bool IsActive { get; set; }

        public void MapFromSubCustomer(SubCustomers subCustomer)
        {
            SubCustID = subCustomer.SubCustID;
            CustomerID = subCustomer.CustomerID;
            SubCustomerID = subCustomer.SubCustomerID;
            Customer = subCustomer.Customer;
            SubCustomer = subCustomer.SubCustomer;
            IsActive = subCustomer.IsActive;
        }

        public SubCustomers MapIntoSubCustomer()
        {
            return new SubCustomers
            {

                SubCustID = SubCustID,
                CustomerID = CustomerID,
                SubCustomerID = SubCustomerID,
                Customer = Customer,
                SubCustomer = SubCustomer,
                IsActive = IsActive

             };
        }
    }
}
