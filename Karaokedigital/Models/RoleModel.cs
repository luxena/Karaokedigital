using ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Karaokedigital.Models
{
    public class RoleModel
    {
        public int RoleID { get; set; }

        public string Role { get; set; }

        public void MapFromRole(Roles role)
        {
            RoleID = role.RoleID;
            Role = role.Role;
        }

        public Roles MapIntoRole()
        {
            return new Roles
            {
                RoleID = RoleID,
                Role = Role

            };
        }
    }
}
