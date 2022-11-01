using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ENTITY
{
    public class Owner
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
    }
}
