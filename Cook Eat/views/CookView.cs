
using Cook_Eat.models;
using Cook_Eat.services;

namespace Cook_Eat.views
{
    public class CookView
    {
        private readonly CookService _cookService;

        public CookView(CookService cookService)
        {
            _cookService = cookService;
        }

        public void CreateCook()
        {
            try
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
            catch (FormatException)
            {
                ConsoleView.DisplayMessage("ID client invalide.");
            }
        }

        public void DisplayClients()
        {
            string[] menuOptions = {
                "Par ID client (<)",
                "Par ID client (>)",
                "Par prénom (A-Z)",
                "Par prénom (Z-A)",
                "Par nom (A-Z)",
                "Par nom (Z-A)",
                "Par ...",
                "Par ...",
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
                        data = _cookService.GetCookSorted("c.client_id", true);
                        title = "CLIENTS PAR ID (<)";
                        break;
                    case 1:
                        data = _cookService.GetCookSorted("c.client_id", false);
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
                    case 8:
                        return;
                    default:
                        continue;
                }
                ConsoleView.DisplayDataWithChoice(data, title);
                ConsoleView.WaitForContinue();
            }
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

        public void ShowDishesWithFrequency()
        {
            try
            {
                int cookId = int.Parse(ConsoleView.GetUserInput("ID du cuisinier : "));
                var dishes = _cookService.GetDishesWithFrequency(cookId);
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
                var dishes = _cookService.GetTodaysDishes(cookId);

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

        public void UpdateCook()
        {
            try
            {
                int cookId = int.Parse(ConsoleView.GetUserInput("ID cuisinier à modifier : "));
                string newUsername = ConsoleView.GetUserInput("Nouveau nom d'utilisateur : ");

                if (_cookService.UpdateCook(cookId, newUsername))
                {
                    ConsoleView.DisplayMessage("Nom d'utilisateur cuisinier mis à jour !");
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
