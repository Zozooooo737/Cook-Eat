using Cook_Eat.services;
using Cook_Eat.views;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Cook_Eat
{
    internal class Program
    {
        public static DatabaseService DatabaseService = new DatabaseService();
        public static UserService UserService = new UserService(DatabaseService);
        public static DishService DishService = new DishService(DatabaseService);


        static void Main()
        {
            Console.Title = "Cook-Eat - Livraison de repas entre voisins";
            Console.ForegroundColor = ConsoleColor.White;
            MainModule.Show(); 

            //Tools.TransportNetwork(true);
        }
    }
    
}