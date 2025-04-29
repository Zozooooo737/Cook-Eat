using Cook_Eat.graph;
using Cook_Eat.models;
using Cook_Eat.services;
using Cook_Eat.views;
using System.Collections.Generic;
using System.Drawing;

namespace Cook_Eat.modules
{
    internal class ColorationModule
    {
        private static readonly ClientService _clientService = new ClientService(Program.DatabaseService);
        private static readonly CookService _cookService = new CookService(Program.DatabaseService);
        private static readonly OrderService _orderService = new OrderService(Program.DatabaseService);
        private static readonly UserService _userService = Program.UserService;


        public static void Show(int index = 0, int id = -1)
        {
            // 1. Charger tous les utilisateurs
            List<Dictionary<string, object>> dataClient = _clientService.GetClientsSorted("c.client_id", true);
            List<Dictionary<string, object>> dataCook = _cookService.GetCookSorted("c.cook_id", true);

            Dictionary<int, int> users = new();

            // Remplir la liste des utilisateurs avec leurs IDs
            foreach (var row in dataClient)
            {
                int userId = Convert.ToInt32(row["user_id"]);
                users[userId] = userId;
            }

            foreach (var row in dataCook)
            {
                int userId = Convert.ToInt32(row["user_id"]);
                users[userId] = userId;
            }

            // 2. Créer un graphe avec des Node<int> (ici, on utilise seulement les IDs)
            Graph<UserNode> userGraph = new Graph<UserNode>(isDirected: true, isWeighted: false);

            // Ajouter les nœuds au graphe
            foreach (var userId in users.Keys)
            {
                userGraph.AddNodeWithEntity(userId, new UserNode(userId));
            }

            // 3. Ajouter les arcs (transactions client ➝ cuisinier)
            DishService dishService = new DishService(Program.DatabaseService);
            Dictionary<int, Tuple<int, int>> transactionMap = dishService.GetTransactionClientCook();

            foreach (var kvp in transactionMap.Values)
            {
                int clientId = kvp.Item1;
                int cookId = kvp.Item2;

                if (users.ContainsKey(clientId) && users.ContainsKey(cookId))
                {
                    userGraph.AddLink(clientId, cookId);
                }
            }

            // 4. Visualisation avec Node<int> (nous n'avons plus besoin de UserAccount)
            GraphVisualizer<UserNode> visualizer = new GraphVisualizer<UserNode>(
                userGraph,
                useRegularPositioning: true,
                useMetroPositioning: false,
                useHorizontalPositioning: false,
                useWelshPowellColors: true
            );

            visualizer.Visualize("coloration_utilisateurs.png", new List<Node<UserNode>>(), false);
        }
    }
}
