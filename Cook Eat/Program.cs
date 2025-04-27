using Cook_Eat.interfaces;
using Cook_Eat.services;
using Cook_Eat.views;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Cook_Eat.views;

namespace Cook_Eat
{
    internal class Program
    {
        public static DatabaseService DatabaseService = new DatabaseService();
        public static UserService UserService = new UserService(DatabaseService);
        public static DishService DishService = new DishService(DatabaseService);

        /// ATTENTION ///
        /// Si votre logiciel est en français, il faut que dans le fichier detailedStations.txt (dans le dossier "data"),
        /// vous remplacez tous les "." par des "," (virgule).
        /// Utiliser la commande "CTRL + H" pour remplacer tous les "." par des "," rapidement.

        static void Main()
        {
            Console.Title = "Cook-Eat - Livraison de repas entre voisins";
            Console.ForegroundColor = ConsoleColor.White;
            MainInterface.Show();
        }
    }
    
}