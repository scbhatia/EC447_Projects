using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Lab6
{
    public partial class Form1 : Form
    {

        //set up board dimensions and cell dimensions
        private const float clientWindow = 100;
        private const float borderlength = 80;
        private const float cell = borderlength / 3;

        //offset for board and cell fillings
        private const float startpoint = 10;
        private const float dist = 5;

        //the grid must resize to the current client area
        private float scaling;

        //current game using game engine
        public GameEngine current;

        public Form1()
        {
            InitializeComponent();
            current = new GameEngine();
            ResizeRedraw = true;
        }

        private void ChangeClient(Graphics g)
        {
            scaling = Math.Min(ClientRectangle.Width / clientWindow, ClientRectangle.Height / clientWindow);
            //if the scale is the same return without any adjustment
            if (scaling == 0f)
            {
                return;
            }

            //transform the board based on the client area and scaling factor
            g.ScaleTransform(scaling, scaling);
            g.TranslateTransform(startpoint, startpoint);

        }

        //draws the X's on the board when clicked and valid
        private void DrawX(int i, int j, Graphics g)
        {
            //draw each "line" cooresponding to the letter X
            g.DrawLine(Pens.Black, (i * cell) + cell - dist, j * cell + dist, (i * cell) + dist, (j * cell) + cell - dist);
            g.DrawLine(Pens.Black, i * cell + dist, j * cell + dist, (i * cell) + cell - dist, (j * cell) + cell - dist);
        }

        private void DrawO(int i, int j, Graphics g)
        {
            //Since O is similar to an ellipse, use the draw ellipe method
            g.DrawEllipse(Pens.Black, i * cell + dist, j * cell + dist, cell - 2 * dist, cell - 2 * dist);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
            Graphics g = e.Graphics;

            //update the board for client size
            ChangeClient(g);

            //draws the board 
            g.DrawLine(Pens.Black, 0, cell, borderlength, cell);
            g.DrawLine(Pens.Black, 0, 2 * cell, borderlength, 2 * cell);
            g.DrawLine(Pens.Black, cell, 0, cell, borderlength);
            g.DrawLine(Pens.Black, 2 * cell, 0, 2 * cell, borderlength);
           
            for (int j = 0; j < 3; ++j)
            {
                for (int i = 0; i < 3; ++i)
                {
                    //if selected slot in grid has been selected by O, draw O at coordinate [j,i] and graphic g
                    if (current.grid[j,i] == GameEngine.CellSelect.O)
                    {
                        DrawO(j, i, g);
                    }
                    //if selected slot in grid has been selected by X, draw X at coordinate [j,i] and graphic object g
                    else if (current.grid[j,i] == GameEngine.CellSelect.X)
                    {
                        DrawX(j, i, g);
                    }
                }
            }
        }

        private void computerStartsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //set up the computer's turn
            current.computer_firstMove = true;
            current.computer_Turn = true;

            //computer takes turn
            current.computerMove(current);

            computerStartsToolStripMenuItem.Enabled = false;
            Invalidate();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //create a new game
            current = new GameEngine();

            //re-enable the computer starting menu item
            computerStartsToolStripMenuItem.Enabled = true;
            Invalidate();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            Graphics g = CreateGraphics();
            ChangeClient(g);

            //get the mouse coordinates of the click and adjust the points to the window
            PointF[] pt = { new Point(e.X, e.Y) };
            g.TransformPoints(CoordinateSpace.World, CoordinateSpace.Device, pt);

            //check to see if the user has started the game and clicked
            current = current.checkUser(e, pt, current);

            //disable the computer starting button if the first move has been made
            if (current.turn >= 1)
            {
                computerStartsToolStripMenuItem.Enabled = false;
            }

            Invalidate();
        }

    }

    public class GameEngine
    {
        //create a two dimensional array and instantiate it on the grid
        public enum CellSelect { N, O, X };
        public CellSelect[,] grid = new CellSelect[3, 3];

        //dimensions of each cell in the grid
        private const float cell = 80 / 3;


        public bool computer_firstMove;
        public bool gameEnd;
        public bool gameTied;
        public bool userWinner;
        public bool cpuWinner;
        public bool computer_Turn;

        public int turn;

        public int middle_0;
        public int middle_1;
        public int middle_2;
        public int middle_3;

        public int diagnol_0;
        public int diagnol_1;

        public int row_0;
        public int row_1;
        public int row_2;

        public int column_0;
        public int column_1;
        public int column_2;

        //default constructor sets defaults for when application is opened
        public GameEngine()
        {
            turn = 0;
            gameEnd = false;
            computer_Turn = false;
        }

        //function that determines the computers move
        public void computerMove(GameEngine current)
        {            
            if (computer_firstMove)
            {
                //computer first move button has been selected, computer makes first move
                if (current.grid[1, 1] == GameEngine.CellSelect.N)
                {
                    current.grid[1, 1] = GameEngine.CellSelect.O;
                }
                //computer first move button is disabled
                computer_firstMove = false;
            }
            else if (!computer_firstMove)
            {
                //measure grid cells
                for (int i = 0; i < 3; ++i)
                {                
                    for (int j = 0; j < 3; ++j)
                    {
                        // check if O is anywhere
                        if (current.grid[i, j] == GameEngine.CellSelect.O)
                        {
                            //check to see if theres an O in the middle row or column
                            //center cell
                            if (i == 1 && j == 1)
                            {
                                middle_2++;
                                middle_3++;
                            }

                            //check diagonals
                            //top right cell
                            if (i == 0 && j == 2)
                            {
                                middle_2++;
                            }

                            //bottom left cell
                            if (i == 2 && j == 0)
                            {
                                middle_2++;
                            }

                            //top left cell
                            if (i == 0 && j == 0)
                            {
                                middle_3++;
                            }

                            //bottom right cell
                            if (i == 2 && j == 2)
                            {
                                middle_3++;
                            }

                            //middle column top cell
                            if (i == 0 && j == 1)
                            {
                                middle_0++;
                            }

                            //middle column middle cell
                            if (i == 1 && j == 1)
                            {
                                middle_0++;
                            }

                            //middle column bottom cell
                            if (i == 2 && j == 1)
                            {
                                middle_0++;
                            }

                            //middle row
                            //middle row left cell
                            if (i == 1 && j == 0)
                            {
                                middle_1++;
                            }

                            //middle row center cell
                            if (i == 1 && j == 1)
                            {
                                middle_1++;
                            }

                            //middle row right cell
                            if (i == 1 && j == 2)
                            {
                                middle_1++;
                            }
                        }

                        // check if X                    
                        if (current.grid[i, j] == GameEngine.CellSelect.X)
                        {

                            //check to see if theres an X in the diagonals
                            //center cell               
                            if (i == 1 && j == 1)
                            {
                                diagnol_0++;
                                diagnol_1++;
                            }

                            //top right cell
                            if (i == 0 && j == 2)
                            {
                                diagnol_0++;
                            }

                            //bottom right cell
                            if (i == 2 && j == 2)
                            {
                                diagnol_1++;
                            }

                            //bottom left cell
                            if (i == 2 && j == 0)
                            {
                                diagnol_0++;
                            }

                            //top left cell
                            if (i == 0 && j == 0)
                            {
                                diagnol_1++;
                            }
                           
                            //depending on the values of i, increase the count of the number of X's in a column
                            switch (i)
                            {
                                //left column
                                case 0:
                                    column_0++;
                                    break;
                                //middle column
                                case 1:
                                    column_1++;
                                    break;
                                //right column
                                case 2:
                                    column_2++;
                                    break;

                            }

                            //depending on the values of j, increase the count of the number of X's in a row
                            switch (j)
                            {
                                //top row
                                case 0:
                                    row_0++;
                                    break;
                                //middle row
                                case 1:
                                    row_1++;
                                    break;
                                //bottom row
                                case 2:
                                    row_2++;
                                    break;
                            }

                        }

                    }

                }

                // make best possible O move
                bool finished = false; //variable to see if she made move
                for (int i = 0; i < 3; ++i)
                {
                    for (int j = 0; j < 3; ++j)
                    {
                        //method to place O's
                        if (current.grid[1, 1] == GameEngine.CellSelect.O)
                        {
                            if (current.grid[0, 0] == GameEngine.CellSelect.O)
                            {
                                if (current.grid[2, 2] == GameEngine.CellSelect.N)
                                {
                                    current.grid[2, 2] = GameEngine.CellSelect.O;
                                    finished = true;
                                    break;
                                }
                            }

                            if (current.grid[0, 0] == GameEngine.CellSelect.N)
                            {
                                if (current.grid[2, 2] == GameEngine.CellSelect.O)
                                {
                                    current.grid[0, 0] = GameEngine.CellSelect.O;
                                    finished = true;
                                    break;
                                }
                            }

                            if (current.grid[0, 2] == GameEngine.CellSelect.O)
                            {
                                if (current.grid[2, 0] == GameEngine.CellSelect.N)
                                {
                                    current.grid[2, 0] = GameEngine.CellSelect.O;
                                    finished = true;
                                    break;
                                }
                            }

                            if (current.grid[0, 2] == GameEngine.CellSelect.N)
                            {
                                if (current.grid[2, 0] == GameEngine.CellSelect.O)
                                {
                                    current.grid[0, 2] = GameEngine.CellSelect.O;
                                    finished = true;
                                    break;
                                }
                            }
                        }

                        if (middle_0 == 2)
                        {
                            if (current.grid[0, 1] == GameEngine.CellSelect.N)
                            {
                                current.grid[0, 1] = GameEngine.CellSelect.O;
                                finished = true;
                                break;
                            }
                        }

                        if (middle_0 == 2)
                        {
                            if (current.grid[2, 1] == GameEngine.CellSelect.N)
                            {
                                current.grid[2, 1] = GameEngine.CellSelect.O;
                                finished = true;
                                break;
                            }
                        }

                        if (middle_1 == 2)
                        {
                            if (current.grid[1, 0] == GameEngine.CellSelect.N)
                            {
                                current.grid[1, 0] = GameEngine.CellSelect.O;
                                finished = true;
                                break;
                            }
                        }

                        if (middle_1 == 2)
                        {
                            if (current.grid[1, 2] == GameEngine.CellSelect.N)
                            {
                                current.grid[1, 2] = GameEngine.CellSelect.O;
                                finished = true;
                                break;
                            }
                        }

                        //check to see if there are any empty corners that can be filled
                        if (current.grid[0, 0] == GameEngine.CellSelect.N)
                        {
                            current.grid[0, 0] = GameEngine.CellSelect.O;
                            finished = true;
                            break;
                        }

                        if (current.grid[0, 2] == GameEngine.CellSelect.N)
                        {
                            current.grid[0, 2] = GameEngine.CellSelect.O;
                            finished = true;
                            break;
                        }

                        if (current.grid[2, 0] == GameEngine.CellSelect.N)
                        {
                            current.grid[2, 0] = GameEngine.CellSelect.O;
                            finished = true;
                            break;
                        }

                        if (current.grid[2, 2] == GameEngine.CellSelect.N)
                        {
                            current.grid[2, 2] = GameEngine.CellSelect.O;
                            finished = true;
                            break;
                        }

                        //check to see if there are any moves that can be made to win the game (i.e. if there are two subsequent O's and an empty cell next to it)
                        if (diagnol_0 == 2)
                        {
                            if (current.grid[0, 2] == GameEngine.CellSelect.N)
                            {
                                current.grid[0, 2] = GameEngine.CellSelect.O;
                                finished = true;
                                break;
                            }
                        }

                        if (diagnol_0 == 2)
                        {
                            if (current.grid[1, 1] == GameEngine.CellSelect.N)
                            {
                                current.grid[1, 1] = GameEngine.CellSelect.O;
                                finished = true;
                                break;
                            }
                        }

                        if (diagnol_0 == 2)
                        {
                            if (current.grid[2, 0] == GameEngine.CellSelect.N)
                            {
                                current.grid[2, 0] = GameEngine.CellSelect.O;
                                finished = true;
                                break;
                            }
                        }

                        if (diagnol_1 == 2)
                        {
                            if (current.grid[0, 0] == GameEngine.CellSelect.N)
                            {
                                current.grid[0, 0] = GameEngine.CellSelect.O;
                                finished = true;
                                break;
                            }
                        }

                        if (diagnol_1 == 2)
                        {
                            if (current.grid[1, 1] == GameEngine.CellSelect.N)
                            {
                                current.grid[1, 1] = GameEngine.CellSelect.O;
                                finished = true;
                                break;
                            }
                        }

                        if (diagnol_1 == 2)
                        {
                            if (current.grid[2, 2] == GameEngine.CellSelect.N)
                            {
                                current.grid[2, 2] = GameEngine.CellSelect.O;
                                finished = true;
                                break;
                            }
                        }

                        if (row_0 == 2)
                        {
                            if (current.grid[0, 0] == GameEngine.CellSelect.N)
                            {
                                current.grid[0, 0] = GameEngine.CellSelect.O;
                                finished = true;
                                break;
                            }
                        }

                        if (row_0 == 2)
                        {
                            if (current.grid[1, 0] == GameEngine.CellSelect.N)
                            {
                                current.grid[1, 0] = GameEngine.CellSelect.O;
                                finished = true;
                                break;
                            }
                        }

                        if (row_0 == 2)
                        {
                            if (current.grid[2, 0] == GameEngine.CellSelect.N)
                            {
                                current.grid[2, 0] = GameEngine.CellSelect.O;
                                finished = true;
                                break;
                            }
                        }

                        if (row_1 == 2)
                        {
                            if (current.grid[0, 1] == GameEngine.CellSelect.N)
                            {
                                current.grid[0, 1] = GameEngine.CellSelect.O;
                                finished = true;
                                break;
                            }
                        }

                        if (row_1 == 2)
                        {
                            if (current.grid[1, 1] == GameEngine.CellSelect.N)
                            {
                                current.grid[1, 1] = GameEngine.CellSelect.O;
                                finished = true;
                                break;
                            }
                        }

                        if (row_1 == 2)
                        {
                            if (current.grid[2, 1] == GameEngine.CellSelect.N)
                            {
                                current.grid[2, 1] = GameEngine.CellSelect.O;
                                finished = true;
                                break;
                            }
                        }

                        if (row_2 == 2)
                        {
                            if (current.grid[0, 2] == GameEngine.CellSelect.N)
                            {
                                current.grid[0, 2] = GameEngine.CellSelect.O;
                                finished = true;
                                break;
                            }
                        }

                        if (row_2 == 2)
                        {
                            if (current.grid[1, 2] == GameEngine.CellSelect.N)
                            {
                                current.grid[1, 2] = GameEngine.CellSelect.O;
                                finished = true;
                                break;
                            }
                        }

                        if (row_2 == 2)
                        {
                            if (current.grid[2, 2] == GameEngine.CellSelect.N)
                            {
                                current.grid[2, 2] = GameEngine.CellSelect.O;
                                finished = true;
                                break;
                            }
                        }

                        if (column_0 == 2)
                        {
                            if (current.grid[0, 0] == GameEngine.CellSelect.N)
                            {
                                current.grid[0, 0] = GameEngine.CellSelect.O;
                                finished = true;
                                break;
                            }
                        }

                        if (column_0 == 2)
                        {
                            if (current.grid[0, 1] == GameEngine.CellSelect.N)
                            {
                                current.grid[0, 1] = GameEngine.CellSelect.O;
                                finished = true;
                                break;
                            }
                        }

                        if (column_0 == 2)
                        {
                            if (current.grid[0, 2] == GameEngine.CellSelect.N)
                            {
                                current.grid[0, 2] = GameEngine.CellSelect.O;
                                finished = true;
                                break;
                            }
                        }

                        if (column_1 == 2)
                        {
                            if (current.grid[1, 0] == GameEngine.CellSelect.N)
                            {
                                current.grid[1, 0] = GameEngine.CellSelect.O;
                                finished = true;
                                break;
                            }
                        }

                        if (column_1 == 2)
                        {
                            if (current.grid[1, 1] == GameEngine.CellSelect.N)
                            {
                                current.grid[1, 1] = GameEngine.CellSelect.O;
                                finished = true;
                                break;
                            }
                        }

                        if (column_1 == 2)
                        {
                            if (current.grid[1, 2] == GameEngine.CellSelect.N)
                            {
                                current.grid[1, 2] = GameEngine.CellSelect.O;
                                finished = true;
                                break;
                            }
                        }

                        if (column_2 == 2)
                        {
                            if (current.grid[2, 0] == GameEngine.CellSelect.N)
                            {
                                current.grid[2, 0] = GameEngine.CellSelect.O;
                                finished = true;
                                break;
                            }
                        }

                        if (column_2 == 2)
                        {
                            if (current.grid[2, 1] == GameEngine.CellSelect.N)
                            {
                                current.grid[2, 1] = GameEngine.CellSelect.O;
                                finished = true;
                                break;
                            }
                        }

                        if (column_2 == 2)
                        {
                            if (current.grid[2, 2] == GameEngine.CellSelect.N)
                            {
                                current.grid[2, 2] = GameEngine.CellSelect.O;
                                finished = true;
                                break;
                            }
                        }

                        // check for empty center
                        if (current.grid[1, 1] == GameEngine.CellSelect.N)
                        {
                            current.grid[1, 1] = GameEngine.CellSelect.O;
                            finished = true;
                            break;
                        }
                       
                        //a default move that's made when there are no opportunities to stop a win
                        if (current.grid[i, j] == GameEngine.CellSelect.N)
                        {
                            current.grid[i, j] = GameEngine.CellSelect.O;
                            finished = true;
                            break;
                        }
                    }

                    if (finished)
                    {
                        break;

                    }
                }
            }

            //check to see if there's a winner
            if (this.checkWin(current))
            {
                //if there's a winner the game ends
                gameEnd = true;
                return;
            }

            current.turn++;            
            this.resetGrid();
            current.computer_Turn = false;
        }

        //function to check whether there is a winner
        public bool checkWin(GameEngine current)
        {
            //check each row for O for a win in one of the columns by the computer
            if (current.grid[0,0] == GameEngine.CellSelect.O)
            {
                if (current.grid[1, 0] == GameEngine.CellSelect.O)
                {
                    if (current.grid[2, 0] == GameEngine.CellSelect.O)
                    {
                        cpuWinner = true;
                        this.endGame(cpuWinner);
                        return cpuWinner;
                    }
                }
            }
            
            if (current.grid[0, 1] == GameEngine.CellSelect.O)
            {
                if (current.grid[1, 1] == GameEngine.CellSelect.O)
                {
                    if (current.grid[2, 1] == GameEngine.CellSelect.O)
                    {
                        cpuWinner = true;
                        this.endGame(cpuWinner);
                        return cpuWinner;
                    }
                }
            }

            if (current.grid[0, 2] == GameEngine.CellSelect.O)
            {
                if (current.grid[1, 2] == GameEngine.CellSelect.O)
                {
                    if (current.grid[2, 2] == GameEngine.CellSelect.O)
                    {
                        cpuWinner = true;
                        this.endGame(cpuWinner);
                        return cpuWinner;
                    }
                }
            }

            //check each column for O for a win in one of the rows by the computer
            if (current.grid[0, 0] == GameEngine.CellSelect.O)
            {
                if (current.grid[0, 1] == GameEngine.CellSelect.O)
                {
                    if (current.grid[0, 2] == GameEngine.CellSelect.O)
                    {
                        cpuWinner = true;
                        this.endGame(cpuWinner);
                        return cpuWinner;
                    }
                }
            }

            if (current.grid[1, 0] == GameEngine.CellSelect.O)
            {
                if (current.grid[1, 1] == GameEngine.CellSelect.O)
                {
                    if (current.grid[1, 2] == GameEngine.CellSelect.O)
                    {
                        cpuWinner = true;
                        this.endGame(cpuWinner);
                        return cpuWinner;
                    }
                }
            }

            if (current.grid[2, 0] == GameEngine.CellSelect.O)
            {
                if (current.grid[2, 1] == GameEngine.CellSelect.O)
                {
                    if (current.grid[2, 2] == GameEngine.CellSelect.O)
                    {
                        cpuWinner = true;
                        this.endGame(cpuWinner);
                        return cpuWinner;
                    }
                }
            }

            //check both diagonals for O for a diagonal win by the computer
            if (current.grid[0, 0] == GameEngine.CellSelect.O)
            {
                if (current.grid[1, 1] == GameEngine.CellSelect.O)
                {
                    if (current.grid[2, 2] == GameEngine.CellSelect.O)
                    {
                        cpuWinner = true;
                        this.endGame(cpuWinner);
                        return cpuWinner;
                    }
                }
            }

            if (current.grid[0, 2] == GameEngine.CellSelect.O)
            {
                if (current.grid[1, 1] == GameEngine.CellSelect.O)
                {
                    if (current.grid[2, 0] == GameEngine.CellSelect.O)
                    {
                        cpuWinner = true;
                        this.endGame(cpuWinner);
                        return cpuWinner;
                    }
                }
            }

            //check each row for X's for a column win by the user
            if (current.grid[0, 0] == GameEngine.CellSelect.X)
            {
                if (current.grid[1, 0] == GameEngine.CellSelect.X)
                {
                    if (current.grid[2, 0] == GameEngine.CellSelect.X)
                    {
                        userWinner = true;
                        this.endGame(userWinner);
                        return userWinner;
                    }
                }
            }

            if (current.grid[0, 1] == GameEngine.CellSelect.X)
            {
                if (current.grid[1, 1] == GameEngine.CellSelect.X)
                {
                    if (current.grid[2, 1] == GameEngine.CellSelect.X)
                    {
                        userWinner = true;
                        this.endGame(userWinner);
                        return userWinner;
                    }
                }
            }

            if (current.grid[0, 2] == GameEngine.CellSelect.X)
            {
                if (current.grid[1, 2] == GameEngine.CellSelect.X)
                {
                    if (current.grid[2, 2] == GameEngine.CellSelect.X)
                    {
                        userWinner = true;
                        this.endGame(userWinner);
                        return userWinner;
                    }
                }
            }

            //check all columns for X's for a row win by the user
            if (current.grid[0, 0] == GameEngine.CellSelect.X)
            {
                if (current.grid[0, 1] == GameEngine.CellSelect.X)
                {
                    if (current.grid[0, 2] == GameEngine.CellSelect.X)
                    {
                        userWinner = true;
                        this.endGame(userWinner);
                        return userWinner;
                    }
                }
            }

            if (current.grid[1, 0] == GameEngine.CellSelect.X)
            {
                if (current.grid[1, 1] == GameEngine.CellSelect.X)
                {
                    if (current.grid[1, 2] == GameEngine.CellSelect.X)
                    {
                        userWinner = true;
                        this.endGame(userWinner);
                        return userWinner;
                    }
                }
            }

            if (current.grid[2, 0] == GameEngine.CellSelect.X)
            {
                if (current.grid[2, 1] == GameEngine.CellSelect.X)
                {
                    if (current.grid[2, 2] == GameEngine.CellSelect.X)
                    {
                        userWinner = true;
                        this.endGame(userWinner);
                        return userWinner;
                    }
                }
            }

            //check both diagonals for X's for a diagonal win by the user
            if (current.grid[0, 0] == GameEngine.CellSelect.X)
            {
                if (current.grid[1, 1] == GameEngine.CellSelect.X)
                {
                    if (current.grid[2, 2] == GameEngine.CellSelect.X)
                    {
                        userWinner = true;
                        this.endGame(userWinner);
                        return userWinner;
                    }
                }
            }

            if (current.grid[0, 2] == GameEngine.CellSelect.X)
            {
                if (current.grid[1, 1] == GameEngine.CellSelect.X)
                {
                    if (current.grid[2, 0] == GameEngine.CellSelect.X)
                    {
                        userWinner = true;
                        this.endGame(userWinner);
                        return userWinner;
                    }
                }
            }

            //check to see if the game is tied
            if (checkTied(current))
            {
                //end the game because no one can make any move without losing
                this.endGame(gameTied);
                return gameTied;
            }

            //otherwise there is no tied game
            return false;
        }

        //function to check the user input
        public GameEngine checkUser(MouseEventArgs e, PointF[] pt, GameEngine current)
        {
            //determines whether a click is on the board
            //if the click is not on the board or client area, it will ignore it
            if (pt[0].X < 0)
            {
                return current;
            }
            
            if (pt[0].Y < 0)
            {
                return current;
            }

            int j = (int)(pt[0].Y / cell);
            int i = (int)(pt[0].X / cell);

            if (i > 2)
            {
                return current;
            }

            if (j > 2)
            {
                return current;
            }

            if (grid[i, j] == CellSelect.N)
            {
                //if game is not over
                if (!gameEnd)
                {
                    if (!current.computer_Turn)
                    {
                        //if the mouse has a left click and the move is valid set click to X
                        if (e.Button == MouseButtons.Left)
                        {
                            grid[i, j] = CellSelect.X;
                        }
                        //increment the turn count for the user
                        current.turn++;

                        //check to see if there is a winner in the game
                        if (this.checkWin(current))
                        {
                            //if there is a winner, the game has ended, jump to game end
                            gameEnd = true;
                            return current;
                        }

                        //if the game is not over
                        if (!gameEnd)
                        {
                            //if the game is still going and the user just made his/her move, set the turn to be the computers turn
                            //computer will make its move
                            current.computer_Turn = true;
                            if (current.computer_Turn)
                            {
                                //computer makes move
                                this.computerMove(current);
                            }
                        }
                    }
                }
                return current;
            }

            //if the game is not over
            else if (!gameEnd)
            {
                //if the user clicks on a grid spot with an O
                MessageBox.Show("Invalid Move!");
                return current;
            }

            else
            {
                return current;
            }
        }

        //when the game is over
        public void endGame(bool winner)
        {
            if (userWinner)
            {
                MessageBox.Show("You win!");
            }
            else if (cpuWinner)
            {
                MessageBox.Show("You lose!");
            }
            else if (gameTied)
            {
                MessageBox.Show("Tied game!");
            }
        }

        //check to see if the game is tied 
        public bool checkTied(GameEngine current)
        {
            //use for loop to account for all grid cells
            for (int i = 0; i < 3; ++i)
            {
                for (int j=0; j<3; ++j)
                {
                    if (current.grid[i,j] == GameEngine.CellSelect.N)
                    {
                        gameTied = false;
                        return false;
                    }
                }
            }
            gameTied = true;
            return gameTied;
        }

        //reset the markers for the grid
        public void resetGrid()
        {
            middle_0 = 0;
            middle_1 = 0;
            column_0 = 0;
            column_1 = 0;
            column_2 = 0;
            row_0 = 0;
            row_1 = 0;
            row_2 = 0;
            diagnol_0 = 0;
            diagnol_1 = 0;
        }
    }
}