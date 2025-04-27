namespace Cook_Eat.views
{
    public class ConsoleView
    {
        public static void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }

        public static string GetUserInput(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }

        public static void ShowMenuTitle(string title)
        {
            Console.WriteLine("=== " + title + " ===");
            Console.WriteLine();
        }
        public static int ShowMenu(string title, string[] options = null)
        {
            int selectedIndex = 0;
            int size = options.Length;

            Console.CursorVisible = false;
            Console.WriteLine("=== " + title + " ===");
            Console.WriteLine();

            while (true)
            {
                for (int i = 0; i < size; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    Console.WriteLine($"  {options[i]}  ");
                    Console.ResetColor();
                }

                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        selectedIndex = (selectedIndex - 1 + size) % size;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedIndex = (selectedIndex + 1) % size;
                        break;
                    case ConsoleKey.Enter:
                        Console.CursorVisible = true;
                        return selectedIndex;
                }

                Console.SetCursorPosition(0, Console.CursorTop - size);
            }
        }

        public static void DisplayTable(List<Dictionary<string, object>> data)
        {
            if (data == null || data.Count == 0)
            {
                Console.WriteLine("Aucune donnée à afficher.");
                return;
            }

            var columns = data[0].Keys;

            // Calcul des largeurs de colonnes
            Dictionary<string, int> columnWidths = new Dictionary<string, int>();
            foreach (var column in columns)
            {
                int maxWidth = column.Length;
                foreach (var row in data)
                {
                    if (row.ContainsKey(column) && row[column] != null)
                    {
                        int valueLength = row[column].ToString().Length;
                        maxWidth = Math.Max(maxWidth, valueLength);
                    }
                }
                columnWidths[column] = Math.Min(maxWidth, 50); // Limite à 50 caractères max
            }

            // Ligne de séparation améliorée
            string topBottomBorder = "┌";
            string headerSeparator = "├";
            string rowSeparator = "├";
            string bottomBorder = "└";

            foreach (var column in columns)
            {
                int width = columnWidths[column] + 2; // +2 pour les espaces
                topBottomBorder += new string('─', width) + "┬";
                headerSeparator += new string('─', width) + "┼";
                rowSeparator += new string('─', width) + "┼";
                bottomBorder += new string('─', width) + "┴";
            }

            // Ajustement des bordures (remplacement du dernier caractère)
            topBottomBorder = topBottomBorder.Remove(topBottomBorder.Length - 1) + "┐";
            headerSeparator = headerSeparator.Remove(headerSeparator.Length - 1) + "┤";
            rowSeparator = rowSeparator.Remove(rowSeparator.Length - 1) + "┤";
            bottomBorder = bottomBorder.Remove(bottomBorder.Length - 1) + "┘";

            // Affichage du tableau
            Console.WriteLine(topBottomBorder);

            // En-tête
            string header = "│";
            foreach (var column in columns)
            {
                header += " " + column.PadRight(columnWidths[column]) + " │";
            }
            Console.WriteLine(header);

            Console.WriteLine(headerSeparator);

            // Données
            foreach (var row in data)
            {
                string rowData = "│";
                foreach (var column in columns)
                {
                    string value = row.ContainsKey(column) && row[column] != null
                        ? row[column].ToString()
                        : "NULL";

                    // Tronquer si nécessaire
                    if (value.Length > columnWidths[column])
                    {
                        value = value.Substring(0, columnWidths[column] - 3) + "...";
                    }

                    rowData += " " + value.PadRight(columnWidths[column]) + " │";
                }
                Console.WriteLine(rowData);
                Console.WriteLine(rowSeparator);
            }

            // Remplace la dernière ligne de séparation par la bordure inférieure
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.WriteLine(bottomBorder);
        }

        public static void DisplayTablePaginated(List<Dictionary<string, object>> data)
        {
            if (data == null || data.Count == 0)
            {
                Console.WriteLine("Aucune donnée à afficher.");
                return;
            }

            int currentIndex = 0;
            ConsoleKey key;

            do
            {
                Console.Clear();
                var currentItem = data[currentIndex];

                Console.WriteLine("═════════════════════════════════════════");
                Console.WriteLine($"        Enregistrement {currentIndex + 1}/{data.Count}");
                Console.WriteLine("═════════════════════════════════════════\n");

                foreach (var column in currentItem.Keys)
                {
                    string value;
                    if (currentItem[column] != null)
                    {
                        value = currentItem[column].ToString();
                    }
                    else
                    {
                        value = "NULL";
                    }

                    Console.WriteLine($"• {column,-10}: {value}");
                }

                Console.WriteLine("\nNaviguez avec <- et -> | ESC pour quitter");

                key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.LeftArrow:
                        currentIndex = (currentIndex - 1 + data.Count) % data.Count;
                        break;
                    case ConsoleKey.RightArrow:
                        currentIndex = (currentIndex + 1) % data.Count;
                        break;
                }

            } while (key != ConsoleKey.Escape);
        }

        public static void DisplayDataWithChoice(List<Dictionary<string, object>> data, string title = "")
        {
            if (data == null || data.Count == 0)
            {
                Console.WriteLine("Aucune donnée à afficher.");
                WaitForContinue();
                return;
            }

            Console.Clear();
            string[] options = new[] { "Affichage tableau complet", "Affichage paginé (un par un)", "Retour" };
            int choice = ShowMenu($"=== {title} ===", options);

            Console.Clear();
            Console.WriteLine($"=== {title} ===");

            switch (choice)
            {
                case 0:
                    DisplayTable(data);
                    break;
                case 1:
                    DisplayTablePaginated(data);
                    break;
                case 2:
                    return;
            }
        }

        public static void WaitForContinue()
        {
            Console.WriteLine("\nAppuyez sur une touche pour continuer...");
            Console.ReadKey(true);
            Console.Clear();
        }
    }
}