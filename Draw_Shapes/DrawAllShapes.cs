using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Draw_Shapes
{
    public partial class DrawAllShapes : Form
    {
        Bitmap bitmap1;
        Bitmap bitmap2;
        public static int line_number;
        public static bool syntaxCheckerClicked = false;
        Graphics g;
        Graphics g1;
        CommandChecker checker = new CommandChecker();
        CommandLine commands = new CommandLine();
        public DrawAllShapes()
        {
            InitializeComponent();
            g = panel1.CreateGraphics();

            richTextBox2.Text += "\nNo errors found";
             bitmap1 = new Bitmap(5,5);
            bitmap2 = new Bitmap(5, 5);
        }
        public void drawBitmap()
        {
            g = Graphics.FromImage(bitmap1);
            g1 = Graphics.FromImage(bitmap2);
            Pen p = new Pen(new SolidBrush(Color.Red), 2);
            g.DrawEllipse(p, 0,0, 5, 5);
            g1.DrawEllipse(p, 0, 0, 5, 5);
        }
        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                commands.commandLineCommands(textBox2, richTextBox1, panel1, g,richTextBox2,textBox1);
               
            }
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadFile file = new LoadFile();
            file.fileLoading(richTextBox1);
        }

        private void saveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile savefile = new SaveFile();
            savefile.fileSave(richTextBox1);
        }

        public void makePoint(int xAxis, int yAxis)
        {
            Pen p = new Pen(new SolidBrush(Color.Red), 2);
            g.DrawEllipse(p, xAxis, yAxis, 4,4);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
              drawBitmap();
                Graphics windowg = e.Graphics;
                windowg.DrawImageUnscaled(bitmap1, 0,0);
            windowg.DrawImageUnscaled(bitmap2, 100,100);
        }

        private void button1_Click(object sender, EventArgs e)
        {
   
            syntaxCheckerClicked = true;
            CommandChecker.errors.Clear();
            line_number = 0;
            richTextBox2.Clear();
            String run_commands = textBox2.Text.Trim();
            String lower = run_commands.ToLower();
            //reads the lines of text from RichTextBox
            string[] RichTextBoxLines = richTextBox1.Lines;
            String singleLineCommand = textBox2.Text.Trim().ToLower();
            foreach (string line in RichTextBoxLines)
            {
                line_number++;
              
                //passed the line of text into the parseCommands method to check the commands are valid or invalid
                checker.parseCommands(line, g);
            }
            //checker.parseCommands(textBox2.Text, g);
            if (CommandChecker.error == true)
            {
                panel1.Invalidate();
                int totalerrors = CommandChecker.errors.Count;
                textBox1.Text = totalerrors + " Errors";
                for (int i = 0; i < CommandChecker.errors.Count; i++)
                {
                    richTextBox2.Text += "\n"+CommandChecker.errors[i]+"\n";
                    //Thread.Sleep(1000);
                }
            }
           if(CommandChecker.error==false)
            {
                panel1.Invalidate();
                int totalerrors = CommandChecker.errors.Count;
                textBox1.Text = totalerrors + " Errors";
                richTextBox2.Text += "\nNo Errors Found";
            }
          
             
            

           
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            button1.BackColor =Color.Blue;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackColor = Color.Black;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Do you really want to exit?", "Exit", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                Application.Exit();
            }
            else
            {
               
            }
        }
    }
}
