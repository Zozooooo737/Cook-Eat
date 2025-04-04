using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cook_Eat
{
    /// <summary>
    /// Représente une station de métro d'une ligne précise.
    /// Une Station possède toutes les informations à cet endroit dans la réalité,
    /// donc son nom, sa ligne et sa position (longitude et latitude).
    /// </summary>
    public class Station : INode
    {

        /// <summary>
        /// Le nom de la station.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// La ligne à laquelle la station appartient.
        /// </summary>
        public int Line { get; }

        /// <summary>
        /// Les coordonnées de la station (longitude, latitude).
        /// </summary>
        public (double, double) Position { get; }


        /// <summary>
        /// Constructeur de la classe Station.
        /// </summary>
        /// <param name="id">L'identifiant unique de la sation.</param>
        /// <param name="line">La ligne à laquelle la station appartient.</param>
        /// <param name="name">Le nom de la station.</param>
        /// <param name="longitude">La longitude de la station.</param>
        /// <param name="latitude">La latitude de la station.</param>
        public Station(string name, int line, double longitude, double latitude)
        {
            Name = name;
            Line = line;
            Position = (longitude, latitude);
        }
    }
}
