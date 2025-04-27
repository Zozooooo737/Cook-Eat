using Cook_Eat.interfaces;
using Cook_Eat.services;
using Cook_Eat.views;

namespace Cook_Eat.modules
{
    public static class CookModule
    {
        private static readonly CookService _cookService = new CookService(Program.DatabaseService);
        private static readonly CookView _cookView = new CookView(_cookService);

        public static void Show(int index = 0, int id = -1)
        {
            string[] menuOptions = null;
            if (index == 0)
            {
                string[] tmp = {
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
                menuOptions = tmp;
            }
            if (index == 1)
            {
                string[] tmp = {
                "Afficher mes cuisiniers favoris",
                "Afficher les plats proposés par un cuisinier (avec fréquence)",
                "Afficher les plats du jour proposés par un cuisinier",
                "Retour au menu principal"
                };
                menuOptions = tmp;
            }

            while (true)
            {
                Console.Clear();

                int choice = ConsoleView.ShowMenu("MODULE CUISINIERS", menuOptions);

                Console.Clear();

                if (index == 0)
                {
                    switch (choice)
                    {
                        case 0:
                            _cookView.CreateCook();
                            break;
                        case 1:
                            _cookView.DisplayCook();
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
                }
                if (index == 1)
                {
                    switch (choice)
                    {
                        case 0:
                            _cookView.DisplayFavoriteCook(id);
                            break;
                        case 1:
                            _cookView.ShowDishesWithFrequency();
                            break;
                        case 2:
                            _cookView.ShowTodaysDishes();
                            break;
                        case 3:
                            return;
                    }
                }
                ConsoleView.WaitForContinue();
            }
        }

        public static void BecomeCook()
        {
            _cookView.CreateCook(false);
            CookInterface.currentId = _cookView.GetLastCookId();
            CookInterface.Show();
        }
    }
}