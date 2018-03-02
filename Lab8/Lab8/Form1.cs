using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Lab8
{
    public partial class Form1 : Form
    {
        //interval
        public int CurrentInt;
        //collections
        private string CurrentColl;

        public Form1()
        {
            InitializeComponent();

            //determine the types of files that an be added and indicies
            openFileDialog4.FileName = "";
            openFileDialog4.Filter = "*.jpg;*.gif;*.png;*.bmp|*.jpg; *.gif; *.png; *.bmp|*.* ( All Files)|*.*";
            openFileDialog4.FilterIndex = 1;
            openFileDialog1.Filter = "*.pix|*.pix";
            openFileDialog1.FilterIndex = 1;
            saveFileDialog2.Filter = "*.pix|*.pix";
            saveFileDialog2.FilterIndex = 1;

            //default
            IntervalTextBox.Text = "5";
        }

        //if menu item is clicked
        private void openCollectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
   
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                //clear the list box
                FileNameListBox.Items.Clear();
                //update the current collection to be put in the listbx to whats in the file dialog
                CurrentColl = openFileDialog1.FileName;
                //read the files from the file dialog
                StreamReader streamRead = new StreamReader(openFileDialog1.OpenFile());

                string File;
                //until reading the file dialog is null
                while ((File = streamRead.ReadLine()) != null)
                {
                    //add file to list box
                    FileNameListBox.Items.Add(File);
                }
                //close the reader
                streamRead.Close();
            }
        }        

        //if menu item is clicked
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //exit
            Application.Exit();
        }

        //if button is clicked
        private void AddFilesButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog4.ShowDialog(this) == DialogResult.OK)
            {
                //add files to list box as selected
                foreach (string FileName in openFileDialog4.FileNames)
                {
                    FileNameListBox.Items.Add(FileName);
                }
            }

            this.Invalidate();
        }

        //if button is clicked
        private void Show_Click(object sender, EventArgs e)
        {
            //if no images in list box display message
            if (FileNameListBox.Items.Count == 0)
            {
                MessageBox.Show("No images to show.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            //try interval set
            try
            {
                CurrentInt = Int32.Parse(IntervalTextBox.Text);
                //if interval is 0 or negative throw an error
                if (this.CurrentInt <= 0)
                {
                    throw new Exception("Please enter an integer time interval > 0");
                }
            }

            catch
            {
                MessageBox.Show("Please enter an integer time interval > 0", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            //connect to slide show viewer and set owner to this form
            Viewer viewform = new Viewer { Owner = this };
            //show viewer
            viewform.ShowDialog();
        }
        
        //if button is clicked
        private void DeleteFilesButton_Click(object sender, EventArgs e)
        {
            for (int i = FileNameListBox.Items.Count - 1; i >= 0; i--)
            {
                //delete the file that is selected if valid/exists
                if (FileNameListBox.SelectedIndices.Contains(i))
                {
                    FileNameListBox.Items.RemoveAt(i);
                }
            }

            this.Invalidate();
        }

        //if menu item is clicked
        private void saveCollectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if the list box is empty display message 
            if (FileNameListBox.Items.Count == 0)
            {
                MessageBox.Show("No file names to save.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            //if there is something in the list box
            if (CurrentColl != null)
            {
                //the files are saved into a new collection
                saveFileDialog2.FileName = CurrentColl;
            }

            else
            {
                //are not saved
                saveFileDialog2.FileName = null;
            }

            //if selected
            if (saveFileDialog2.ShowDialog() == DialogResult.OK)
            {
                //write filenames into collection save
                StreamWriter streamWrite = new StreamWriter(saveFileDialog2.OpenFile());
                foreach (string File in this.FileNameListBox.Items)
                {
                    streamWrite.WriteLine(File);
                }
                //close stream
                streamWrite.Close();
            }
        }
    }
}
