using Cook_Eat.services;

namespace Cook_Eat.views
{
    public static class OrderModule
    {
        private static readonly OrderService _orderService = new OrderService(Program.DatabaseService);
        private static readonly ClientService _clientService = new ClientService(Program.DatabaseService);
        private static readonly OrderView _orderView = new OrderView(_orderService, _clientService);

        public static void Show()
        {
            string[] menuOptions = {
                "Créer une nouvelle commande",
                "Afficher le prix d'une commande",
                "Simuler le chemin de livraison",
                "Retour au menu principal"
            };

            while (true)
            {
                Console.Clear();

                int choice = ConsoleView.ShowMenu("MODULE COMMANDES", menuOptions);

                Console.Clear();

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
                        return;
                }
                ConsoleView.WaitForContinue();
            }
        }
    }
}