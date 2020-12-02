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

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                String run_commands = textBox2.Text.Trim();
                String lower = run_commands.ToLower();
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
    }
}
