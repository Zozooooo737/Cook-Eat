using Cook_Eat.models;
using Cook_Eat.services;
using System;
using System.Net;
using System.Runtime.Intrinsics.Arm;

namespace Cook_Eat.views
{
    public class DishView
    {
        private readonly DishService _dishService;

        public DishView(DishService dishService)
        {
            _dishService = dishService;
        }

        public void DisplayAvailableDishes(int id = -1)
        {
            var dishes = _dishService.GetAvailableDishes(id);
            ConsoleView.ShowMenuTitle("PLATS DISPONIBLES");
            ConsoleView.DisplayTable(dishes);
        }

        public void ShowDishesWithFrequency()
        {
            try
            {
                int cookId = int.Parse(ConsoleView.GetUserInput("Pseudo du cuisinier : "));
                var dishes = _dishService.GetDishesWithFrequency(cookId);
                ConsoleView.DisplayDataWithChoice(dishes, $"PLATS ET VENTE - CUISINIER #{cookId}");
            }
            catch (FormatException)
            {
                ConsoleView.DisplayMessage("ID invalide.");
            }
        }

        public void ShowTodaysDishes()
        {
            try
            {
                int cookId = int.Parse(ConsoleView.GetUserInput("ID du cuisinier : "));
                var dishes = _dishService.GetTodaysDishes(cookId);

                if (dishes.Count == 0)
                {
                    ConsoleView.DisplayMessage("Aucun plat du jour trouvé.");
                    return;
                }

                ConsoleView.DisplayDataWithChoice(dishes, $"PLATS DU JOUR - CUISINIER #{cookId}");
            }
            catch (FormatException)
            {
                ConsoleView.DisplayMessage("ID invalide.");
            }
        }

        public void ShowFavouriteCookDishes(int id = -1)
        {
            var dishes = _dishService.GetFavouriteCookDishes(id);

            if (dishes.Count == 0)
            {
                ConsoleView.DisplayMessage("Aucun plat disponible.");
                return;
            }

            ConsoleView.DisplayDataWithChoice(dishes, "PLATS DES CUISINIERS FAVORIS");
        }
        public void DisplayAllDishes(int id = -1)
        {
            var dishes = _dishService.GetAllDishes(id);
            ConsoleView.DisplayTable(dishes);
        }

        public void AddNewDish(int id = -1)
        {
            try
            {
                string dishName = ConsoleView.GetUserInput("Nom du plat : ");
                int cookId;
                if (id == -1) cookId = int.Parse(ConsoleView.GetUserInput("ID du cuisinier : "));
                else cookId = id;

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

        public void AddDishPreparation(int id = -1)
        {
            try
            {
                int dishId;
                if (id == -1) dishId = int.Parse(ConsoleView.GetUserInput("ID du plat de référence : "));
                else 
                { 
                    List<Dictionary<string, object>> data = _dishService.GetAllDishes(id); 
                    dishId = ConsoleView.DataChoice(data, "Nom du plat", "ID", "PLAT DE RÉFÉRENCE À UTILISER"); 
                }
                if (dishId == -1)
                {
                    ConsoleView.DisplayMessage("Aucun plat de référence encore créé...");
                    ConsoleView.WaitForContinue();
                }

                Console.WriteLine();
                ConsoleView.ShowMenuTitle("DÉTAILS DE LA PRÉPARATION");
                decimal price = decimal.Parse(ConsoleView.GetUserInput("Prix : "));
                int portions = int.Parse(ConsoleView.GetUserInput("Nombre de portions : "));

                var preparation = new DishPreparation
                {
                    DishId = dishId,
                    Price = price,
                    NumberOfPerson = portions,
                    IsPublished = true,
                    PreparationDate = DateTime.Now,
                    PeremptionDate = DateTime.Now.AddDays(3)
                };

                Console.WriteLine();

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

        public void DeleteDishPreparation(int id)
        {
            List<Dictionary<string, object>> data = _dishService.GetAvailableDishes(id);
            int prepId = ConsoleView.DataChoice(data, "Nom du plat", "ID Préparation", "PLAT DE RÉFÉRENCE À SUPPRIMER");

            Console.WriteLine();

            if (_dishService.DeleteDishPreparation(prepId))
            {
                ConsoleView.DisplayMessage("Plat supprimé avec succès !");
            }
            else
            {
                ConsoleView.DisplayMessage("Échec de la suppression du plat.");
            }
        }
    }
}