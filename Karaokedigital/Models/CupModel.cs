using ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Karaokedigital.Models
{
    public class CupModel
    {
        public int CupID { get; set; }
        public string Cup { get; set; }

        public void MapFromCup(Cups cup)
        {
            CupID = cup.CupID;
            Cup = cup.Cup;
        }

        public Cups MapIntoCup()
        {
            return new Cups
            {
                CupID = CupID,
                Cup = Cup
            };
        }
    }
}
