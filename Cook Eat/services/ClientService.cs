using Cook_Eat.models;
using MySql.Data.MySqlClient;

namespace Cook_Eat.services
{
    public class ClientService
    {
        private readonly DatabaseService _databaseService;

        public ClientService(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public bool CreateClient(Client client)
        {
            string query = @"INSERT INTO client (client_username, user_id) 
                             VALUES (@Username, @UserId)";

            MySqlParameter[] parameters = {
                new MySqlParameter("@Username", client.ClientUsername),
                new MySqlParameter("@UserId", client.UserId)
            };

            return _databaseService.ExecuteNonQuery(query, parameters);
        }

        public List<Dictionary<string, object>> GetClientsSorted(string orderBy, bool ascending = true)
        {
            string direction = ascending ? "ASC" : "DESC";
            string query = $@"
                SELECT 
                    c.client_id as 'ID',
                    u.user_id as 'User ID',
                    u.first_name as 'Prénom',
                    u.last_name as 'Nom',
                    u.email as 'Email'
                FROM client c
                JOIN user_account u ON c.user_id = u.user_id
                ORDER BY {orderBy} {direction}";

            return _databaseService.ExecuteQuery(query);
        }

        public List<Dictionary<string, object>> GetClientsByAddress()
        {
            string query = @"
                SELECT 
                    c.client_id as 'ID',
                    u.user_id as 'User ID',
                    u.first_name as 'Prénom',
                    u.last_name as 'Nom',
                    CONCAT(a.street_number, ' ', a.street_address, ', ', a.postcode, ' ', a.city) as 'Adresse'
                FROM client c
                JOIN user_account u ON c.user_id = u.user_id
                LEFT JOIN address a ON u.user_id = a.user_id
                ORDER BY a.street_address";

            return _databaseService.ExecuteQuery(query);
        }

        public List<Dictionary<string, object>> GetClientsByTotalSpending()
        {
            string query = @"
                SELECT 
                    c.client_id as 'ID',
                    u.user_id as 'User ID',
                    u.first_name as 'Prénom',
                    u.last_name as 'Nom',
                    COALESCE(SUM(t.amount), 0) as 'Dépenses totales ($)'
                FROM client c
                JOIN user_account u ON c.user_id = u.user_id
                LEFT JOIN client_transaction t ON c.client_id = t.client_id
                GROUP BY c.client_id
                ORDER BY SUM(t.amount) DESC";

            return _databaseService.ExecuteQuery(query);
        }

        public bool UpdateClient(int clientId, string newUsername)
        {
            string query = @"
                UPDATE client 
                SET client_username = @Username
                WHERE client_id = @ClientId";

            MySqlParameter[] parameters = {
                new MySqlParameter("@Username", newUsername),
                new MySqlParameter("@ClientId", clientId)
            };

            return _databaseService.ExecuteNonQuery(query, parameters);
        }

        public bool DeleteClient(int clientId)
        {
            string query = "DELETE FROM client WHERE client_id = @ClientId";
            MySqlParameter[] parameters = { new MySqlParameter("@ClientId", clientId) };

            return _databaseService.ExecuteNonQuery(query, parameters);
        }
    }
}