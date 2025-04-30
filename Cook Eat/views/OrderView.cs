using Cook_Eat.models;
using Cook_Eat.services;
using Cook_Eat.interfaces;
using System;
using System.Transactions;
using Mysqlx.Crud;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Cook_Eat.views
{
    public class OrderView
    {
        private readonly OrderService _orderService;
        private readonly ClientService _clientService;
        private readonly DishService _dishService;
        public static UserService _userService = new UserService(Program.DatabaseService);
        public static CookService _cookService = new CookService(Program.DatabaseService);
        public OrderView(OrderService orderService, ClientService clientService, DishService dishService) 
        {
            _orderService = orderService;
            _clientService = clientService;
            _dishService = dishService;
        }

        public void CreateOrderLine(int id = -1, int idT = -1)
        {
            Console.Clear();
            ConsoleView.ShowMenuTitle("MENU CRÉATION LIGNE DE COMMANDE");

            try
            {
                // Vérifier si le client existe
                int clientId;
                if (id == -1) { clientId = int.Parse(ConsoleView.GetUserInput("ID du client : ")); }
                else { clientId = id; }

                int transactionId;
                if (idT == -1) { transactionId = int.Parse(ConsoleView.GetUserInput("ID de la transaction : ")); }
                else { transactionId = idT; }

                int addressId;
                if(id == -1) addressId = int.Parse(ConsoleView.GetUserInput("ID de l'adresse de livraison : "));
                else
                {
                    Console.Clear();

                    List<Dictionary<string, object>> data = _userService.GetAddresses(UserInterface.currentId);

                    addressId = ConsoleView.DataChoice(data,"Adresse","ID", "CHOIX ADRESSE DE LIVRAISON");
                }

                int preparationId;
                if(id == -1) preparationId = int.Parse(ConsoleView.GetUserInput("ID de la préparation commandée : "));
                else
                {
                    Console.Clear();

                    preparationId = GetAvailableDishesForChoice();

                    Console.WriteLine();
                }

                    var order = new models.Order
                    {
                        OrderDate = DateTime.Now,
                        TransactionId = transactionId,
                        AddressId = addressId,
                        PreparationId = preparationId
                    };

                if (_orderService.CreateOrder(order) && _dishService.SwitchDishStatus(preparationId))
                {
                    ConsoleView.DisplayMessage("Commande créée avec succès !");
                    AddTransactionAmount(transactionId, preparationId);
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

        public List<Dictionary<string,object>> ApplyDishesFormat(List<Dictionary<string,object>> data)
        {
            List<Dictionary<string, object>> newData = [];

            foreach (Dictionary<string, object> element in data)
            {
                Dictionary<string, object> tmp = [];
                tmp["intitule"] = Convert.ToString(element["Nom du plat"]) + ", " + Convert.ToString(element["Portions"]) + " portions pour " + Convert.ToString(element["Prix"]) + "€ : du " + Convert.ToString(element["Date préparation"]) + " au " + Convert.ToString(element["Date péremption"]);
                tmp["id"] = element["ID Préparation"];

                newData.Add(tmp);
            }

            return newData;
        }

        public int GetAvailableDishesForChoice()
        {
            string[] menuOptions = {
                "Afficher tous les plats disponibles",
                "Afficher tous les plats des cuisiniers favoris",
                "Afficher les plats par cuisinier",
                }
            ;

            int choice = ConsoleView.ShowMenu("COMMENT VOUlEZ-VOUS CHERCHER UN PLAT ?", menuOptions);

            Console.Clear();

            switch (choice)
            {
                    default:
                        return GetAvailableDishesOverall();
                    case 1:
                        return GetAvailableDishesWithFavouriteSelection();
                    case 2:
                        return GetAvailableDishesPerCook();
            }
        }

        public int GetAvailableDishesOverall()
        {
            List<Dictionary<string, object>> data = _dishService.GetAvailableDishes();
            data = ApplyDishesFormat(data);

            return ConsoleView.DataChoice(data,"intitule","id","TOUS LES PLATS DISPONIBLES");
        }

        public int GetAvailableDishesWithFavouriteSelection()
        {
            List<Dictionary<string, object>> favoriteData = _cookService.GetFavoriteCook(ClientInterface.currentId);
            if (favoriteData == null || favoriteData.Count == 0)
            {
                ConsoleView.ShowMenuTitle("CHOISIR UN CUISINIER FAVORI");
                ConsoleView.DisplayMessage("Aucun cuisinier ajouté en favori.");
                ConsoleView.WaitForContinue();
                return GetAvailableDishesForChoice();
            }
            else
            {;
                Dictionary<string, object> tmp = [];
                tmp["PSEUDO"] = "Retour";
                tmp["ID"] = -1;
                favoriteData.Add(tmp);

                int cookId = ConsoleView.DataChoice(favoriteData, "PSEUDO", "ID", "CHOISIR UN CUISINIER FAVORI");
                if(cookId == -1) { return GetAvailableDishesForChoice(); }

                Console.Clear();

                List<Dictionary<string, object>> data = _dishService.GetAvailableDishes(cookId);

                if (data == null || data.Count == 0)
                {
                    ConsoleView.ShowMenuTitle("TOUS LES PLATS DU CUISINIER CHOISI");
                    ConsoleView.DisplayMessage("Aucun plat disponible pour le moment.");
                    ConsoleView.WaitForContinue();
                    return GetAvailableDishesWithFavouriteSelection();
                }
                
                data = ApplyDishesFormat(data);
                return ConsoleView.DataChoice(data, "intitule", "id", "TOUS LES PLATS DU CUISINIER CHOISI");
            }
        }

        public int GetAvailableDishesPerCook()
        {
            List<Dictionary<string, object>> cookData = _cookService.GetCookSorted("c.cook_id");
            int cookId = ConsoleView.DataPaginatedChoice(cookData, "Pseudo", "ID", "CHOISIR UN CUISINIER FAVORI");

            Console.Clear();

            List<Dictionary<string, object>> data = _dishService.GetAvailableDishes(cookId);

            if (data == null || data.Count == 0)
            {
                ConsoleView.ShowMenuTitle("TOUS LES PLATS DU CUISINIER CHOISI");
                ConsoleView.DisplayMessage("Aucun plat disponible pour le moment.");
                ConsoleView.WaitForContinue();
                return GetAvailableDishesPerCook();
            }

            data = ApplyDishesFormat(data);
            return ConsoleView.DataChoice(data, "intitule", "id", "TOUS LES PLATS DU CUISINIER CHOISI");
        }

        public void CreateTransaction(int id = -1)
        {
            try
            {
                int clientId;
                if (id == -1) { clientId = int.Parse(ConsoleView.GetUserInput("ID du client : ")); }
                else { clientId = id; }

                var transaction = new models.Transaction
                {
                    OrderDate = DateTime.Now,
                    Amount = 0,
                    IsDelivered = true,
                    ClientId = id
                };

                _orderService.CreateTransaction(transaction);
            }
            catch (FormatException)
            {
                ConsoleView.DisplayMessage("Données invalides.");
            }
        }

        public void AddTransactionAmount(int tId, int pId)
        {
            _orderService.AddTransactionAmount(tId, Convert.ToDouble(_orderService.GetPreparationAmount(pId)[0]["price"]));
        }

        public void CreateOrder(int id = -1)
        {
            CreateTransaction(id);
            int idT = GetLastTransactionId();

            string[] menuOptions = {
                "Ajouter une ligne de commande",
                "Valider la commande",
            };

            while (true)
            {
                Console.Clear();

                int choice = ConsoleView.ShowMenu("MENU CRÉATION COMMANDE", menuOptions);

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

        public void ShowCookDeliveryPath()
        {
            Console.Clear();

            List<Dictionary<string, object>> data = _orderService.GetCookOrders(CookInterface.currentId);
            List<Dictionary<string, object>> orders = [];

            foreach (Dictionary<string, object> element in data)
            {
                Dictionary<string, object> tmp = [];
                tmp["intitule"] = Convert.ToString(element["Nom du plat"]) + ", " + Convert.ToString(element["Portions"]) + " portions pour " + Convert.ToString(element["Prix"]) + "€ : du " + Convert.ToString(element["Date préparation"]) + " au " + Convert.ToString(element["Date péremption"]);
                tmp["id"] = element["ID Adresse"];

                orders.Add(tmp);
            }

            int choice = ConsoleView.DataChoice(orders, "intitule", "id", "CHOISIR COMMANDE À SIMULER");
            string endStation = _orderService.GetNearestMetroStation(choice);

            Console.Clear();

            List<Dictionary<string, object>> addresses = _userService.GetAddresses(UserInterface.currentId);
            choice = ConsoleView.DataChoice(addresses, "Adresse", "ID", "CHOISIR ADRESSE DE DÉPART");
            string startStation = _orderService.GetNearestMetroStation(choice);

            Console.Clear();
            ConsoleView.DisplayMessage(_orderService.SimulateDeliveryPathWithAddresses(startStation, endStation));
        }

        public void GetClientTransactions(int id)
        {
            List<Dictionary<string, object>> data = _orderService.GetClientTransactions(id);
            ConsoleView.ShowMenuTitle("MES COMMANDES");
            ConsoleView.DisplayTable(data);
        }

        public void GetClientOrders(int id)
        {
            List<Dictionary<string, object>> data = _orderService.GetClientOrders(id);
            ConsoleView.ShowMenuTitle("MES PLATS COMMANDÉS");
            ConsoleView.DisplayTable(data);
        }

        public void GetCookOrders(int id)
        {
            List<Dictionary<string, object>> data = _orderService.GetCookOrders(id);
            ConsoleView.ShowMenuTitle("MES PLATS COMMANDÉS");
            ConsoleView.DisplayTable(data);
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