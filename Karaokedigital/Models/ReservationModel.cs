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
        public int CustomerID { get; set; }
        public string Customer { get; set; }
        public int TrackID { get; set; }
        public string TrackTitle { get; set; }
        public string TrackAuthor { get; set; }
        public string User { get; set; }
        public int UserID { get; set; }
        public int NumberUsers { get; set; }
        public int ReservationStateID { get; set; }
        public string State { get; set; }
        public string Date { get; set; }
        public bool Social { get; set; }
        public int Votation { get; set; }

        public void MapFromReservation(Reservation reservation)
        {
            ReservationID = reservation.ReservationID;
            CustomerID = reservation.CustomerID;
            Customer = reservation.Customer;
            TrackID = reservation.TrackID;
            TrackTitle = reservation.TrackTitle;
            TrackAuthor = reservation.TrackAuthor;
            User = reservation.User;
			UserID = reservation.UserID;
            NumberUsers = reservation.NumberUsers;
            ReservationStateID = reservation.ReservationStateID;
            State = reservation.State;
            Date = reservation.Date;
            Social = reservation.Social;
            Votation = reservation.Votation;
        }

        public Reservation MapIntoReservation()
        {
            return new Reservation
            {
                ReservationID = ReservationID,
                CustomerID = CustomerID,
                Customer = Customer,
                TrackID = TrackID,
                TrackTitle = TrackTitle,
                TrackAuthor = TrackAuthor,
                User = User,
				UserID = UserID,
                NumberUsers = NumberUsers,
                ReservationStateID = ReservationStateID,
                State = State,
                Date = Date,
                Social = Social,
                Votation = Votation
            };
           
        }
    }
}
