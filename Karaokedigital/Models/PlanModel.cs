using ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Karaokedigital.Models
{
    public class PlanModel
    {
        public int PlanID { get; set; }
        public string Plan { get; set; }
        public string Duration { get; set; }
        public decimal Cost { get; set; }
        public string Properties { get; set; }

        public void MapFromPlan(Plans plan)
        {
            PlanID = plan.PlanID;
            Plan = plan.Plan;
            Duration = plan.Duration;
            Cost = plan.Cost;
            Properties = plan.Properties;
        }

        public Plans MapIntoPlan()
        {
            return new Plans
            {
                PlanID = PlanID,
                Plan = Plan,
                Duration = Duration,
                Cost = Cost,
                Properties = Properties,
            };
        }
    }
}
