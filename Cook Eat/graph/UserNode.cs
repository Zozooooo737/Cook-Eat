using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cook_Eat.graph
{
    /// <summary>
    /// Représente un utilisateur générique (client ou cuisinier) en tant que nœud dans un graphe.
    /// Implémente l'interface INode pour être compatible avec les outils de visualisation de graphes.
    /// </summary>
    public class UserNode : INode
    {
        /// <summary>
        /// Identifiant unique de l'utilisateur (client ou cuisinier).
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Initialise une nouvelle instance de UserNode avec l'identifiant de l'utilisateur.
        /// </summary>
        /// <param name="userId">Identifiant unique de l'utilisateur.</param>
        public UserNode(int userId)
        {
            UserId = userId;
        }

        /// <summary>
        /// Nom de l'utilisateur utilisé pour l'affichage dans le graphe.
        /// Format : "User {UserId}".
        /// </summary>
        public string Name => $"User {UserId}";
        public int Line => 0; // Valeur par défaut, inutile ici
        public (double, double) Position => (0.0, 0.0); // Position fictive
    }
}
