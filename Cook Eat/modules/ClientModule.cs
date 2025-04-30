using Cook_Eat.services;
using Cook_Eat.views;
using Cook_Eat.interfaces;

namespace Cook_Eat.modules
{
    public static class ClientModule
    {
        private static readonly ClientService _clientService = new ClientService(Program.DatabaseService);
        private static readonly ClientView _clientView = new ClientView(_clientService);

        public static void Show(bool adminMode = true, int id = -1)
        {
            string[] menuOptions = null;
            if (adminMode)
            {
                string[] tmp = {
                "Créer un compte client",
                "Afficher les clients",
                "Modifier un compte client",
                "Supprimer un compte client",
                "Retour au menu principal"
                };
                menuOptions = tmp;
            }
            else
            {
                string[] tmp = {
                "Afficher mes infos",
                "Modifier mon compte client",
                "Retour au menu principal"
                };
                menuOptions = tmp;
            }

            while (true)
            {
                Console.Clear();

                int choice;
                if(adminMode) choice = ConsoleView.ShowMenu("MODULE CLIENTS", menuOptions);
                else choice = ConsoleView.ShowMenu("MON PROFIL CLIENT", menuOptions);

                Console.Clear();

                if (adminMode)
                {
                    switch (choice)
                    {
                        case 0:
                            _clientView.CreateClient();
                            break;
                        case 1:
                            _clientView.DisplayClients();
                            break;
                        case 2:
                            _clientView.UpdateClient();
                            break;
                        case 3:
                            _clientView.DeleteClient();
                            break;
                        case 4:
                            return;
                    }
                }
                else
                {
                    switch (choice)
                    {
                        case 0:
                            _clientView.DisplayClients(id);
                            break;
                        case 1:
                            _clientView.UpdateClient(id);
                            break;
                        case 2:
                            return;
                    }
                }
                ConsoleView.WaitForContinue();
            }
        }

        public static void BecomeClient()
        {
            _clientView.CreateClient(false);
            ClientInterface.currentId = _clientView.GetLastClientId();
            ClientInterface.Show();
        }
    }
}