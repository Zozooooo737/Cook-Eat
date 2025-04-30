using Cook_Eat.modules;
using Cook_Eat.views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cook_Eat.interfaces
{
    internal class CookInterface
    {
        public static int currentId;

        public static void Show()
        {
            string[] cookMenuOptions = {
                "Gérer mon profil Cuisinier",
                "Mes Plats",
                "Mes Commandes",
                "Mes Statistiques",
                "Quitter le profil"
            };

            while (true)
            {
                Console.Clear();

                int choice = ConsoleView.ShowMenu("MON COMPTE CUISINIER", cookMenuOptions);

                switch (choice)
                {
                    case 0:
                        CookModule.Show(2, currentId);
                        break;
                    case 1:
                        DishModule.Show(2, currentId);
                        break;
                    case 2:
                        OrderModule.Show(2, currentId);
                        break;
                    case 3:
                        StatsModule.Show(2, currentId);
                        break;
                    case 4:
                        Console.WriteLine("\nMerci d'avoir utilisé Cook'Eat. À bientôt !");
                        return;
                }
            }
        }
    }
}
