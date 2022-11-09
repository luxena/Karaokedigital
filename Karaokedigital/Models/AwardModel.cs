using ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Karaokedigital.Models
{
    public class AwardModel
    {
        public int AwardID { get; set; }
        public int CustomerID { get; set; }
        public string Customer { get; set; }
        public string Award { get; set; }
        public string Cup { get; set; }
        public int CupID { get; set; }
        public string Reward { get; set; }
        public string Duration { get; set; }
        public bool IsActive { get; set; }

        public void MapFromAward(Awards award)
        {
            AwardID = award.AwardID;
            CustomerID = award.CustomerID;
            Customer = award.Customer;
            Award = award.Award;
            Cup = award.Cup;
            CupID = award.CupID;
            Reward = award.Reward;
            Duration = award.Duration;
            IsActive = award.IsActive;
        }

        public Awards MapIntoAward()
        {
            return new Awards
            {
                AwardID = AwardID,
                CustomerID = CustomerID,
                Customer = Customer,
                Award = Award,
                Cup = Cup,
                CupID = CupID,
                Reward = Reward,
                Duration = Duration,
                IsActive = IsActive
            };
        }
    }
}
