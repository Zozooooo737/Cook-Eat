using Cook_Eat.services;
using System;

namespace Cook_Eat.views
{
    public class StatsView
    {
        private readonly StatsService _statsService;
        private static readonly OrderService _orderService = new OrderService(Program.DatabaseService);

        public StatsView(StatsService statsService)
        {
            _statsService = statsService;
        }

        public void ShowDeliveryCountByCook()
        {
            var stats = _statsService.GetDeliveryCountByCook();
            ConsoleView.DisplayTable(stats);
        }

        public void ShowAverageOrderPrice()
        {
            var stats = _statsService.GetAverageOrderPrice();
            ConsoleView.DisplayTable(stats);
        }

        public void ShowAverageClientSpending()
        {
            var stats = _statsService.GetAverageClientSpending();
            ConsoleView.DisplayTable(stats);
        }

        public void ShowClientOrdersByDish()
        {
            try
            {
                int clientId = int.Parse(ConsoleView.GetUserInput("ID du client : "));
                var stats = _statsService.GetClientOrdersByDish(clientId);
                ConsoleView.DisplayTable(stats);
            }
            catch (FormatException)
            {
                ConsoleView.DisplayMessage("ID client invalide.");
            }
        }

        public void ShowOrdersNumber(int mode, int id = -1)
        {
            int tmp;
            if (mode == 0) tmp = int.Parse(ConsoleView.GetUserInput("ID du cuisinier : "));
            else if (mode == 1) tmp = int.Parse(ConsoleView.GetUserInput("ID du client : "));
            else tmp = id;

            List<Dictionary<string, object>> data;
            if (mode == 0 || mode == 2) data = null; //_orderService.GetClientOrders(tmp);
            else data = _orderService.GetCookOrders(tmp);

            ConsoleView.ShowMenuTitle("MES STATISTIQUES");

            if (data != null && data.Count != 0) ConsoleView.DisplayMessage("Nombre total de commandes effectuées : " + data.Count);
            else ConsoleView.DisplayMessage("Aucune commande effectuée pour le moment.");
        }

        public void ShowOrdersAmount(int mode, int id = -1)
        {
            int tmp;
            if (mode == 0) tmp = int.Parse(ConsoleView.GetUserInput("ID du client : "));
            else if (mode == 1) tmp = int.Parse(ConsoleView.GetUserInput("ID du cuisinier : "));
            else tmp = id;

            List<Dictionary<string, object>> data;
            if (mode == 0 || mode == 2) data = null; //_orderService.GetClientOrders(tmp);
            else data = _orderService.GetCookOrders(tmp);

            ConsoleView.ShowMenuTitle("MES STATISTIQUES");

            if (data != null && data.Count != 0) ConsoleView.DisplayMessage("Somme totale des commandes effectuées : " + data.Count);
            else ConsoleView.DisplayMessage("Aucune commande effectuée pour le moment.");
        }
    }
}