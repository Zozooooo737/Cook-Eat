using Cook_Eat.services;
using Cook_Eat.views;

namespace Cook_Eat.modules
{
    public static class OrderModule
    {
        private static readonly OrderService _orderService = new OrderService(Program.DatabaseService);
        private static readonly ClientService _clientService = new ClientService(Program.DatabaseService);
        private static readonly DishService _dishService = new DishService(Program.DatabaseService);
        private static readonly OrderView _orderView = new OrderView(_orderService, _clientService, _dishService);

        public static void Show(int mode = 0, int id = -1)
        {
            string[] menuOptions = null;
            if (mode == 0)
            {
                string[] tmp = {
                "Créer une nouvelle commande",
                "Afficher le prix d'une commande",
                "Simuler le chemin de livraison",
                "Simuler un trajet entre deux stations de métro",
                "Retour au menu principal"
                };
                menuOptions = tmp;
            }
            else if (mode == 1)
            {
                string[] tmp = {
                "Commander un/des plat(s)",
                "Afficher toutes mes commandes",
                "Afficher tous les plats commandés",
                "Afficher une commande détaillée",
                "Retour au menu principal"
                };
                menuOptions = tmp;
            }
            else
            {
                string[] tmp = {
                "Afficher toutes mes commandes",
                "Afficher le trajet pour livrer une commande",
                "Retour au menu principal"
                };
                menuOptions = tmp;
            }

                while (true)
                {
                    Console.Clear();

                    int choice;
                    if (mode == 0) choice = ConsoleView.ShowMenu("MODULE COMMANDES", menuOptions);
                    else choice = ConsoleView.ShowMenu("MES COMMANDES", menuOptions);

                    Console.Clear();

                    if (mode == 0)
                    {
                        switch (choice)
                        {
                            case 0:
                                _orderView.CreateOrder();
                                break;
                            case 1:
                                _orderView.ShowOrderPrice();
                                break;
                            case 2:
                                _orderView.ShowDeliveryPath();
                                break;
                            case 3:
                                _orderView.SimulateMetroPath();
                                break;
                            case 4:
                                return;
                        }
                    }
                    else if (mode == 1)
                    {
                        switch (choice)
                        {
                            case 0:
                                _orderView.CreateOrder(id);
                                break;
                            case 1:
                                _orderView.GetClientTransactions(id);
                                break;
                            case 2:
                                _orderView.GetClientOrders(id);
                                break;
                            case 3:
                                ConsoleView.Maintenance();
                                //orderView.GetClientDetailedOrder(id);
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
                                _orderView.GetCookOrders(id);
                                break;
                            case 1:
                                _orderView.ShowCookDeliveryPath();
                                break;
                            case 2:
                                return;
                        }
                }
                    ConsoleView.WaitForContinue();
                }
        }
    }
}