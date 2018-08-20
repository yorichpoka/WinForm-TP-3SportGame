using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3SpotGameWinForms.Model
{
    public class Plateau
    {
        int nb_line = 3;
        int nb_column = 3;
        public string[,] plat = new string[3, 3];
        
        public Plateau()
        {
            // Initial state
            plat[0, 0] = "";
            plat[0, 1] = "R";
            plat[0, 2] = "R";
            plat[1, 0] = "";
            plat[1, 1] = "W";
            plat[1, 2] = "W";
            plat[2, 0] = "";
            plat[2, 1] = "B";
            plat[2, 2] = "B";
        }

        /// <summary>
        /// Method that calculates the move score
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        int ComputeScore(int i,int j,string color)
        {
            if (j == 2 && !color.ToLower().Equals("w"))
                return 1;
            return 0;
        }

        /// <summary>
        /// Methode : une action que l'objet peut faire ( Action : what the object can do)
        /// Le joueur peut déplacer un spot
        /// </summary>
        public void Move(Player p, string position)
        {
            int score = 0;
            // Delete all cases that have the same p.color and other numbers
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    // Delete cases containing the same p.color
                    if (plat[i, j].Equals(p.color))
                    {
                        plat[i, j] = string.Empty;
                    }
                    // Delete cases containing other numbers
                    if (IsNumeric(plat[i, j]) && !plat[i, j].Equals(position.ToString()))
                    {
                        plat[i, j] = string.Empty;
                    }
                    if (plat[i, j].Contains("-") && !plat[i, j].Contains(position.ToString()))
                    {
                        plat[i, j] = string.Empty;
                    }
                }
            }
            // Loop the whole matrix
            for (int i = 0; i < 3; i++)
            {
                // i =0/i=1; i =2;
                for (int j = 0; j < 3; j++)
                {
                    // j =0; j=1;j = 2
                    // Si la case actuelle dans le plat contient la position qu'on cherche
                    if (plat[i, j].Contains(position.ToString()))
                    {
                        score += ComputeScore(i, j, p.color);
                        plat[i, j] = p.color;
                        // case we have 1-2
                        if (plat[i, j].Contains("-"))
                        {
                            if (plat[i, j].Contains("-" + position))
                            {
                                if (j + 1 < 3 && (plat[i, j + 1].Equals("") || plat[i, j + 1].Equals(p.color)))
                                {
                                    score += ComputeScore(i, j + 1, p.color);
                                    plat[i, j + 1] = p.color;
                                }
                            }
                            else
                            {
                                if ((i - 1 >= 0) && (plat[i - 1, j].Equals("") || plat[i - 1, j].Equals(p.color)))
                                {
                                    score += ComputeScore(i-1, j, p.color);
                                    plat[i - 1, j] = p.color;
                                }
                            }
                        }
                        // case we have pnly pne positon
                        else
                        {
                            // chercher à droite
                            if (j + 1 < 3 && (plat[i, j + 1].Equals("") || plat[i, j + 1].Equals(p.color)))
                            {
                                score += ComputeScore(i, j + 1, p.color);
                                plat[i, j + 1] = p.color;
                            }
                            // search left
                            //else if (j - 1 >= 0 && (plat[i, j - 1].Equals("") || plat[i, j - 1].Equals(p.color)))
                            //{
                            //    score += ComputeScore(i, j - 1, p.color);
                            //    plat[i, j - 1] = p.color;
                            //}
                            // chercher en haut
                            else if ((i - 1 >= 0) && (plat[i - 1, j].Equals("") || plat[i - 1, j].Equals(p.color)))
                            {
                                score += ComputeScore(i-1, j, p.color);
                                plat[i - 1, j] = p.color;
                            }
                            // chercher down
                            else if ((i + 1 < 3) && (plat[i + 1, j].Equals("") || plat[i + 1, j].Equals(p.color)))
                            {
                                score += ComputeScore(i+1, j, p.color);
                                plat[i + 1, j] = p.color;
                            }
                        }
                    }
                }
            }
            p.score += score;
        }

        /// <summary>
        /// This method decides whether one player wins
        /// </summary>
        /// <param name="p1"> The first player</param>
        /// <param name="p2">The second player</param>
        /// <returns>return null if no player wins</returns>
        /// <returns>returns the player who won</returns>
        public Player PlayerWin(Player p1, Player p2)
        {
            // Traitement
            if ((p1.score >= 12 && p2.score >= 6) || (p2.score >= 12 && p1.score < 6))
                return p1;// P1 Wins
            if ((p2.score >= 12 && p1.score >= 6) || (p1.score >= 12 && p2.score < 6))
                return p2;// P2 Wins
            return null;  // If no player wins we return null

        }

        public void AddNumbersToPlateau(string color)
        {
            int counter = 1;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    // Search at right
                    // If the position contains nothing and the right case contains the same color
                    if (j + 1 < 3 && plat[i, j].Equals("") && plat[i, j + 1].Equals(color))
                    {
                        plat[i, j] = counter.ToString();
                        counter++;
                    }
                    // If the position contains nothing and the right case contains nothing 
                    else if (j + 1 < 3 && plat[i, j].Equals(string.Empty) && plat[i, j + 1].Equals(string.Empty))
                    {
                        plat[i, j] = counter.ToString();
                        counter++;
                    }
                    // If the position contains nothing and the right case contains a numeric
                    else if (j + 1 < 3 && plat[i, j].Equals(string.Empty) && IsNumeric(plat[i, j + 1]))
                    {
                        plat[i, j] = counter.ToString();
                        counter++;
                    }

                    else if (j + 1 < 3 && plat[i, j].Equals(color) && plat[i, j + 1].Equals(""))
                    {
                        plat[i, j + 1] = counter.ToString();
                        counter++;
                    }
                    //else if (j - 1 >= 0 && plat[i, j].Equals(string.Empty) && IsNumeric(plat[i, j - 1]))
                    //{
                    //    plat[i, j] = counter.ToString();
                    //    counter++;
                    //}
                    // Search up
                    // On doit dire que si c'est numérique donc c'est comme si la case est vide => je peux placer mon spot
                    if (i - 1 >= 0 && plat[i, j].Equals(string.Empty) && plat[i - 1, j].Equals(color))
                    {
                        plat[i, j] = counter.ToString();
                        counter++;
                    }
                    else if (i - 1 >= 0 && plat[i, j].Equals(color) && plat[i - 1, j].Equals(""))
                    {
                        plat[i - 1, j] = counter.ToString();
                        counter++;
                    }
                    else if (i - 1 >= 0 && plat[i, j].Equals(string.Empty) && IsNumeric(plat[i - 1, j]))
                    {
                        plat[i, j] = counter.ToString();
                        counter++;
                    }
                    else if (i - 1 >= 0 && plat[i, j].Equals(string.Empty) && plat[i - 1, j].ToString().Equals(string.Empty))
                    {
                        plat[i, j] = counter.ToString();
                        counter++;
                        if (i + 1 <3 && plat[i+1, j].Equals(color) )
                        {
                            plat[i-1, j] = counter.ToString();
                            counter++;
                        }

                        }
                    //if (j + 1 >= 0 && IsNumeric(plat[i, j]) && plat[i, j + 1].Equals(color))
                    //{
                    //    plat[i, j] = plat[i, j] + "-" + counter.ToString();
                    //    counter++;
                    //}

                    else if (i - 1 >= 0 && IsNumeric(plat[i, j]) && plat[i - 1, j].Equals(""))
                    {
                        plat[i, j] = plat[i, j] + "-" + counter.ToString();
                        counter++;
                    }
                    else if (i - 1 >= 0 && IsNumeric(plat[i, j]) && plat[i - 1, j].Equals(color))
                    {
                        plat[i, j] = plat[i, j] + "-" + counter.ToString();
                        counter++;
                    }
                    else if (i - 1 >= 0 && plat[i, j].Equals(string.Empty) && plat[i - 1, j].Contains("-"))
                    {
                        plat[i, j] = counter.ToString();
                        counter++;
                    }
                    // TODO

                }
            }
        }

        bool IsNumeric(string chaine)
        {
            if (chaine.Equals("4") || chaine.Equals("3") || chaine.Equals("2") || chaine.Equals("1"))
                return true;
            return false;

        }
        public void ShowPlateau()
        {
            Console.WriteLine("* * * * * * * * * * * * *");
            Console.WriteLine("* * * * * * * * * * * * *");
            Console.WriteLine("*    " + plat[0, 0] + "*    " + plat[0, 1] + "*    " + plat[0, 2] + "   *");
            Console.WriteLine("* * * * * * * * * * * * *");
            Console.WriteLine("* * * * * * * * * * * * *");
            Console.WriteLine("* * * * * * * * * * * * *");
            Console.WriteLine("*    " + plat[1, 0] + "*    " + plat[1, 1] + "*    " + plat[1, 2] + "   *");
            Console.WriteLine("* * * * * * * * * * * * *");
            Console.WriteLine("* * * * * * * * * * * * *");
            Console.WriteLine("* * * * * * * * * * * * *");
            Console.WriteLine("*    " + plat[2, 0] + "*    " + plat[2, 1] + "*    " + plat[2, 2] + "   *");
            Console.WriteLine("* * * * * * * * * * * * *");
            Console.WriteLine("* * * * * * * * * * * * *");
        }
    }
}
