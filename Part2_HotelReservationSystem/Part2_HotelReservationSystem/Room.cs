using System;
using System.Collections.Generic;
using System.Text;

namespace Part2_HotelReservationSystem
{

    public enum RoomType
    {
        Single,
        Double,
        Suite
    }
    public class Room
    {
        public int RoomNumber { get; init; }
        public RoomType RoomType { get; }
        public decimal NightlyRate { get; private set; }

        public bool IsUnderMaintenance { get; private set; }

        public Room(int roomNumber, RoomType roomType, decimal nightlyRate)
        {
            if (roomNumber <= 0)
            {
                throw new ArgumentException("Room number must be a positive integer.", nameof(roomNumber));
            }
            RoomNumber = roomNumber;

            if (!Enum.IsDefined(typeof(RoomType), roomType))
            {
                throw new ArgumentException("Invalid room type.", nameof(roomType));
            }
            RoomType = roomType;

            if (nightlyRate <= 0)
            {
                throw new ArgumentException("Nightly rate must be greater than zero.", nameof(nightlyRate));
            }
            NightlyRate = nightlyRate;

            IsUnderMaintenance = false;
        }

        public void StartMaintenance()
        {
            IsUnderMaintenance = true;
        }

        public void EndMaintenance()
        {
            IsUnderMaintenance = false;
        }

        public void ChangeNightlyRate(decimal newRate)
        {
            if (newRate <= 0)
            {
                throw new ArgumentException(
                    "Nightly rate must be greater than zero.",
                    nameof(newRate));
            }

            NightlyRate = newRate;
        }
    }
}
