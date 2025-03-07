using Cook_Eat.models;
using Cook_Eat.services;

namespace Cook_Eat.views
{
    public class UserView
    {
        private UserService _userService;

        public UserView(UserService userService)
        {
            _userService = userService;
        }

        public void CreateUser()
        {
            string firstName = ConsoleView.GetUserInput("Entrez votre prénom : ");
            string lastName = ConsoleView.GetUserInput("Entrez votre nom : ");
            string email = ConsoleView.GetUserInput("Entrez votre email : ");
            string password = ConsoleView.GetUserInput("Entrez votre mot de passe : ");
            bool isBusiness = ConsoleView.GetUserInput("Êtes-vous une entreprise ? (Oui/Non) : ").ToLower() == "oui";

            var user = new UserAccount
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password,
                IsBusiness = isBusiness
            };

            _userService.CreateUser(user);

            ConsoleView.DisplayMessage("Compte créé avec succès !");
        }
    }
}
