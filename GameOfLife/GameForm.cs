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
    public partial class GameForm : Form
    {
        //fields needed for the game to proceed 
        public Game game;
        private Color dead = Color.Black;
        private Color alive = Color.Yellow;
        private Button[,] grid;
        private Button start;
        private Button stop;
        private Button save;
        private Label generations;
        //timer to get the ticks at which a new generation must appear
        private Timer myTimer = new Timer();

        //constructor for the gameform taking the new size and rate from the properties form 
        //and setting up all tha values of the needed variables
        public GameForm(int newSize, int newRate)
        {
            //initialize component
            InitializeComponent();
            //set ticker to uset the timer_Ticke event handler method ( user defined )
            myTimer.Tick += new EventHandler(this.timer_Tick);

            //set up new game and new grid
            game = new Game(newSize, newRate);
            //grid is the appearance of the game board on the game form
            grid = new Button[Game.SIZE, Game.SIZE];

            //set up the start button
            start = new Button();
            // give it green back color
            start.BackColor = Color.Green;
            //add it a user defined event handler method startEvent_Click
            start.Click += new EventHandler(this.startEvent_Click);
            
            //create new label for the generation number to appear
            generations = new Label();
            //add it new text
            generations.Text = "Generations:";
            //generations.Enabled = false;
            //set up its appearance
            generations.BorderStyle = BorderStyle.None;
            generations.Font = new Font(FontFamily.GenericMonospace, 12.5f, FontStyle.Bold);

            //set up the stop button
            stop = new Button();
            // give it red back color
            stop.BackColor = Color.Red;
            //add it a user defined event handler method stopEvent_Click
            stop.Click += new EventHandler(this.stopEvent_Click);

            //Set up the save button and its appearance
            save = new Button();
            save.Text = "SAVE";
            save.Font = new Font(FontFamily.GenericMonospace, 12.5f, FontStyle.Bold);
            //add it a user defined event handler method saveEvent_Click
            save.Click += new EventHandler(this.saveEvent_Click);

            /*
             * Set up different boundaries for the game dependent on the size of the board:
             * if the board is 16 x 16 or bigger than the size of the game form would be generated
             * relevent to the size of the board so that the game would keep one and the same ratio proportions
             * in look and feel no matter what size the board is.
             * If on the other hand the board is smaller than 16 x 16 again to preserve the look and fell of the game,
             * the size of the game board and the game form would be generated using the size at 16 x 16 to 
             * obtain a more pleasant look.
             */
            if (Game.SIZE >= 16)
            {
                this.Size = new Size((Game.SIZE) * 20 + 17, Game.SIZE * 20 + 180);
                start.SetBounds((Game.SIZE + 1) * 20 / 2 - 150, (Game.SIZE + 1) * 20, 40, 40);
                generations.SetBounds((Game.SIZE + 1) * 20 / 2 - 90, (Game.SIZE + 1) * 20 + 10, 160, 40);
                stop.SetBounds((Game.SIZE + 1) * 20 / 2 + 100, (Game.SIZE + 1) * 20, 40, 40);
                save.SetBounds((Game.SIZE + 1) * 20 / 2 - 90 , (Game.SIZE + 1) * 20 + 50, 160, 40);
            }
            else
            {
                this.Size = new Size(16 * 20 + 17, 16* 20 + 180);
                start.SetBounds(17 * 20 / 2 - 150, 17 * 20, 40, 40);
                generations.SetBounds(17 * 20 / 2 - 90, 17 * 20 + 10, 160, 40);
                stop.SetBounds(17 * 20 / 2 + 100, 17 * 20, 40, 40);
                save.SetBounds(17 * 20 / 2 - 90, 17 * 20 + 50, 160, 40);
            }

            //variable to center the board it is is smaller than 16 x 16 so that it is in the middle of the form (used as an indent variable)
            int center = (this.Size.Width - ((Game.SIZE + 1) * 20)) / 2;

            //Creates the grid as a board of buttons of a particular size, on a particular place and particular color.
            for (int x = 0; x < Game.SIZE; x++)
            {
                for (int y = 0; y < Game.SIZE; y++)
                {
                    grid[x, y] = new Button();

                    /* 
                     * the tag of the button would be the mechanism to sense what the place of the cell changed by the user is.
                     * it uses a formula to calculate a single number to withhold the row and the column index all together
                     * tag number = (row * grid size) + column
                     * thus then row = tag / grid size
                     * then column = tag % grid size
                     */
                    grid[x, y].Tag = x * Game.SIZE + y;

                    grid[x, y].SetBounds(y * 20 + center, x * 20 + center, 20, 20);
                    grid[x, y].BackColor = dead;
                    grid[x, y].UseVisualStyleBackColor = false;
                    //adds an event handle for the button click
                    grid[x, y].Click += new EventHandler(this.btnEvent_Click);
                    //adds the control to the board
                    Controls.Add(grid[x, y]);
                }
            }
            //adds the rest of the controls to the board
            Controls.Add(start);
            Controls.Add(generations);
            Controls.Add(stop);
            Controls.Add(save);
        }

        /*
         * This game form constructor uses exactly the same gameform mechanism to load up the game form but this time it gets the information
         * from the previously parsed file by its name and just uses the game object returned from the file reading as basis to create the game and update the board.
         */
        public GameForm(string file)
        {
            InitializeComponent();
            myTimer.Tick += new EventHandler(this.timer_Tick);

            game = Game.loadGame(file);

            grid = new Button[Game.SIZE, Game.SIZE];

            start = new Button();
            start.BackColor = Color.Green;
            start.Click += new EventHandler(this.startEvent_Click);

            generations = new Label();
            generations.Text = "Generations:";
            //generations.Enabled = false;
            generations.BorderStyle = BorderStyle.None;
            generations.Font = new Font(FontFamily.GenericMonospace, 12.5f, FontStyle.Bold);

            stop = new Button();
            stop.BackColor = Color.Red;
            stop.Click += new EventHandler(this.stopEvent_Click);

            save = new Button();
            save.Text = "SAVE";
            save.Font = new Font(FontFamily.GenericMonospace, 12.5f, FontStyle.Bold);
            save.Click += new EventHandler(this.saveEvent_Click);

            if (Game.SIZE >= 16)
            {
                this.Size = new Size((Game.SIZE) * 20 + 17, Game.SIZE * 20 + 180);
                start.SetBounds((Game.SIZE + 1) * 20 / 2 - 150, (Game.SIZE + 1) * 20, 40, 40);
                generations.SetBounds((Game.SIZE + 1) * 20 / 2 - 90, (Game.SIZE + 1) * 20 + 10, 160, 40);
                stop.SetBounds((Game.SIZE + 1) * 20 / 2 + 100, (Game.SIZE + 1) * 20, 40, 40);
                save.SetBounds((Game.SIZE + 1) * 20 / 2 - 90, (Game.SIZE + 1) * 20 + 50, 160, 40);
            }
            else
            {
                this.Size = new Size(16 * 20 + 17, 16 * 20 + 180);
                start.SetBounds(17 * 20 / 2 - 150, 17 * 20, 40, 40);
                generations.SetBounds(17 * 20 / 2 - 90, 17 * 20 + 10, 160, 40);
                stop.SetBounds(17 * 20 / 2 + 100, 17 * 20, 40, 40);
                save.SetBounds(17 * 20 / 2 - 90, 17 * 20 + 50, 160, 40);
            }

            int center = (this.Size.Width - ((Game.SIZE + 1) * 20)) / 2;

            for (int x = 0; x < Game.SIZE; x++)
            {
                for (int y = 0; y < Game.SIZE; y++)
                {
                    grid[x, y] = new Button();

                    grid[x, y].Tag = x * Game.SIZE + y;

                    grid[x, y].SetBounds(y * 20 + center, x * 20 + center, 20, 20);
                    if(game.valueAt(x,y))grid[x, y].BackColor = alive;
                    else grid[x, y].BackColor = dead;
                    grid[x, y].UseVisualStyleBackColor = false;
                    grid[x, y].Click += new EventHandler(this.btnEvent_Click);
                    Controls.Add(grid[x, y]);
                }
            }

            Controls.Add(start);
            Controls.Add(generations);
            Controls.Add(stop);
            Controls.Add(save);
        }

        //button click event for each button in the grid to check the current cell place,
        //check the current cell state and toggle the state by logic and appearance
        void btnEvent_Click(object sender, EventArgs e)
        {
            int place = (int)((Button)sender).Tag;

            if (game.generations != 0) game.generations = 0;

            if (((Button)sender).BackColor == dead)
            {
                game.cellAt(place / Game.SIZE, place % Game.SIZE).setCell(true);
                ((Button)sender).BackColor = alive;
            }
            else if (((Button)sender).BackColor == alive)
            {
                game.cellAt(place / Game.SIZE, place % Game.SIZE).setCell(false);
                ((Button)sender).BackColor = dead;
            }
        }

        //disable each button in the grid for the user to be UNABLE to click on and change the cell state
        public void disableGrid()
        {
            for (int x = 0; x < Game.SIZE; x++)
            {
                for (int y = 0; y < Game.SIZE; y++)
                {
                    grid[x, y].Enabled = false;
                }
            }
        }

        //enables each button in the grid for the user to be able to click on it and toggle the state of the cell
        public void enableGrid()
        {
            for (int x = 0; x < Game.SIZE; x++)
            {
                for (int y = 0; y < Game.SIZE; y++)
                {
                    grid[x, y].Enabled = true;
                }
            }
        }

        //start event handler
        private void startEvent_Click(object sender, EventArgs e)
        {
            //disable grid for the user
            disableGrid();
            // disable start button and enable stop
            start.Enabled = false;
            stop.Enabled = true;
            //Set the timer to start at the specified interval
            myTimer.Interval = Game.RATE;
            myTimer.Enabled = true;
        }

        //timer tick event
        private void timer_Tick(object sender, EventArgs e)
        {       
            game.doGeneration();
            updateGrid();
                
            generations.Text = "Generations: " + game.generations;
        }

        //stop event click
        private void stopEvent_Click(object sender, EventArgs e)
        {
            //timer disabled
            myTimer.Enabled = false;
            // start button enabled and stop disabled
            start.Enabled = true;
            stop.Enabled = false;
            //enable the grid of buttons for the user
            enableGrid();
        }

        //save event click
        void saveEvent_Click(object sender, EventArgs e)
        {
            // saves the game and siposes of the current form
            game.saveGame();
            this.Dispose();
        }
       
        //a method to update the grid thus based on the logical game cell values the colors to update
        public void updateGrid()
        {
            for (int x = 0; x < Game.SIZE; x++)
            {
                for (int y = 0; y < Game.SIZE; y++)
                {
                    if (game.valueAt(x, y) == true)
                    {
                        grid[x, y].BackColor = alive;
                        //grid[x, y].UseVisualStyleBackColor = false;
                    }
                    else
                    {
                        grid[x, y].BackColor = dead;
                        //grid[x, y].UseVisualStyleBackColor = false;
                    }
                }
            }
        }

        private void GameForm_Load(object sender, EventArgs e)
        {

        }
    }
}
