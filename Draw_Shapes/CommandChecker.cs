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
    /// <summary>
    /// This class checks if the user typed a valid commands in the system.
    /// If user typed a valid commands then the commands get executed and 
    /// shows the appropriate output according to the typed commands.
    /// </summary>
    public class CommandChecker
    {
     
        public static ArrayList errors = new ArrayList();
        public static Boolean error = false;
        /// <summary>
        /// Uses a public modifier to give access to other classes also.
        /// Declaired as a static variable which will get accessed by through the class name.
        /// It is a boolean type variable to check if the pen command is get executed or not.
        /// It returns true if the pen command is executed if not it returns false.
        /// </summary>
        public static Boolean isPen=false;
        /// <summary>
        /// Uses a private visibility modifier to give access to this class only.
        /// It's a integer type which will store the values of xAxis from the moveTo command.
        /// </summary>
        public static int xAxis;
        /// <summary>
        /// Uses a private visibility modifier to give access to this class only.
        /// It's a integer type which will store the values of yAxis from the moveTo command.
        /// </summary>
        public static int yAxis;
        public static Boolean isAppear = false;
        /// <summary>
        /// Local variable with the boolean property which will check if the fill command is get executed or not.
        /// It will return true if the fillOn command is executed if not it will return false.
        /// </summary>
        Boolean fillOn = false;
        /// <summary>
        /// It is a reference variable of the Color class.
        /// It store the pen color entered by the user to draw a shapes.
        /// </summary>
        Color colour;
        /// <summary>
        /// The object of Canvas class is get instantiated.
        /// Will call the method of Canvas class through the canvas reference variable.
        /// </summary>
        Canvas canvas = new Canvas();
        
        public String[] splitTextBySpace(String text)
        {
            String[] splitter = text.Split(' ');
            return splitter;
        }

        public String[] splitParameterByComma(String parameter)
        {
            String[] split_parameters = parameter.Split(',');
            return split_parameters;
        }
        /// <summary>
        /// This method takes the text from the richTextBox and textBox and it 
        /// split the text by the spaces and commas to get the commands and the parameter.
        /// The main purpose of creating this method is to check if the commands typed
        /// by user are valid if they are valid then commands are passed to the Shape abstract
        /// class to generate a appropriate output.
        /// </summary>
        /// <param name="line"></param>
        /// <param name="g"></param>
        public void parseCommands(String line, Graphics g)
        {
            int[] parameter = new int[2];
            //change the text in lower form and removing the whitespaces from the text.
            String text = line.ToLower().Trim();
            //splits the text by the space.
            String[] splitter = splitTextBySpace(text);
            //The first text splitted by the space are commands.
            String commands = splitter[0];
            //if the triangle command is typed then this block of code will get executed.
            if (commands.Equals("triangle"))
            { 
                //calls the method drawTriangle from canvas class to draw a triangle.
                canvas.drawTriangle(colour, xAxis, yAxis, fillOn, g);

            }
            
            try
            {
                //The second text splitted by the space are parameters.
                String parameters = splitter[1];
                //splitting the parameters by the comma
                String[] split_parameters = splitParameterByComma(parameters);
               
                if (commands.Equals("moveto"))
                {
                    //if many parameters are passed then the system requires then this block will get executed.
                    if (split_parameters.Length != 2)
                    {
                        error = true;
                        
                        errors.Add("Invalid parameters at line"+DrawAllShapes.line_number);
                    }
                    else
                    {
                        isAppear = true;
                        try
                        {
                            //converting the parameters from string to integer
                            xAxis = Convert.ToInt32(split_parameters[0]);
                            yAxis = Convert.ToInt32(split_parameters[1]);
                            //creates a pen
                            Pen p = new Pen(new SolidBrush(Color.Red), 2);
                    
                            g.DrawEllipse(p, xAxis, yAxis, 4, 4);
                        }
                        //catch the exception thrown by the try block
                        catch (FormatException e)
                        {
                            
                            error = true;
                            errors.Add("Non numeric values at line" + DrawAllShapes.line_number);
                        }
        }
                }
                //if user type a drawto command this block will get executed
                else if (commands.Equals("drawto"))
                {
                    //if many parameters are passed then the system requires then this block will get executed.
                    if (split_parameters.Length != 2)
                    {
                        error = true;
                        errors.Add("Invalid parameters at line" + DrawAllShapes.line_number);
                    }
                    else
                    {
                        try
                        {
                            //converting the parameters from string to integer
                            int firstParameter = Convert.ToInt32(split_parameters[0]);
                            int secondParameter = Convert.ToInt32(split_parameters[1]);
                            //passed the parameters required to draw a line
                            canvas.drawLine(colour, xAxis, yAxis, fillOn, firstParameter, secondParameter, g);
                            xAxis = firstParameter;
                            yAxis = secondParameter;
                        }
                        //catch the exception thrown by the try block
                        catch (FormatException e)
                        {
                            error = true;
                            errors.Add("Non nummeric values at line" + DrawAllShapes.line_number);
                        }
                    }
                }
                //if user type a pen command this block will get executed
                else if (commands.Equals("pen"))
                {
                    //if pen command executed then isPen boolean value becomes true
                    isPen = true;    
                    //Instantiating the object of PenColor class
                    PenColor colors = new PenColor();
                    //passing colors to getPenColor method of PenColor class 
                    colour=colors.getPenColor(parameters);
                }
                //if user type a fill command this block will get executed
                else if (commands.Equals("fill"))
                {
                    if (parameters.Equals("on"))
                    {
                        //if fill command is on then make the fillOn boolean value to true
                        fillOn = true;
                    }
                }
                //if user type a rectangle command this block will get executed
                else if (commands.Equals("rectangle"))
                {
                    if (split_parameters.Length != 2)
                    {
                        error = true;
                        errors.Add("Invalid parameters");
                    }
                    else
                    {
                        try
                        {
                            //converts the width and height to integer from string.
                            int firstParameter = Convert.ToInt32(split_parameters[0]);
                            int secondParameter = Convert.ToInt32(split_parameters[1]);
                           
                                //passing the parameters to draw a  rectangle
                                canvas.drawRectangle(colour, xAxis, yAxis, fillOn, firstParameter, secondParameter, g);
                            
                        }
                        catch (FormatException e)
                        {
                            error = true;
                            errors.Add("Non nummeric values at line" + DrawAllShapes.line_number);
                        }
        }
                }
                //if user type a circle command this block will get executed
                else if (commands.Equals("circle"))
                {
                    try
                    {
                        //converting from string to integer
                        int radius = Convert.ToInt32(parameters);
                        //draws the cricle shape
                        canvas.drawCircle(colour, xAxis, yAxis, fillOn, radius, g);
                    }
                    catch (FormatException e)
                    {
                        error = true;
                        errors.Add("Non nummeric values at line" + DrawAllShapes.line_number);
                    }

                }
                //if user type a command which is not valid then this block executes
                else
                {
                    error = true;
                    errors.Add("Invalid command at line " + DrawAllShapes.line_number);
                }
            }
            catch (IndexOutOfRangeException e)
            {
                error = true;
                errors.Add("Invalid command at line " + DrawAllShapes.line_number);
            }


        }

        public void Reset()
        {
            xAxis = 0;
            yAxis = 0;
        }

        public void pointer( PaintEventArgs e,Bitmap bitmap1 )
         {
            Graphics windowg = e.Graphics;
            windowg.DrawImageUnscaled(bitmap1, xAxis, yAxis);
        }
        




    }
}
