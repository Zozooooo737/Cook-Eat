using System.Drawing;
using System.Drawing.Imaging;

namespace Cook_Eat
{
    /// <summary>
    /// Classe s'occupant de la visualisation d'un graphe sous forme d'image.
    /// Permet de dessiner les nœuds et les liens du graphe, avec des options pour le positionnement des nœuds et la coloration.
    /// </summary>
    public class GraphVisualizer<T> where T : INode
    {
        /// <summary>
        /// Le graphe à visualiser.
        /// </summary>
        private Graph<T> graph;

        /// <summary>
        /// Bitmap utilisé pour dessiner l'image du graphe.
        /// </summary>
        private Bitmap bitmap;

        /// <summary>
        /// Objet Graphics utilisé pour dessiner sur le bitmap.
        /// </summary>
        private Graphics graphics;

        /// <summary>
        /// Largeur de l'image.
        /// </summary>
        private int width;

        /// <summary>
        /// Hauteur de l'image.
        /// </summary>
        private int height;

        /// <summary>
        /// Dictionnaire stockant les positions des nœuds sur l'image.
        /// </summary>
        private Dictionary<Node<T>, PointF> nodePositions;

        /// <summary>
        /// Dictionnaire stockant les couleurs attribuées à chaque nœud.
        /// </summary>
        private Dictionary<Node<T>, Brush> nodeColors;

        /// <summary>
        /// Indique si les nœuds doivent être positionnés de manière régulière (en cercle) ou aléatoire.
        /// </summary>
        private bool useRegularPositioning;

        /// <summary>
        /// Indique si les nœuds doivent être positionnés en fonction de leurs coordonnées.
        /// </summary>
        private bool useMetroPositioning;

        /// <summary>
        /// Indique si les nœuds doivent être positionnés sur une ligne horizontale.
        /// </summary>
        private bool useHorizontalPositioning;

        /// <summary>
        /// Dictionnaire stockant la longitude et la latitude des stations.
        /// </summary>
        Dictionary<int, (double, double)> pos;



        /// <summary>
        /// Constructeur de la classe GraphVisualizer.
        /// </summary>
        /// <param name="graph">Le graphe à visualiser.</param>
        /// <param name="useRegularPositioning">True pour positionner les nœuds en cercle, False pour un positionnement aléatoire.</param>
        /// <param name="width">Largeur de l'image.</param>
        /// <param name="height">Hauteur de l'image.</param>
        public GraphVisualizer(Graph<T> graph, bool useRegularPositioning, bool useMetroPositioning, bool useHorizontalPositioning,bool useWelshPowellColors, int width = 1600, int height = 1200)
        {
            this.graph = graph;
            if (useHorizontalPositioning) { this.width = 3500; }
            else { this.width = width; }
            this.height = height;
            this.useRegularPositioning = useRegularPositioning;
            this.useMetroPositioning = useMetroPositioning;
            this.useHorizontalPositioning = useHorizontalPositioning;

            List<Node<Station>> nodes = Tools.ReadFileToCreateStationNodes("../../../data/detailedStations.txt");
            pos = [];
            foreach(Node<Station> element in nodes) { pos[element.Id] = (element.Entity.Position.Item1, element.Entity.Position.Item2); }

            bitmap = new Bitmap(this.width,this.height);
            graphics = Graphics.FromImage(bitmap);

            nodePositions = new Dictionary<Node<T>, PointF>();
            nodeColors = new Dictionary<Node<T>, Brush>();

            if (useMetroPositioning || useHorizontalPositioning)
            {
                AssignMetroLineColors(); // Attribue les couleurs des lignes de métro
            }
            else if(useWelshPowellColors)
            {
                WelshPowellColors(graph);
            }
            else
            {
                AssignRandomColors(); // Attribue des couleurs aléatoires
            }
        }



