using Cook_Eat.services;
using Cook_Eat.views;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Cook_Eat
{
    internal class Program
    {
        public static DatabaseService DatabaseService = new DatabaseService();
        public static UserService UserService = new UserService(DatabaseService);
        public static DishService DishService = new DishService(DatabaseService);

        static void Main(string[] args)
        {

            /*Console.Title = "Cook-Eat - Livraison de repas entre voisins";
            Console.ForegroundColor = ConsoleColor.White;
            MainModule.Show();*/
            Tools.TransportNetwork();

            Graph<int> graph = new Graph<int>(true, true);

            List<Node<int>> nodes = Tools.ReadFileToCreateDetailedNodes("../../../data/stations.txt");

            Dictionary<int, (double, double)> positions = Tools.ReadFileToHaveNodesPosition("../../../data/pos.txt");


            // Afficher les informations des deux stations
            Console.WriteLine("Station de départ :");
            Console.WriteLine($"ID: {12}, Nom: Hôtel De Ville");
            Console.WriteLine("Station d'arrivée :");
            Console.WriteLine($"ID: {113}, Nom: Place d'Italie");

            // Calculer le chemin le plus court entre les deux stations avec Dijkstra
            var (pathNodes, cost) = graph.ShortestPath(nodes[11], nodes[112], 1);

            // Vérifier si pathNodes est nul
            if (pathNodes != null)
            {
                // Transformer la liste de nœuds en liste de paires de nœuds
                List<(Node<int>, Node<int>)> shortestPath = new List<(Node<int>, Node<int>)>();
                for (int i = 0; i < pathNodes.Count - 1; i++)
                {
                    shortestPath.Add((pathNodes[i], pathNodes[i + 1]));
                }

                Console.WriteLine("Path nodes from source to target:");
                foreach (var node in pathNodes)
                {
                    Console.Write($"{node.Id} -> ");
                }
                Console.WriteLine("END");

                using (Bitmap bitmap = new Bitmap(1600, 1200))
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    // Instancier le visualiseur avec le graphe et les positions
                    GraphVisualizer<int> visualizer = new GraphVisualizer<int>(graph);

                    // Dessiner le graphe avec le chemin en surbrillance
                    visualizer.DrawGraph(shortestPath);

                    // Sauvegarder l'image générée
                    bitmap.Save("graph_output.png");
                }
            }
            else
            {
                Console.WriteLine("Aucun chemin trouvé.");
            }

            Console.WriteLine("Graph output saved as 'graph_output.png'.");
        }
    }
    
}