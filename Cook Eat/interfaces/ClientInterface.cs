using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cook_Eat.modules;
using Cook_Eat.views;

namespace Cook_Eat.interfaces
{
    internal class ClientInterface
    {
        public static int currentId;

        public static void Show()
        {
            string[] clientMenuOptions = {
                "Gérer mon profil Client",
                "Les Cuisiniers",
                "Les Plats",
                "Mes Commandes",
                "Mes Statistiques",
                "Quitter le profil"
            };

            while (true)
            {
                Console.Clear();

                int choice = ConsoleView.ShowMenu("MON COMPTE CLIENT", clientMenuOptions);

                switch (choice)
                {
                    case 0:
                        ClientModule.Show(false, currentId);
                        break;
                    case 1:
                        CookModule.Show(1, currentId);
                        break;
                    case 2:
                        DishModule.Show(1);
                        break;
                    case 3:
                        OrderModule.Show(1, currentId);
                        break;
                    case 4:
                        StatsModule.Show(1, currentId);
                        break;
                    case 5:
                        Console.WriteLine("\nMerci d'avoir utilisé Cook'Eat. À bientôt !");
                        return;
                }
            }
        }
    }
}
