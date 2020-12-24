using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draw_Shapes
{
    /// <summary>
    /// This class is mainly used to get the color for the pen.
    /// </summary>
    public class PenColor
    {
        /// <summary>
        /// Reference of the Color class
        /// </summary>
        Color colour;
        /// <summary>
        /// This method takes the String parameter and returns the color for the pen.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns>colour</returns>
        public Color getPenColor(String parameters)
        {
            //if parameter is red then colour is set  to red
            if (parameters.Equals("red"))
            {
                //sets pen color to red
                colour = Color.Red;
            }
            //if parameter is blue then colour is set  to blue
            else if (parameters.Equals("blue"))
            {
                //sets pen color to blue
                colour = Color.Blue;
            }
            //if parameter is yellow then colour is set  to yellow
            else if (parameters.Equals("yellow"))
            {
                //sets pen color to yellow
                colour = Color.Yellow;
            }
            //if parameter is gray then colour is set  to gray
            else if (parameters.Equals("gray"))
            {
                //sets pen color to gray
                colour = Color.Gray;
            }
            //if parameter is green then colour is set  to green
            else if (parameters.Equals("green"))
            {
                //sets pen color to green
                colour = Color.Green;
            }
            else
            {
                CommandLine.error = true;
                ErrorRepository.errorsList.Add("Invalid color name at line " + DrawAllShapes.line_number);
            }
            //returning the color for the pen
            return colour;
        }
    }
}
