using System.Collections.Generic;
using System.IO;
using Cook_Eat.views;
using Org.BouncyCastle.Asn1.X509;

namespace Cook_Eat
{
    public class Tools
    {
        /// <summary>
        /// Méthode pour la création du réseau de métros.
        /// </summary>
        /// <param name="model">Le modèle d'affichage à utiliser dans la fonction PathTest.</param>
        public static void TransportNetwork(bool model)
        {
            Graph<Station> TransportNetwork = new Graph<Station>(true, true);
            List<Node<Station>> nodes = ReadFileToCreateStationNodes("../../../data/detailedStations.txt");
            TransportNetwork.FromAdjacencyMatrixWithNodes(ReadFileForNetworkAdjacencyMatrix("../../../data/network.txt", nodes), nodes);

            Console.WriteLine("Réseau des métros de Paris :\n");
            Console.WriteLine(TransportNetwork.ToString()+"\n");

            int source = int.Parse(ConsoleView.GetUserInput("ID du noeud de départ : "));
            int target = int.Parse(ConsoleView.GetUserInput("ID du noeud d'arrivée : "));
            PathTest(TransportNetwork, source, target, model);
        }



        /// <summary>
        /// Lit un fichier texte pour le transformer en une liste de noeuds représentant 
        /// des stations avec leurs informations détaillées (noms, lignes et positions).
        /// </summary>
        /// <param name="path">Chemin du fichier contenant les données.</param>
        /// <returns>Une liste contenant l'ensemble des noeuds créés.</returns>
        public static List<Node<Station>> ReadFileToCreateStationNodes(string path)
        {
            string[] lines = File.ReadAllLines(path);
            List<Node<Station>> nodes = [];

            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(";");
                int id = int.Parse(parts[0]);
                int line = int.Parse(parts[1]);
                string name = parts[2];
                double longitude = double.Parse(parts[3]);
                double latitude = double.Parse(parts[4]);
                Station station = new Station(name, line, longitude, latitude);
                Node<Station> tmp = new Node<Station>(id, station);
                nodes.Add(tmp);
            }

