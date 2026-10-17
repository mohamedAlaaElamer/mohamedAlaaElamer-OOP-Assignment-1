namespace Part2_HotelReservationSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Room room = new Room(1, RoomType.Single, 100.00m);

            Console.WriteLine(room.RoomNumber);
        }
    }
}
