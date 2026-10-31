namespace Part2_HotelReservationSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HotelSystem hotelSystem = new HotelSystem();

            hotelSystem.CreateGuest(1, "John Doe", "123-456-7890");

            hotelSystem.CreateRoom(101, RoomType.Single, 0m);

            Room? room = hotelSystem.Rooms.FirstOrDefault(r => r.RoomNumber == 101);

            hotelSystem.CreateReservation(1, room, 1, new DateTime(2024, 6, 1), new DateTime(2024, 6, 5));


            


        }
    }
}