        /// <summary>
        /// Visualise le graphe en générant une image et en l'enregistrant à l'emplacement spécifié.
        /// </summary>
        /// <param name="outputPath">Le chemin où enregistrer l'image.</param>
        public void Visualize(string outputPath, List<Node<T>> path, bool DrawOnlyPath)
        {
            if (DrawOnlyPath)
            {
                List<Node<T>> nodes = [];
                foreach(Node<T> element in path) { if (!nodes.Contains(element)) { nodes.Add(element); }
                }
                Graph<T> tmp = new Graph<T>(graph, nodes);
                GraphVisualizer<T> visualizer = new GraphVisualizer<T>(tmp, false, false, true, false);
                visualizer.Visualize("stations.png", path, false);
            }
            else
            {
                CalculateNodePositions(); // Calcule les positions des nœuds
                DrawGraph(path); // Dessine le graphe
                SaveImage(outputPath); // Enregistre l'image
                OpenImage(outputPath); // Ouvre l'image
            }
        }


        /// <summary>
        /// Calcule les positions des nœuds sur l'image.
        /// Utilise un positionnement régulier (en cercle) ou aléatoire selon la configuration.
        /// </summary>
        private void CalculateNodePositions()
        {
            if (useRegularPositioning)
            {
                CalculateRegularPositions(); // Positionnement en cercle
            }
            else if (useMetroPositioning)
            {
                CalculateStationPosition(pos);
            }
            else if (useHorizontalPositioning)
            {
                CalculateHorizontalPositions();
            }
            else
            {
                CalculateRandomPositions(); // Positionnement aléatoire
            }
        }


        /// <summary>
        /// Calcule des positions aléatoires pour les nœuds, en évitant les chevauchements.
        /// </summary>
        private void CalculateRandomPositions()
        {
            Random rand = new Random();
            int margin = 50; // Marge pour éviter les bords
            int minDistance = 100; // Distance minimale entre deux nœuds
            int maxAttempts = 50; // Nombre maximal de tentatives pour trouver une position valide

            foreach (var node in graph.Nodes)
            {
                PointF newPos;
                bool validPosition;
                int attempts = 0;

                do
                {
                    newPos = new PointF(rand.Next(margin, width - margin), rand.Next(margin, height - margin));
                    validPosition = true;

                    // Vérifie que la nouvelle position est suffisamment éloignée des autres nœuds
                    foreach (var existingPos in nodePositions.Values)
                    {
                        if (Distance(newPos, existingPos) < minDistance)
                        {
                            validPosition = false;
                            break;
                        }
                    }
                    attempts++;
                } while (!validPosition && attempts < maxAttempts);

                nodePositions[node] = newPos; // Enregistre la position du nœud
            }
        }


        /// <summary>
        /// Calcule les positions normalisées des stations sur un graphique en fonction de leurs coordonnées géographiques (latitude, longitude).
        /// Les coordonnées sont normalisées dans une plage spécifique pour ajuster leur position sur un plan 2D.
        /// </summary>
        /// <param name="position">
        /// Dictionnaire contenant les identifiants des stations comme clé et un tuple de coordonnées géographiques (longitude, latitude) comme valeur.
        /// La clé représente l'identifiant de la station, et la valeur représente sa position géographique sous forme de tuple (longitude, latitude).
        /// </param>
        /// <remarks>
        /// Cette méthode prend en entrée un dictionnaire de positions des stations sous forme de coordonnées géographiques et ajuste ces positions 
        /// pour les rendre adaptées à une représentation 2D sur un graphique. Elle calcule les coordonnées minimales et maximales de 
        /// la longitude et de la latitude, puis effectue une normalisation des coordonnées pour les faire entrer dans une plage définie 
        /// (de 50 à 1600 pour la longitude et de 50 à 1200 pour la latitude). L'axe Y est inversé pour correspondre à l'orientation 
        /// attendue du graphique, où le bas du graphique a les valeurs les plus grandes. Les positions normalisées sont ensuite 
        /// enregistrées dans un dictionnaire pour être utilisées lors de l'affichage du graphique.
        /// </remarks>
        private void CalculateStationPosition(Dictionary<int, (double, double)> position)
        {
            if (position == null || position.Count == 0)
                return;

            double minLon = double.MaxValue, maxLon = double.MinValue;
            double minLat = double.MaxValue, maxLat = double.MinValue;

            foreach (var item in position.Values)
            {
                if (item.Item1 < minLon) minLon = item.Item1;
                if (item.Item1 > maxLon) maxLon = item.Item1;
                if (item.Item2 < minLat) minLat = item.Item2;
                if (item.Item2 > maxLat) maxLat = item.Item2;
            }

            double rangeLon = maxLon - minLon;
            double rangeLat = maxLat - minLat;

            foreach (var key in position.Keys)
            {
                double x = ((position[key].Item1 - minLon) / rangeLon) * (1600 - 100) + 50;
                double y = ((position[key].Item2 - minLat) / rangeLat) * (1200 - 100) + 50;
                y = 1200 - y; // Inversion de l'axe Y

                // Trouve le nœud correspondant dans le graphe
                Node<T> node = graph.Nodes.FirstOrDefault(n => n.Id == key);
                if (node != null)
                {
                    nodePositions[node] = new PointF((float)x, (float)y); // Met à jour nodePositions
                }
            }
        }


