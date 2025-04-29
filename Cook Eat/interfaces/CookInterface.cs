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
                "Module Utilisateur",
                "Module Client",
                "Module Cuisiniers",
                "Module Plats",
                "Module Commandes",
                "Module Statistiques",
                "Quitter le profil"
            };

            while (true)
            {
                Console.Clear();

                int choice = ConsoleView.ShowMenu("MENU CUISINIER", cookMenuOptions);

                switch (choice)
                {
                    case 0:
                        UserModule.Show(1, currentId);
                        break;
                    case 1:
                        ClientModule.Show(1, currentId);
                        break;
                    case 2:
                        CookModule.Show(1, currentId);
                        break;
                    case 3:
                        DishModule.Show(1);
                        break;
                    case 4:
                        OrderModule.Show();
                        break;
                    case 5:
                        StatsModule.Show();
                        break;
                    case 6:
                        Console.WriteLine("\nMerci d'avoir utilisé Cook'Eat. À bientôt !");
                        return;
                }
            }
        }
    }
}
