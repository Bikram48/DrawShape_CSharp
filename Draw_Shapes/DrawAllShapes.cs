using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Draw_Shapes
{
    public partial class DrawAllShapes : Form
    {
        Graphics g;
        CommandChecker checker = new CommandChecker();
        public DrawAllShapes()
        {
            InitializeComponent();
            g = panel1.CreateGraphics();
        }

        /*public void commandParser(String commands)
        {
            //moveTo 12,13
           
            String command = commands.Trim();
           
                String[] splitter = command.Split(' ');
                String c = splitter[0];
            if (c.Equals("moveTo") || commands.Equals("drawTo"))
            {
                String p = splitter[1];

                String[] parameter_splitter = p.Split(',');
                int parameter1 = Convert.ToInt32(parameter_splitter[0]);
                int parameter2 = Convert.ToInt32(parameter_splitter[1]);
            }
            else if (c.Equals("Pen"))
            {
                MessageBox.Show("Pen command");
            }
            else
            {
                MessageBox.Show("Invalid commands");
            }

        }
        */

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {

                


                /*for(int i = 0; i <= richTextBox1.Lines.Length; i++)
                {
                    string textBox = string.Format("textBox{0}", i + 1);
                    Controls[textBox].Text = richTextBox1.Lines[i];
                    MessageBox.Show(textBox);
                }
                */
              

                String run_commands = textBox2.Text.Trim();
                String lower = run_commands.ToLower();
                //if (lower.Equals("run"))
                //{
                string[] RichTextBoxLines = richTextBox1.Lines;

                String singleLineCommand = textBox2.Text.Trim().ToLower();

             
                if (singleLineCommand.Equals("run"))
                {
                    foreach (string line in RichTextBoxLines)
                    {
                        checker.parseCommands(line, g);
                    }
                    
                }
                else if (singleLineCommand.Equals("clear"))
                {
                    richTextBox1.Clear();
                    panel1.Refresh();
                }
               
                else
                {
                    checker.parseCommands(textBox2.Text,g);
                }
                //}
                /*
                else if(lower.Equals("clear"))
                {
                    panel1.Invalidate();
                    richTextBox1.Clear();
                }
                else
                {
                    MessageBox.Show("Invalid command");
                }
                */

            }
        }

      
    }
}