        /// <summary>
        /// Calcule des positions régulières pour les nœuds, en les plaçant en cercle.
        /// </summary>
        private void CalculateRegularPositions()
        {
            double angleIncrement = 2 * Math.PI / graph.Nodes.Count; // Angle entre chaque nœud
            double radius = Math.Min(width, height) / 2.3; // Rayon du cercle
            int centerX = width / 2, centerY = height / 2; // Centre de l'image
            int index = 0;

            foreach (var node in graph.Nodes)
            {
                double angle = index * angleIncrement;
                nodePositions[node] = new PointF(
                    (float)(centerX + radius * Math.Cos(angle)), // Coordonnée X
                    (float)(centerY + radius * Math.Sin(angle))  // Coordonnée Y
                );
                index++;
            }
        }


        /// <summary>
        /// Calcule des positions régulières pour les nœuds, en les plaçant sur une ligne horizontale centrée.
        /// </summary>
        private void CalculateHorizontalPositions()
        {
            int centerX = width / 2;
            int centerY = height / 2; // Centre de l'écran sur l'axe vertical
            int count = graph.Nodes.Count;

            if (count == 0) return;

            double spacing = (double)width / (count + 3); // Espace entre chaque nœud
            int index = 2;

            foreach (var node in graph.Nodes)
            {
                float posX = (float)(index * spacing); // Calcul de la position X
                nodePositions[node] = new PointF(posX, centerY); // Fixe Y au centre
                index++;
            }
        }


        /// <summary>
        /// Attribue des couleurs aléatoires à chaque nœud.
        /// </summary>
        private void AssignRandomColors()
        {
            Random rand = new Random();
            foreach (var node in graph.Nodes)
            {
                nodeColors[node] = new SolidBrush(Color.FromArgb(rand.Next(100, 255), rand.Next(100, 255), rand.Next(100, 255)));
            }
        }


        /// <summary>
        /// Dessine le graphe sur l'image.
        /// </summary>
        public void DrawGraph(List<Node<T>> path)
        {
            graphics.Clear(Color.White); // Efface l'image avec un fond blanc

            // Dessine les liens
            foreach (var link in graph.Links)
            {
                DrawLink(link, path);
            }

            // Dessine les nœuds
            foreach (var node in graph.Nodes)
            {
                if (useHorizontalPositioning) { DrawNodeWithName(node); }
                else { DrawNode(node); }
            }
        }


