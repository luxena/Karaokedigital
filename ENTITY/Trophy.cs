using System;
using System.Collections.Generic;
using System.Text;

namespace ENTITY
{
    public class Trophy
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
        public string UserImg { get; set; }
        public string WinDate { get; set; }
        public string DueDate { get; set; }
        public bool Consumed { get; set; }
    }
}
