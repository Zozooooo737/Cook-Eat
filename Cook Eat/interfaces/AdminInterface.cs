using Cook_Eat.views;
using Cook_Eat.modules;

namespace Cook_Eat.interfaces
{
    public static class AdminInterface
    {
        public static void Connexion()
        {
            Console.Clear();

            ConsoleView.ShowMenuTitle("CONNEXION ADMIN");

            string adminUsername = ConsoleView.GetUserInput("Identifiant Admin : ");
            string adminPassword = ConsoleView.GetUserInput("Mot de passe Admin : ");
            if (adminUsername == "admin" && adminPassword == "Adm1n")
            {
                Console.WriteLine();
                ConsoleView.DisplayMessage("Connexion Admin établie");
                ConsoleView.WaitForContinue();
                Show();
            }
            else
            {
                Console.WriteLine();
                ConsoleView.DisplayMessage("Échec de la connexion");
                ConsoleView.WaitForContinue();
            }
        }

        public static void Show()
        {
            string[] adminMenuOptions = {
                "Module Utilisateurs",
                "Module Clients",
                "Module Cuisiniers",
                "Module Plats",
                "Module Commandes",
                "Module Statistiques",
                "Quitter l'application"
            };

            while (true)
            {
                Console.Clear();

                int choice = ConsoleView.ShowMenu("MENU ADMIN", adminMenuOptions);

                switch (choice)
                {
                    case 0:
                        UserModule.Show();
                        break;
                    case 1:
                        ClientModule.Show();
                        break;
                    case 2:
                        CookModule.Show();
                        break;
                    case 3:
                        DishModule.Show();
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