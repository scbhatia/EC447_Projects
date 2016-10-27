using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab4
{
    public partial class Form1 : Form
    {

        const int cell_size = 50; //dimensions for the cell are 50x50
        bool hintbox_check = false; //hints are originally unchecked
        int counter = 0; //counter for queens to keep track

        //array lists to keep track of items in form
        private ArrayList queen_tot = new ArrayList(); //current queens
        private ArrayList hints = new ArrayList(); //hints 
        private ArrayList board_cells = new ArrayList(); //cells on board-end should be 64


        public Form1()
        {

            InitializeComponent();

            int x_coord = 0;
            int y_coord = 0;

            for (int i = 0; i < 32; i++) //count to 32 to also get in y coordinate counter for even rows
            {
                //top left coordinates of each cell
                int X = 100 + (cell_size * x_coord);
                int Y = 100 + (cell_size * y_coord);
                Point coord = new Point(X, Y);
                Tile t = new Tile(coord);
                
                //determine the colors of each tile
                if (x_coord % 2 == 0)
                {
                    t.color = 0;
                }
                else
                {
                    t.color = 1;
                }

                //add to board cells
                board_cells.Add(t);
                //increase the x coordinate counter (should be 8)
                x_coord++;

                if (x_coord == 8)
                {
                    x_coord = 0;
                    y_coord = y_coord + 2;
                }
            }

            //repeat to get every other y coordinate counter (odd rows)
            y_coord = 1;
            for (int i = 0; i < 32; i++)
            {
                int X = 100 + (cell_size * x_coord);
                int Y = 100 + (cell_size * y_coord);
                Point coord = new Point(X, Y);
                Tile t = new Tile(coord);

                //same as above but t.color is switched (0->1) because of the alternating rows
                if (x_coord % 2 == 0)
                {
                    t.color = 1;
                }
                else
                {
                    t.color = 0;
                }

                board_cells.Add(t);
                x_coord++;
                if (x_coord == 8)
                {
                    x_coord = 0;
                    y_coord = y_coord + 2;
                }
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            //left mouse button click
            if (e.Button == MouseButtons.Left)
            {
                foreach (Tile t in board_cells)
                {
                    //if in a tile borders
                    if ((e.X - t.coord.X > 0 && e.X - t.coord.X < cell_size) && (e.Y - t.coord.Y > 0 && e.Y - t.coord.Y < cell_size))
                    {
                        //if queen is allowed
                        if (t.queen_allowed(queen_tot, t.coord.X, t.coord.Y) == true)
                        {
                            //if there are less than 8 queens
                            if (counter < 8)
                            {
                                counter++; //increase counter
                                t.qp = true; 
                                queen_tot.Add(t); //added to queen array
                                if (counter == 8) //if counter is 8, display message box
                                {
                                    DialogResult click = MessageBox.Show("You did it!", " ", MessageBoxButtons.OK);
                                }

                                Invalidate();
                            }
                        }
                        //if queen is not allowed or already placed play sound
                        else
                        {
                            System.Media.SystemSounds.Beep.Play();
                        }
                       
                    }
                }
             }
                
            //if right button is clicked, clear the queen 
                if (e.Button == MouseButtons.Right)
                {
                    foreach(Tile t in board_cells)
                    {
                        if ((e.X - t.coord.X > 0 && e.X - t.coord.X < cell_size) && (e.Y - t.coord.Y > 0 && e.Y - t.coord.Y < cell_size))
                        {
                        //if there is a queen present
                            if (t.qp == true)
                            {
                                counter--; //decrease queen counter
                                t.qp = false; //no queen present anymore
                                hints.Clear(); //remove hints (red tiles) if hints are on
                                queen_tot.Remove(t); //remove queen from list
                                Invalidate();
                            }
                        }
                    }
                }

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Font font = new Font("Arial", 30, FontStyle.Bold);

            //counter for queens to show user
            g.DrawString(String.Format("You have {0} queens on the board", counter), Font, Brushes.Black, new Point(200, 23));

            //format for the Q's placement in the tiles
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;
            
            //for al the tiles, determine and draw the rectangle with the correct collor 
            foreach (Tile t in board_cells)
            {
                //if white
                if (t.color == 0)
                {
                    //draw white tile with black border
                    g.FillRectangle(Brushes.White, new Rectangle(t.coord.X, t.coord.Y, cell_size, cell_size));
                    g.DrawRectangle(Pens.Black, new Rectangle(t.coord.X, t.coord.Y, cell_size, cell_size));

                    //if queen is present, draw the correct Q in black
                    if (t.qp == true)
                    {
                        g.DrawString("Q", font, Brushes.Black, new Rectangle(t.coord.X, t.coord.Y, cell_size, cell_size), format); 
                    }
                }

                //if black
                else if (t.color == 1)
                {
                    //draw black tile with black border
                    g.FillRectangle(Brushes.Black, new Rectangle(t.coord.X, t.coord.Y, cell_size, cell_size));
                    g.DrawRectangle(Pens.Black, new Rectangle(t.coord.X, t.coord.Y, cell_size, cell_size));

                    //if queen is present, draw correct q in white
                    if (t.qp == true)
                    {
                        g.DrawString("Q", font, Brushes.White, new Rectangle(t.coord.X, t.coord.Y, cell_size, cell_size), format);
                    }
                }
            }

            //if hints are on
            if (hintbox_check == true)
            {
                //checks to see if a queen is not allowed and if there is already a queen on a certain tile for every tile
                foreach (Tile t in board_cells)
                {
                    if (t.queen_allowed(queen_tot, t.coord.X, t.coord.Y) == false || t.qp == true)
                    {
                        hints.Add(t);
                    }
                }

                //creation of red tiles to layer on board for hints
                foreach (Tile s in hints)
                {
                    g.FillRectangle(Brushes.Red, new Rectangle(s.coord.X, s.coord.Y, cell_size, cell_size));
                    g.DrawRectangle(Pens.Black, new Rectangle(s.coord.X, s.coord.Y, cell_size, cell_size));
                    //redraws q's so that they are black
                    if (s.qp == true)
                    {
                        g.DrawString("Q", font, Brushes.Black, s.coord.X + 2, s.coord.Y + 2);
                    }
                } 
            }
        }

        //if clear button is clicked
        private void button1_Click(object sender, EventArgs e)
        {
            hints.Clear(); //all hints are turned off
            queen_tot.Clear(); //queens erased
            counter = 0; //queen counter is reset
            foreach(Tile t in board_cells)
            {
                t.qp = false; //queen is no longer in tile
            }

            Invalidate();
        }

        //function for checked hintbox/if is has been changed
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //if not checked, hints turned off and array list cleared (red cell layer disappears)
            if (checkBox1.Checked == false)
            {
                hintbox_check = false;
                hints.Clear();
            }
            //otherwise hints are turned on
            else
            {
                hintbox_check = true;
            }

            Invalidate();
        }

        //public class to reference for each individual tile, includes coordinates, the color of the tile, and determination of whether a queen is already present/allowed
        public class Tile
        {
            public Point coord;
            public bool qp;
            public int color;
            public Tile(Point P)
            {
                this.coord = P;
                this.qp = false;
            }

            //function to determine if a queen is allowed or placing a queen there is valid
            public bool queen_allowed(ArrayList queen_tot, int x, int y)
            {
                //foreach loop for where t is where queens are on board
                foreach (Tile t in queen_tot)
                {
                    if (!((x - t.coord.X > 0 && x - t.coord.X < cell_size) && (y - t.coord.Y > 0 && y - t.coord.Y < cell_size)) )
                    {
                        if (t.qp == true)
                        {
                            if (t.coord.X == x || t.coord.Y == y || (Math.Abs(t.coord.X - x) - Math.Abs(t.coord.Y - y)) == 0)
                            {
                                return false;
                            }
                        }
                    }
                }
                return true;
            }
        }
    }
}
