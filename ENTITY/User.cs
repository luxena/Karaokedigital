using Microsoft.AspNetCore.Http;
using System;

namespace ENTITY
{
    public class User
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Img { get; set; }
        public string Role { get; set; }
        public IFormFile ImgFile { get; set; }
        public string ImgPath { get; set; }
        public bool IsActive { get; set; }
    }
}
