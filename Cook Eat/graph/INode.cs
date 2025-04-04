using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cook_Eat
{
    /// <summary>
    /// Interface permettant de faire le lien entre un noeud et une station.
    /// Cela nous permet de récupérer les attributs d'une station lorsque l'on veut utiliser Node<Station>.
    /// </summary>
    public interface INode
    {
        /// <summary>
        /// Permet de récupérer le nom de la station.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Permet de récupérer la ligne correspondante à la station.
        /// </summary>
        public int Line { get; }

        /// <summary>
        /// Permet de récupérer la posiiton complète de la station.
        /// </summary>
        public (double,double) Position { get; }
    }
}