        /// <summary>
        /// Dessine un nœud sur l'image.
        /// </summary>
        /// <param name="node">Le nœud à dessiner.</param>
        private void DrawNode(Node<T> node)
        {
            var position = nodePositions[node];
            int totalNodes = graph.Nodes.Count;
            float nodeSize = Math.Max(20, 80 - totalNodes); // Taille du nœud en fonction du nombre total de nœuds

            // Dessine le cercle représentant le nœud
            if (nodeColors.ContainsKey(node))
            {
                graphics.FillEllipse(nodeColors[node], position.X - nodeSize / 2, position.Y - nodeSize / 2, nodeSize, nodeSize);
            }
            else
            {
                graphics.FillEllipse(new SolidBrush(Color.Black), position.X - nodeSize / 2, position.Y - nodeSize / 2, nodeSize, nodeSize);
            }

            // Ajoute l'identifiant du nœud au centre
            string nodeId = node.Id.ToString();
            Font font = new Font("Arial", nodeSize / 3);
            SizeF textSize = graphics.MeasureString(nodeId, font);
            graphics.DrawString(nodeId, font, Brushes.Black, position.X - textSize.Width / 2, position.Y - textSize.Height / 2);
        }


        /// <summary>
        /// Dessine un nœud avec son nom incliné à côté du cercle.
        /// </summary>
        /// <param name="node">Le nœud à dessiner.</param>
        private void DrawNodeWithName(Node<T> node)
        {
            var position = nodePositions[node];
            int totalNodes = graph.Nodes.Count;
            float nodeSize = Math.Max(20, 80 - totalNodes); // Taille du nœud en fonction du nombre total de nœuds

            // Dessine le cercle représentant le nœud
            if (nodeColors.ContainsKey(node))
            {
                graphics.FillEllipse(nodeColors[node], position.X - nodeSize / 2, position.Y - nodeSize / 2, nodeSize, nodeSize);
            }
            else
            {
                graphics.FillEllipse(new SolidBrush(Color.Black), position.X - nodeSize / 2, position.Y - nodeSize / 2, nodeSize, nodeSize);
            }

            // Récupère le nom du nœud
            string nodeName = node.Entity.Name;
            switch (node.Entity.Line)
            {
                case 1:
                    nodeName += "\n(Ligne 1)";
                    break;
                case 2:
                    nodeName += "\n(Ligne 2)";
                    break;
                case 3:
                    nodeName += "\n(Ligne 3)";
                    break;
                case 31:
                    nodeName += "\n(Ligne 3 BIS)";
                    break;
                case 4:
                    nodeName += "\n(Ligne 4)";
                    break;
                case 5:
                    nodeName += "\n(Ligne 5)";
                    break;
                case 6:
                    nodeName += "\n(Ligne 6)";
                    break;
                case 7:
                    nodeName += "\n(Ligne 7)";
                    break;
                case 71:
                    nodeName += "\n(Ligne 7 BIS)";
                    break;
                case 8:
                    nodeName += "\n(Ligne 8)";
                    break;
                case 9:
                    nodeName += "\n(Ligne 9)";
                    break;
                case 10:
                    nodeName += "\n(Ligne 10)";
                    break;
                case 11:
                    nodeName += "\n(Ligne 11)";
                    break;
                case 12:
                    nodeName += "\n(Ligne 12)";
                    break;
                case 13:
                    nodeName += "\n(Ligne 13)";
                    break;
                case 14:
                    nodeName += "\n(Ligne 14)";
                    break;
            }

            // Police pour le texte vertical
            Font font = new Font("Arial", nodeSize / 4, FontStyle.Italic);
            SizeF textSize = graphics.MeasureString(nodeName, font);

            // Position du texte (juste au-dessus du cercle)
            float textX = position.X;
            float textY = position.Y - nodeSize / 1.5f - textSize.Height;

            // Appliquer la transformation pour l'inclinaison verticale (90°)
            graphics.TranslateTransform(textX, textY);
            graphics.RotateTransform(-60); // Inclinaison verticale du texte
            graphics.DrawString(nodeName, font, Brushes.DarkBlue, 0, 0);
            graphics.ResetTransform(); // Réinitialise la transformation

            // Ajoute l'identifiant du nœud au centre
            string nodeId = node.Id.ToString();
            Font idFont = new Font("Arial", nodeSize / 3);
            SizeF idSize = graphics.MeasureString(nodeId, idFont);
            graphics.DrawString(nodeId, idFont, Brushes.White, position.X - idSize.Width / 2, position.Y - idSize.Height / 2);
        }


