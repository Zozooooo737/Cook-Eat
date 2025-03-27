using Cook_Eat.services;

namespace Cook_Eat.views
{
    public static class UserModule
    {
        private static UserService _userService = Program.UserService;
        private static UserView _userView = new UserView(_userService);

        public static void Show()
        {
            string[] menuOptions = {
                "Créer un utilisateur",
                "Afficher les utilisateurs",
                "Modifier un utilisateur",
                "Supprimer un utilisateur",
                "Retour au menu principal"
            };

            while (true)
            {
                Console.Clear();
                int choice = ConsoleView.ShowMenu("MODULE UTILISATEURS", menuOptions);

                Console.Clear();
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
                ConsoleView.WaitForContinue();
            }
        }
    }
}