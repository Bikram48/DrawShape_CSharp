using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Draw_Shapes
{
    class CommandChecker
    {
        public static Boolean isPen=false;
        Boolean fillOn = false;
        private int xAxis;
        private int yAxis;
        Color colour;
        public void parseCommands(String line, Graphics g)
        {
            int[] parameter = new int[2];
            String text = line.ToLower().Trim();
            String[] splitter = text.Split(' ');
            String commands = splitter[0];
            try
            {
                String parameters = splitter[1];
                String[] split_parameters = parameters.Split(',');
                for(int i = 0; i < split_parameters.Length; i++)
                {
                    try
                    {
                        parameter[i] = Convert.ToInt32(split_parameters[i]);
                    }
                    catch (FormatException e)
                    {
                        MessageBox.Show("Please enter the integer values");
                    }
                }

                if (commands.Equals("moveto"))
                {
                    if (split_parameters.Length != 2)
                    {
                        MessageBox.Show("Invalid parameters");
                    }
                    else
                    {
                        xAxis =parameter[0];
                        yAxis = parameter[1];      
                    }
                }
                else if (commands.Equals("drawto"))
                {
                    if (split_parameters.Length != 2)
                    {
                        MessageBox.Show("Invalid parameters");
                    }
                    else
                    {
                            int firstParameter = parameter[0]; ;
                            int secondParameter = parameter[1]; ;
                       
                    }
                }
                else if (commands.Equals("pen"))
                {
                    isPen = true;                  
                    PenColor colors = new PenColor();
                    colour=colors.getPenColor(parameters);
                }
                else if (commands.Equals("fill"))
                {
                    if (parameters.Equals("on"))
                    {
                        fillOn = true;
                    }
                }
                else if (commands.Equals("rectangle"))
                {
                    if (split_parameters.Length != 2)
                    {
                        MessageBox.Show("Invalid parameters");
                    }
                    else
                    {
                            Shapes shape = new Rectangle(colour, xAxis, yAxis, fillOn, parameter[0], parameter[1]); 
                    }
                }
                else if (commands.Equals("circle"))
                {
                    int radius = Convert.ToInt32(parameters);
                    Shapes circle = new Circle(colour, xAxis, yAxis, fillOn, radius);
                    circle.draw(g);
                }
                else if (commands.Equals("triangle"))
                {
                    Triangle triangle = new Triangle();
                    triangle.drawTriangle(g, parameter[0], parameter[1]);
                }
                else
                {
                    MessageBox.Show("Invalid command");
                }
            }
            catch (IndexOutOfRangeException e)
            {

            }


        }

        
    }
}
