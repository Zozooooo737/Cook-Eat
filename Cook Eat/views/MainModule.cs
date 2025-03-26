using System;

namespace Cook_Eat.views
{
    public static class MainModule
    {
        public static void Show()
        {
            string[] mainMenuOptions = {
                "Module Utilisateurs",
                "Module Plats",
                "Module Commandes",
                "Module Statistiques",
                "Quitter l'application"
            };

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== COOK-EAT - LIVRAISON DE REPAS ENT VOISINS ===");
                Console.WriteLine();

                int choice = ConsoleView.ShowMenu("MENU PRINCIPAL", mainMenuOptions);

                switch (choice)
                {
                    case 0:
                        UserModule.Show();
                        break;
                    case 1:
                        // DishModule.Show();
                        break;
                    case 2:
                        // OrderModule.Show();
                        break;
                    case 3:
                        // StatsModule.Show();
                        break;
                    case 4:
                        Console.WriteLine("\nMerci d'avoir utilisé Cook-Eat. À bientôt !");
                        return;
                }
            }
        }
    }
}