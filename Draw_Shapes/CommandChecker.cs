using System;
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
        Boolean fillOn = false;
        int xAxis;
        int yAxis;
        Color colour;
        public void parseCommands(String line,Graphics g)
        {
            String text = line.ToLower().Trim();
            String[] splitter = text.Split(' ');

            String commands = splitter[0];
            String parameters = splitter[1];

            String[] split_parameters = parameters.Split(',');

            if (commands.Equals("moveto"))
            {
                if (split_parameters.Length != 2)
                {
                    MessageBox.Show("Invalid parameters");
                }
                else
                {
                    try
                    {
                        
                        xAxis = Convert.ToInt32(split_parameters[0]);
                        yAxis = Convert.ToInt32(split_parameters[1]);

                       
                    }
                    catch (FormatException e)
                    {
                        MessageBox.Show("Please enter the integer values");
                    }
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
                    try
                    {
                        int firstParameter = Convert.ToInt32(split_parameters[0]);
                        int secondParameter = Convert.ToInt32(split_parameters[1]);

                      
                    }
                    catch (FormatException e)
                    {
                        MessageBox.Show("Please enter the integer values");
                    }
                }
            }
            else if (commands.Equals("pen"))
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
                    try
                    {

                        int firstParameter = Convert.ToInt32(split_parameters[0]);
                        int secondParameter = Convert.ToInt32(split_parameters[1]);

                        Shapes shape = new Rectangle(colour, xAxis,yAxis,fillOn,firstParameter,secondParameter);
                        shape.draw(g);
                    }
                    catch (FormatException e)
                    {
                        MessageBox.Show("Please enter the integer values");
                    }
                }
            }
            else if (commands.Equals("circle"))
            {
                int radius = Convert.ToInt32(parameters);
                Shapes circle = new Circle(colour, xAxis, yAxis, fillOn, radius);
                circle.draw(g);
            }
            else
            {
                MessageBox.Show("Invalid command");
            }


        }
    }
}
