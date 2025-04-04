using Cook_Eat.services;
using System;

namespace Cook_Eat.views
{
    public class StatsView
    {
        private readonly StatsService _statsService;

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
    }
}