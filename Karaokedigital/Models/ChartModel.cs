using ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Karaokedigital.Models
{
    public class ChartModel
    {
        public int Number { get; set; }
        public int ReservationID { get; set; }
        public string Customer { get; set; }
        public int CustomerID { get; set; }
        public string TrackTitle { get; set; }
        public string TrackAuthor { get; set; }
        public string User { get; set; }
        public int Votation { get; set; }
        public string Date { get; set; }

        public void MapFromChart(Chart chart)
        {
            Number = chart.Number;
            ReservationID = chart.ReservationID;
            CustomerID = chart.CustomerID;
            Customer = chart.Customer;
            TrackTitle = chart.TrackTitle;
            TrackAuthor = chart.TrackAuthor;
            User = chart.User;
            Votation = chart.Votation;
            Date = chart.Date;
        }

        public Chart MapIntoChart()
        {
            return new Chart
            {
                Number = Number,
                ReservationID = ReservationID,
                CustomerID = CustomerID,
                Customer = Customer,
                TrackTitle = TrackTitle,
                TrackAuthor = TrackAuthor,
                User = User,
                Votation = Votation,
                Date = Date
            };
        }
    }
}
