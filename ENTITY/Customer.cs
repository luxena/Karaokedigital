using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ENTITY
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string Boss { get; set; }
        public string CustomerType { get; set; }
        public string Society { get; set; }
        public string PIvaFiscalCode { get; set; }
        public string URL { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string Plan { get; set; }
        public string StartDate { get; set; }
        public string DueDate { get; set; }
        public string Logo { get; set; }
        public string LogoPath { get; set; }
        public IFormFile LogoFile { get; set; }
        public bool IsActive { get; set; }
    }
}
