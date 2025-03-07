namespace Cook_Eat.services
{
    public class DishService
    {
        private DatabaseService _databaseService;

        public DishService(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public List<Dictionary<string, object>> GetAvailableDishies()
        {
            string query = @"SELECT
                                dp.preparation_id AS preparation_id,
                                d.dish_name AS dish_name,
                                DATE_FORMAT(dp.preparation_date, '%Y-%m-%d') AS preparation_date,
                                DATE_FORMAT(dp.peremption_date, '%Y-%m-%d') AS peremption_date,
                                CONCAT(dp.number_of_person, ' personne(s)') AS number_of_person,
                                CONCAT(dp.price, ' $') AS price
                            FROM 
                                dish_preparation dp
                            JOIN 
                                dish d ON dp.dish_id = d.dish_id
                            WHERE 
                                dp.is_published = 1;";

            List<Dictionary<string, object>> dishPrepa;
            dishPrepa = _databaseService.ExecuteQuery(query);

            return dishPrepa;
        }
    }
}