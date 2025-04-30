using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cook_Eat.modules;
using Cook_Eat.views;

namespace Cook_Eat.interfaces
{
    internal class MainInterface
    {
        public static Graph<Station> Graph;

        public static void Show()
        {
            string[] mainMenuOptions = {
                "Connexion Admin",
                "Connexion Utilisateur",
                "Nouveau Compte",
                "Quitter l'application"
            };

            while (true)
            {
                Console.Clear();

                int choice = ConsoleView.ShowMenu("CONNEXION COOK-EAT", mainMenuOptions);

                switch (choice)
                {
                    case 0:
                        AdminInterface.Connexion();
                        break;
                    case 1:
                        UserModule.Connexion();
                        break;
                    case 2:
                        UserModule.NewConnexion();
                        break;
                    case 3:
                        Console.WriteLine("\nMerci d'avoir utilisé Cook'Eat. À bientôt !");
                        return;
                }
            }
        }
    }
}
