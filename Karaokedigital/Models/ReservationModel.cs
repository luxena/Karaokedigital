using ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Karaokedigital.Models
{
    public class ReservationModel
    {
        public int ReservationID { get; set; }
        public string Customer { get; set; }
        public string TrackTitle { get; set; }
        public string TrackAuthor { get; set; }
        public string User { get; set; }
        public string State { get; set; }
        public string Date { get; set; }
        public bool Social { get; set; }

        public void MapFromReservation(Reservation reservation)
        {
            ReservationID = reservation.ReservationID;
            Customer = reservation.Customer;
            TrackTitle = reservation.TrackTitle;
            TrackAuthor = reservation.TrackAuthor;
            User = reservation.User;
            State = reservation.State;
            Date = reservation.Date;
            Social = reservation.Social;
        }

        public Reservation MapIntoReservation()
        {
            return new Reservation
            {
                ReservationID = ReservationID,
                Customer = Customer,
                TrackTitle = TrackTitle,
                TrackAuthor = TrackAuthor,
                User = User,
                State = State,
                Date = Date,
                Social = Social
            };
           
        }
    }
}
