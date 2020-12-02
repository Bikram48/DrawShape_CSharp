using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draw_Shapes
{
    class PenColor
    {
        Color colour;
        public Color getPenColor(String parameters)
        {
            if (parameters == "red")
            {
                colour = Color.Red;
            }
            else if (parameters == "blue")
            {
                colour = Color.Blue;
            }
            else if (parameters == "yellow")
            {
                colour = Color.Yellow;
            }
            else if (parameters == "gray")
            {
                colour = Color.Gray;
            }
            else if (parameters == "green")
            {
                colour = Color.Green;
            }
            else
            {
                colour = Color.Black;
            }
            return colour;
        }
    }
}
