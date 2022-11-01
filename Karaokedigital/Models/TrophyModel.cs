using ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Karaokedigital.Models
{
    public class TrophyModel
    {
        public int TrophyID { get; set; }
        public string Customer { get; set; }
        public string Award { get; set; }
        public string Reward { get; set; }
        public string Cup { get; set; }
        public string User { get; set; }
        public string WinDate { get; set; }
        public string DueDate { get; set; }
        public bool Consumed { get; set; }

        public void MapFromTrophy(Trophy trophy)
        {
            TrophyID = trophy.TrophyID;
            Customer = trophy.Customer;
            Award = trophy.Award;
            Reward = trophy.Reward;
            Cup = trophy.Cup;
            User = trophy.User;
            WinDate = trophy.WinDate;
            DueDate = trophy.DueDate;
            Consumed = trophy.Consumed;
        }

        public Trophy MapIntoTrophy()
        {
            return new Trophy
            {
                TrophyID = TrophyID,
                Customer = Customer,
                Award = Award,
                Reward = Reward,
                Cup = Cup,
                User = User,
                WinDate = WinDate,
                DueDate = DueDate,
                Consumed = Consumed
            };
        }
    }
}
