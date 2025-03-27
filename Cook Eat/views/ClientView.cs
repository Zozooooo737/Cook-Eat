using Cook_Eat.models;
using Cook_Eat.services;

namespace Cook_Eat.views
{
    public class ClientView
    {
        private readonly ClientService _clientService;

        public ClientView(ClientService clientService)
        {
            _clientService = clientService;
        }

        public void CreateClient()
        {
            try
            {
                int userId = int.Parse(ConsoleView.GetUserInput("ID utilisateur existant à associer : "));
                string username = ConsoleView.GetUserInput("Nom d'utilisateur client : ");

                var client = new Client
                {
                    UserId = userId,
                    ClientUsername = username
                };

                if (_clientService.CreateClient(client))
                {
                    ConsoleView.DisplayMessage("Compte client créé avec succès !");
                }
                else
                {
                    ConsoleView.DisplayMessage("Échec de la création du compte client.");
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
                "Par adresse",
                "Par dépenses totales",
                "Retour"
            };

            while (true)
            {
                Console.Clear();
                int choice = ConsoleView.ShowMenu("=== AFFICHAGE CLIENTS ===", menuOptions);

                List<Dictionary<string, object>> data;
                string title;

                switch (choice)
                {
                    case 0:
                        data = _clientService.GetClientsSorted("c.client_id", true);
                        title = "CLIENTS PAR ID (<)";
                        break;
                    case 1:
                        data = _clientService.GetClientsSorted("c.client_id", false);
                        title = "CLIENTS PAR ID (>)";
                        break;
                    case 2:
                        data = _clientService.GetClientsSorted("u.first_name", true);
                        title = "CLIENTS PAR PRENOM (A-Z)";
                        break;
                    case 3:
                        data = _clientService.GetClientsSorted("u.first_name", false);
                        title = "CLIENTS PAR PRENOM (Z-A)";
                        break;
                    case 4:
                        data = _clientService.GetClientsSorted("u.last_name", true);
                        title = "CLIENTS PAR NOM (A-Z)";
                        break;
                    case 5:
                        data = _clientService.GetClientsSorted("u.last_name", false);
                        title = "CLIENTS PAR NOM (Z-A)";
                        break;
                    case 6:
                        data = _clientService.GetClientsByAddress();
                        title = "CLIENTS PAR ADRESSE";
                        break;
                    case 7:
                        data = _clientService.GetClientsByTotalSpending();
                        title = "CLIENTS PAR DEPENSES";
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

        public void UpdateClient()
        {
            try
            {
                int clientId = int.Parse(ConsoleView.GetUserInput("ID client à modifier : "));
                string newUsername = ConsoleView.GetUserInput("Nouveau nom d'utilisateur : ");

                if (_clientService.UpdateClient(clientId, newUsername))
                {
                    ConsoleView.DisplayMessage("Nom d'utilisateur client mis à jour !");
                }
                else
                {
                    ConsoleView.DisplayMessage("Échec de la mise à jour.");
                }
            }
            catch (FormatException)
            {
                ConsoleView.DisplayMessage("ID client invalide.");
            }
        }

        public void DeleteClient()
        {
            try
            {
                int clientId = int.Parse(ConsoleView.GetUserInput("ID client à supprimer : "));

                if (_clientService.DeleteClient(clientId))
                {
                    ConsoleView.DisplayMessage("Compte client supprimé (le compte utilisateur reste actif).");
                }
                else
                {
                    ConsoleView.DisplayMessage("Échec de la suppression.");
                }
            }
            catch (FormatException)
            {
                ConsoleView.DisplayMessage("ID client invalide.");
            }
        }
    }
}