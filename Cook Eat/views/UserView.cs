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

        public int Connexion()
        {
            string param = ConsoleView.GetUserInput("Entrez votre id Utilisateur : ");
            string password = ConsoleView.GetUserInput("Entrez votre mot de passe : ");

            List<Dictionary<string, object>> data = _userService.Connexion(param, password);
            if (data == null) return -1;
            else return Convert.ToInt32(data[0]["user_id"]);
        }
        public void CreateUser()
        {
            string firstName = ConsoleView.GetUserInput("Entrez votre prénom : ");
            string lastName = ConsoleView.GetUserInput("Entrez votre nom : ");
            string phoneNumber = ConsoleView.GetUserInput("Entrez votre numéro de téléphone : ");
            string email = ConsoleView.GetUserInput("Entrez votre email : ");
            string password = ConsoleView.GetUserInput("Entrez votre mot de passe : ");
            bool isBusiness = ConsoleView.GetUserInput("Êtes-vous une entreprise ? (Oui/Non) : ").ToLower() == "oui";

            var user = new UserAccount
            {
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = phoneNumber,
                Email = email,
                Password = password,
                IsBusiness = isBusiness
            };

            _userService.CreateUser(user);

            ConsoleView.DisplayMessage("Compte créé avec succès !");
        }

        public int GetUserClientId()
        {
            List<Dictionary<string, object>> data = _userService.GetUserClientId();
            if (data.Count == 0) return -1;
            else return Convert.ToInt32(data[0]["client_id"]);
        }
        public int GetUserCookId()
        {
            List<Dictionary<string, object>> data = _userService.GetUserCookId();
            if (data.Count == 0) return -1;
            else return Convert.ToInt32(data[0]["cook_id"]);
        }

        public void DisplayUsers(int id = -1)
        {
            if (id == -1)
            {
                string[] menuOptions = {
                "Par ID (<)",
                "Par ID (>)",
                "Par prénom (A-Z)",
                "Par prénom (Z-A)",
                "Par nom (A-Z)",
                "Par nom (Z-A)",
                "Par adresse",
                "Retour"
            };

                while (true)
                {
                    int choice = ConsoleView.ShowMenu("AFFICHAGE UTILISATEURS", menuOptions);

                    List<Dictionary<string, object>> data;
                    string title;

                    switch (choice)
                    {
                        case 0:
                            data = _userService.GetUsersSorted("user_id", true);
                            title = "UTILISATEURS PAR ID (<)";
                            break;
                        case 1:
                            data = _userService.GetUsersSorted("user_id", false);
                            title = "UTILISATEURS PAR ID (>)";
                            break;
                        case 2:
                            data = _userService.GetUsersSorted("first_name", true);
                            title = "UTILISATEURS PAR PRENOM (A-Z)";
                            break;
                        case 3:
                            data = _userService.GetUsersSorted("first_name", false);
                            title = "UTILISATEURS PAR PRENOM (Z-A)";
                            break;
                        case 4:
                            data = _userService.GetUsersSorted("last_name", true);
                            title = "UTILISATEURS PAR NOM (A-Z)";
                            break;
                        case 5:
                            data = _userService.GetUsersSorted("last_name", false);
                            title = "UTILISATEURS PAR NOM (Z-A)";
                            break;
                        case 6:
                            data = _userService.GetUsersByAddress();
                            title = "UTILISATEURS PAR ADRESSE";
                            break;
                        case 7:
                            return;
                        default:
                            continue;
                    }
                    ConsoleView.DisplayDataWithChoice(data, title);
                    ConsoleView.WaitForContinue();
                }
            }
            else 
            {
                List<Dictionary<string, object>> data = _userService.GetUser(id); ;
                string title = "UTILISATEURS D'ID CUISINIER " + id;
                ConsoleView.DisplayDataWithChoice(data, title);
                ConsoleView.WaitForContinue();
            }
        }

        public void UpdateUser(int id = -1)
        {
            try
            {
                int userId;
                if (id == -1) { userId = int.Parse(ConsoleView.GetUserInput("Entrez l'ID de l'utilisateur à modifier : ")); }
                else { userId = id; }
                var user = _userService.GetUserById(userId);

                if (user == null)
                {
                    ConsoleView.DisplayMessage("Utilisateur non trouvé.");
                    return;
                }

                ConsoleView.DisplayMessage("\nLaissez le champ vide pour conserver la valeur actuelle.\n");

                string newFirstName = ConsoleView.GetUserInput($"Prénom ({user.FirstName}) : ");
                string newLastName = ConsoleView.GetUserInput($"Nom ({user.LastName}) : ");
                string newPhone = ConsoleView.GetUserInput($"Téléphone ({user.PhoneNumber}) : ");
                string newEmail = ConsoleView.GetUserInput($"Email ({user.Email}) : ");
                string newPassword = ConsoleView.GetUserInput("Nouveau mot de passe (laisser vide pour ne pas changer) : ");
                string businessInput = ConsoleView.GetUserInput($"Statut entreprise (actuel: {(user.IsBusiness ? "Oui" : "Non")}) [Oui/Non] : ");

                user.FirstName = string.IsNullOrEmpty(newFirstName) ? user.FirstName : newFirstName;
                user.LastName = string.IsNullOrEmpty(newLastName) ? user.LastName : newLastName;
                user.PhoneNumber = string.IsNullOrEmpty(newPhone) ? user.PhoneNumber : newPhone;
                user.Email = string.IsNullOrEmpty(newEmail) ? user.Email : newEmail;
                user.Password = string.IsNullOrEmpty(newPassword) ? user.Password : newPassword;
                user.IsBusiness = string.IsNullOrEmpty(businessInput) ? user.IsBusiness : businessInput.Trim() == "Oui";

                if (_userService.UpdateUser(user))
                {
                    ConsoleView.DisplayMessage("\nUtilisateur mis à jour avec succès !");
                }
                else
                {
                    ConsoleView.DisplayMessage("\nÉchec de la mise à jour de l'utilisateur.");
                }
            }
            catch (FormatException)
            {
                ConsoleView.DisplayMessage("\nID utilisateur invalide.");
            }
        }

        public void DeleteUser()
        {
            try
            {
                int userId = int.Parse(ConsoleView.GetUserInput("Entrez l'ID de l'utilisateur à supprimer : "));

                if (_userService.DeleteUser(userId))
                {
                    ConsoleView.DisplayMessage("Utilisateur supprimé avec succès !");
                }
                else
                {
                    ConsoleView.DisplayMessage("Une erreur est survenue lors de la suppression de l'utilisateur.");
                }
            }
            catch (FormatException)
            {
                ConsoleView.DisplayMessage("Veuillez entrer un ID valide (nombre entier).");
            }
        }
    }
}
