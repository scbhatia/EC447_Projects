using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace Lab3
{
    public partial class Form1 : Form
    {
        private ArrayList coordinates = new ArrayList();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e) //mouseclick event handler
        {
            if (e.Button == MouseButtons.Left) //if left button is pushed
            {
                PointValues p = new PointValues(e.X, e.Y, false); //create and add coordinates to an array list
                this.coordinates.Add(p);
                this.Invalidate();
            }

            if (e.Button == MouseButtons.Right) //right button pushed
            {
                bool in_circle = false;
                int total = coordinates.Count;
                int index = total - 1;
                for (int i = index; i >= 0; i--)
                {
                    PointValues p = (PointValues)coordinates[i];
                    if ((Math.Abs(e.X-p.X) < 10) && (Math.Abs(e.Y-p.Y) < 10))
                    {
                        if (p.red_black == false) //if black
                        {
                            p.red_black = true; //turned red on right click
                            in_circle = true; //you're in the circle
                            this.Invalidate();
                        }

                        else if (p.red_black == true) //if red
                        {
                            coordinates.RemoveAt(i); //remove coordinates from array list on right click
                            in_circle = true; //you're in the circle
                            this.Invalidate();
                        }
                    }
                }

                if (in_circle == false) //if right click not in circle
                {
                    this.coordinates.Clear(); //clear coordinates
                    this.Invalidate();
                }
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            const int WIDTH = 20;
            const int HEIGHT = 20;
            Graphics g = e.Graphics;
            foreach (PointValues p in this.coordinates)
            {
                if (p.red_black)
                {
                    g.FillEllipse(Brushes.Red, p.X - WIDTH / 2, p.Y - WIDTH / 2, WIDTH, HEIGHT); //fill red if circle
                }
                if (!p.red_black)
                {
                    //string coordinates = string.Format"(({0}, {1})", p.x_coord, p.y_coord);
                    g.FillEllipse(Brushes.Black, p.X - WIDTH / 2, p.Y - WIDTH / 2, WIDTH, HEIGHT); //fill black if no circle
                    //g.DrawString(coordinates, Font, Brushes.Black, p.x_coord + 15, p.y_coord - 5);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.coordinates.Clear(); //clear if button is pushed
            this.Invalidate();
        }

        private void clearToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.coordinates.Clear(); //clear if clear button on menu is clicked
            this.Invalidate();
        }
    }

    public class PointValues //class to help distinguish circles, locations & colors
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool red_black { get; set; }
        public PointValues(int x_coord, int y_coord, bool color)
        {
            X = x_coord;
            Y = y_coord;
            red_black = color;
        }
    }
}
