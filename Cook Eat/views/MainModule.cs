namespace Cook_Eat.views
{
    public static class MainModule
    {
        public static void Show()
        {
            string[] mainMenuOptions = {
                "Module Utilisateurs",
                "Module Clients",
                "Module Cuisiniers",
                "Module Commandes",
                "Module Statistiques",
                "Quitter l'application"
            };

            while (true)
            {
                Console.Clear();

                int choice = ConsoleView.ShowMenu("MENU PRINCIPAL", mainMenuOptions);

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
                        // StatsModule.Show();
                        break;
                    case 5:
                        Console.WriteLine("\nMerci d'avoir utilisé Cook'Eat. À bientôt !");
                        return;
                }
            }
        }
    }
}