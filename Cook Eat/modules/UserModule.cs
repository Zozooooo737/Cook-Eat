using System;
using Cook_Eat.services;
using Cook_Eat.views;
using Cook_Eat.interfaces;

namespace Cook_Eat.modules
{
    public static class UserModule
    {
        private static readonly UserService _userService = Program.UserService;
        private static readonly UserView _userView = new UserView(_userService);

        public static void Show(int index = 0, int id = -1)
        {
            string[] menuOptions = null;
            if (index == 0)
            {
                string[] tmp = {
                "Créer un utilisateur",
                "Afficher les utilisateurs",
                "Modifier un utilisateur",
                "Supprimer un utilisateur",
                "Retour au menu principal"
                };
                menuOptions = tmp;
            }
            if (index == 1)
            {
                string[] tmp = {
                "Afficher mon compte utilisateur",
                "Modifier mon compte utilisateur",
                "Retour au menu principal"
                };
                menuOptions = tmp;
            }

            while (true)
            {
                Console.Clear();
                int choice = ConsoleView.ShowMenu("MODULE UTILISATEURS", menuOptions);

                Console.Clear();
                if (index == 0)
                {
                    switch (choice)
                    {
                        case 0:
                            _userView.CreateUser();
                            break;
                        case 1:
                            _userView.DisplayUsers();
                            break;
                        case 2:
                            _userView.UpdateUser();
                            break;
                        case 3:
                            _userView.DeleteUser();
                            break;
                        case 4:
                            return;
                    }
                }
                if (index == 1)
                {
                    switch (choice)
                    {
                        case 0:
                            _userView.DisplayUsers(id);
                            break;
                        case 1:
                            _userView.UpdateUser(id);
                            break;
                        case 2:
                            return;
                    }
                }
                ConsoleView.WaitForContinue();
            }
        }

        public static void Connexion()
        {
            string[] menuOptions = {
                "Connexion avec ID et MOT DE PASSE",
                "Retour au menu de connexion"
            };

            bool connected = false;
            int userId = -1;

            while (!connected)
            {
                Console.Clear();
                int choice = ConsoleView.ShowMenu("CONNEXION UTILISATEUR", menuOptions);

                Console.Clear();
                switch (choice)
                {
                    case 0:
                        ConsoleView.ShowMenuTitle("CONNEXION UTILISATEUR");
                        userId = _userView.Connexion();
                        if (userId != -1) { ConsoleView.DisplayMessage("Connexion Utilisateur établie !"); connected = true; }
                        else ConsoleView.DisplayMessage("Échec de la connexion : identifiants invalides.");
                        break;
                    case 1:
                        return;
                }
                ConsoleView.WaitForContinue();
            }
            UserInterface.currentId = userId;
            UserInterface.Show();
        }

        public static void NewConnexion()
        {
            Console.Clear();
            ConsoleView.ShowMenuTitle("CRÉATION COMPTE UTILISATEUR");

            _userView.CreateUser();
            ConsoleView.WaitForContinue();
        }

        public static int GetUserClientId()
        {
            return _userView.GetUserClientId();
        }

        public static int GetUserCookId()
        {
            return _userView.GetUserCookId();
        }
    }
}