using System;
using System.Collections.Generic;
using System.Text;

namespace Part2_HotelReservationSystem
{
    public class HotelSystem
    {
        private List<Reservation> reservation = new List<Reservation>();

        private List<Guest> guests = new List<Guest>();

        public void CreateReservation(int guestId , Room room , int reservationId ,DateTime checkIn , DateTime checkOut)
        {
            Guest? guest = guests.FirstOrDefault(g => g.GuestId == guestId);

            if (room == null)
                throw new ArgumentNullException(nameof(room));

            if (room.IsUnderMaintenance)
                throw new InvalidOperationException("Cannot reserve a room that is under maintenance.");

            if (guest == null)
                throw new InvalidOperationException("Guest not found.");

            foreach (var ExistReservation in reservation)
            {
                if (ExistReservation.Room.RoomNumber == room.RoomNumber && 
                    (ExistReservation.Status != ReservationStatus.Cancelled && ExistReservation.Status != ReservationStatus.CheckedOut) && 
                         ExistReservation.CheckInDate < checkOut && checkIn < ExistReservation.CheckOutDate)
                {
                    throw new InvalidOperationException("The room is already reserved.");
                }
            }

            Reservation NewReservation = new Reservation(reservationId, checkIn, checkOut, room);

            guest.AddReservation(NewReservation);

            reservation.Add(NewReservation);
        }
      
    }
}
