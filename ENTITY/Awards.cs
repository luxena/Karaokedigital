using System;
using System.Collections.Generic;
using System.Text;

namespace ENTITY
{
    public class Awards
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
    }
}
