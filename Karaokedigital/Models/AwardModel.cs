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
        public string Customer { get; set; }
        public string Award { get; set; }
        public string Cup { get; set; }
        public string Reward { get; set; }
        public string Duration { get; set; }
        public bool IsActive { get; set; }

        public void MapFromAward(Awards award)
        {
            AwardID = award.AwardID;
            Customer = award.Customer;
            Award = award.Award;
            Cup = award.Cup;
            Reward = award.Reward;
            Duration = award.Duration;
            IsActive = award.IsActive;
        }

        public Awards MapIntoAward()
        {
            return new Awards
            {
                AwardID = AwardID,
                Customer = Customer,
                Award = Award,
                Cup = Cup,
                Reward = Reward,
                Duration = Duration,
                IsActive = IsActive
            };
        }
    }
}
