using ENTITY;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Karaokedigital.Models
{
    public class CustomerUserModel
    {
        public int CustomerUserID { get; set; }
        public string Customer { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string DateOfBirth { get; set; }
        public string BornCountry { get; set; }
        public string BornProvince { get; set; }
        public string BornCity { get; set; }
        public string FiscalCode { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string Img { get; set; }
        public string Role { get; set; }
        public IFormFile ImgFile { get; set; }
        public string ImgPath { get; set; }
        public bool IsActive { get; set; }

        public void MapFromCustomerUser(CustomerUser customerUser)
        {
            CustomerUserID = customerUser.CustomerUserID;
            Customer = customerUser.Customer;
            Name = customerUser.Name;
            Surname = customerUser.Surname;
            Username = customerUser.Username;
            Password = customerUser.Password;
            Email = customerUser.Email;
            Phone = customerUser.Phone;
            DateOfBirth = customerUser.DateOfBirth;
            BornCountry = customerUser.BornCountry;
            BornProvince = customerUser.BornProvince;
            BornCity = customerUser.BornCity;
            FiscalCode = customerUser.FiscalCode;
            Country = customerUser.Country;
            Province = customerUser.Province;
            City = customerUser.City;
            Address = customerUser.Address;
            ZipCode = customerUser.ZipCode;
            Img = customerUser.Img;
            Role = customerUser.Role;
            ImgFile = customerUser.ImgFile;
            ImgPath = customerUser.ImgPath;
            IsActive = customerUser.IsActive;
        }
        public CustomerUser MapIntoCustomerUser()
        {
            return new CustomerUser
            {
                CustomerUserID = CustomerUserID,
                Customer = Customer,
                Name = Name,
                Surname = Surname,
                Username = Username,
                Password = Password,
                Email = Email,
                Phone = Phone,
                DateOfBirth = DateOfBirth,
                BornCountry = BornCountry,
                BornProvince = BornProvince,
                BornCity = BornCity,
                FiscalCode = FiscalCode,
                Country = Country,
                Province = Province,
                City = City,
                Address = Address,
                ZipCode = ZipCode,
                Img = Img,
                Role = Role,
                ImgFile = ImgFile,
                ImgPath = ImgPath,
                IsActive = IsActive
            };
        }
    }
}
