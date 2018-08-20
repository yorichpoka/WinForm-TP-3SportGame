using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3SpotGameWinForms.Model
{
    public class Player
    {
        // Le joueur a un score entier (nombre) égal à zero à l'état initial
        public int score = 0;
        // attribut pour décrire la classe (attribute to describe the class)
        public string color = "";


        /// <summary>
        /// Constructeur : porte le même nom de la classe
        /// Its principal role is to create a class instance (an object)
        /// </summary>
        public Player(string couleur)
        {
            color = couleur;
        }
    }
}
