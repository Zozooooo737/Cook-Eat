using Cook_Eat.models;
using MySql.Data.MySqlClient;

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
    }
}