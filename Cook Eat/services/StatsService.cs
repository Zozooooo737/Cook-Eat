using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace Cook_Eat.services
{
    public class StatsService
    {
        private readonly DatabaseService _databaseService;

        public StatsService(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public List<Dictionary<string, object>> GetDeliveryCountByCook()
        {
            string query = @"
                SELECT 
                    c.cook_id AS 'ID Cuisinier',
                    c.cook_username AS 'Nom Cuisinier',
                    COUNT(DISTINCT t.transaction_id) AS 'Nombre de livraisons'
                FROM client_transaction t
                JOIN client_order o ON t.transaction_id = o.transaction_id
                JOIN dish_preparation p ON o.preparation_id = p.preparation_id
                JOIN dish d ON p.dish_id = d.dish_id
                JOIN cook c ON d.cook_id = c.cook_id
                WHERE t.is_delivered = TRUE
                GROUP BY c.cook_id
                ORDER BY COUNT(DISTINCT t.transaction_id) DESC";

            return _databaseService.ExecuteQuery(query);
        }

        public List<Dictionary<string, object>> GetAverageOrderPrice()
        {
            string query = @"
                SELECT 
                    CONCAT(ROUND(AVG(t.amount), 2), ' $') AS 'Prix moyen des commandes'
                FROM client_transaction t";

            return _databaseService.ExecuteQuery(query);
        }

        public List<Dictionary<string, object>> GetAverageClientSpending()
        {
            string query = @"
                SELECT 
                    CONCAT(ROUND(AVG(total_spent), 2), ' $') AS 'Dépense moyenne par client'
                FROM (
                    SELECT 
                        c.client_id,
                        SUM(t.amount) AS total_spent
                    FROM client_transaction t
                    JOIN client c ON t.client_id = c.client_id
                    GROUP BY c.client_id
                ) AS client_totals";

            return _databaseService.ExecuteQuery(query);
        }

        public List<Dictionary<string, object>> GetClientOrdersByDish(int clientId)
        {
            string query = @"
                SELECT 
                    d.dish_name AS 'Plat commandé',
                    COUNT(o.order_id) AS 'Nombre de commandes',
                    CONCAT(ROUND(SUM(t.amount), 2), ' $') AS 'Total dépensé',
                    MAX(DATE_FORMAT(t.order_date, '%d/%m/%Y')) AS 'Dernière commande'
                FROM client_transaction t
                JOIN client_order o ON t.transaction_id = o.transaction_id
                JOIN dish_preparation p ON o.preparation_id = p.preparation_id
                JOIN dish d ON p.dish_id = d.dish_id
                WHERE t.client_id = @ClientId
                GROUP BY d.dish_id
                ORDER BY COUNT(o.order_id) DESC";

            MySqlParameter[] parameters = {
                new MySqlParameter("@ClientId", clientId)
            };

            return _databaseService.ExecuteQuery(query, parameters);
        }
    }
}