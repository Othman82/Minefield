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
    class GameState
    {
        //create a global variables 
        private int lives = 3; // 3 lives 
        private int score = 0; // Score increases by 10 each time sprite move on raw
        private Label scoreLabel;
        private Label livesLabel;

        // this is a constrcutor which has the same name of the class 
        public GameState(Label livesLabel, Label scoreLabel)
        {

            this.scoreLabel = scoreLabel;
            scoreLabel.Text = "Score " + score;

            this.livesLabel = livesLabel;
            livesLabel.Text = "Lives " + lives;

        }

        // Score increases by 10 each time sprite move on raw
        public void increaseScore() 
        {
            score = score + 10;
            scoreLabel.Text = "Score " + score.ToString();
        }

        // lives  decreases by 1 each time bombs checked 
        public bool isGameOver()
        { 
            lives = lives - 1;
            livesLabel.Text = "Lives " + lives.ToString();

            if (lives == 0)
            {
                return true;
            }

            return false;
        }

    }
}
