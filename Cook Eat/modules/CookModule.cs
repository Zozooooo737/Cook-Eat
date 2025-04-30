using Cook_Eat.interfaces;
using Cook_Eat.services;
using Cook_Eat.views;

namespace Cook_Eat.modules
{
    public static class CookModule
    {
        private static readonly CookService _cookService = new CookService(Program.DatabaseService);
        private static readonly CookView _cookView = new CookView(_cookService);

        public static void Show(int mode = 0, int id = -1)
        {
            string[] menuOptions = null;
            if (mode == 0)
            {
                string[] tmp = {
                "Créer un compte cuisinier",
                "Afficher les cuisiniers",
                "Afficher les clients servis par un cuisinier",
                "Afficher les commandes servies par un cuisinier",
                "Modifier un compte cuisinier",
                "Supprimer un compte cuisinier",
                "Retour au menu principal"
                };
                menuOptions = tmp;
            }
            else if (mode == 1)
            {
                string[] tmp = {
                "Afficher mes cuisiniers favoris",
                "Ajouter un cuisinier dans mes favoris",
                "Retour au menu principal"
                };
                menuOptions = tmp;
            }
            else
            {
                string[] tmp = {
                "Afficher mes infos",
                "Modifier mon compte cuisinier",
                "Retour au menu principal"
                };
                menuOptions = tmp;
            }

            while (true)
            {
                Console.Clear();

                int choice;
                if (mode == 0) choice = ConsoleView.ShowMenu("MODULE CUISINIER", menuOptions);
                if (mode == 1) choice = ConsoleView.ShowMenu("LES CUISINIERS", menuOptions);
                else choice = ConsoleView.ShowMenu("MON PROFIL CUISINIER", menuOptions);

                Console.Clear();

                if (mode == 0)
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
                            _cookView.UpdateCook();
                            break;
                        case 5:
                            _cookView.DeleteCook();
                            break;
                        case 6:
                            return;
                    }
                }
                else if (mode == 1)
                {
                    switch (choice)
                    {
                        case 0:
                            _cookView.DisplayFavoriteCook(id);
                            break;
                        case 1:
                            _cookView.AddFavoriteCook(id);
                            break;
                        case 2:
                            return;
                    }
                }
                else
                {
                    switch (choice)
                    {
                        case 0:
                            _cookView.DisplayCook(id);
                            break;
                        case 1:
                            _cookView.UpdateCook(id);
                            break;
                        case 2:
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