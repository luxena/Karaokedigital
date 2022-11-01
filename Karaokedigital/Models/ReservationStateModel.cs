using ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Karaokedigital.Models
{
    public class ReservationStateModel
    {
        public int ReservationStateID { get; set; }
        public string State { get; set; }

        public void MapFromReservationState(ReservationState reservationState)
        {
            ReservationStateID = reservationState.ReservationStateID;
            State = reservationState.State;
        }

        public ReservationState MapIntoReservationState()
        {
            return new ReservationState
            {
                ReservationStateID = ReservationStateID,
                State = State
            };
        }
    }
}
