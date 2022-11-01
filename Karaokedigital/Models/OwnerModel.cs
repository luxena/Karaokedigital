using ENTITY;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Karaokedigital.Models
{
    public class OwnerModel
    {
        public int OwnerID { get; set; }
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

        public void MapFromOwner(Owner owner)
        {
            OwnerID = owner.OwnerID;
            Name = owner.Name;
            Surname = owner.Surname;
            Username = owner.Username;
            Password = owner.Password;
            Email = owner.Email;
            Phone = owner.Phone;
            DateOfBirth = owner.DateOfBirth;
            BornCountry = owner.BornCountry;
            BornProvince = owner.BornProvince;
            BornCity = owner.BornCity;
            FiscalCode = owner.FiscalCode;
            Country = owner.Country;
            Province = owner.Province;
            City = owner.City;
            Address = owner.Address;
            ZipCode = owner.ZipCode;
            Img = owner.Img;
            Role = owner.Role;
            ImgPath = owner.ImgPath;
            IsActive = owner.IsActive;
        }
        public Owner MapIntoOwner()
        {
            return new Owner
            {
                OwnerID = OwnerID,
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
                ImgPath = ImgPath,
                IsActive = IsActive
            };
        }
    }
}
