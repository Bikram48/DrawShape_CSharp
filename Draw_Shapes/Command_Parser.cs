using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Draw_Shapes
{
    class Command_Parser
    {

        private String[] all_shapes = { "Rectangle", "rectangle", "Circle", "circle", "triangle", "Triangle" };
        public static Boolean point_appear = false;
        String[] colors = { "red", "yellow", "black", "blue", "brown" };
        
       

        public void checkShapes(String shapes)
        {
            String input_shapes = shapes.Trim();
            if (input_shapes.Equals(""))
            {
                MessageBox.Show("TextBox is empty");
            }
            else if(all_shapes.Contains(input_shapes))
            {
                DrawingBox box=new DrawingBox();
                box.Show();

            }
            else
            {
                MessageBox.Show("Shape doesn't exist");
            }
        }

        public void parseCommands(String command,Graphics g,String width,String height,String color)
        {
            int w = Convert.ToInt32(width);
            int h = Convert.ToInt32(height);

            String text = command.ToLower().Trim();
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
                    point_appear = true;
                    try
                    {
                        Color col = checkPenColor(color);
                        int firstParameter = Convert.ToInt32(split_parameters[0]);
                        int secondParameter = Convert.ToInt32(split_parameters[1]);

                       // Shapes shape = new Rectangle(col, firstParameter, secondParameter, w, h);
                        //shape.draw(g);
                    }
                    catch(FormatException e)
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

                       // Shapes shape = new Rectangle(Color.Red, firstParameter, secondParameter, 300, 300);
                       // shape.drawLine(g);
                    }
                    catch (FormatException e)
                    {
                        MessageBox.Show("Please enter the integer values");
                    }
                }
            }
            
        }

        public Color checkPenColor(String color)
        {
            Color c;
            String text = color.ToLower();
            String[] colour = text.Split(' ');
            String commands = colour[0];
            String pencolor = colour[1];

            if (commands.Equals("pen"))
            {
                if (pencolor.Equals("red"))
                {
                    c = Color.Red;
                    return c;
                }
                else if (pencolor.Equals("blue"))
                {
                     c = Color.Blue;
                    return c;
                }
                else if (pencolor.Equals("yellow"))
                {
                     c = Color.Yellow;
                    return c;
                }
                else if (pencolor.Equals("green"))
                {
                     c = Color.Green;
                    return c;
                }
                else if (pencolor.Equals("gray"))
                {
                     c = Color.Gray;
                    return c;
                }
                else
                {
                    MessageBox.Show("This color is not available at the moment");
                }
                
            }
            return Color.Red;
        }

      
    }
}
