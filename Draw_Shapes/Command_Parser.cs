using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Draw_Shapes
{
    class Command_Parser
    {
        private String[] all_shapes = { "Rectangle", "rectangle", "Circle", "circle", "triangle", "Triangle" };

        public void checkShapes(String shapes)
        {
            String input_shapes = shapes.Trim();
            if (input_shapes.Equals(""))
            {
                MessageBox.Show("TextBox is empty");
            }
            else if(all_shapes.Contains(input_shapes))
            {
                MessageBox.Show("Shape exist");
            }
            else
            {
                MessageBox.Show("Shape doesn't exist");
            }
        }
    }
}
