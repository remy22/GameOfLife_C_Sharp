using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace GameOfLife
{
    public class Game
    {
        //array of cells to hold previous states
        private Cell[,] previous;
        //array of cells to hold the next generated state
        private Cell[,] next;
        //default size of the board 20 x 20
        public static int SIZE = 20;
        //default rate of generation 240ms = 4s
        public static int RATE = 240;
        //number of generations created of one cell board
        private int generationsUR;


        /*
         * Contructor for the game object taking size and rate as parameters 
         * so that they are customisable by the user.
         */
        public Game(int size, int rate)
        {
            generationsUR = 0;

            SIZE = size;
            RATE = rate;

            //created the two arrays with the newly specified size
            previous = new Cell[Game.SIZE, Game.SIZE];
            next = new Cell[Game.SIZE, Game.SIZE];

            //creates each of the cells on this arrays
            for (int x = 0; x < Game.SIZE; x++)
            {
                for (int y = 0; y < Game.SIZE; y++)
                {
                    previous[x, y] = new Cell();
                    next[x, y] = new Cell();
                }
            }
        }

        //Method to reset the next cell board in order for it to be able to take in the next generation
        public void resetNext()
        {
            for (int x = 0; x < Game.SIZE; x++)
            {
                for (int y = 0; y < Game.SIZE; y++)
                {
                    //next is initialised again thus it is dead
                    next[x, y] = new Cell();
                }
            }
        }

        /*
         * A method to get the number of living neighbours surrounding a particular cell
         * with row index and column index passed as parameters. The neighbours are detected 
         * as if in a circular manner thus if the cell checked is the end cell on row 1 then its
         * right neighbour would be cell 1 on row 1.
         */
        public int getLivingNeighbours(int row, int col)
        {
            int counter = 0;
            //adding size number to the row nad the column number to ensure no array index is out of boundaries when substracting from the indexes
            row += Game.SIZE;
            col += Game.SIZE;

            //Substracting or adding one to ensure all the neighbours are visited and checked. 
            //Modular division on game size to ensure no array index is out of bounds and simulate the above mentioned circular pattern.
            if (previous[(row - 1) % Game.SIZE, (col) % Game.SIZE].isAlive()) counter++; //check up
            if (previous[(row + 1) % Game.SIZE, (col) % Game.SIZE].isAlive()) counter++; // check down
            if (previous[(row) % Game.SIZE, (col + 1) % Game.SIZE].isAlive()) counter++; // check right
            if (previous[(row) % Game.SIZE, (col - 1) % Game.SIZE].isAlive()) counter++; // check left
            if (previous[(row - 1) % Game.SIZE, (col - 1) % Game.SIZE].isAlive()) counter++; // checl left up
            if (previous[(row - 1) % Game.SIZE, (col + 1) % Game.SIZE].isAlive()) counter++; // check right up
            if (previous[(row + 1) % Game.SIZE, (col - 1) % Game.SIZE].isAlive()) counter++; // check left down
            if (previous[(row + 1) % Game.SIZE, (col + 1) % Game.SIZE].isAlive()) counter++; // check right down

            return counter;
        }

        //A method to create a new generation using previous as current cell board and saving the new cell states to next.
        // Each cell is responsible for itself but using its own method change state.
        public void doGeneration()
        {
            for (int x = 0; x < Game.SIZE; x++)
            {
                for (int y = 0; y < Game.SIZE; y++)
                {
                    next[x, y].changeState(getLivingNeighbours(x, y), previous[x, y].getCell());
                }
            }

            //Incrementing the number of generations generated
            generationsUR++;

            //Saving next to previous 
            nextToPrevious();
            //Reset (clear) next array
            resetNext();
        }


        /*
         * A method to transfer the values from next to previous 
         * to ensure that in the following generation previous would be the rightful holder of the current state of the cells
         */
        public void nextToPrevious()
        {
            for (int x = 0; x < Game.SIZE; x++)
            {
                for (int y = 0; y < Game.SIZE; y++)
                {
                    previous[x, y] = next[x, y];
                }
            }
        }

        //A method to return the cell value at an appointed row and column index
        public bool valueAt(int row, int col)
        {
            return previous[row, col].getCell();
        }

        //A method to set the cell value at an appointed row and column index
        public void setValueAt(int row, int col, bool value)
        {
            previous[row, col].setCell(value);
        }

        //A method to return the cell at an appointed row and column index
        public Cell cellAt(int row, int col)
        {
            return previous[row, col];
        }

        //A method to return and set the generation number's value
        public int generations
        {
            set
            {
                generationsUR = value;
            }
            get
            {
                return generationsUR;
            }
        }

        /* 
         * A method to parse the board information in order to be written to a text file in the form:
         * size
         * rate
         * 1,0,1,0,....
         * where each board row is on new line and each cell value is either 1 or 0 dependent on wheather
         * alive or dead and is seperated by a comma from the previous.
         * The method returns the concatenated string with the whole text to be written to a text file.
         */
        public string parseToWrite()
        {
            string text = "";

            text += SIZE.ToString();
            text += System.Environment.NewLine;
            text += RATE.ToString();
            text += System.Environment.NewLine;

            for (int x = 0; x < Game.SIZE; x++)
            {
                for (int y = 0; y < Game.SIZE; y++)
                {
                    if (previous[x, y].getCell()) text += "1";
                    else text += "0";
                    text += ",";
                }
                text += System.Environment.NewLine;
            }

            return text;
        }

        /*
         * A method to save the current game by giving it an unique name consisting of the date and the time at which the game is saved 
         * by substituting any characters that might not be allowed in windows file names. Then taking the text from the above parsing method
         * and saving it to a file located in a particular directory in the parent directory of the game itself.
         */
        public void saveGame()
        {
            string now = DateTime.Now.ToShortDateString() + "_" + DateTime.Now.ToLongTimeString();
            now = now.Replace("/", "-");
            now = now.Replace(":", "-");
            string text = parseToWrite();
            string file = @".\PreloadedGames\"+now+"h.txt";
            System.IO.File.WriteAllText(file, text);
        }

        /*
         * Based on a chosen name from the games saved ( which is passed as a aprameter) 
         * the game would look for this file, read it line by line and parse it to fill 
         * the game cell boards.
         */
        public static Game loadGame(string fileName)
        {
            using (System.IO.StreamReader sr = new System.IO.StreamReader(@".\PreloadedGames\"+fileName+".txt"))
            {
                String line = sr.ReadToEnd();
                return parseFromFile(line);
            }
        }

        /*
         * Having received the whole information saved in the file as to what
         * elements the game has, now this method is parsing it from string 
         * and populating a new game object which could be returned to the main game and 
         * take over the flow of information.
         */
        public static Game parseFromFile(String info)
        {
            //define deliminator of new line
            string [] delim = new string[1];
            delim[0]= (string)(System.Environment.NewLine);
            //split the text by new lines
            string[] splitText = info.Split(delim,System.StringSplitOptions.RemoveEmptyEntries);
            //get size from the string
            int size = Convert.ToInt32(splitText[0]);
            //get rate from the string
            int rate = Convert.ToInt32(splitText[1]);
            //create a new game object with the new size and rate
            Game theGame = new Game(size, rate);

            //set new deliminator
            delim[0] = ",";

            //start splitting each row into seperate values - for each value check if it is a zero or one and populate the cell
            for (int x = 2; x < splitText.Length; x++)
            {
                //split row into characters by the comma
                string[] array = splitText[x].Split(delim, System.StringSplitOptions.RemoveEmptyEntries);

                //for each of the characters check one or zero and set the value at row and column index to proper cell value
                for (int y = 0; y < array.Length; y++)
                {
                    //(x-2) is the row index because the 0th and the 1st element in the array for the rows (whose index is x) where size and rate
                    if (array[y] == "1") theGame.setValueAt(x - 2, y, true);
                    else theGame.setValueAt(x - 2, y, false);
                }
            }

            return theGame;
        }
    }
}
