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

namespace Lab5
{
    public partial class Form1 : Form
    {
        private ArrayList drawObj = new ArrayList();
        private Point firstClick;
        private Point secondClick;
        private bool mouseclk = true;

        public Form1()
        {
            InitializeComponent();
            this.penWidth.SelectedIndex = 0; //pen width
            this.fillColor.SelectedIndex = 0; //fill color
            this.penColor.SelectedIndex = 0; //pen color
        }

        //clear the entire paint panel of objects when clear item on menu is selected
        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.drawObj.Clear();
            this.paintpanel.Invalidate();
            base.Update();
        }

        //exit the windows forms application
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        //reverses the last drawn object if there are none, otherwise just invalidates and refreshes paint panel
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.drawObj.Count != 0)
            {
                this.drawObj.RemoveAt(this.drawObj.Count - 1);
            }
            this.paintpanel.Invalidate();
            base.Update();
        }

        //handles mouse down/mouse click in the paint panel (helps draw objects)
        private void paintpanel_MouseDown(object sender, MouseEventArgs e)
        {

            if (this.mouseclk)
            {
                this.firstClick = e.Location;
                this.mouseclk = false;
                return;
            }

            this.secondClick = e.Location;
            this.mouseclk = true;
            Pen pen = null; //pen 
            Brush brush_1 = null; //pen color
            Brush brush_2 = null; //fill color

            //set pen color
            switch (this.penColor.SelectedIndex)
            {
                case 0:
                    brush_1 = Brushes.Black;
                    break;
                case 1:
                    brush_1 = Brushes.Red;
                    break;
                case 2:
                    brush_1 = Brushes.Blue;
                    break;
                case 3:
                    brush_1 = Brushes.Green;
                    break;
            }
            //if line object or outline is checked, pen must be the correct color and width
            if (this.outlineBox.Checked || this.lineButton.Checked)
            {

                pen = new Pen(brush_1, (float)int.Parse((string)this.penWidth.SelectedItem));
            }


            //set fill color when fill is checked
            if (this.fillBox.Checked)
            {
                switch (this.fillColor.SelectedIndex)
                {
                    case 0:
                        brush_2 = Brushes.White;
                        break;
                    case 1:
                        brush_2 = Brushes.Black;
                        break;
                    case 2:
                        brush_2 = Brushes.Red;
                        break;
                    case 3:
                        brush_2 = Brushes.Blue;
                        break;
                    case 4:
                        brush_2 = Brushes.Green;
                        break;
                }
            }

            //draw a line
            if (this.lineButton.Checked)
            {
                this.drawObj.Add(new drawLine(this.firstClick, this.secondClick, pen));
            }

            //If pen working and fill checked

            if (brush_2 != null || pen != null)

            {

                //Draw a Rectangle
                if (this.rectangleButton.Checked)
                {
                    this.drawObj.Add(new drawRect(this.firstClick, this.secondClick, pen, brush_2));
                }

                //Draw an Ellipse            
                if (this.EllipseButton.Checked)
                {
                    this.drawObj.Add(new drawEllipse(this.firstClick, this.secondClick, pen, brush_2));
                }
            }

            //If Text box is checked      
            if (this.textButton.Checked && this.textBox1.Text != "")
            {
                this.drawObj.Add(new drawText(this.textBox1.Text, brush_1, this.firstClick, this.secondClick, this.Font));
            }

            //Refresh the paint panel
            this.paintpanel.Invalidate();
            base.Update();

        }

        //handles objects drawn/"painted" in the paint panel
        private void paintpanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            //foreach object in drawObj, draw the new object with its respective graphic
            foreach(Object newObject in this.drawObj)
            {
                newObject.draw(graphics);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //ignore
        }

        private void penWidth_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ignore
        }

        private void fillColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ignore
        }

        private void penColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ignore
        }
    }

    //base class from which all graphics are derived
    public class Object
    {
        //virtual draw method that is overridden in every derived class
        public virtual void draw(Graphics g)
        {

        }
    }

    //class for drawing a line derived from Object
    public class drawLine : Object
    {
        private Pen pen;
        private Point firstpoint;
        private Point secondpoint;

        //instantiates the points and pen for the line
        public drawLine(Point firstpoint, Point secondpoint, Pen pen)
        {
            this.firstpoint = firstpoint;
            this.secondpoint = secondpoint;
            this.pen = pen;
        }

        //override draw method from base class
        public override void draw(Graphics g)
        {
            g.DrawLine(this.pen, this.firstpoint, this.secondpoint);
        }
    }
    //class for drawing rectangle derived from object
    public class drawRect : Object
    {
        private Point firstpoint;
        private Point secondpoint;
        private Pen pen;
        private Brush brush;

        //instantiates variables for rectangle
        public drawRect(Point firstpoint, Point secondpoint, Pen pen, Brush brush)
        {
            this.firstpoint = firstpoint;
            this.secondpoint = secondpoint;
            this.pen = pen;
            this.brush = brush;
        }

        //overrides draw method from base class
        public override void draw(Graphics g)
        {
            int wdth = Math.Abs(this.secondpoint.X - this.firstpoint.X);
            int height = Math.Abs(this.secondpoint.Y - this.firstpoint.Y);
            int x = Math.Min(this.firstpoint.X, this.secondpoint.X);
            int y = Math.Min(this.firstpoint.Y, this.secondpoint.Y);

            //draws outline of rectangle
            if (this.pen != null)
            {
                g.DrawRectangle(this.pen, x, y, wdth, height);
            }

            //fills in rectangle
            if (this.brush != null)
            {
                g.FillRectangle(this.brush, x, y, wdth, height);
            }
        }
    }

    //class for drawing textbox derived from object
    public class drawText : Object
    {
        private Point firstpoint;
        private Point secondpoint;
        private string str;
        private Brush brush;
        private Font font;

        //instantiates points, string, and color for pen
        public drawText(string str, Brush brush, Point firstpoint, Point secondpoint, Font font)
        {
            this.str = str;
            this.brush = brush;
            this.firstpoint = firstpoint;
            this.secondpoint = secondpoint;
            this.font = font;
        }

        //overrides draw method from base class
        public override void draw(Graphics g)
        {
            int wdth = Math.Abs(this.secondpoint.X - this.firstpoint.X);
            int height = Math.Abs(this.secondpoint.Y - this.secondpoint.Y);
            int y = Math.Min(this.firstpoint.Y, this.secondpoint.Y);
            int x = Math.Min(this.firstpoint.X, this.secondpoint.X);
            //draws the box for the text
            g.DrawString(this.str, this.font, this.brush, new Rectangle(x, y, wdth, height));
        }

    }

    //public class for drawing ellipse derived from object
    public class drawEllipse : Object
    {
        private Point firstpoint;
        private Point secondpoint;
        private Pen pen;
        private Brush brush;

        //instantiates variables for ellipse
        public drawEllipse(Point firstpoint, Point secondpoint, Pen pen, Brush brush)
        {
            this.firstpoint = firstpoint;
            this.secondpoint = secondpoint;
            this.pen = pen;
            this.brush = brush;
        }

        //overrides draw method from base class
        public override void draw(Graphics g)
        {
            //create "rectangle for boundaries of ellipse
            int wdth = Math.Abs(this.secondpoint.X - this.firstpoint.X);
            int height = Math.Abs(this.secondpoint.Y - this.firstpoint.Y);
            int x = Math.Min(this.firstpoint.X, this.secondpoint.X);
            int y = Math.Min(this.firstpoint.Y, this.secondpoint.Y);
            //draws the outline of the ellipse
            if (this.pen != null)
            {
                g.DrawEllipse(this.pen, x, y, wdth, height);
            }
            //fills in the ellipse
            if (this.brush != null)
            {
                g.FillEllipse(this.brush, x, y, wdth, height);
            }
        }
    }
}
