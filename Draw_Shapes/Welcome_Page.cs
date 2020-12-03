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
    public partial class Welcome_Page : Form
    {
        public Welcome_Page()
        {
            InitializeComponent();
            label1.Parent = pictureBox1;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            Hide();
            DrawAllShapes shapes = new DrawAllShapes();
            shapes.ShowDialog();
            Close();
        }
    }
}
