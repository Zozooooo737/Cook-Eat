using Cook_Eat.models;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System.Collections.Generic;
using static Mysqlx.Crud.Order.Types;

namespace Cook_Eat.services
{
    public class DishService
    {
        private readonly DatabaseService _databaseService;

        public DishService(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public List<Dictionary<string, object>> GetAvailableDishes()
        {
            string query = @"SELECT
                    dp.preparation_id AS 'ID Préparation',
                    d.dish_name AS 'Nom du plat',
                    DATE_FORMAT(dp.preparation_date, '%d/%m/%Y %H:%i') AS 'Date préparation',
                    DATE_FORMAT(dp.peremption_date, '%d/%m/%Y') AS 'Date péremption',
                    CONCAT(dp.number_of_person, ' pers.') AS 'Portions',
                    CONCAT(dp.price, ' $') AS 'Prix',
                    CASE 
                        WHEN dp.is_published = 1 THEN 'Disponible'
                        ELSE 'Indisponible'
                    END AS 'Statut'
                FROM dish_preparation dp
                JOIN dish d ON dp.dish_id = d.dish_id
                WHERE dp.is_published = 1
                ORDER BY dp.preparation_date DESC";

            return _databaseService.ExecuteQuery(query);
        }

        public bool AddDish(Dish dish)
        {
            string query = "INSERT INTO dish (dish_name, cook_id) VALUES (@DishName, @CookId)";

            MySqlParameter[] parameters = {
                new MySqlParameter("@DishName", dish.DishName),
                new MySqlParameter("@CookId", dish.CookId)
            };

            return _databaseService.ExecuteNonQuery(query, parameters);
        }

        public bool AddDishPreparation(DishPreparation preparation)
        {
            string query = @"INSERT INTO dish_preparation 
                            (is_published, preparation_date, peremption_date, price, number_of_person, dish_id) 
                            VALUES (@IsPublished, @PreparationDate, @PeremptionDate, @Price, @NumberOfPerson, @DishId)";

            MySqlParameter[] parameters = {
                new MySqlParameter("@IsPublished", preparation.IsPublished),
                new MySqlParameter("@PreparationDate", preparation.PreparationDate),
                new MySqlParameter("@PeremptionDate", preparation.PeremptionDate),
                new MySqlParameter("@Price", preparation.Price),
                new MySqlParameter("@NumberOfPerson", preparation.NumberOfPerson),
                new MySqlParameter("@DishId", preparation.DishId)
            };

            return _databaseService.ExecuteNonQuery(query, parameters);
        }

        public List<Dictionary<string, object>> GetAllDishes()
        {
            string query = @"SELECT 
                            dish_id AS 'ID', 
                            dish_name AS 'Nom du plat', 
                            cook_id AS 'ID Cuisinier'
                        FROM dish
                        ORDER BY dish_name";

            return _databaseService.ExecuteQuery(query);
        }

        public Dictionary<int, Tuple<int, int>> GetTransactionClientCook()
        {
            string query = @" SELECT 
                    t.transaction_id,
                    c.client_id,
                    d.cook_id
                FROM client_transaction t
                JOIN client_order o ON t.transaction_id = o.transaction_id
                JOIN client c ON t.client_id = c.client_id
                JOIN dish_preparation p ON o.preparation_id = p.preparation_id
                JOIN dish d ON p.dish_id = d.dish_id";

            var results = _databaseService.ExecuteQuery(query);

            var transactionClientCook = new Dictionary<int, Tuple<int, int>>();

            foreach (var row in results)
            {
                int transactionId = Convert.ToInt32(row["transaction_id"]);
                int clientId = Convert.ToInt32(row["client_id"]);
                int cookId = Convert.ToInt32(row["cook_id"]);

                transactionClientCook[transactionId] = Tuple.Create(clientId, cookId);
            }

            return transactionClientCook;
        }

        public bool SwitchDishStatus(int id)
        {
            string query = $@"UPDATE 
                            dish_preparation
                        SET is_published = FALSE
                        WHERE preparation_id = {id}";

            return _databaseService.ExecuteNonQuery(query);
        }
    }
}