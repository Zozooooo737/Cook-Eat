using Cook_Eat.models;
using MySql.Data.MySqlClient;

namespace Cook_Eat.services
{
    public class CookService
    {
        private readonly DatabaseService _databaseService;

        public CookService(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public bool CreateCook(Cook cook)
        {
            string query = @"INSERT INTO cook (cook_username, user_id) 
                             VALUES (@Username, @UserId)"
            ;

            MySqlParameter[] parameters = {
                new MySqlParameter("@Username", cook.CookUsername),
                new MySqlParameter("@UserId", cook.UserId)
            };

            return _databaseService.ExecuteNonQuery(query, parameters);
        }

        public List<Dictionary<string, object>> GetCookSorted(string orderBy, bool ascending = true)
        {
            string direction = ascending ? "ASC" : "DESC";
            string query = $@"
                SELECT 
                    c.cook_id as 'ID',
                    u.user_id as 'User ID',
                    u.first_name as 'Prénom',
                    u.last_name as 'Nom',
                    u.email as 'Email'
                FROM cook c
                JOIN user_account u ON c.user_id = u.user_id
                ORDER BY {orderBy} {direction}";

            return _databaseService.ExecuteQuery(query);
        }

        public List<Dictionary<string, object>> GetClientsServed(int cookId, DateTime? startDate = null, DateTime? endDate = null)
        {
            string query = @"
                SELECT DISTINCT
                    c.client_id as 'ID',
                    u.first_name as 'Prénom',
                    u.last_name as 'Nom',
                    COUNT(t.transaction_id) as 'Commandes'
                FROM client_transaction t
                JOIN client c ON t.client_id = c.client_id
                JOIN user_account u ON c.user_id = u.user_id
                JOIN client_order o ON t.transaction_id = o.transaction_id
                JOIN dish_preparation p ON o.preparation_id = p.preparation_id
                JOIN dish d ON p.dish_id = d.dish_id
                WHERE d.cook_id = @CookId";

            var parameters = new List<MySqlParameter> { new MySqlParameter("@CookId", cookId) };

            if (startDate.HasValue)
            {
                query += " AND t.order_date >= @StartDate";
                parameters.Add(new MySqlParameter("@StartDate", startDate.Value));
            }

            if (endDate.HasValue)
            {
                query += " AND t.order_date <= @EndDate";
                parameters.Add(new MySqlParameter("@EndDate", endDate.Value));
            }

            query += " GROUP BY c.client_id ORDER BY COUNT(t.transaction_id) DESC";

            return _databaseService.ExecuteQuery(query, parameters.ToArray());
        }

        public List<Dictionary<string, object>> ShowDishPreparations(int cookId)
        {
            string query = @"
                SELECT 
                    p.preparation_id as 'ID Préparation',
                    d.dish_name as 'Nom du plat',
                    CASE 
                        WHEN p.is_published = TRUE THEN 'Publié'
                        ELSE 'Non publié'
                    END as 'Statut',
                    DATE_FORMAT(p.preparation_date, '%d/%m/%Y %H:%i') as 'Date préparation',
                    DATE_FORMAT(p.peremption_date, '%d/%m/%Y') as 'Date péremption',
                    CONCAT(p.price, ' €') as 'Prix',
                    CONCAT(p.number_of_person, ' pers.') as 'Portions'
                FROM dish_preparation p
                JOIN dish d ON p.dish_id = d.dish_id
                WHERE d.cook_id = @CookId
                ORDER BY p.preparation_date DESC";

            MySqlParameter[] parameters = { new MySqlParameter("@CookId", cookId) };

            return _databaseService.ExecuteQuery(query, parameters);
        }

        public List<Dictionary<string, object>> GetDishesWithFrequency(int cookId)
        {
            string query = @"
    SELECT 
        d.dish_id as 'ID',
        d.dish_name as 'Plat',
        SUM(CASE WHEN p.is_published = FALSE THEN 1 ELSE 0 END) as 'Ventes'
    FROM dish d
    LEFT JOIN dish_preparation p ON d.dish_id = p.dish_id
    WHERE d.cook_id = @CookId
    GROUP BY d.dish_id
    ORDER BY Ventes DESC";

            MySqlParameter[] parameters = { new MySqlParameter("@CookId", cookId) };

            return _databaseService.ExecuteQuery(query, parameters);
        }

        public List<Dictionary<string, object>> GetTodaysDishes(int cookId)
        {
            string query = @"
                SELECT 
                    d.dish_name as 'Plat du jour',
                    p.price as 'Prix',
                    p.number_of_person as 'Pour'
                FROM dish_preparation p
                JOIN dish d ON p.dish_id = d.dish_id
                WHERE d.cook_id = @CookId 
                AND p.is_published = TRUE";

            MySqlParameter[] parameters = { new MySqlParameter("@CookId", cookId) };

            return _databaseService.ExecuteQuery(query, parameters);
        }

        public bool UpdateCook(int cookId, string newUsername)
        {
            string query = @"
                UPDATE cook 
                SET cook_username = @Username
                WHERE cook_id = @CookId";

            MySqlParameter[] parameters = {
                new MySqlParameter("@Username", newUsername),
                new MySqlParameter("@CookId", cookId)
            };

            return _databaseService.ExecuteNonQuery(query, parameters);
        }

        public bool DeleteCook(int cookId)
        {
            string query = @"DELETE FROM cook WHERE cook_id = @CookId";
            MySqlParameter[] parameters = { new MySqlParameter("@CookId", cookId) };

            return _databaseService.ExecuteNonQuery(query, parameters);
        }
    }
}
