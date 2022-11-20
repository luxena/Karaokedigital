using ENTITY;
using System.Data;

namespace Karaokedigital.Models
{
    public class ReservationUserModel
    {
        public int ReservationUserID { get; set; }
        public int CustomerID { get; set; }
        public string Customer { get; set; }
        public int ReservationID { get; set; }
        public int UserID { get; set; }
        public string User { get; set; }
        public int Tone { get; set; }

        public void MapFromReservationUser(ReservationUser reservationUser)
        {
            ReservationUserID = reservationUser.ReservationUserID;
            CustomerID = reservationUser.CustomerID;
            Customer = reservationUser.Customer;
            ReservationID = reservationUser.ReservationID;
            UserID = reservationUser.UserID;
            User = reservationUser.User;
            Tone = reservationUser.Tone;
        }

        public ReservationUser MapIntoReservationUser()
        {
            return new ReservationUser
            {
                ReservationUserID = ReservationUserID,
                CustomerID = CustomerID,
                Customer = Customer,
                ReservationID = ReservationID,
                UserID = UserID,
                User = User,
                Tone = Tone
            };
        }
    }
}
