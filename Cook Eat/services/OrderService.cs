using Cook_Eat.models;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System.Collections.Generic;

namespace Cook_Eat.services
{
    public class OrderService
    {
        private readonly DatabaseService _databaseService;

        public OrderService(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public bool CreateOrder(models.Order order)
        {
            string query = @"INSERT INTO client_order (order_date, transaction_id, address_id, preparation_id) 
                            VALUES (@OrderDate, @TransactionId, @AddressId, @PreparationId)";

            MySqlParameter[] parameters = {
                new MySqlParameter("@OrderDate", order.OrderDate),
                new MySqlParameter("@TransactionId", order.TransactionId),
                new MySqlParameter("@AddressId", order.AddressId),
                new MySqlParameter("@PreparationId", order.PreparationId)
            };

            return _databaseService.ExecuteNonQuery(query, parameters);
        }

        public bool CreateTransaction(Transaction transaction)
        {
            string query = @"INSERT INTO client_transaction (transaction_id, order_date, amount, is_delivered, client_id) 
                            VALUES (@TransactionId, @OrderDate, @Amount, @IsDelivered, @ClientId)";

            MySqlParameter[] parameters = {
                new MySqlParameter("@TransactionId", transaction.TransactionId),
                new MySqlParameter("@OrderDate", transaction.OrderDate),
                new MySqlParameter("@Amount", transaction.Amount),
                new MySqlParameter("@IsDelivered", transaction.IsDelivered),
                new MySqlParameter("@ClientId", transaction.ClientId)
            };

            return _databaseService.ExecuteNonQuery(query, parameters);
        }

        public Dictionary<string, object> GetLastTransaction()
        {
            string query = @"
                SELECT 
                    transaction_id
                FROM client_transaction
                ORDER BY transaction_id DESC"
            ;

            var result = _databaseService.ExecuteQuery(query);
            return result[0];
        }

        public decimal CalculateOrderPrice(int orderId)
        {
            string query = @"
                SELECT p.price 
                FROM client_order o
                JOIN dish_preparation p ON o.preparation_id = p.preparation_id
                WHERE o.order_id = @OrderId";

            MySqlParameter[] parameters = { new MySqlParameter("@OrderId", orderId) };

            var result = _databaseService.ExecuteQuery(query, parameters);
            return result.Count > 0 ? Convert.ToDecimal(result[0]["price"]) : 0;
        }

        public Dictionary<string, object> GetOrderDeliveryInfo(int orderId)
        {
            string query = @"
                SELECT 
                    c.client_id,
                    u.first_name,
                    u.last_name,
                    a.street_address,
                    a.city,
                    a.postcode,
                    d.dish_name,
                    p.preparation_date
                FROM client_order o
                JOIN client_transaction t ON o.transaction_id = t.transaction_id
                JOIN client c ON t.client_id = c.client_id
                JOIN user_account u ON c.user_id = u.user_id
                JOIN address a ON o.address_id = a.address_id
                JOIN dish_preparation p ON o.preparation_id = p.preparation_id
                JOIN dish d ON p.dish_id = d.dish_id
                WHERE o.order_id = @OrderId";

            MySqlParameter[] parameters = { new MySqlParameter("@OrderId", orderId) };

            var result = _databaseService.ExecuteQuery(query, parameters);
            return result.Count > 0 ? result[0] : null;
        }



        public string SimulateDeliveryPath(int orderId)
        {
            // 1. Récupérer l'ID de l'adresse du cuisinier depuis la commande
            int cookAddressId = GetCookAddressIdFromOrder(orderId);
            if (cookAddressId == 0) return "Adresse du cuisinier introuvable";

            // 2. Récupérer l'adresse du client
            int clientAddressId = GetClientAddressIdFromOrder(orderId);
            if (clientAddressId == 0) return "Adresse du client introuvable";

            // 3. Récupérer les stations de métro
            string cookStation = GetNearestMetroStation(cookAddressId);
            string clientStation = GetNearestMetroStation(clientAddressId);

            if (string.IsNullOrEmpty(cookStation)) return "Station du cuisinier introuvable";
            if (string.IsNullOrEmpty(clientStation)) return "Station du client introuvable";

            // 4. Générer le trajet
            Tools.GenererTrajetParNoms(cookStation, clientStation, true);

            return $"Trajet généré entre {cookStation} (cuisinier) et {clientStation} (client)";
        }

        // Méthodes auxiliaires
        private int GetCookAddressIdFromOrder(int orderId)
        {
            string query = @"
                SELECT o.address_id 
                FROM client_order o 
                WHERE o.order_id = @OrderId";

            MySqlParameter[] parameters = { new MySqlParameter("@OrderId", orderId) };

            var result = _databaseService.ExecuteQuery(query, parameters);

            return result.Count > 0 ? Convert.ToInt32(result[0]["address_id"]) : 0;
        }

        private int GetClientAddressIdFromOrder(int orderId)
        {
            string query = @"
            SELECT a.address_id 
            FROM client_order o
            JOIN client_transaction t ON o.transaction_id = t.transaction_id
            JOIN client c ON t.client_id = c.client_id
            JOIN user_account u ON c.user_id = u.user_id
            JOIN address a ON u.user_id = a.user_id
            WHERE o.order_id = @OrderId
            LIMIT 1";

            MySqlParameter[] parameters = { new MySqlParameter("@OrderId", orderId) };
            var result = _databaseService.ExecuteQuery(query, parameters );

            return result.Count > 0 ? Convert.ToInt32(result[0]["address_id"]) : 0;
        }

        private string GetNearestMetroStation(int addressId)
        {
            string query = @"
                SELECT nearest_metro_station 
                FROM address 
                WHERE address_id = @AddressId"
            ;

            MySqlParameter[] parameters = { new MySqlParameter("@AddressId", addressId) };

            var result = _databaseService.ExecuteQuery(query, parameters);

            return result.Count > 0 ? result[0]["nearest_metro_station"].ToString() : null;
        }
    }
}