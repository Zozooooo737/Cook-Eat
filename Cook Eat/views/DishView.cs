using Cook_Eat.models;
using Cook_Eat.services;
using System;

namespace Cook_Eat.views
{
    public class DishView
    {
        private readonly DishService _dishService;

        public DishView(DishService dishService)
        {
            _dishService = dishService;
        }

        public void DisplayAvailableDishes()
        {
            var dishes = _dishService.GetAvailableDishes();
            ConsoleView.DisplayTable(dishes);
        }

        public void DisplayAllDishes()
        {
            var dishes = _dishService.GetAllDishes();
            ConsoleView.DisplayTable(dishes);
        }

        public void AddNewDish()
        {
            try
            {
                string dishName = ConsoleView.GetUserInput("Nom du plat : ");
                int cookId = int.Parse(ConsoleView.GetUserInput("ID du cuisinier : "));

                var dish = new Dish
                {
                    DishName = dishName,
                    CookId = cookId
                };

                if (_dishService.AddDish(dish))
                {
                    ConsoleView.DisplayMessage("Plat ajouté avec succès !");
                }
                else
                {
                    ConsoleView.DisplayMessage("Échec de l'ajout du plat.");
                }
            }
            catch (FormatException)
            {
                ConsoleView.DisplayMessage("Données invalides.");
            }
        }

        public void AddDishPreparation()
        {
            try
            {
                int dishId = int.Parse(ConsoleView.GetUserInput("ID du plat : "));
                decimal price = decimal.Parse(ConsoleView.GetUserInput("Prix : "));
                int portions = int.Parse(ConsoleView.GetUserInput("Nombre de portions : "));
                bool isPublished = ConsoleView.GetUserInput("Publier maintenant ? (oui/non) : ").ToLower() == "oui";

                var preparation = new DishPreparation
                {
                    DishId = dishId,
                    Price = price,
                    NumberOfPerson = portions,
                    IsPublished = isPublished,
                    PreparationDate = DateTime.Now,
                    PeremptionDate = DateTime.Now.AddDays(3)
                };

                if (_dishService.AddDishPreparation(preparation))
                {
                    ConsoleView.DisplayMessage("Préparation ajoutée avec succès !");
                }
                else
                {
                    ConsoleView.DisplayMessage("Échec de l'ajout de la préparation.");
                }
            }
            catch (FormatException)
            {
                ConsoleView.DisplayMessage("Données invalides.");
            }
        }
    }
}