using _3SpotGameWinForms.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _3SpotGameWinForms.View
{
    public partial class AppForm : Form
    {
        int i = 0;
        Player p1 = new Player("R");
        Player anonyme = new Player("R");
        Player p2 = new Player("B");
        Plateau p = new Plateau();
        string actualPlayingColor = string.Empty;
        string nextPlayingColor = string.Empty;
        List<string> turns = new List<string> { };
        string color = "";

        public AppForm()
        {

            InitializeComponent();
            InfoLbl.Text = "It is R turn!";
            p.AddNumbersToPlateau("R");
            ProjectPlateauOnButtons(p);
        }

        //private void InitializePlateau()
        //{
        //    button2.BackColor = Color.Gray;
        //    button3.BackColor = Color.Red;
        //    button4.BackColor = Color.Red;
        //    button5.BackColor = Color.Gray;
        //    button6.BackColor = Color.White;
        //    button7.BackColor = Color.White;
        //    button8.BackColor = Color.Gray;
        //    button9.BackColor = Color.Blue;
        //    button10.BackColor = Color.Blue;
        //}

        private void ProjectPlateauOnButtons(Plateau plateau)
        {
            List<Button> ListButton = new List<Button>() {button2,button3,button4,button5,button6,button7,button8,button9,button10 };
            int n = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    DesignButton(ListButton[n], plateau.plat[i, j]);
                    n++;
                }
            }
        }

        public void DesignButton(Button button, string color)
        {
            if (color.Equals(""))
            {
                button.Text = "";
                button.BackColor = Color.Gray;
            }
            else if (color.Equals("R"))
            {
                button.Text = "";
                button.BackColor = Color.Red;
            }
            else if (color.Equals("W"))
            {
                button.Text = "";
                button.BackColor = Color.White;
            }
            else if (color.Equals("B"))
            {
                button.Text = "";
                button.BackColor = Color.Blue;
            }
            else
            {
                button.BackColor = Color.Gray;
                button.Text = color;
            }
        }
        void Play(string actualPlayingColor)
        {



            anonyme.color = "W";
                //Console.WriteLine();
                //p.AddNumbersToPlateau(anonyme.color);
                //ProjectPlateauOnButtons(p);
                //Console.WriteLine("Enter the postion " + anonyme.color + ": ");
                string position = positionTxtb.Text.ToString();
                if (actualPlayingColor == "B")
                    p.Move(p2, position);
                if (actualPlayingColor == "R")
                    p.Move(p1, position);
                if (actualPlayingColor == "W")
                    p.Move(anonyme, position);
            
               
                ProjectPlateauOnButtons(p);

                
                //p.AddNumbersToPlateau("W");
                //ProjectPlateauOnButtons(p);
                //Console.WriteLine("Enter the postion W: ");
                //position = Console.ReadLine();
                //p.Move("W", position);
                //Console.Clear(); // supprimer le console
                //ProjectPlateauOnButtons(p);
                //Console.ReadLine();


                
                //while (p.PlayerWin(p1, p2) == null)
                //{

                //}
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// Btn Ok click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OkBtn_Click(object sender, EventArgs e)
        {            
            if (i % 4 == 0)
                actualPlayingColor = "R";
            else if (i % 2 == 0)
                actualPlayingColor = "B";
            else if (i % 2 == 1)
                actualPlayingColor = "W";
            i++;
            if (i % 4 == 0)
                nextPlayingColor = "R";
            else if (i % 2 == 0)
                nextPlayingColor = "B";
            else if (i % 2 == 1)
                nextPlayingColor = "W";
            Play(actualPlayingColor);
            InfoLbl.Text = "It is " + nextPlayingColor + " turn!";
            BScoreLbl.Text = "Score Blue Player : " + p2.score.ToString();
            RScoreLbl.Text = "Score Red Player : " + p1.score.ToString();
            p.AddNumbersToPlateau(nextPlayingColor);
            ProjectPlateauOnButtons(p);
            positionTxtb.Focus();
            if(PlayerWin(p1, p2) != null)
            {
                OkBtn.Enabled = false;
                positionTxtb.Visible = false;
                InfoLbl.Text = "The " + PlayerWin(p1, p2).color + " Wins !!!";
            }
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
    }
}
