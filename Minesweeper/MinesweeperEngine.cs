using Minesweeper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Minesweeper
{
    public class MinesweeperEngine
    {
        public Button[,] grid;
        private Random random = new Random();
        static int numLive = 0;

        public MinesweeperEngine()
        {

        }

        public Button[,] createBoard()
        {
            grid = new Button[15, 15];
            int id = 1;

            for(int i = 0; i < 15; i++)
            {
                for(int j = 0; j < 15; j++)
                {
                    grid[i, j] = new Button();
                    grid[i, j].Column = j;
                    grid[i, j].Row = i;
                    grid[i, j].Id = "" + id;
                    id++;
                }
            }

            numLive = 0;
            foreach(Button c in grid)
            {
                int activate = random.Next(100);
                if (activate < 15)
                {
                    c.Live = true;
                    c.Neighbors = 9;
                    numLive++;
                }
            }

            foreach(Button c in grid)
            {
                int liveNeighbors = 0;
                for(int i = -1; i < 2; i++)
                {
                    for(int j = -1; j < 2; j++)
                    {
                        if((c.Row +i)>=0 &&(c.Row+i) <=(15 -1) && (c.Column + j) >=0 &&(c.Column+j) <= (15 - 1))
                        {
                            if(grid[(c.Row+i),(c.Column+j)].Live == true)
                            {
                                liveNeighbors++;
                            }
                            grid[c.Row, c.Column].Neighbors = liveNeighbors;
                        }
                    }
                }
            }
            return grid;
        }

        public Button[,] onClick(Button c)
        {
            revealNeighbors(c);
            checkWin(c);
            if(c.Live == true)
            {
                foreach(Button b in grid)
                {
                    b.Visited = true;
                }
            }
            return grid;
        }

        public void revealNeighbors(Button c)
        {
            if (grid[c.Row, c.Column].Visited == true)
            {
                return;
            }

            if (grid[c.Row, c.Column].Neighbors == 0)
            {
                grid[c.Row, c.Column].Visited = true;
                grid[c.Row, c.Column].Text = "";

                for(int i = -1; i <= 1; i++)
                {
                    for(int j = -1; j <= 1; j++)
                    {
                        if((c.Row+i)>=0 && (c.Row+i)<=(15-1) && (c.Column+j) >=0 && (c.Column + j) <= (15 - 1))
                        {
                            if (grid[(c.Row + i), (c.Column + j)].Neighbors == 0)
                            {
                                revealNeighbors(grid[(c.Row + i), (c.Column + j)]);
                            }
                            else if (grid[(c.Row + i), (c.Column + j)].Neighbors > 0)
                            {
                                grid[(c.Row + i), (c.Column + j)].Visited = true;
                                grid[(c.Row + i), (c.Column + j)].Text = grid[(c.Row + i), (c.Column + j)].Neighbors + "";
                            }
                        }
                    }
                }
            }
            else
            {
                grid[c.Row, c.Column].Visited = true;
                grid[c.Row, c.Column].Text = c.Neighbors + "";
            }
        }

        public void checkWin(Button current)
        {
            int unvisited = 0;
            foreach(Button c in grid)
            {
                if (c.Visited == false)
                {
                    unvisited++;
                }
            }

            if ((unvisited == numLive) && (current.Live))
            {
                foreach(Button c in grid)
                {
                    c.Visited = true;
                    c.Win = true;
                }
            }
        }

        public Button[,] getGrid()
        {
            return grid;
        }

        public void createSavedGame(Button[] game)
        {
            Button[,] savedGame = new Button[15, 15];
            int x = 0;
            for(int i = 0; i < 15; i++)
            {
                for(int j = 0; j < 15; j++)
                {
                    savedGame[i, j] = new Button();
                    savedGame[i, j] = game[x];
                    x++;
                }
            }
            this.grid = savedGame;
        }
    }
}