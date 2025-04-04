using Cook_Eat.services;

namespace Cook_Eat.views
{
    public static class DishModule
    {
        private static readonly DishService _dishService = new DishService(Program.DatabaseService);
        private static readonly DishView _dishView = new DishView(_dishService);

        public static void Show()
        {
            string[] menuOptions = {
                "Afficher les plats disponibles",
                "Afficher tous les plats",
                "Ajouter un nouveau plat",
                "Ajouter une préparation de plat",
                "Retour au menu principal"
            };

            while (true)
            {
                Console.Clear();

                int choice = ConsoleView.ShowMenu("MODULE PLATS", menuOptions);

                Console.Clear();

                switch (choice)
                {
                    case 0:
                        _dishView.DisplayAvailableDishes();
                        break;
                    case 1:
                        _dishView.DisplayAllDishes();
                        break;
                    case 2:
                        _dishView.AddNewDish();
                        break;
                    case 3:
                        _dishView.AddDishPreparation();
                        break;
                    case 4:
                        return;
                }
                ConsoleView.WaitForContinue();
            }
        }
    }
}