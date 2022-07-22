using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tools;

namespace SleakDirectFile
{
    public partial class encode : Form
    {
        public encode()
        {
            InitializeComponent(); 
            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(encode_DragEnter);
            this.DragDrop += new DragEventHandler(encode_DragDrop);
        }

        private void button1_DragDrop(object sender, DragEventArgs e)
        {

        }

        private void encode_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void encode_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                string[] file = (string[])e.Data.GetData(DataFormats.FileDrop);
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "TXT File (*.txt)|*.txt";
                    saveFileDialog.Title = "Save Converted Base64 ...";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        byte[] Key = Convert.FromBase64String("TxM4zjDL+WrdkYmUb5TvoQ==");

                        byte[] IV = Convert.FromBase64String("e2iOhnoh6dc=");


                        byte[] encrypt = Algroithum.EncryptTripleDES(File.ReadAllBytes(file[0]), Key, IV);

                        string data = Convert.ToBase64String(encrypt);
                        File.WriteAllText(saveFileDialog.FileName, data);

                        MessageBox.Show("Raw File Crypted Successfully!", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
       
        private void encode_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        Point mouseLocation;
        private void mouse_down(object sender, MouseEventArgs e)
        {
            mouseLocation = new Point(-e.X, -e.Y);

        }

        private void mouse_Move(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mousePose = Control.MousePosition;
                mousePose.Offset(mouseLocation.X, mouseLocation.Y);
                Location = mousePose;
            }
        }
    }
}