        /// <summary>
        /// Dessine un lien entre deux nœuds.
        /// Dessine de manière différente les liens du chemin emprunté pour mieux le repérer.
        /// </summary>
        /// <param name="link">Le lien à dessiner.</param>
        /// <param name="path">Le chemin emprunté.</param>
        private void DrawLink(Link<T> link, List<Node<T>> path)
        {
            var start = nodePositions[link.StartNode];
            var end = nodePositions[link.EndNode];

            bool isHighlighted = false;
            for(int i=0; i<path.Count-1;i++)
            {
                isHighlighted |= (path[i].Id == link.StartNode.Id && path[i + 1].Id == link.EndNode.Id);
            }

            if (link.StartNode.Id != link.EndNode.Id)
            {
                if (isHighlighted)
                {
                    using (Pen highlightPen = new Pen(nodeColors[link.StartNode], 10))
                    {
                        graphics.DrawLine(highlightPen, start, end);
                    }
                }

                if (nodeColors.ContainsKey(link.StartNode))
                {
                    using (Pen penFin = new Pen(nodeColors[link.StartNode], 2))
                    {
                        graphics.DrawLine(penFin, start, end);
                    }
                }
                else
                {
                    using (Pen penFin = new Pen(Color.Black, 5))
                    {
                        graphics.DrawLine(penFin, start, end);
                    }
                }
            }
            else // Lien réflexif (boucle)
            {
                using (Pen penFin = new Pen(nodeColors[link.StartNode]))
                {
                    graphics.DrawEllipse(penFin, new RectangleF(start.X - 5, start.Y - 5, 10, 10));
                }
            }

            // Ajoute le poids du lien si le graphe est pondéré
            if (graph.isWeighted)
            {
                var midPoint = new PointF((start.X + end.X) / 2, (start.Y + end.Y) / 2);
                graphics.DrawString(link.Weight.ToString(), new Font("Arial", 15), Brushes.Red, midPoint);
            }

            // Ajoute une flèche si le graphe est orienté
            if (graph.isDirected)
            {
                DrawArrowhead(start, end, link.StartNode);
            }
        }


        /// <summary>
        /// Attribue les couleurs des lignes de métro à chaque nœud.
        /// </summary>
        private void AssignMetroLineColors()
        {
            foreach (var node in graph.Nodes)
            {
                switch (node.Entity.Line)
                {
                    case 1:
                        nodeColors[node] = new SolidBrush(Color.Yellow);
                        break;
                    case 2:
                        nodeColors[node] = new SolidBrush(Color.Blue);
                        break;
                    case 3:
                        nodeColors[node] = new SolidBrush(Color.Olive);
                        break;
                    case 31:
                        nodeColors[node] = new SolidBrush(Color.LightGreen);
                        break;
                    case 4:
                        nodeColors[node] = new SolidBrush(Color.Magenta);
                        break;
                    case 5:
                        nodeColors[node] = new SolidBrush(Color.Orange);
                        break;
                    case 6:
                        nodeColors[node] = new SolidBrush(Color.LightBlue);
                        break;
                    case 7:
                        nodeColors[node] = new SolidBrush(Color.Pink);
                        break;
                    case 71:
                        nodeColors[node] = new SolidBrush(Color.LightPink);
                        break;
                    case 8:
                        nodeColors[node] = new SolidBrush(Color.Cyan);
                        break;
                    case 9:
                        nodeColors[node] = new SolidBrush(Color.Lime);
                        break;
                    case 10:
                        nodeColors[node] = new SolidBrush(Color.DarkGreen);
                        break;
                    case 11:
                        nodeColors[node] = new SolidBrush(Color.Brown);
                        break;
                    case 12:
                        nodeColors[node] = new SolidBrush(Color.DarkBlue);
                        break;
                    case 13:
                        nodeColors[node] = new SolidBrush(Color.LightSlateGray);
                        break;
                    case 14:
                        nodeColors[node] = new SolidBrush(Color.Purple);
                        break;
                    default:
                        nodeColors[node] = new SolidBrush(Color.Black); // Couleur par défaut si la ligne n'est pas reconnue
                        break;
                }
            }
        }


