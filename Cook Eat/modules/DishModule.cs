using Cook_Eat.services;
using Cook_Eat.views;

namespace Cook_Eat.modules
{
    public static class DishModule
    {
        private static readonly DishService _dishService = new DishService(Program.DatabaseService);
        private static readonly DishView _dishView = new DishView(_dishService);

        public static void Show(int index = 0)
        {
            string[] menuOptions = null;
            if (index == 0)
            {
                string[] tmp = {
                "Afficher tous les plats disponibles",
                "Afficher tous les plats",
                "Ajouter un nouveau plat",
                "Ajouter une préparation de plat",
                "Retour au menu principal"
                };
                menuOptions = tmp;
            }
            if (index == 1)
            {
                string[] tmp = {
                "Afficher tous les plats disponibles",
                "Retour au menu principal"
                };
                menuOptions = tmp;
            }

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
                        return;
                }
                ConsoleView.WaitForContinue();
            }
        }
    }
}