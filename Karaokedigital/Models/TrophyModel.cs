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
        public int CustomerID { get; set; }
        public string Customer { get; set; }
        public int AwardID { get; set; }
        public string Award { get; set; }
        public string Reward { get; set; }
        public int CupID { get; set; }
        public string Cup { get; set; }
        public int UserID { get; set; }
        public string User { get; set; }
        public string WinDate { get; set; }
        public string DueDate { get; set; }
        public bool Consumed { get; set; }

        public void MapFromTrophy(Trophy trophy)
        {
            TrophyID = trophy.TrophyID;
            CustomerID = trophy.CustomerID;
            Customer = trophy.Customer;
            AwardID = trophy.AwardID;
            Award = trophy.Award;
            Reward = trophy.Reward;
            CupID = trophy.CupID;
            Cup = trophy.Cup;
            UserID = trophy.UserID;
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
                CustomerID = CustomerID,
                Customer = Customer,
                AwardID = AwardID,
                Award = Award,
                Reward = Reward,
                CupID = CupID,
                Cup = Cup,
                UserID = UserID,
                User = User,
                WinDate = WinDate,
                DueDate = DueDate,
                Consumed = Consumed
            };
        }
    }
}
