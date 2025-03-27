using Cook_Eat.models;

namespace Cook_Eat.services
{
    class CookService
    {
        private DatabaseService _databaseService;

        public CookService(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public List<Dictionary<string, object>> GetSavedDishiesByCook(Cook cook)
        {
            string query = @"SELECT
                                d.dish_id AS dish_id,
                                d.dish_name AS dish_name,
                                CONCAT(dp.number_of_person, ' personne(s)') AS number_of_person,
                                CONCAT(dp.price, ' $') AS price
                            FROM 
                                dish d
                            JOIN 
                                dish_preparation dp ON d.dish_id = dp.dish_id 
                            WHERE 
                                dp.is_published = 1;";

            List<Dictionary<string, object>> dishPrepa;
            dishPrepa = _databaseService.ExecuteQuery(query);

            return dishPrepa;
        }
    }
}
