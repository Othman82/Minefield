using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minefield
{
    public partial class Form1 : Form
    {
        int atX = 10;
        int atY = 20;
        bool[,] bombs = new bool[21, 21]; //a boolean array that indicates where the mines are

        public Form1()
        {
            InitializeComponent();
            //place the sprite at its start-up location
            drawsprite(atX, atY);
            placeBombs(80);  // make bombs;
        }

        private void drawsprite (int x, int y)
        {
            Label lbl = getLabel(atX, atY);
            lbl.BackColor = Color.White;
            lbl.Image = Properties.Resources.Ship;
        }

        //Using this it is easy to set up the start position of the sprite on start-up…
        private Label getLabel(int x, int y)
        {
            int k = (y - 1) * 20 + x;
            string s = "label" + k.ToString();

            foreach (Control c in panel1.Controls)
            {
                if (c.GetType()==typeof(System.Windows.Forms.Label))
                {
                    if (c.Name==s)
                    {
                        return (Label)c;
                    }
                }
            }
            return null;
        }


        //function to undraw the sprite at location (x,y)
        private void wipesprite(int x, int y)
        {
            Label lbl = getLabel(atX, atY);
            lbl.Image = null;

        }

        private void btnUp_Click_1(object sender, EventArgs e)
        {
            if (atY > 1)
            {
                wipesprite(atX, atY);
                atY--;
                drawsprite(atX, atY);
                chkBomb(atX, atY);
            }
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            if (atY > 1)
            {
                wipesprite(atX, atY);
                atX++;
                drawsprite(atX, atY);
                chkBomb(atX, atY);
            }

        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            if (atY > 1)
            {
                wipesprite(atX, atY);
                atY++;
                drawsprite(atX, atY);
                chkBomb(atX, atY);
            }
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            if (atY > 1) // if allowed, update location
            {
                wipesprite(atY, atX); //delete sprite at current location
                atX--;
                drawsprite(atX, atY); //draw sprite at current location
                chkBomb(atX, atY);


            }
        }
        
        private void placeBombs(int target) //place those mines
        {
            Random r = new Random(); //create a random number generator
            int x;
            int y;     //set variables
            int k = target;

            Array.Clear(bombs, 0, bombs.Length); //clear the current mines list

            do
            {
                x = r.Next(1, 20);
                y = r.Next(1, 20);

                if (!bombs [x, y])
                {
                    bombs[x, y] = true;
                    k--;
                }

            } while (k > 0);
        }

        private void countBombs(int X, int Y)
        {
            int count = 0;
            int newx;
            int newy;

            newx = X - 1;
            if (newx > -1)
            {
                if (bombs[newx, Y])
                    count++;
            }
            newx = X + 1;
            if (newx < 21)
            {
                if (bombs[newx, Y])
                    count++;
            }
            newy = Y - 1;
            if (newy > -1)
            {
                if (bombs[X, newy])
                    count++;
            }
            newy = Y + 1;
            if (newy < 21)
            {
                if (bombs[X, newy])
                    count++;
            }

            label400.Text = count.ToString();

        }

        private void chkBomb(int X, int Y) //check for bomb at current location
        {
            if (bombs[X, Y])
            {
                this.BackColor = Color.Red; //end of game
                btnDown.Enabled = false;
                btnUp.Enabled = false;
                btnRight.Enabled = false;
                btnLeft.Enabled = false;
                showBombs();
            }
            else
            {
                countBombs(X, Y); //count bombs around current location
            }
        }

        private void showBombs()
        {
            Label lbl;
            for (int y = 1; y < 21; y++)
            {
                for (int x = 1; x < 21; x++)
                {
                    lbl = getLabel(x, y);
                    if (bombs[x, y])
                    {
                        lbl.BackColor = Color.Yellow;
                    }
                    else
                    {
                        lbl.BackColor = Color.DarkGray;
                    }
                }
            }
        }

        int timeLeft = 30;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timeLeft > 0)
            {
                timeLeft = timeLeft - 1;
                lblMyTime.Text = timeLeft + " Seconds";
            }
            else
            {
                MessageBox.Show("Sorry, you run out of time");
                
                
            }
        }
    }
}
