using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GameOfLife
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Sets up the main form to be the menu so that the program would start of the menu form
            MenuForm MainForm = new MenuForm();
            Application.Run(MainForm);
        }
    }
}
