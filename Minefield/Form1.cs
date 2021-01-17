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
        // global variables to store location 
        int atX = 10;    
        int atY = 20;

        // set the maximum and minmum limits to prevent the sprite from going off the edges of the grid of labels 
        int MAX_Y = 20;
        int MAX_X = 20;
        int MIN_Y = 1;
        int MIN_X = 1;

        bool[,] bombs = new bool[21, 21]; //a boolean array that indicates where the mines are
        private GameState gameState;

        public Form1()
        {
            InitializeComponent();
            gameState = new GameState(livesLabel, scoreLabel);
            drawsprite(atX, atY);  //place the sprite at its start-up location
            placeBombs(80);       // make bombs

        }

        //a function to draw the sprite at location (x,y)
        private void drawsprite (int x, int y)
        {
            Label lbl = getLabel(atX, atY);
            lbl.BackColor = Color.White;
            lbl.Image = Properties.Resources.Ship;
        }

        //function to return the Label that is at location (X,Y)
        //using this it is easy to set up the start position of the sprite on start-up…
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

        private bool isGameOver = false; // this to help to prevent the sprite moving after it check bomb
        // using keycodes e.g. WSDA keys to move the sprite 
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            if (this.isGameOver) // used this to set WSDA kys to false/ disable the keys when boms cheked 
            {
                return;
            }

            if (e.KeyCode.Equals(Keys.W)) //moves the sprite up 
            {
                if (atY > MIN_Y) // prvent the sprite from going off the top of the grid 
                {
                    wipesprite(atX, atY); //delete the sprite at current location
                    atY--;                // move up by one row
                    drawsprite(atX, atY); //draw the sprite at the new location
               
                    chkBomb(atX, atY); // check if gone over bomb

                }

            }

            if (e.KeyCode.Equals(Keys.S)) //moves the sprite down
            {
                if (atY  < MAX_Y) // prvent the sprite from going off the bottom of the grid 
                {
                    wipesprite(atX, atY); //delete the sprite at current location
                    atY++;                // move down by one row
                    drawsprite(atX, atY); //draw the sprite at the new location
                    chkBomb(atX, atY); // check if gone over bomb
                }

            }
            if (e.KeyCode.Equals(Keys.D)) //moves the sprite right
            {
                if (atX < MAX_X) // prvent the sprite from going off the right side  of the grid 
                {
                    wipesprite(atX, atY); //delete the sprite at current location
                    atX++;                // move right by one row
                    drawsprite(atX, atY); //draw the sprite at the new location
                    chkBomb(atX, atY); // check if gone over bomb                   
                }

            }
            if (e.KeyCode.Equals(Keys.A)) //moves the sprite right
            {
                if (atX > MIN_X) // prvent the sprite from going off left side  of the grid 
                {
                    wipesprite(atX, atY); //delete the sprite at current location
                    atX--;                // move left by one row
                    drawsprite(atX, atY); //draw the sprite at the new location
                    chkBomb(atX, atY); // check if gone over bomb
                }

            }
        }
        //using buttons on the form to move the sprite
        private void btnUp_Click_1(object sender, EventArgs e) //moves the sprite up 
        {
            if (atY > 1) // prvent the sprite from going off the top of the grid 
            {
                wipesprite(atX, atY); //delete the sprite at current location
                atY--;                // move up by one row
                drawsprite(atX, atY); //draw the sprite at the new location
                chkBomb(atX, atY);    // check if gone over bomb
            }
        }

        private void btnRight_Click(object sender, EventArgs e)  //moves the sprite right
        {
            if (atY > 1)    // prvent the sprite from going off the top of the grid 
            {
                wipesprite(atX, atY);//delete the sprite at current location
                atX++;              //move right by ine row
                drawsprite(atX, atY); //draw the sprite at the new location
                chkBomb(atX, atY); // check if gone over bomb
            }

        }

        private void btnDown_Click(object sender, EventArgs e) //moves the sprite down
        {
            if (atY > 1) // prvent the sprite from going off the top of the grid 
            {
                wipesprite(atX, atY); //delete the sprite at current location
                atY++;
                drawsprite(atX, atY); //draw the sprite at the new location
                chkBomb(atX, atY); // check if gone over bomb
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

            do //loop to fill with desired number of mines
            {
                x = r.Next(1, 20); // random number of mines on X axis location
                y = r.Next(1, 20); // random number of mines on Y axis location

                if (!bombs [x, y])
                {
                    bombs[x, y] = true;
                    k--;
                }

            } while (k > 0);
        }

        // count and show adjacent bombs
        private void countBombs(int X, int Y)
        {
            int count = 0;
            int newx;
            int newy;

            newx = X - 1;   // this to count and display the bombs around X and Y
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

            label400.Text = count.ToString();  // displays  bombs whenever moved takes place at label400

        }

        private void chkBomb(int x, int y) //check for bomb at current location
        {
            if (bombs[x, y])
            {
                this.BackColor = Color.Red; //end of game
                btnDown.Enabled = false;
                btnUp.Enabled = false;
                btnRight.Enabled = false;
                btnLeft.Enabled = false;
                isGameOver = true;
                timer1.Stop(); // timer stops when bomb checks 
                restartGame.Visible = true; // restart game button will be visible again in the form
                showBombs();

                // the explotion sound will play when bomb checked and game is over
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(Properties.Resources.Explosion);
                player.Play();

            }
            else
            {
                countBombs(x, y); //count bombs around current location
                gameState.increaseScore(); // when ever the sprite move one label up score will increase by 10
            }
        }

        //show the bombs 
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



        // timer to set the game duration to 20 seconds 
        int timeLeft =20; 
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timeLeft > 0 )
            {
                timeLeft = timeLeft - 1;
                lblMyTime.Text = timeLeft + " Seconds";
                restartGame.Visible = false;  // restart game button is not visible in this stage 
                
            }
            else
            {
                // this text message will show when the 20 sconds pass        
                lblMessage.Text = ("Sorry, you run out of time");
                restartGame.Visible = true; // restart game button is visible in this stage 
                //timer1.Stop();

            }

        }

        // this to help the player see the bombs 
        private void btnShowbombs_Click(object sender, EventArgs e)
        {

            showBombs();
           // btnShowbombs.Enabled = true;
            
        }

        // the restartGame button will restart the application/game 
        private void restartGame_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
        
        
        //hide bombs after clicking btnShowBombs
        private void btnHidebombs_Click(object sender, EventArgs e)
        {

            Label lbl;
            for (int y = 1; y < 21; y++)
            {
                for (int x = 1; x < 21; x++)
                {
                    lbl = getLabel(x, y);


                    if (lbl.BackColor==Color.DarkGray)
                    {
                        lbl.BackColor = Color.SkyBlue; //flip back color to SkyBlue 
                       
                    }
                    
                    
                    if (lbl.BackColor == Color.Yellow)
                    {
                        lbl.BackColor = Color.SkyBlue;
                        
                    }
                      
                          
                        

                    
                    

                }
            }



        }

        
    }

   
}
