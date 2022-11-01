using ENTITY;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Karaokedigital.Models
{
    public class BossModel
    {
        public int BossID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
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

        public void MapFromBoss(Boss boss)
        {
                BossID = boss.BossID; 
                Name = boss.Name; 
                Surname = boss.Surname; 
                Gender = boss.Gender; 
                Username = boss.Username; 
                Password = boss.Password; 
                Email = boss.Email; 
                Phone = boss.Phone; 
                DateOfBirth = boss.DateOfBirth; 
                BornCountry = boss.BornCountry; 
                BornProvince = boss.BornProvince; 
                BornCity = boss.BornCity; 
                FiscalCode = boss.FiscalCode; 
                Country = boss.Country; 
                Province = boss.Province; 
                City = boss.City; 
                Address = boss.Address; 
                ZipCode = boss.ZipCode; 
                Img = boss.Img; 
                Role = boss.Role; 
                ImgPath = boss.ImgPath;
                ImgFile = boss.ImgFile;
                IsActive = boss.IsActive;
        }
        public Boss MapIntoBoss()
        {
            return new Boss
            {
                BossID = BossID, 
                Name = Name, 
                Surname = Surname,
                Gender = Gender, 
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
                ImgFile = ImgFile,
                IsActive = IsActive
            };
        }
    }
}
