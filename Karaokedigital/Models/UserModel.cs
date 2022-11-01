using ENTITY;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Karaokedigital.Models
{
    public class UserModel
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

        public void MapFromUser(User user)
        {
            UserID = user.UserID;
            Name = user.Name;
            Surname = user.Surname;
            Gender = user.Gender;
            Username = user.Username;
            Password = user.Password;
            Phone = user.Phone;
            Email = user.Email;
            Img = user.Img;
            Role = user.Role;
            ImgPath = user.ImgPath;
            ImgFile = user.ImgFile;
            IsActive = user.IsActive;
        }

        public User MapIntoUser()
        {
            return new User
            {
                UserID = UserID,
                Name = Name,
                Surname = Surname,
                Gender = Gender,
                Username = Username,
                Password = Password,
                Phone = Phone,
                Email = Email,
                Img = Img,
                Role = Role,
                ImgPath = ImgPath,
                ImgFile = ImgFile,
                IsActive = IsActive,
            };
        }

    }
}
