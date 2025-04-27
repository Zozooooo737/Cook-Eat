using Cook_Eat.models;
using Cook_Eat.services;
using System;

namespace Cook_Eat.views
{
    public class OrderView
    {
        private readonly OrderService _orderService;
        private readonly ClientService _clientService;
        private readonly DishService _dishService;

        public OrderView(OrderService orderService, ClientService clientService, DishService dishService)
        {
            _orderService = orderService;
            _clientService = clientService;
            _dishService = dishService;
        }

        public void CreateOrderLine(int id = -1, int idT = -1)
        {
            try
            {
                // Vérifier si le client existe
                int clientId = int.Parse(ConsoleView.GetUserInput("ID du client : "));
                int transactionId;
                if (idT == -1) { transactionId = int.Parse(ConsoleView.GetUserInput("ID de la transaction : ")); }
                else { transactionId = idT; }
                int addressId = int.Parse(ConsoleView.GetUserInput("ID de l'adresse de livraison : "));
                int preparationId = int.Parse(ConsoleView.GetUserInput("ID de la préparation commandée : "));

                var order = new Order
                {
                    OrderDate = DateTime.Now,
                    TransactionId = transactionId,
                    AddressId = addressId,
                    PreparationId = preparationId
                };

                if (_orderService.CreateOrder(order) && _dishService.SwitchDishStatus(preparationId))
                {
                    ConsoleView.DisplayMessage("Commande créée avec succès !");
                }
                else
                {
                    ConsoleView.DisplayMessage("Échec de la création de la commande.");
                }
            }
            catch (FormatException)
            {
                ConsoleView.DisplayMessage("Données invalides.");
            }
        }

        public void CreateTransaction(int id = -1)
        {
            try
            {
                int clientId;
                if (id == -1) { clientId = int.Parse(ConsoleView.GetUserInput("ID du client : ")); }
                else { clientId = id; }

                var transaction = new Transaction
                {
                    OrderDate = DateTime.Now,
                    Amount = 0,
                    IsDelivered = false,
                    ClientId = id
                };

                _orderService.CreateTransaction(transaction);
            }
            catch (FormatException)
            {
                ConsoleView.DisplayMessage("Données invalides.");
            }
        }

        public void CreateOrder(int id = -1)
        {
            CreateTransaction(id);
            int idT = GetLastTransactionId();
            double amount;

            string[] menuOptions = {
                "Ajouter une ligne de commande",
                "Valider la commande",
            };

            while (true)
            {
                Console.Clear();

                int choice = ConsoleView.ShowMenu("MODULE COMMANDES", menuOptions);

                Console.Clear();

                switch (choice)
                {
                    case 0:
                        CreateOrderLine(id, idT);
                        break;
                    case 1:
                        
                        return;
                }
                ConsoleView.WaitForContinue();
            }
        }

        public int GetLastTransactionId()
        {
            Dictionary<string, object> data = _orderService.GetLastTransaction();
            return Convert.ToInt32(data["transaction_id"]);
        }

        public void ShowOrderPrice()
        {
            try
            {
                int orderId = int.Parse(ConsoleView.GetUserInput("ID de la commande : "));
                decimal price = _orderService.CalculateOrderPrice(orderId);
                ConsoleView.DisplayMessage($"Prix de la commande : {price} $");
            }
            catch (FormatException)
            {
                ConsoleView.DisplayMessage("ID de commande invalide.");
            }
        }

        public void ShowDeliveryPath()
        {
            try
            {
                int orderId = int.Parse(ConsoleView.GetUserInput("ID de la commande : "));
                string path = _orderService.SimulateDeliveryPath(orderId);
                ConsoleView.DisplayMessage(path);
            }
            catch (FormatException)
            {
                ConsoleView.DisplayMessage("ID de commande invalide.");
            }
        }

        // OrderView.cs
        public void SimulateMetroPath()
        {
            try
            {
                Console.WriteLine("SIMULATION DE TRAJET MÉTRO");
                Console.WriteLine("--------------------------\n");

                string stationDepart = ConsoleView.GetUserInput("Station de départ : ");
                string stationArrivee = ConsoleView.GetUserInput("Station d'arrivée : ");

                // Appel direct à la méthode Tools avec les noms de stations
                Tools.GenererTrajetParNoms(stationDepart, stationArrivee, true);

                ConsoleView.DisplayMessage("\nTrajet généré avec succès !");
            }
            catch (Exception ex)
            {
                ConsoleView.DisplayMessage($"Erreur : {ex.Message}");
            }
        }
    }
}