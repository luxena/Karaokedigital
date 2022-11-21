using System;
using System.Collections.Generic;
using System.Text;

namespace ENTITY
{
    public class ReservationUser
    {
        public int ReservationUserID { get; set; }
        public int CustomerID { get; set; }
        public string Customer { get; set; }
        public int ReservationID { get; set; }
        public int UserID { get; set; }
        public string User { get; set; }
        public string UserImg { get; set; }
        public int Tone { get; set; }
    }
}
