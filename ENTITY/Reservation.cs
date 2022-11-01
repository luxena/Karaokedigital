using System;
using System.Collections.Generic;
using System.Text;

namespace ENTITY
{
    public class Reservation
    {
        public int ReservationID { get; set; }
        public string Customer { get; set; }
        public string TrackTitle { get; set; }
        public string TrackAuthor { get; set; }
        public string User { get; set; }
        public string State { get; set; }
        public string Date { get; set; }
        public bool Social { get; set; }
    }
}
