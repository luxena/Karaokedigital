using ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Karaokedigital.Models
{
    public class CustomerTypeModel
    {
        public int CustomerTypeID { get; set; }
        public string Type { get; set; }

        public void MapFromCustomerType(CustomerType customerType)
        {
            CustomerTypeID = customerType.CustomerTypeID;
            Type = customerType.Type;
        }

        public CustomerType MapIntoCustomerType()
        {
            return new CustomerType
            {
                CustomerTypeID = CustomerTypeID,
                Type = Type
             };
        }
    }
}
