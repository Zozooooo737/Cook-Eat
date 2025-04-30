using Cook_Eat.services;
using Cook_Eat.views;

namespace Cook_Eat.modules
{
    public static class StatsModule
    {
        private static readonly StatsService _statsService = new StatsService(Program.DatabaseService);
        private static readonly StatsView _statsView = new StatsView(_statsService);

        public static void Show(int mode = 0, int id = -1)
        {
            string[] menuOptions = null;
            if (mode == 0) {
                string[] tmp = {
                "Livraisons par cuisinier",
                "Prix moyen des commandes",
                "Dépense moyenne par client",
                "Commandes par plat pour un client",
                "Nombre de commandes effectuées par un client",
                "Nombre de commandes effectuées par un cuisinier",
                "Somme totale des commandes effectuées par un client",
                "Somme totale des commandes effectuées par un cuisinier",
                "Retour au menu principal"
                };
                menuOptions = tmp;
            }
            else if (mode == 1)
            {
                string[] tmp = {
                "Nombre total de mes commandes",
                "Somme total de mes commandes",
                "Retour au menu principal"
                };
                menuOptions = tmp;
            }
            else
            {
                string[] tmp = {
                "Nombre total de plats commandés",
                "Somme total de mes commandes",
                "Nombre de clients m'ayant en favori",
                "Retour au menu principal"
                };
                menuOptions = tmp;
            }

            while (true)
            {
                Console.Clear();

                int choice;
                if (mode == 0) choice = ConsoleView.ShowMenu("MODULE STATISTIQUES", menuOptions);
                else choice = ConsoleView.ShowMenu("MES STATISTIQUES", menuOptions);

                Console.Clear();

                if (mode == 0)
                {
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
                            _statsView.ShowOrdersNumber(0);
                            break;
                        case 5:
                            _statsView.ShowOrdersNumber(1);
                            break;
                        case 6:
                            _statsView.ShowOrdersAmount(0);
                            break;
                        case 7:
                            _statsView.ShowOrdersAmount(1);
                            break;
                        case 8:
                            return;
                    }
                }
                else if (mode == 1)
                {
                    switch (choice)
                    {
                        case 0:
                            _statsView.ShowOrdersNumber(2, id);
                            break;
                        case 1:
                            _statsView.ShowOrdersAmount(2, id);
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
                            _statsView.ShowOrdersNumber(3, id);
                            break;
                        case 1:
                            _statsView.ShowOrdersAmount(3, id);
                            break;
                        case 2:
                            //_statsView.ShowCookFavouriteNumber(id);
                            break;
                        case 3:
                            return;
                    }
                }
                ConsoleView.WaitForContinue();
            }
        }
    }
}