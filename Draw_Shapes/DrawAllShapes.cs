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
        CommandLine commands = new CommandLine();
        public DrawAllShapes()
        {
            InitializeComponent();
            g = panel1.CreateGraphics();

           
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
          
                commands.commandLineCommands(textBox2, richTextBox1, panel1, g);
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
            checker.pointer(g);
            
        }
    }
}
