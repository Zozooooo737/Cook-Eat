using Cook_Eat.services;
using Cook_Eat.views;

namespace Cook_Eat
{
    internal class Program
    {
        public static DatabaseService DatabaseService = new DatabaseService();
        public static UserService UserService = new UserService(DatabaseService);
        public static DishService DishService = new DishService(DatabaseService);

        static void Main(string[] args)
        {
            /*Console.Title = "Cook-Eat - Livraison de repas entre voisins";
            Console.ForegroundColor = ConsoleColor.White;

            MainModule.Show();*/

            Tools.TransportNetwork();
        }
    }
}