using Cook_Eat.services;

namespace Cook_Eat.views
{
    public static class ClientModule
    {
        private static readonly ClientService _clientService = new ClientService(Program.DatabaseService);
        private static readonly ClientView _clientView = new ClientView(_clientService);

        public static void Show()
        {
            string[] menuOptions = {
                "Créer un compte client",
                "Afficher les clients",
                "Modifier un client",
                "Supprimer un compte client",
                "Retour au menu principal"
            };

            while (true)
            {
                Console.Clear();

                int choice = ConsoleView.ShowMenu("MODULE CLIENTS", menuOptions);

                Console.Clear();

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

                Console.WriteLine("\nAppuyez sur une touche pour continuer...");
                Console.ReadKey();
            }
        }
    }
}