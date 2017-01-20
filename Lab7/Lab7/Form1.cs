using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace Lab7
{
    public partial class Form1 : Form
    {

        //the text written in the key box
        private String textKey = "";

        //the text that gives the path to the file
        private String filePath = "";

        public Form1()
        {
            InitializeComponent();
            openFileDialog1.FileName = "";
        }

        //function to encrypt the files
        //method taken from MSDN Microsoft website as reference with modifications
        private void Encryption()
        {            
            try
            {
                //file before encryption
                string input = this.filePath;

                //file after encryption has .des extension 
                string output = this.filePath + ".des";

                //throw an exception if there is no key so that the code stops executing 
                if (this.textBox_key.Text == "")
                {
                    throw new Exception("Please enter a key.");
                }

                //if user does not want to overwrite file, do nothing
                if (!Overwrite(output))
                {

                }

                //turn the string in the key text box into bytes for the key
                byte[] desKey = this.keyByte();
                byte[] desIV = this.keyByte();

                //I/O handling
                FileStream file_in = new FileStream(input, FileMode.Open, FileAccess.Read);
                FileStream file_out = new FileStream(output, FileMode.OpenOrCreate, FileAccess.Write);
                file_out.SetLength(0);

                byte[] bin = new byte[100]; 
                long bytes_written = 0;    
                long total = file_in.Length;
                int byteNumber;

                DES des = new DESCryptoServiceProvider();
                CryptoStream encStream = new CryptoStream(file_out, des.CreateEncryptor(desKey, desIV), CryptoStreamMode.Write);

                while (bytes_written < total)
                {
                    //read from the file
                    byteNumber = file_in.Read(bin, 0, 100);
                    //encrypt and write to the output (.des) file
                    encStream.Write(bin, 0, byteNumber);
                    //increase number of bytes written until bytes written are equal to total
                    bytes_written = bytes_written + byteNumber;
                }

                //close encryption and I/O streams
                encStream.Close();
                file_out.Close();
                file_in.Close();
            }

            //Catch exceptions from above
            catch (Exception e)
            {
                //if file is corrupted or does not exist
                if (e is System.IO.FileNotFoundException)
                {
                    MessageBox.Show("Could not open source or destination file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }

                //if keys do not match
                else if (e is System.Security.Cryptography.CryptographicException)
                {               
                    MessageBox.Show("Bad key or file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }

                //exception if an exception is thrown above
                else
                {
                    MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
        }

        //function to decrypt the files
        //method taken from MSDN Microsoft website as reference with modifications
        private void Decryption()
        {
            try
            {
                //file before decryption
                string input = this.filePath;
               
                //turn the string in the key text box into bytes for the key            
                byte[] desKey = this.keyByte();
                byte[] desIV = this.keyByte();

                //throw an exception if there is no key so that the code stops executing 
                if (this.textBox_key.Text == "")
                {
                    throw new Exception("Please enter a key.");
                }

                //throw an exception if there is no .des file extention so that the code stops executing
                if (Path.GetExtension(input) != ".des")
                {
                    throw new Exception("Not a .des file.");
                }

                //remove the .des extension after decryption
                string output = Path.ChangeExtension(filePath, "");

                //if user does not want to overwrite file, do nothing
                if (!Overwrite(output)) 
                {
                    
                }

                //I/O handling
                FileStream file_in = new FileStream(input, FileMode.Open, FileAccess.Read);
                FileStream file_out = new FileStream(output, FileMode.OpenOrCreate, FileAccess.Write);
                file_out.SetLength(0);

                byte[] bin = new byte[100];    
                long written_bytes = 0; 
                long total = file_in.Length; 
                int byteNumber;

                DES des = new DESCryptoServiceProvider();
                CryptoStream desStream = new CryptoStream(file_out, des.CreateDecryptor(desKey, desIV), CryptoStreamMode.Write);

                while (written_bytes < total)
                {
                    //read from the file
                    byteNumber = file_in.Read(bin, 0, 100);
                    //decrypt and write to the output (.des) file
                    desStream.Write(bin, 0, byteNumber);
                    //increase number of bytes written until bytes written are equal to total
                    written_bytes = written_bytes + byteNumber;
                }

                //close decryption and I/O streams
                desStream.Close();
                file_out.Close();
                file_in.Close();
            }

            //Catch exceptions from above
            catch (Exception e)
            {
                //if keys do not match
                if (e is System.Security.Cryptography.CryptographicException)
                {
                    MessageBox.Show("Bad key or file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }

                //if file is corrupted or does not exist
                else if (e is System.IO.FileNotFoundException)
                {
                    MessageBox.Show("Could not open source or destination file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }

                //exception if an exception is thrown above
                else
                {
                    MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
        }

        //when file selector button (image) is clicked
        private void openFile_Click(object sender, EventArgs e)
        {
            //opens the library window and if something is highlighed and OK is pressed
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                //the test filled in the file name box is whatever was selected
                FN_text.Text = openFileDialog1.FileName;
            }

            this.Invalidate();
        }

        //if the encrypt button is clicked
        private void encryptFile_Click(object sender, EventArgs e)
        {
            //use the encryption function
            Encryption();
            this.Invalidate();
        }

        //if the decrypt button is clicked
        private void decryptFile_Click(object sender, EventArgs e)
        {
            //use the decryption function
            Decryption();
            this.Invalidate();
        }

        //if the text inside of the key text box is changed
        private void textBox_key_TextChanged(object sender, EventArgs e)
        {
            //if something is written
            if (textBox_key.Text != null)
            {
                //the text written is going to be the key associated with the encryption or decryption
                this.textKey = textBox_key.Text;
            }
        }

        //if the text inside of the filename text box is changed
        private void FN_text_TextChanged(object sender, EventArgs e)
        {
            //if something is written
            if (FN_text.Text != null)
            {
                //the text written is going to be the new path to the new file
                this.filePath = FN_text.Text;
            }
            //else noting
        }

        //function that is called to see if a file with the same name already exists and needs to be overwritten
        public bool Overwrite(string output)
        {
            //if the file exists
            if (File.Exists(output))
            {
                //show a message box prompting user to see if the file should be overwritten
                var sol = MessageBox.Show("Output file exists. Overwrite?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                //if no then return true, else return false
                if (sol != DialogResult.No)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        //turns the string in the key box into byte per array
        private byte[] keyByte()
        {
            //Keys are 64 bits which are represented by an 8 byte array. The key is formed from the password string by taking the low order 8 bits of each Unicode character(cast to a byte) and storing it in the byte array. 
            //If the string is more than 8 characters the 9th character's 8 bit value is added to the first byte in the array, the 10th character is added to the second byte etc. Continue to wrap around and add character values until the string is depleted. 
            //The byte array is initialized to zero. 
            byte[] keyDet = Enumerable.Repeat((byte)0, 8).ToArray();
            for (int i = 0; i < textKey.Length; i++)
            {
                byte b = (byte)textKey[i];
                keyDet[i % 8] = (byte)(keyDet[i % 8] + b);
            }
            return keyDet;
        }
    }
}
