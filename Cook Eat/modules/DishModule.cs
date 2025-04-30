using Cook_Eat.services;
using Cook_Eat.views;

namespace Cook_Eat.modules
{
    public static class DishModule
    {
        private static readonly DishService _dishService = new DishService(Program.DatabaseService);
        private static readonly DishView _dishView = new DishView(_dishService);

        public static void Show(int mode = 0, int id = -1)
        {
            string[] menuOptions = null;
            if (mode == 0)
            {
                string[] tmp = {
                "Afficher tous les plats disponibles",
                "Afficher tous les plats",
                "Afficher les plats proposés par un cuisinier (avec fréquence)",
                "Afficher les plats du jour proposés par un cuisinier",
                "Ajouter un nouveau plat",
                "Ajouter une préparation de plat",
                "Retour au menu principal"
                };
                menuOptions = tmp;
            }
            else if (mode == 1)
            {
                string[] tmp = {
                "Afficher tous les plats disponibles",
                "Afficher les plats proposés par un cuisinier (avec fréquence)",
                "Afficher les plats du jour proposés par un cuisinier",
                "Afficher les plats proposés par mes cuisiniers favoris",
                "Retour au menu principal"
                };
                menuOptions = tmp;
            }
            else
            {
                string[] tmp = {
                "Afficher tous mes plats disponibles",
                "Ajouter une préparation",
                "Supprimer une préparation",
                "Afficher mes plats de référence",
                "Ajouter un plat de référence",
                "Afficher mes plats de référence",
                "Retour au menu principal"
                };
                menuOptions = tmp;
            }

            while (true)
            {
                Console.Clear();

                int choice;
                if (mode == 0) choice = ConsoleView.ShowMenu("MODULE PLATS", menuOptions);
                if (mode == 1) choice = ConsoleView.ShowMenu("LES PLATS", menuOptions);
                else choice = ConsoleView.ShowMenu("MES PLATS", menuOptions);

                Console.Clear();

                if (mode == 0)
                {
                    switch (choice)
                    {
                        case 0:
                            _dishView.DisplayAvailableDishes();
                            break;
                        case 1:
                            _dishView.DisplayAllDishes();
                            break;
                        case 2:
                            _dishView.ShowDishesWithFrequency();
                            break;
                        case 3:
                            _dishView.ShowTodaysDishes();
                            break;
                        case 4:
                            _dishView.AddNewDish();
                            break;
                        case 5:
                            _dishView.AddDishPreparation();
                            break;
                        case 6:
                            return;
                    }
                }
                else if(mode == 1)
                {
                    switch (choice)
                    {
                        case 0:
                            _dishView.DisplayAvailableDishes();
                            break;
                        case 1:
                            _dishView.ShowDishesWithFrequency();
                            break;
                        case 2:
                            _dishView.ShowTodaysDishes();
                            break;
                        case 3:
                            _dishView.ShowFavouriteCookDishes(id);
                            break;
                        case 4:
                            return;
                    }
                }
                else
                {
                    switch (choice)
                    {
                        case 0:
                            _dishView.DisplayAvailableDishes(id);
                            break;
                        case 1:
                            _dishView.AddDishPreparation(id);
                            break;
                        case 2:
                            _dishView.DeleteDishPreparation(id);
                            break;
                        case 3:
                            _dishView.DisplayAllDishes(id);
                            break;
                        case 4:
                            _dishView.AddNewDish(id);
                            break;
                        case 5:
                            _dishView.DisplayAllDishes(id);
                            break;
                        case 6:
                            return;
                    }
                }
                ConsoleView.WaitForContinue();
            }
        }
    }
}