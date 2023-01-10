using ENTITY;
using static System.Formats.Asn1.AsnWriter;

namespace Karaokedigital.Models
{
    public class ScoreModel
    {
        public int ScoreID { get; set; }
        public int CustomerID { get; set; }
        public string Customer { get; set; }
        public int ReservationID { get; set; }
        public string TrackTitle { get; set; }
        public string TrackAuthor { get; set; }
        public string Users { get; set; }
        public int UserID { get; set; }
        public int Vote { get; set; }
        public string Date { get; set; }

        public void MapFromScore(Score score)
        {
            ScoreID = score.ScoreID;
            CustomerID = score.CustomerID;
            Customer = score.Customer;
            ReservationID = score.ReservationID;
            TrackTitle = score.TrackTitle;
            TrackAuthor = score.TrackAuthor;
            Users = score.Users;
            UserID = score.UserID;
            Vote = score.Vote;
            Date = score.Date;
        }

        public Score MapIntoScore()
        {
            return new Score
            {
                ScoreID = ScoreID,
                CustomerID = CustomerID,
                Customer = Customer,
                ReservationID = ReservationID,
                TrackTitle = TrackTitle,
                TrackAuthor = TrackAuthor,
                Users = Users,
                UserID = UserID,
                Vote = Vote,
                Date = Date
            };
        }


    }
}
