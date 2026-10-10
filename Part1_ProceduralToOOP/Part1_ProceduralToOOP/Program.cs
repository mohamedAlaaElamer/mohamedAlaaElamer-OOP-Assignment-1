namespace Part1_ProceduralToOOP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu(new OrderSystem());

            menu.Run();
        }
    }
}
