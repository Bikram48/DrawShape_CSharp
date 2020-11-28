using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Draw_Shapes
{
    public partial class DrawingBox : Form
    {
        Graphics g;
        public DrawingBox()
        {
            InitializeComponent();
            g = panel1.CreateGraphics();
            panel1.Cursor = Cursors.Hand;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                String text_Commands = textBox3.Text;
                Command_Parser obj = new Command_Parser();
                String width = textBox1.Text.Trim();
                String height = textBox2.Text.Trim();
                String pen_color = textBox5.Text.Trim();
                obj.parseCommands(text_Commands, g,width,height,pen_color);

              
                
               
            }
        }

       
    }
}
