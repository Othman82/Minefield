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

        public Form1()
        {
            InitializeComponent();
            //place the sprite at its start-up location
            drawsprite(atX, atY);
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


        private void Form1_Load(object sender, EventArgs e)
        {

        }


    }
}
