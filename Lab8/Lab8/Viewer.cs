using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab8
{
    public partial class Viewer : Form
    {
        //current slide
        private int cSlide = 0;
        //current file
        private string cFile;
        //current image
        private Image cImage;
        //client size to later scale images
        private SizeF client_size;
        //width and height of current original image
        private int width, height;
        //scaling the image and centering it
        private float image_place; 
        //font
        private Font font = new Font("Arial", 25);
        //brush
        private Brush brush = Brushes.Red;
        //file from Form1 class
        private Form1 file;
        Graphics graphics;

        public Viewer()
        {
            InitializeComponent();

            //slide show viewer should not have a border and should be maximized
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
        }

        //when a key is pressed
        private void Viewer_KeyDown(object sender, KeyEventArgs e)
        {
            //if esc is pressed, exit slide show viewer early
            if (e.KeyData == Keys.Escape)
            {
                base.DialogResult = DialogResult.OK;
            }
        }

        //once slide show viewer turns on
        private void Viewer_Activated(object sender, EventArgs e)
        {
            //make sure form 1 is the owner of the file to be shown and loaded
            file = (Form1)base.Owner;
            //convert interval to seconds
            intervalTime.Interval = file.CurrentInt * 1000;
            //interval is enabled for switching between photos
            intervalTime.Enabled = true;
        }

        //interval timer
        private void intervalTime_Tick(object sender, EventArgs e)
        {
            //every time interval ticks go to next slide and clear old one
            this.cSlide++;
            base.Invalidate();
        }

        //what to draw on the viewer
        private void Viewer_Paint(object sender, PaintEventArgs e)
        {
            graphics = e.Graphics;

            //if at last slide, exit
            if (cSlide > file.FileNameListBox.Items.Count - 1)
            {
                DialogResult = DialogResult.OK;
                return;
            }

            //initialize current file to current file in list box item based on what slide you're on (goes in order)
            cFile = (string)file.FileNameListBox.Items[cSlide];

            try
            {
                cImage = Image.FromFile(cFile);
                client_size = base.ClientSize;
                width = cImage.Width;
                height = cImage.Height;
                
                //center, scale, and place the image
                image_place = Math.Min(client_size.Height / (float)height, client_size.Width / (float)width);
                graphics.DrawImage(cImage, (client_size.Width - (float)width * image_place) / 2f, (client_size.Height - (float)height * image_place) / 2f, (float)width * image_place, (float)height * image_place);
            }

            //if not an image file
            catch
            {             
                graphics.DrawString("Not an image file!", font, brush, 25, 25);
            }
        }
    }
}
