using Cook_Eat.services;
using Cook_Eat.views;

namespace Cook_Eat.modules
{
    public static class StatsModule
    {
        private static readonly StatsService _statsService = new StatsService(Program.DatabaseService);
        private static readonly StatsView _statsView = new StatsView(_statsService);

        public static void Show()
        {
            string[] menuOptions = {
                "Livraisons par cuisinier",
                "Prix moyen des commandes",
                "Dépense moyenne par client",
                "Commandes par plat pour un client",
                "Retour au menu principal"
            };

            while (true)
            {
                Console.Clear();

                int choice = ConsoleView.ShowMenu("MODULE STATISTIQUES", menuOptions);

                Console.Clear();

                switch (choice)
                {
                    case 0:
                        _statsView.ShowDeliveryCountByCook();
                        break;
                    case 1:
                        _statsView.ShowAverageOrderPrice();
                        break;
                    case 2:
                        _statsView.ShowAverageClientSpending();
                        break;
                    case 3:
                        _statsView.ShowClientOrdersByDish();
                        break;
                    case 4:
                        return;
                }
                ConsoleView.WaitForContinue();
            }
        }
    }
}