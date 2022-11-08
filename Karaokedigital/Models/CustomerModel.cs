using ENTITY;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Karaokedigital.Models
{
    public class CustomerModel
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
        public string MainCustomer { get; set; }

        public void MapFromCustomer(Customer customer)
        {
            CustomerID = customer.CustomerID;
            Boss = customer.Boss;
            CustomerType = customer.CustomerType;
            Society = customer.Society;
            PIvaFiscalCode = customer.PIvaFiscalCode;
            URL = customer.URL;
            Phone = customer.Phone;
            Email = customer.Email;
            Country = customer.Country;
            Province = customer.Province;
            City = customer.City;
            Address = customer.Address;
            ZipCode = customer.ZipCode;
            Plan = customer.Plan;
            StartDate = customer.StartDate;
            DueDate = customer.DueDate;
            Logo = customer.Logo;
            LogoFile = customer.LogoFile;
            LogoPath = customer.LogoPath;
            IsActive = customer.IsActive;
            MainCustomer = customer.MainCustomer;
        }
        public Customer MapIntoCustomer()
        {
            return new Customer
            {
                CustomerID = CustomerID,
                Boss = Boss,
                CustomerType = CustomerType,
                Society = Society,
                PIvaFiscalCode = PIvaFiscalCode,
                URL = URL,
                Phone = Phone,
                Email = Email,
                Country = Country,
                Province = Province,
                City = City,
                Address = Address,
                ZipCode = ZipCode,
                Plan = Plan,
                StartDate = StartDate,
                DueDate = DueDate,
                Logo = Logo,
                LogoFile = LogoFile,
                LogoPath = LogoPath,
                IsActive = IsActive,
                MainCustomer = MainCustomer
            };
        }
    }
}
