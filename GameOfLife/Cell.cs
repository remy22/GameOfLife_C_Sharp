using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    public class Cell
    {
        //boolean for the cell state to hold wheather dead or alive (logical value)
        public bool cellState;

        //constructor for cell as cell initially is dead, cellState is false
        public Cell()
        {
            cellState = false;
        }

        //mutator method for setting the cell's value thus the cellState
        public void setCell(bool state)
        {
            cellState = state;
        }

        //accessor method for the cell thus the cellState
        public bool getCell()
        {
            return cellState;
        }

        //method to return a boolean result of wheather the cell is dead or alive
        public bool isAlive()
        {
            if (cellState) return true;
            else return false;
        }

        /* 
           For a space that is 'populated': 
                Each cell with one or no neighbours dies, as if by loneliness. 
                Each cell with four or more neighbours dies, as if by overpopulation. 
                Each cell with two or three neighbours survives. 
           For a space that is 'empty' or 'unpopulated' 
                Each cell with three neighbours becomes populated. 
        */

        /*
         *  A method to change the cell state dependent on the previous cell state and the living 
         *  neighbours number which are passed to the method as parameters. This method uses 
         *  a summary of the above mentioned rules as to how the cell state should change. 
         *  
         *  Thus if the cell has been alive and 2 or 3 of its neighbours are alive then it survives, otherwise it  dies.
         *  If the cell is dead but it has exactly 3 living neighbours, then it becomes alive, otherwise it is dead.
         */
        public void changeState(int livingNeighbours, bool previous)
        {
            if (previous)
            {
                if (livingNeighbours == 2 || livingNeighbours == 3) cellState = true;
                else cellState = false;
            }
            else
            {
                if (livingNeighbours == 3) cellState = true;
                else cellState = false;
            }
        }
    }
}
