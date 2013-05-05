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
    public partial class MenuForm : Form
    {
        //constructor for the menu form setting up its appearance
        public MenuForm()
        {
            InitializeComponent();
            this.Text = "Game Of Life";
            this.title.Text = "GAME OF LIFE\nMENU";
            this.title.Font = new Font(FontFamily.GenericMonospace, 12.5f, FontStyle.Bold);
            this.title.TextAlign = ContentAlignment.MiddleCenter;
            this.newGame.Text = "NEW GAME";
            this.newGame.Font = new Font(FontFamily.GenericMonospace, 12.5f, FontStyle.Bold);
            this.loadGame.Text = "LOAD GAME";
            this.loadGame.Font = new Font(FontFamily.GenericMonospace, 12.5f, FontStyle.Bold);
            this.exitGame.Text = "EXIT GAME";
            this.exitGame.Font = new Font(FontFamily.GenericMonospace, 12.5f, FontStyle.Bold);
        }

        //on click new game event
        private void newGame_Click(object sender, EventArgs e)
        {
            //creates a new properties form to request information about size and rate of the game
            PropertiesForm newForm = new PropertiesForm();
            //show the properties form
            newForm.Show();
        }

        //on click load game event
        private void loadGame_Click(object sender, EventArgs e)
        {
            //initialises new load menu form
            LoadMenu loadingMenu = new LoadMenu();
            //shows the new load menu form
            loadingMenu.Show();
        }

        //on click exit game event
        private void exitGame_Click(object sender, EventArgs e)
        {
            //dispose this menu form
            this.Dispose();
        }

        private void title_Click(object sender, EventArgs e)
        {

        }

        private void MenuForm_Load(object sender, EventArgs e)
        {

        }
    }
}
