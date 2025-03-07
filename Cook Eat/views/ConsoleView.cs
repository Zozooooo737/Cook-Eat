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

        public static void DisplayTable(List<Dictionary<string, object>> data)
        {
            if (data == null || data.Count == 0)
            {
                Console.WriteLine("Aucune donnée à afficher.");
                return;
            }

            var columns = data[0].Keys;

            Dictionary<string, int> columnWidths = new Dictionary<string, int>();
            foreach (var column in columns)
            {
                int maxWidth = column.Length;
                foreach (var row in data)
                {
                    if (row.ContainsKey(column) && row[column] != null)
                    {
                        int valueLength = row[column].ToString().Length;
                        if (valueLength > maxWidth)
                        {
                            maxWidth = valueLength;
                        }
                    }
                }
                columnWidths[column] = maxWidth;
            }

            string separator = "+";
            foreach (var column in columns)
            {
                separator += new string('-', columnWidths[column] + 2) + "+";
            }
            Console.WriteLine(separator);

            string header = "|";
            foreach (var column in columns)
            {
                header += " " + column.PadRight(columnWidths[column]) + " |";
            }
            Console.WriteLine(header);

            Console.WriteLine(separator);

            foreach (var row in data)
            {
                string rowData = "|";
                foreach (var column in columns)
                {
                    string value;

                    if (row.ContainsKey(column) && row[column] != null)
                    {
                        value = row[column].ToString();
                    }
                    else
                    {
                        value = "NULL";
                    }
                    
                    rowData += " " + value.PadRight(columnWidths[column]) + " |";
                }
                Console.WriteLine(rowData);
            }

            Console.WriteLine(separator);
        }
    }
}