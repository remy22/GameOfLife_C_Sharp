using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace GameOfLife
{
    public partial class LoadMenu : Form
    {
        //Constructor for the form - initialising its components and populating the options of the combobox
        public LoadMenu()
        {
            InitializeComponent();
            getOptions();
        }

        /*
         * A method to iterate through all files with extension.txtx in the specified folder where the saved games are,
         * in order to get an ordered list of games available for the player to choose to load.
         */
        public void getOptions()
        {
            string[] files = Directory.GetFiles(@".\PreloadedGames", "*.txt");
            for (int i = 0; i < files.Length; i++)
                //Adds each of the found names of files with extentions .txt without their full path
                comboFiles.Items.Add(Path.GetFileNameWithoutExtension(files[i]));
        }

        //On click of the button ok event to deal with getting the selected option and loading the game from the file
        private void buttonOK_Click(object sender, EventArgs e)
        {
            //load the game by file name in game form
            GameForm loadGame = new GameForm(comboFiles.SelectedItem.ToString());
            //show the loaded game form
            loadGame.Show();
            //dispose this load menu form
            this.Dispose();
        }

        private void comboFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void LoadMenu_Load(object sender, EventArgs e)
        {

        }
    }
}
