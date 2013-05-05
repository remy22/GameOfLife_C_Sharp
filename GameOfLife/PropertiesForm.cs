using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GameOfLife
{
    public partial class PropertiesForm : Form
    {
        //constructor for the properties form
        public PropertiesForm()
        {
            InitializeComponent();
            this.Text = "Game Of Life Properties";
        }

        //on click button ok event 
        private void buttonOK_Click(object sender, EventArgs e)
        {
            //default caluse of size and rate
            int size = 20;
            int rate = 240;
            //Check if new information in the text boxes different to empty string get that info as size and rate
            if(sizeText.Text.ToString()!= "") size = (int)Convert.ToDouble(sizeText.Text.ToString()) + 1;
            //rate entered by the user in seconds but used in the game as milliseconds thus multiplication of the number by 60
            if(genRateText.Text.ToString() != "") rate = ((int)(Convert.ToDouble(genRateText.Text.ToString()) + 1))* 60 ;
            //creates a new game form with the new size and rate
            GameForm theGame = new GameForm(size, rate);
            //show the new game form to start playing
            theGame.Show();
            this.Dispose();
        }

        private void PropertiesForm_Load(object sender, EventArgs e)
        {

        }
    }
}
