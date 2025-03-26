using Cook_Eat.models;
using MySql.Data.MySqlClient;
using System.Reflection.PortableExecutable;

namespace Cook_Eat.services
{
    public class UserService
    {
        private DatabaseService _databaseService;

        public UserService(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public void CreateUser(UserAccount user)
        {
            string query = "INSERT INTO user_account (first_name, last_name, email, password, is_business) " +
                           "VALUES (@FirstName, @LastName, @Email, @Password, @IsBusiness);";

            MySqlParameter[] parameters = {
            new MySqlParameter("@FirstName", user.FirstName),
            new MySqlParameter("@LastName", user.LastName),
            new MySqlParameter("@Email", user.Email),
            new MySqlParameter("@Password", user.Password),
            new MySqlParameter("@IsBusiness", user.IsBusiness)
            };

            _databaseService.ExecuteNonQuery(query, parameters);
        }

        public List<Dictionary<string, object>> GetAllUsersForDisplay()
        {
            string query = @"SELECT 
                        user_id as 'ID',
                        CONCAT(first_name, ' ', last_name) as 'Nom complet',
                        COALESCE(phone_number, 'Non renseigné') as 'Téléphone',
                        email as 'Email',
                        CASE WHEN is_business THEN 'Entreprise' ELSE 'Particulier' END as 'Type'
                    FROM user_account";

            return _databaseService.ExecuteQuery(query);
        }

        // Méthode générique de base
        public List<Dictionary<string, object>> GetUsersSorted(string orderBy = "last_name", bool ascending = true)
        {
            string direction = ascending ? "ASC" : "DESC";
            string query = $@"SELECT 
                        user_id as 'ID',
                        CONCAT(first_name, ' ', last_name) as 'Nom',
                        email as 'Email',
                        phone_number as 'Numéro',
                        CASE WHEN is_business THEN 'Entreprise' ELSE 'Particulier' END as 'Type'
                      FROM user_account
                      ORDER BY {orderBy} {direction}";

            return _databaseService.ExecuteQuery(query);
        }

        // Méthodes spéciales (exemples)
        public List<Dictionary<string, object>> GetUsersByAddress()
        {
            string query = @"SELECT 
                        u.user_id as 'ID',
                        CONCAT(u.first_name, ' ', u.last_name) as 'Nom',
                        COALESCE(a.street_address, 'Non renseigné') as 'Adresse'
                      FROM user_account u
                      LEFT JOIN address a ON u.user_id = a.user_id
                      ORDER BY a.street_address";

            return _databaseService.ExecuteQuery(query);
        }

        public UserAccount GetUserById(int userId)
        {
            string query = "SELECT * FROM user_account WHERE user_id = @UserId";

            MySqlParameter[] parameters = {
                new MySqlParameter("@UserId", userId)
            };

            var data = _databaseService.ExecuteQuery(query, parameters);

            if (data == null || data.Count == 0)
            {
                return null;
            }

            var userData = data[0];

            return new UserAccount
            {
                UserId = Convert.ToInt32(userData["user_id"]),
                FirstName = Convert.ToString(userData["first_name"]),
                LastName = Convert.ToString(userData["last_name"]),
                Email = Convert.ToString(userData["email"]),
                Password = Convert.ToString(userData["password"]),
                IsBusiness = Convert.ToBoolean(userData["is_business"])
            };
        }

        public bool UpdateUser(UserAccount user)
        {
            string query = @"UPDATE user_account 
                    SET first_name = @FirstName, 
                        last_name = @LastName, 
                        email = @Email, 
                        password = @Password, 
                        is_business = @IsBusiness 
                    WHERE user_id = @UserId";

            MySqlParameter[] parameters = {
                new MySqlParameter("@FirstName", user.FirstName),
                new MySqlParameter("@LastName", user.LastName),
                new MySqlParameter("@Email", user.Email),
                new MySqlParameter("@Password", user.Password),
                new MySqlParameter("@IsBusiness", user.IsBusiness),
                new MySqlParameter("@UserId", user.UserId)
            };

            return _databaseService.ExecuteNonQuery(query, parameters);
        }

        // On renvoit true mais le user n'existait pas, améliorer ?
        public bool DeleteUser(int userId)
        {
            try
            {
                string query = "DELETE FROM user_account WHERE user_id = @UserId;";

                MySqlParameter[] parameters = {
                    new MySqlParameter("@UserId", userId)
                };

                _databaseService.ExecuteNonQuery(query, parameters);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}