            return nodes;
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
        public static int[,] ReadFileForNetworkAdjacencyMatrix(string path, List<Node<Station>> nodes)
        {
            string[] lines = File.ReadAllLines(path);

            int size = nodes.Count;
            int[,] adjacencyMatrix = new int[size, size];

            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(";");
                int id = int.Parse(parts[0]);
                int left = int.Parse(parts[1]);
                int right = int.Parse(parts[2]);

                Node<Station> idN = null;
                Node<Station> leftN = null;
                Node<Station> rightN = null;

                foreach (Node<Station> element in nodes)
                {
                    if (element.Id == id) { idN = element; }
                    if (element.Id == left) { leftN = element; }
                    if (element.Id == right) { rightN = element; }
                }

                double sourceLong = idN.Entity.Position.Item1;
                double sourceLat = idN.Entity.Position.Item2;

                double targetLong;
                double targetLat;
                double difLong;
                double difLat;
                double dist;
                int time;

                if (left != -1)
                {
                    if (idN.Entity.Name == leftN.Entity.Name) { adjacencyMatrix[id - 1, left - 1] = 3; }
                    else
                    {
                        targetLong = leftN.Entity.Position.Item1;
                        targetLat = leftN.Entity.Position.Item2;
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
                    if (idN.Entity.Name == rightN.Entity.Name) { adjacencyMatrix[id - 1, right - 1] = 3; }
                    else
                    {
                        targetLong = rightN.Entity.Position.Item1;
                        targetLat = rightN.Entity.Position.Item2;
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


        /// <summary>
        /// Méthode pour tester le calcul du plus court chemin possible entre deux stations de métro.
        /// Cette méthode affiche également le chemin trouvé soit sur la carte soit tout seul pour une meilleure visibilité.
        /// On peut choisir d'afficher le chemin seul avec le booléen model.
        /// </summary>
        /// <param name="graph">Le graphe utilisé du réseau de métros.</param>
        /// <param name="source">Le noeud depuis lequel on part.</param>
        /// <param name="target">Le noeud vers lequel on veut aller.</param>
        /// <param name="model">Le modèle d'affichage : true pour le chemin seul, false pour la carte entière.</param>
        public static void PathTest(Graph<Station> graph, int source, int target, bool model)
        {
            Node<Station> A = null;
            Node<Station> B = null;
            foreach (Node<Station> node in graph.Nodes)
            {
                if (node.Id == source) { A = node; }
                if (node.Id == target) { B = node; }
            }

            (List<Node<Station>>, double) pathN = graph.ShortestPathFloyd(A, B);
            List<Node<Station>> path = pathN.Item1;
            Console.WriteLine("\nDistance totale : " + pathN.Item2 + "\nChemin :");
            foreach (Node<Station> node in path)
            {
                Console.WriteLine(node.Entity.Name + " (" + node.Entity.Line + ")");
            }

            GraphVisualizer<Station> visualizer1 = new GraphVisualizer<Station>(graph, false, true, false);
            visualizer1.Visualize("stations.png", path, model);
        }


        /// <summary>
        /// Génère le trajet entre deux stations de métro à partir de leurs noms
        /// </summary>
        /// <param name="stationDepart">Nom de la station de départ</param>
        /// <param name="stationArrivee">Nom de la station d'arrivée</param>
        /// <param name="model">Modèle d'affichage (true pour chemin seul, false pour carte entière)</param>
        public static void GenererTrajetParNoms(string stationDepart, string stationArrivee, bool model)
        {
            // Création du réseau de transport
            Graph<Station> TransportNetwork = new Graph<Station>(true, true);
            List<Node<Station>> nodes = ReadFileToCreateStationNodes("../../../data/detailedStations.txt");
            TransportNetwork.FromAdjacencyMatrixWithNodes(ReadFileForNetworkAdjacencyMatrix("../../../data/network.txt", nodes), nodes);

            // Recherche des noeuds correspondants aux noms de stations
            Node<Station> departNode = null;
            Node<Station> arriveeNode = null;
    
            foreach (Node<Station> node in TransportNetwork.Nodes)
            {
                if (node.Entity.Name.Equals(stationDepart, StringComparison.OrdinalIgnoreCase))
                {
                    departNode = node;
                }
                if (node.Entity.Name.Equals(stationArrivee, StringComparison.OrdinalIgnoreCase))
                {
                    arriveeNode = node;
                }
            }

            // Vérification que les stations existent
            if (departNode == null)
            {
                Console.WriteLine($"Station de départ '{stationDepart}' non trouvée.");
                return;
            }
            if (arriveeNode == null)
            {
                Console.WriteLine($"Station d'arrivée '{stationArrivee}' non trouvée.");
                return;
            }

            // Calcul et affichage du trajet
            Console.WriteLine($"\nTrajet de {stationDepart} à {stationArrivee} :");
            (List<Node<Station>>, double) pathN = TransportNetwork.ShortestPathFloyd(departNode, arriveeNode);
            List<Node<Station>> path = pathN.Item1;
    
            Console.WriteLine($"\nDistance totale : {pathN.Item2} minutes");
            Console.WriteLine("Itinéraire :");
    
            string currentLine = null;
            foreach (Node<Station> node in path)
            {
                // Affiche le changement de ligne si nécessaire
                if (currentLine != node.Entity.Line.ToString())
                {
                    if (currentLine != null)
                        Console.WriteLine($"↳ Correspondance à {node.Entity.Name} - Ligne {node.Entity.Line}");
                    currentLine = node.Entity.Line.ToString();
                }
                Console.WriteLine($"{node.Entity.Name} (Ligne {node.Entity.Line})");
            }

            // Visualisation graphique
            GraphVisualizer<Station> visualizer = new GraphVisualizer<Station>(TransportNetwork, false, true, false);
            visualizer.Visualize("trajet.png", path, model);
            Console.WriteLine("\nVisualisation du trajet sauvegardée dans 'trajet.png'");
        }
    }
}