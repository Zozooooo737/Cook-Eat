using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Cook_Eat.graph
{
    public class StationsCalculation
    {
        public static string[] SearchingClosestStations(Graph<Station> graph, string address)
        {
            var location = GetAddressCoordinates(address);
            if (location == (0, 0))
            {
                Console.WriteLine("Adresse introuvable.");
                return null;
            }

            return GetClosestStations(location.Item1, location.Item2, graph.Nodes);
        }

        public static (double, double) GetAddressCoordinates(string address)
        {
            try
            {
                string url = $"https://nominatim.openstreetmap.org/search?q={Uri.EscapeDataString(address)}&format=json";

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("User-Agent", "SimpleApp");
                    var response = client.GetStringAsync(url).Result;

                    var results = JsonConvert.DeserializeObject<List<dynamic>>(response);

                    if (results.Count > 0)
                    {
                        double lat = double.Parse((string)results[0].lat, CultureInfo.InvariantCulture);
                        double lon = double.Parse((string)results[0].lon, CultureInfo.InvariantCulture);
                        return (lon, lat);
                    }
                }
            }
            catch
            {
                // Gestion des erreurs
                Console.WriteLine("Erreur lors de la récupération des coordonnées.");
            }
            return (0, 0);
        }

        public static double CalculateDistance(double lon1, double lat1, double lon2, double lat2)
        {
            const double R = 6371; // Rayon de la Terre en km
            double dLat = (lat2 - lat1) * Math.PI / 180.0;
            double dLon = (lon2 - lon1) * Math.PI / 180.0;

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(lat1 * Math.PI / 180.0) * Math.Cos(lat2 * Math.PI / 180.0) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return R * c;
        }

        public static string[] GetClosestStations(double longitude, double latitude, List<Node<Station>> stations)
        {
            List<(string name, double distance)> stationDistances = new List<(string, double)>();

            foreach (var station in stations)
            {
                double distance = CalculateDistance(longitude, latitude, station.Entity.Position.Item1, station.Entity.Position.Item2);
                stationDistances.Add((station.Entity.Name, distance));
            }

            string[] closestStations = new string[3];

            for (int i = 0; i < 3; i++)
            {
                double min = double.MaxValue;
                int indexMin = -1;
                for (int j = 0; j < stationDistances.Count; j++)
                {
                    if (stationDistances[j].Item2 < min) { indexMin = j; min = stationDistances[j].Item2; }
                }
                closestStations[i] = stationDistances[indexMin].Item1;
                stationDistances.RemoveAt(indexMin);
            }

            return closestStations;
        }
    }
}
