using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cook_Eat.services;
using Cook_Eat.views;
using Cook_Eat.modules;

namespace Cook_Eat.interfaces
{
    internal class UserInterface
    {
        private static readonly UserService _userService = Program.UserService;
        public static int currentId;

        public static void Show()
        {
            while (true)
            {
                Console.Clear();

                int clientId = UserModule.GetUserClientId();
                int cookId = UserModule.GetUserCookId();

                string[] userMenuOptions = null;

                if (clientId == -1 && cookId == -1)
                {
                    string[] tmp = {
                    "Profil Utilisateur",
                    "Créer son profil Client",
                    "Créer son profil Cuisinier",
                    "Se déconnecter"
                    };
                    userMenuOptions = tmp;
                }
                else if (clientId == -1)
                {
                    string[] tmp = {
                    "Profil Utilisateur",
                    "Créer son profil Client",
                    "Compte Cuisinier",
                    "Se déconnecter"
                    };
                    userMenuOptions = tmp;
                }
                else if (cookId == -1)
                {
                    string[] tmp = {
                    "Profil Utilisateur",
                    "Compte Client",
                    "Créer son profil Cuisinier",
                    "Se déconnecter"
                    };
                    userMenuOptions = tmp;
                }
                else
                {
                    string[] tmp = {
                    "Profil Utilisateur",
                    "Compte Client",
                    "Compte Cuisinier",
                    "Se déconnecter"
                    };
                    userMenuOptions = tmp;
                }

                int choice = ConsoleView.ShowMenu("MENU UTILISATEUR", userMenuOptions);

                Console.Clear();

                switch (choice)
                {
                    case 0:
                        UserModule.Show(false);
                        break;
                    case 1:
                        if (clientId != -1) { ClientInterface.currentId = clientId; ClientInterface.Show(); }
                        else ClientModule.BecomeClient();
                        break;
                    case 2:
                        if (cookId != -1) { CookInterface.currentId = cookId; CookInterface.Show(); }
                        else CookModule.BecomeCook();
                        break;
                    case 3:
                        Console.WriteLine("\nDéconnexion réussie !");
                        return;
                }
            }
        }
    }
}
