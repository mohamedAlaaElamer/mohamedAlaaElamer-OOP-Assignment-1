

namespace Part2_HotelReservationSystem
{
    public class Guest
    {
        public int GuestId { get; init; }

        public string FullName { get; init; }

        public string PhoneNumber { get; init; }

        public IReadOnlyList<Reservation> Reservations => (reservation.AsReadOnly());

        private List<Reservation> reservation = new List<Reservation>();

        public Guest(int guestId, string fullName, string phoneNumber)
        {
            if (guestId <= 0)
            {
                throw new ArgumentException("Guest ID must be a positive integer.", nameof(guestId));
            }
            GuestId = guestId;
            if (string.IsNullOrWhiteSpace(fullName))
            {
                throw new ArgumentException("Full name cannot be null or empty.", nameof(fullName));
            }
            FullName = fullName;
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                throw new ArgumentException("Phone number cannot be null or empty.", nameof(phoneNumber));
            }
            PhoneNumber = phoneNumber;
        }


        public void AddReservation(Reservation reservation)
        {
            if (reservation == null)
            {
                throw new ArgumentNullException(nameof(reservation), "Reservation cannot be null.");
            }

            this.reservation.Add(reservation);
        }
    }
}
