using Cook_Eat.services;
using Cook_Eat.views;

namespace Cook_Eat
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var databaseService = new DatabaseService();
            var userService = new UserService(databaseService);
            var userView = new UserView(userService);
            var dishService = new DishService(databaseService);
            var dishView = new DishView(dishService);

            userView.CreateUser();
            dishView.DisplayAvailableDishes();
        }
    }
}
