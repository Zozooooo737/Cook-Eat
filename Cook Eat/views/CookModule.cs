using Cook_Eat.services;

namespace Cook_Eat.views
{
    public static class CookModule
    {
        private static readonly CookService _cookService = new CookService(Program.DatabaseService);
        private static readonly CookView _cookView = new CookView(_cookService);

        public static void Show()
        {
            string[] menuOptions = {
                "Créer un compte cuisinier",
                "Afficher les cuisiniers",
                "Afficher les clients servis par un cuisinier",
                "Afficher les commandes servies par un cuisinier",
                "Afficher les plats proposés par un cuisinier (avec fréquence)",
                "Afficher les plats du jour proposés par un cuisinier",
                "Modifier un compte cuisinier",
                "Supprimer un compte cuisinier",
                "Retour au menu principal"
            };

            while (true)
            {
                Console.Clear();

                int choice = ConsoleView.ShowMenu("MODULE CUISINIERS", menuOptions);

                Console.Clear();

                switch (choice)
                {
                    case 0:
                        _cookView.CreateCook();
                        break;
                    case 1:
                        _cookView.DisplayClients();
                        break;
                    case 2: 
                        _cookView.ShowClientsServed(); 
                        break;
                    case 3:
                        _cookView.ShowDishPreparations();
                        break;
                    case 4: 
                        _cookView.ShowDishesWithFrequency(); 
                        break;
                    case 5: 
                        _cookView.ShowTodaysDishes(); 
                        break;
                    case 6:
                        _cookView.UpdateCook();
                        break;
                    case 7:
                        _cookView.DeleteCook();
                        break;
                    case 8:
                        return;
                }
                ConsoleView.WaitForContinue();
            }
        }
    }
}