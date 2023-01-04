using System;
using System.Collections.Generic;
using System.Text;

namespace ENTITY
{
    public class Reservation
    {
        public int ReservationID { get; set; }
        public int CustomerID { get; set; }
        public string Customer { get; set; }
        public int TrackID { get; set; }
        public string TrackTitle { get; set; }
        public string TrackAuthor { get; set; }
        public int UserID { get; set; }
        public int NumberUsers { get; set; }
        public int ReservationStateID { get; set; }
        public string State { get; set; }
        public string Date { get; set; }
        public bool Social { get; set; }
        public int Votation { get; set; }
    }
}
