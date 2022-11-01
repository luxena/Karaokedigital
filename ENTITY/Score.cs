using System;
using System.Collections.Generic;
using System.Text;

namespace ENTITY
{
    public class Score
    {
        public int ScoreID { get; set; }
        public int CustomerID { get; set; }
        public int ReservationID { get; set; }
        public int UserID { get; set; }
        public int Vote { get; set; }
        public string Date { get; set; }
    }
}
