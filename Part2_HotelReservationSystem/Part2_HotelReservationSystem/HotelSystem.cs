using System;
using System.Collections.Generic;
using System.Text;

namespace Part2_HotelReservationSystem
{
    public class HotelSystem
    {
        private List<Reservation> reservation = new List<Reservation>();

        private List<Guest> guests = new List<Guest>();

        private List<Room> rooms = new List<Room>();

        public void CreateReservation(int guestId, Room room, int reservationId, DateTime checkIn, DateTime checkOut)
        {
            Guest? guest = guests.FirstOrDefault(g => g.GuestId == guestId);

            if (room == null)
                throw new ArgumentNullException(nameof(room));

            Room ? CheckedRoom = rooms.FirstOrDefault(r => r.RoomNumber == room.RoomNumber);

            if (CheckedRoom == null)
                throw new InvalidOperationException("Room not found.");

            if (CheckedRoom.IsUnderMaintenance)
                throw new InvalidOperationException("Cannot reserve a room that is under maintenance.");

            if (guest == null)
                throw new InvalidOperationException("Guest not found.");

            foreach (var ExistReservation in reservation)
            {
                if (ExistReservation.Room.RoomNumber == CheckedRoom.RoomNumber &&
                    (ExistReservation.Status != ReservationStatus.Cancelled && ExistReservation.Status != ReservationStatus.CheckedOut) &&
                         ExistReservation.CheckInDate < checkOut && checkIn < ExistReservation.CheckOutDate)
                {
                    throw new InvalidOperationException("The room is already reserved.");
                }
            }

            if(reservation.Any(r => r.ReservationId == reservationId))
            {
                throw new InvalidOperationException("A reservation with the same ID already exists.");
            }

            Reservation NewReservation = new Reservation(reservationId, checkIn, checkOut, CheckedRoom);

            guest.AddReservation(NewReservation);

            reservation.Add(NewReservation);
        }

        public IReadOnlyList<Room> Rooms => rooms.AsReadOnly();
        public void CreateGuest(int guestId, string fullName, string phoneNumber)
        {
            if (guests.Any(g => g.GuestId == guestId))
            {
                throw new InvalidOperationException("A guest with the same ID already exists.");
            }
            Guest newGuest = new Guest(guestId, fullName, phoneNumber);
            guests.Add(newGuest);
        }

        public void CreateRoom(int roomNumber, RoomType roomType, decimal nightlyRate)
        {
            if (rooms.Any(r => r.RoomNumber == roomNumber))
            {
                throw new InvalidOperationException("A room with the same number already exists.");
            }
            Room newRoom = new Room(roomNumber, roomType, nightlyRate);

            rooms.Add(newRoom);
        }

        public void ConfirmReservation(int reservationId)
        {
            Reservation? reservationToConfirm = reservation.FirstOrDefault(r => r.ReservationId == reservationId);

            if (reservationToConfirm == null)
                throw new InvalidOperationException("Room not found");

            reservationToConfirm.Confirm();
        }

        public void CheckIn(int reservationId)
        {
            Reservation? reservationToCheckIn = reservation.FirstOrDefault(r => r.ReservationId == reservationId);
            if (reservationToCheckIn == null)
                throw new InvalidOperationException("Reservation not found");
            reservationToCheckIn.CheckIn();
        }

        public void CheckOut(int reservationId)
        {
            Reservation? reservationToCheckOut = reservation.FirstOrDefault(r => r.ReservationId == reservationId);
            if (reservationToCheckOut == null)
                throw new InvalidOperationException("Reservation not found");
            reservationToCheckOut.CheckOut();
        }

        public void CancelReservation(int reservationId)
        {
            Reservation? reservationToCancel = reservation.FirstOrDefault(r => r.ReservationId == reservationId);
            if (reservationToCancel == null)
                throw new InvalidOperationException("Reservation not found");
            reservationToCancel.Cancel();
        }

        public void PrintGuestReservations(int guestId)
        {
            Guest? guest = guests.FirstOrDefault(g => g.GuestId == guestId);
            if (guest == null)
                throw new InvalidOperationException("Guest not found");
            guest.PrintReservations();
        }

        public void PrintTotalCost(int reservationId)
        {
            Reservation? reservationToPrint = reservation.FirstOrDefault(r => r.ReservationId == reservationId);
            if (reservationToPrint == null)
                throw new InvalidOperationException("Reservation not found");
            decimal totalCost = reservationToPrint.TotalCost();
            Console.WriteLine($"Total cost for Reservation ID {reservationId}: {totalCost:C}");
        }

        public void PrintStatus(int reservationId)
        {
            Reservation? reservationToPrint = reservation.FirstOrDefault(r => r.ReservationId == reservationId);
            if (reservationToPrint == null)
                throw new InvalidOperationException("Reservation not found");
            Console.WriteLine($"Status for Reservation ID {reservationId}: {reservationToPrint.Status}");
        }
    }
}
