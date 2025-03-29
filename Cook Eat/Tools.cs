using System.IO;
using Org.BouncyCastle.Asn1.X509;

namespace Cook_Eat
{
    public class Tools
    {
        // <summary>
        /// Méthode pour la création du réseau de métros.
        /// </summary>
        public static void TransportNetwork()
        {
            Graph g = new Graph(isDirected: true, isWeighted: true);
            Dictionary<int, (double, double)> pos = ReadFileToHaveNodesPosition("../../../data/pos.txt");
            List<Node<int>> nodes = ReadFileToCreateDetailedNodes("../../../data/stations.txt");
            g.FromAdjacencyMatrixWithNodes(ReadFileForNetworkAdjacencyMatrix("../../../data/network.txt", pos, nodes), nodes);

            Console.WriteLine("Réseau des métros de Paris :\n");

            Console.WriteLine(g.ToString());

            GraphVisualizer<int> visualizer = new GraphVisualizer<int>(g, false);
            visualizer.Visualize("stations.png");
        }



        /// <summary>
        /// Lit un fichier texte pour le transformer en une liste de noeuds contenant 
        /// des informations détaillées (groupes de noeuds, description des noeuds).
        /// </summary>
        /// <param name="path">Chemin du fichier contenant les données.</param>
        /// <returns>Une liste contenant l'ensemble des noeuds créés.</returns>
        private static List<Node<int>> ReadFileToCreateDetailedNodes(string path)
        {
            string[] lines = File.ReadAllLines(path);
            List<Node<int>> nodes = [];

            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(";");
                int id = int.Parse(parts[0]);
                string group = parts[1];
                string description = parts[2];
                Node<int> tmp = new Node<int>(id, group, description);
                nodes.Add(tmp);
            }
            return nodes;
        }


        /// <summary>
        /// Lit un fichier texte pour le transformer en une collection qui associe à chaque
        /// noeud (représenté par son id) ses coordonnées dans l'espace (longitude et latitude).
        /// <param name="path">Chemin du fichier contenant les données.</param>
        /// <returns>Un dictionnaire assiociant chaque noeud à sa position.</returns>
        private static Dictionary<int,(double,double)> ReadFileToHaveNodesPosition(string path)
        {
            string[] lines = File.ReadAllLines(path);
            Dictionary<int,(double,double)> pos = [];

            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(";");
                int id = int.Parse(parts[0]);
                double longitude = double.Parse(parts[1]);
                double latitude = double.Parse(parts[2]);
                pos[id] = (longitude, latitude);
            }
            return pos;
        }


        /// <summary>
        /// Lit un fichier texte pour le transformer en une matrice d'ajacence.
        /// Méthode spécifique à la création du réseau de lignes de métro.
        /// Utilise un dictionnaire associant chaque noeud (station) à sa position pour calculer la distance réelle entre chacune.
        /// Utilise une liste contenant tous les noeuds utilisés pour étudier le cas des correspondances (même nom mais pas même id).
        /// La vitesse moyenne d'un métro parisien utilisée dans le calcul est la suivante : 30km/h.
        /// </summary>
        /// <param name="path">Chemin du fichier contenant les données.</param>
        /// <param name="pos">Dictionnaire des positions des stations.</param>
        /// <param name="nodes">Liste de toutes les noeuds représentant les stations.</param>
        /// <returns></returns>
        private static int[,] ReadFileForNetworkAdjacencyMatrix(string path, Dictionary<int,(double,double)> pos, List<Node<int>> nodes)
        {
            string[] lines = File.ReadAllLines(path);

            int size = pos.Keys.Count;
            Console.WriteLine(size);
            int[,] adjacencyMatrix = new int[size, size];

            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(";");
                int id = int.Parse(parts[0]);
                int left = int.Parse(parts[1]);
                int right = int.Parse(parts[2]);

                double sourceLong = pos[id].Item1;
                double sourceLat = pos[id].Item2;

                double targetLong;
                double targetLat;
                double difLong;
                double difLat;
                double dist;
                int time;

                Node<int> idN = null;
                Node<int> leftN = null;
                Node<int> rightN = null;
                
                foreach(Node<int> element in nodes)
                {
                    if (element.Id == id) { idN = element; }
                    if (element.Id == left) { leftN = element; }
                    if (element.Id == right) { rightN = element; }
                }

                if (left != -1)
                {
                    if (idN.Description == leftN.Description) { adjacencyMatrix[id - 1, left - 1] = 3; }
                    else
                    {
                        targetLong = pos[left].Item1;
                        targetLat = pos[left].Item2;
                        difLong = (targetLong - sourceLong) * Math.PI / 180;
                        difLat = (targetLat - sourceLat) * Math.PI / 180;
                        dist = 2 * 6371 * Math.Asin(Math.Sqrt(Math.Sin(difLat / 2) * Math.Sin(difLat / 2) + Math.Cos(sourceLat * Math.PI / 180) *
                        Math.Cos(targetLat * Math.PI / 180) * Math.Sin(difLong / 2) * Math.Sin(difLong / 2)));
                        time = Convert.ToInt32(dist * 60 / 30);
                        adjacencyMatrix[id - 1, left - 1] = time;
                    }
                }

                if (right != -1)
                {
                    if (idN.Description == rightN.Description) { adjacencyMatrix[id - 1, right - 1] = 3; }
                    else
                    {
                        targetLong = pos[right].Item1;
                        targetLat = pos[right].Item2;
                        difLong = (targetLong - sourceLong) * Math.PI / 180;
                        difLat = (targetLat - sourceLat) * Math.PI / 180;
                        dist = 2 * 6371 * Math.Asin(Math.Sqrt(Math.Sin(difLat / 2) * Math.Sin(difLat / 2) + Math.Cos(sourceLat * Math.PI / 180) *
                        Math.Cos(targetLat * Math.PI / 180) * Math.Sin(difLong / 2) * Math.Sin(difLong / 2)));
                        time = Convert.ToInt32(dist * 60 / 30);
                        adjacencyMatrix[id - 1, right - 1] = time;
                    }
                }
            }
            return adjacencyMatrix;
        }
    }
}