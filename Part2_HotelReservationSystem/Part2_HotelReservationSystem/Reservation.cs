using System;
using System.Collections.Generic;
using System.Text;

namespace Part2_HotelReservationSystem
{
    public enum ReservationStatus
    {
        Pending,
        Confirmed,
        CheckedIn,
        CheckedOut,
        Cancelled
    }
    public class Reservation
    {
        public int ReservationId { get; init; }
        public DateTime CheckInDate { get; init; }
        public DateTime CheckOutDate { get; init; }
        public Room? Room { get; }
        public ReservationStatus Status { get; private set; } = ReservationStatus.Pending;


        public Reservation(int reservationId, DateTime checkInDate, DateTime checkOutDate , 
            Room room)
        {
            if (reservationId <= 0)
            {
                throw new ArgumentException("Reservation ID must be a positive integer.", nameof(reservationId));
            }
            ReservationId = reservationId;

            if (checkInDate >= checkOutDate)
            {
                throw new ArgumentException("Check-in date must be earlier than check-out date.");
            }
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;

            Room = room ?? throw new ArgumentNullException(nameof(room), "Room cannot be null.");

            if (room.IsUnderMaintenance)
            {
                throw new ArgumentException("Cannot reserve a room that is under maintenance.", nameof(room));
            }

            Status = ReservationStatus.Pending;
        }

        public void Confirm()
        {
            if (Status == ReservationStatus.Pending)
            {
                Status = ReservationStatus.Confirmed;
            }
            else
            {
                throw new InvalidOperationException("Reservation can only be confirmed if it is in Pending status.");
            }
        }

        public void CheckIn()
        {
            if (Status == ReservationStatus.Confirmed)
            {
                Status = ReservationStatus.CheckedIn;
            }
            else
            {
                throw new InvalidOperationException("Reservation can only be checked in if it is in Confirmed status.");
            }
        }

        public void CheckOut()
        {
            if (Status == ReservationStatus.CheckedIn)
            {
                Status = ReservationStatus.CheckedOut;
            }
            else
            {
                throw new InvalidOperationException("Reservation can only be checked out if it is in CheckedIn status.");
            }
        }

        public void Cancel()
        {
            if (Status == ReservationStatus.Pending || Status == ReservationStatus.Confirmed)
            {
                Status = ReservationStatus.Cancelled;
            }
            else
            {
                throw new InvalidOperationException("Reservation can only be cancelled if it is in Pending or Confirmed status.");
            }
        }
    }
}
