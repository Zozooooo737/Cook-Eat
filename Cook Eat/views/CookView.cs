
using Cook_Eat.models;
using Cook_Eat.services;
using Cook_Eat.interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Cook_Eat.views
{
    public class CookView
    {
        private readonly CookService _cookService;

        public CookView(CookService cookService)
        {
            _cookService = cookService;
        }

        public void CreateCook(bool adminMode = true)
        {
            ConsoleView.ShowMenuTitle("CRÉATION DU PROFIL CUISINIER");

            try
            {
                if (adminMode)
                {
                    int userId = int.Parse(ConsoleView.GetUserInput("ID utilisateur existant à associer : "));
                    string username = ConsoleView.GetUserInput("Nom d'utilisateur cuisinier : ");

                    var cook = new Cook
                    {
                        UserId = userId,
                        CookUsername = username
                    };

                    if (_cookService.CreateCook(cook))
                    {
                        ConsoleView.DisplayMessage("Compte cuisinier créé avec succès !");
                    }
                    else
                    {
                        ConsoleView.DisplayMessage("Échec de la création du compte cuisinier.");
                    }
                }
                else
                {
                    string username = ConsoleView.GetUserInput("Entrez votre nom d'utilisateur Cuisinier : ");

                    var cook = new Cook
                    {
                        UserId = UserInterface.currentId,
                        CookUsername = username
                    };

                    if (_cookService.CreateCook(cook))
                    {
                        ConsoleView.DisplayMessage("Compte cuisinier créé avec succès !");
                    }
                    else
                    {
                        ConsoleView.DisplayMessage("Échec de la création du compte cuisinier.");
                    }
                }
            }
            catch (FormatException)
            {
                ConsoleView.DisplayMessage("ID utilisateur invalide.");
            }
        }

        public int GetLastCookId()
        {
            List<Dictionary<string, object>> data = _cookService.GetCookSorted("c.cook_id", false);
            return Convert.ToInt32(data[0]["ID"]);
        }

        public void DisplayCook(int id = -1)
        {
            if (id == -1)
            {
                string[] menuOptions = {
                "Par ID cook (<)",
                "Par ID cook (>)",
                "Par prénom (A-Z)",
                "Par prénom (Z-A)",
                "Par nom (A-Z)",
                "Par nom (Z-A)",
                "Retour"
                };

                while (true)
                {
                    Console.Clear();
                    int choice = ConsoleView.ShowMenu("AFFICHAGE CUISINIERS", menuOptions);

                    List<Dictionary<string, object>> data;
                    string title;

                    switch (choice)
                    {
                        case 0:
                            data = _cookService.GetCookSorted("c.cook_id", true);
                            title = "CLIENTS PAR ID (<)";
                            break;
                        case 1:
                            data = _cookService.GetCookSorted("c.cook_id", false);
                            title = "CLIENTS PAR ID (>)";
                            break;
                        case 2:
                            data = _cookService.GetCookSorted("u.first_name", true);
                            title = "CLIENTS PAR PRENOM (A-Z)";
                            break;
                        case 3:
                            data = _cookService.GetCookSorted("u.first_name", false);
                            title = "CLIENTS PAR PRENOM (Z-A)";
                            break;
                        case 4:
                            data = _cookService.GetCookSorted("u.last_name", true);
                            title = "CLIENTS PAR NOM (A-Z)";
                            break;
                        case 5:
                            data = _cookService.GetCookSorted("u.last_name", false);
                            title = "CLIENTS PAR NOM (Z-A)";
                            break;
                        case 6:
                            return;
                        default:
                            continue;
                    }
                    ConsoleView.DisplayDataWithChoice(data, title);
                }
            }
            else
            {
                List<Dictionary<string, object>> data = _cookService.GetCook(id);
                ConsoleView.ShowMenuTitle("MON PROFIL CUISINIER");
                ConsoleView.DisplayTable(data);
            }
        }

        public void AddFavoriteCook(int id)
        {
            ConsoleView.ShowMenuTitle("AJOUT D'UN CUISINIER FAVORI");

            string cookName = ConsoleView.GetUserInput("Entrez le pseudo du cuisinier à ajouter : ");
            List<Dictionary<string,object>> data = _cookService.GetCookIdWithName(cookName);
            Console.WriteLine();

            if (data != null && data.Count != 0)
            {
                if (_cookService.AddFavoriteCook(id, Convert.ToInt32(data[0]["cook_id"])))
                {
                    ConsoleView.DisplayMessage("Cuisinier ajouté avec succès !");
                }
                else
                {
                    ConsoleView.DisplayMessage("Échec de l'ajout.");
                }
            }
            else ConsoleView.DisplayMessage("Pseudo du cuisinier invalide.");
        }

        public void DisplayFavoriteCook(int id)
        {
            List<Dictionary<string, object>> data = _cookService.GetFavoriteCook(id);
            ConsoleView.ShowMenuTitle("MES CUISINIERS FAVORIS");
            ConsoleView.DisplayTable(data);
        }

        public void DisplayCookDishes(int id)
        {
            List<Dictionary<string, object>> data = _cookService.GetFavoriteCook(id);
            string title = "CUISINIERS FAVORIS DU CLIENT D'ID " + id;
            ConsoleView.DisplayDataWithChoice(data, title);
        }

        public void ShowClientsServed()
        {
            try
            {
                int cookId = int.Parse(ConsoleView.GetUserInput("ID du cuisinier : "));

                Console.WriteLine("\n(Optionnel) Période :");
                DateTime? startDate = GetOptionalDate("Date de début (JJ/MM/AAAA) : ");
                DateTime? endDate = GetOptionalDate("Date de fin (JJ/MM/AAAA) : ");

                var clients = _cookService.GetClientsServed(cookId, startDate, endDate);
                ConsoleView.DisplayDataWithChoice(clients, $"CLIENTS SERVIS PAR CUISINIER #{cookId}");
            }
            catch (FormatException)
            {
                ConsoleView.DisplayMessage("ID ou date invalide.");
            }
        }

        private DateTime? GetOptionalDate(string prompt)
        {
            string input = ConsoleView.GetUserInput(prompt);
            return string.IsNullOrEmpty(input) ? null : DateTime.Parse(input);
        }

        public void ShowDishPreparations()
        {
            try
            {
                int cookId = int.Parse(ConsoleView.GetUserInput("ID du cuisinier : "));
                var dishes = _cookService.ShowDishPreparations(cookId);
                ConsoleView.DisplayDataWithChoice(dishes, $"PREPARATIONS - CUISINIER #{cookId}");
            }
            catch (FormatException)
            {
                ConsoleView.DisplayMessage("ID invalide.");
            }
        }

        public void UpdateCook(int id = -1)
        {
            try
            {
                int cookId;
                if (id == -1) { ConsoleView.ShowMenuTitle("MODIFICATION D'UN PROFIL CUISINIER"); cookId = int.Parse(ConsoleView.GetUserInput("ID cuisinier à modifier : ")); }
                else { ConsoleView.ShowMenuTitle("MODIFICATION DU PROFIL CUISINIER"); cookId = id; }

                string newUsername = ConsoleView.GetUserInput("Nouveau pseudo : ");

                Console.WriteLine();

                if (_cookService.UpdateCook(cookId, newUsername))
                {
                    ConsoleView.DisplayMessage("Pseudo du profil cuisinier mis à jour !");
                }
                else
                {
                    ConsoleView.DisplayMessage("Échec de la mise à jour.");
                }
            }
            catch (FormatException)
            {
                ConsoleView.DisplayMessage("ID cuisinier invalide.");
            }
        }

        public void DeleteCook()
        {
            try
            {
                int cookId = int.Parse(ConsoleView.GetUserInput("ID cuisnier à supprimer : "));

                if (_cookService.DeleteCook(cookId))
                {
                    ConsoleView.DisplayMessage("Compte cuisnier supprimé.");
                }
                else
                {
                    ConsoleView.DisplayMessage("Échec de la suppression.");
                }
            }
            catch (FormatException)
            {
                ConsoleView.DisplayMessage("ID cuisinier invalide.");
            }
        }
    }
}
