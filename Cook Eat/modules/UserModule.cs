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

        public static void Show(bool adminMode = true)
        {
            string[] menuOptions = null;
            if (adminMode)
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
            else
            {
                string[] tmp = {
                "Afficher mon compte utilisateur",
                "Modifier mon compte utilisateur",
                "Ajouter une adresse",
                "Afficher toutes mes adresses",
                "Supprimer une adresse",
                "Retour au menu principal"
                };
                menuOptions = tmp;
            }

            while (true)
            {
                Console.Clear();

                int choice;
                if (adminMode) choice = ConsoleView.ShowMenu("MODULE UTILISATEUR", menuOptions);
                else choice = ConsoleView.ShowMenu("MON PROFIL UTILISATEUR", menuOptions);

                Console.Clear();

                if (adminMode)
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
                else
                {
                    int userId = UserInterface.currentId;

                    switch (choice)
                    {
                        case 0:
                            _userView.DisplayUsers(userId);
                            break;
                        case 1:
                            _userView.UpdateUser(userId);
                            break;
                        case 2:
                            _userView.AddAddress(userId);
                            break;
                        case 3:
                            _userView.GetAddresses(userId);
                            break;
                        case 4:
                            _userView.DeleteAddress(userId);
                            break;
                        case 5:
                            return;
                    }
                }
                ConsoleView.WaitForContinue();
            }
        }

        public static void Connexion()
        {
            string[] menuOptions = {
                "Connexion avec EMAIL et MOT DE PASSE",
                "Connexion avec ID et MOT DE PASSE",
                "Retour au menu de connexion"
            };

            bool connected = false;
            int userId = -1;
            string email = null;

            while (!connected)
            {
                Console.Clear();
                int choice = ConsoleView.ShowMenu("CONNEXION UTILISATEUR", menuOptions);

                Console.Clear();
                switch (choice)
                {
                    case 0:
                        ConsoleView.ShowMenuTitle("CONNEXION UTILISATEUR");
                        userId = _userView.Connexion(1);
                        Console.WriteLine();
                        if (userId != -1) { ConsoleView.DisplayMessage("Connexion Utilisateur établie !"); connected = true; }
                        else ConsoleView.DisplayMessage("Échec de la connexion : identifiants invalides.");
                        break;
                    case 1:
                        ConsoleView.ShowMenuTitle("CONNEXION UTILISATEUR");
                        userId = _userView.Connexion(2);
                        Console.WriteLine();
                        if (userId != -1) { ConsoleView.DisplayMessage("Connexion Utilisateur établie !"); connected = true; }
                        else ConsoleView.DisplayMessage("Échec de la connexion : identifiants invalides.");
                        break;
                    case 2:
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