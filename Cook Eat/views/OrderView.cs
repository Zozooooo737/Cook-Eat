using Cook_Eat.models;
using Cook_Eat.services;
using System;

namespace Cook_Eat.views
{
    public class OrderView
    {
        private readonly OrderService _orderService;
        private readonly ClientService _clientService;

        public OrderView(OrderService orderService, ClientService clientService)
        {
            _orderService = orderService;
            _clientService = clientService;
        }

        public void CreateOrder()
        {
            try
            {
                // Vérifier si le client existe
                int clientId = int.Parse(ConsoleView.GetUserInput("ID du client : "));

                int transactionId = int.Parse(ConsoleView.GetUserInput("ID de la transaction : "));
                int addressId = int.Parse(ConsoleView.GetUserInput("ID de l'adresse de livraison : "));
                int preparationId = int.Parse(ConsoleView.GetUserInput("ID de la préparation commandée : "));

                var order = new Order
                {
                    OrderDate = DateTime.Now,
                    TransactionId = transactionId,
                    AddressId = addressId,
                    PreparationId = preparationId
                };

                if (_orderService.CreateOrder(order))
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