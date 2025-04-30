using Cook_Eat.models;
using Cook_Eat.services;
using Cook_Eat.interfaces;

namespace Cook_Eat.views
{
    public class ClientView
    {
        private readonly ClientService _clientService;

        public ClientView(ClientService clientService)
        {
            _clientService = clientService;
        }

        public void CreateClient(bool adminMode = true)
        {
            ConsoleView.ShowMenuTitle("CRÉATION DU PROFIL CLIENT");

            try
            {
                if (adminMode)
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
                else
                {
                    string username = ConsoleView.GetUserInput("Entrez votre nom d'utilisateur Client : ");

                    var client = new Client
                    {
                        UserId = UserInterface.currentId,
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
            }
            catch (FormatException)
            {
                ConsoleView.DisplayMessage("ID client invalide.");
            }
        }

        public int GetLastClientId()
        {
            List<Dictionary<string,object>> data = _clientService.GetClientsSorted("c.client_id", false);
            return Convert.ToInt32(data[0]["ID"]);
        }

        public void DisplayClients(int id = -1)
        {
            if (id == -1)
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
                    int choice = ConsoleView.ShowMenu("AFFICHAGE CLIENTS", menuOptions);

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
                }
            }
            else
            {
                List<Dictionary<string, object>> data = _clientService.GetClient(id);
                ConsoleView.ShowMenuTitle("MON PROFIL CLIENT");
                ConsoleView.DisplayTable(data);
            }
        }

        public void UpdateClient(int id = -1)
        {
            try
            {
                int clientId;
                if (id == -1) { ConsoleView.ShowMenuTitle("MODIFICATION D'UN PROFIL CLIENT"); clientId = int.Parse(ConsoleView.GetUserInput("ID client à modifier : ")); }
                else { ConsoleView.ShowMenuTitle("MODIFICATION DU PROFIL CLIENT"); clientId = id; }

                string newUsername = ConsoleView.GetUserInput("Nouveau pseudo: ");

                Console.WriteLine();

                if (_clientService.UpdateClient(clientId, newUsername))
                {
                    ConsoleView.DisplayMessage("Pseudo du profil client mis à jour !");
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
                    ConsoleView.DisplayMessage("Compte client supprimé.");
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