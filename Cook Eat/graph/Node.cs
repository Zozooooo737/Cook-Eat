using System.Drawing; 

namespace Cook_Eat
{
    /// <summary>
    /// Représente un nœud (ou sommet) dans un graphe.
    /// Un nœud possède un identifiant unique et une liste de successeurs (nœuds connectés).
    /// </summary>
    public class Node<T>
    {
        /// <summary>
        /// L'identifiant unique du nœud.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Le groupe auquel le noeud appartient.
        /// </summary>
        public string Group { get; }

        /// <summary>
        /// La description du noeud, ce qu'il représente.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// L'ensemble des successeurs du nœud.
        /// Un successeur est un nœud directement accessible depuis ce nœud via un lien.
        /// </summary>
        public HashSet<Node<T>> Successors { get; }

        /// <summary>
        /// La couleur de la ligne de métro associée au nœud.
        /// </summary>
        public Color color { get; }

        /// <summary>
        /// Constructeur de la classe Node.
        /// </summary>
        /// <param name="id">L'identifiant unique du nœud.</param>
        /// <param name="group">Le groupe du noeud.</param>
        /// <param name="description">La description du noeud.</param>
        public Node(int id, string group, string description)
        {
            Id = id;
            Group = group;
            Description = description;
            Successors = new HashSet<Node<T>>();
            color = GetMetroLineColor(group); // Définit la couleur en fonction du groupe
        }

        /// <summary>
        /// Ajoute un nœud successeur à la liste des successeurs du nœud actuel.
        /// </summary>
        /// <param name="node">Le nœud à ajouter comme successeur.</param>
        public void AddSuccessor(Node<T> node)
        {
            Successors.Add(node);
        }

        /// <summary>
        /// Renvoie la couleur de la ligne de métro en fonction du nom du groupe.
        /// </summary>
        /// <param name="group">Le nom de la ligne de métro (groupe).</param>
        /// <returns>La couleur de la ligne de métro.</returns>
        private Color GetMetroLineColor(string group)
        {
            switch (group)
            {
                case "Ligne 1":
                    return Color.Yellow;
                case "Ligne 2":
                    return Color.Blue;
                case "Ligne 3":
                    return Color.Olive;
                case "Ligne 3bis":
                    return Color.LightGreen;
                case "Ligne 4":
                    return Color.Magenta;
                case "Ligne 5":
                    return Color.Orange;
                case "Ligne 6":
                    return Color.LightBlue;
                case "Ligne 7":
                    return Color.Pink;
                case "Ligne 7bis": 
                    return Color.LightPink;
                case "Ligne 8":
                    return Color.Cyan;
                case "Ligne 9":
                    return Color.Lime;
                case "Ligne 10":
                    return Color.DarkGreen;
                case "Ligne 11":
                    return Color.Brown;
                case "Ligne 12":
                    return Color.DarkBlue;
                case "Ligne 13":
                    return Color.LightSlateGray;
                case "Ligne 14":
                    return Color.Purple;
                default:
                    return Color.Black; // Couleur par défaut si la ligne n'est pas reconnue
            }
        }
    }
}