        /// <summary>
        /// Applique l'algorithme de Welsh-Powell pour colorier les nœuds d'un graphe en minimisant le nombre de couleurs.
        /// </summary>
        /// <typeparam name="T">Le type des entités contenues dans les nœuds du graphe.</typeparam>
        /// <param name="graph">Le graphe à colorier.</param>
        /// <returns>
        /// Un dictionnaire associant chaque entité du graphe à un entier représentant sa couleur.
        /// Les entités adjacentes reçoivent des couleurs différentes.
        /// </returns>
        /// <remarks>
        /// L'algorithme trie d'abord les nœuds par degré décroissant (nombre de voisins),
        /// puis assigne les couleurs de façon gloutonne en évitant les conflits de couleur avec les voisins.
        /// Cette méthode ne garantit pas le nombre minimal de couleurs (NP-difficile), mais fournit une bonne approximation.
        /// </remarks>
        public static Dictionary<T, int> WelshPowellColors<T>(Graph<T> graph)
        {
            // 1. Trier les sommets du graphe par nombre de voisins décroissant
            var sommets = graph.Nodes
                .OrderByDescending(node => graph.Voisins(node.Entity).Count)
                .Select(node => node.Entity)
                .ToList();

            var couleurs = new Dictionary<T, int>();
            int couleurActuelle = 1;

            while (sommets.Any())
            {
                var aColorier = new List<T>();

                foreach (var sommet in sommets)
                {
                    // Vérifie s'il n'y a pas de conflit de couleur avec ses voisins
                    bool aucunConflit = graph.Voisins(sommet)
                        .All(voisin => !couleurs.ContainsKey(voisin) || couleurs[voisin] != couleurActuelle);

                    if (aucunConflit)
                    {
                        couleurs[sommet] = couleurActuelle;
                        aColorier.Add(sommet);
                    }
                }

                // Retire les sommets colorés du traitement
                foreach (var sommet in aColorier)
                {
                    sommets.Remove(sommet);
                }

                couleurActuelle++;
            }

            return couleurs;
        }



        /// <summary>
        /// Dessine une flèche (carré) à l'extrémité d'un lien orienté.
        /// </summary>
        /// <param name="start">Point de départ du lien.</param>
        /// <param name="end">Point d'arrivée du lien.</param>
        /// <param name="startNode">Le noeud de départ de l'arc.</param>
        private void DrawArrowhead(PointF start, PointF end, Node<T> startNode)
        {
            float size = 6; // Taille de la flèche
            float offset = 28; // Distance entre la flèche et le nœud

            float dx = end.X - start.X;
            float dy = end.Y - start.Y;
            float length = (float)Math.Sqrt(dx * dx + dy * dy);

            float squareX = end.X - dx / length * offset - size / 2;
            float squareY = end.Y - dy / length * offset - size / 2;

            // Vérifier si le nœud a une couleur attribuée
            if (nodeColors.ContainsKey(startNode))
            {
                graphics.FillRectangle(nodeColors[startNode], squareX, squareY, size, size);
            }
        }


        /// <summary>
        /// Calcule la distance entre deux points.
        /// </summary>
        /// <param name="a">Premier point.</param>
        /// <param name="b">Deuxième point.</param>
        /// <returns>La distance entre les deux points.</returns>
        private float Distance(PointF a, PointF b)
        {
            return (float)Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
        }



        /// <summary>
        /// Enregistre l'image du graphe à l'emplacement spécifié.
        /// </summary>
        /// <param name="filePath">Le chemin où enregistrer l'image.</param>
        private void SaveImage(string filePath)
        {
            bitmap.Save(filePath, ImageFormat.Png);
        }


        /// <summary>
        /// Ouvre l'image du graphe avec l'application par défaut.
        /// </summary>
        /// <param name="filePath">Le chemin de l'image à ouvrir.</param>
        private void OpenImage(string filePath)
        {
            if (File.Exists(filePath))
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(filePath) { UseShellExecute = true });
            }
        }
    }
}