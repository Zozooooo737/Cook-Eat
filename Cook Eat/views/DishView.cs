using Cook_Eat.services;

namespace Cook_Eat.views
{
    public class DishView
    {
        private DishService _dishService;

        public DishView(DishService dishService)
        {
            _dishService = dishService;
        }

        public void DisplayAvailableDishes()
        {
            List<Dictionary<string, object>> dishPrepa = _dishService.GetAvailableDishies();

            ConsoleView.DisplayTable(dishPrepa);
        }
    }
}