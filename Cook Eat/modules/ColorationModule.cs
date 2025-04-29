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
            // 2. Identifier clients et cuisiniers
            // 3. Charger les commandes (via transactions)
            // 4. Construire le graphe
            // 5. Colorier le graphe
            // 6. Visualiser
        }
    }
}
