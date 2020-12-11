using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
       
        bool equal,greater_equals,smaller_equals,greater,smaller,not_equals = false;
        String[] signs = { "<=", ">=", "<", ">", "==", "!=" };
        IDictionary<String, int> store_variables = new Dictionary<String,int>();
        private int firstParameter;
        private int secondParameter;
        private bool isVariable = false;
        private bool isIf = false;
        private String para1;
        int value1;
        int value2;
        private String para2;
        /// <summary>
        /// ArrayList is created with static keyword to access this arraylist by a classname.
        /// Stores all the errors throws by the commands or program.
        /// </summary>
        public static ArrayList errors = new ArrayList();
        /// <summary>
        /// Uses a public modifier to give access to other classes also.
        /// Declaired as a static variable which will get accessed  through the class name.
        /// It returns the true value if errors are detected if not then it returns false.
        /// </summary>
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
        private  int xAxis;
        /// <summary>
        /// Uses a private visibility modifier to give access to this class only.
        /// It's a integer type which will store the values of yAxis from the moveTo command.
        /// </summary>
        private  int yAxis;
   
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
        
        /// <summary>
        /// uses public access modifier to give access to other classes also.
        /// It splits the text by spaces and returns string array with splitted text
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public String[] splitTextBySpace(String text)
        {
            //storing the splitted text in array
            String[] splitter = text.Split(' ');
            //returning array with splitted text
            return splitter;
        }

        public String[] splitVariables(String text)
        {
            String[] splitter = text.Split(' ');
            return splitter;
        }

        public String[] splitValues(String parameter)
        {
            String[] split_parameter = parameter.Split('=');
            return split_parameter;
        }

        /// <summary>
        /// uses public access modifier to give access to other classes also.
        /// It splits the parameters by comma and returns string array with splitted parameter
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public String[] splitParameterByComma(String parameter)
        {
            //storing the splitted parameters in array
            String[] split_parameters = parameter.Split(',');
            //returning an array with splitted parameter
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
        public void parseCommands(String line, Graphics g,RichTextBox richTextBox1)
        {
            /*String match = @"if\s*\(((?!\s*\{).+)\)";
            Regex reg = new Regex(match);
            if (reg.IsMatch(line))
            {
                MessageBox.Show("Matched");
            }
            */
            if (line.Contains("var"))
            {
                
                isVariable = true;
                String txt = line.ToLower().Trim();
                String[] var_splitter = splitVariables(txt);
                String variables = var_splitter[1];
                String[] values = splitValues(variables);
                String variable_name = values[0];
                String variable_value = values[1];
                int v = Convert.ToInt32(variable_value);
                store_variables.Add(variable_name, v);
            }

            if (line.Contains("if"))
            {
                int linen = CommandLine.lineNumber;
               for(int i = linen; i == linen+1; i++)
                {
                    String[] message = richTextBox1.Lines;
                    MessageBox.Show(message+"");
                }
                isVariable = false;
                isIf = true;
                IfCommand command = new IfCommand();
                
                String[] para = command.commandCheck(line);
                value1 = store_variables[para[0]];
                value2 = store_variables[para[1]];
                if (line.Contains("=="))
                {
                    if (value1 == value2)
                        equal = true;
                }
                else if (line.Contains(">="))
                {
                    if (value1 >= value2)
                        greater_equals = true;

                }
                else if (line.Contains("<="))
                {
                    if (value1 <= value2)
                        smaller_equals = true;
                }
                else if (line.Contains("!="))
                {
                    if (value1 != value2)
                        not_equals = true;
                }
                else if (line.Contains(">"))
                {
                    if (value1 > value2)
                        greater = true;
                }
                else if (line.Contains("<"))
                {
                    if (value1 < value2)
                        smaller = true;
                }
                else 
                {
                    MessageBox.Show("Invalid operator ");
                }
            }
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
                canvas.drawTriangle(colour, xAxis, yAxis, fillOn,isPen, g);
            }
            //If there is no commands entered then return nothing
            if (commands.Equals(""))
            {
                return;
            }
            //try block starts
            try
            {
                //The second text splitted by the space are parameters.
                String parameters = splitter[1];
                //splitting the parameters by the comma
                String[] split_parameters = splitParameterByComma(parameters);
               //if moveto command is executed then this block will be called
                if (commands.Equals("moveto"))
                {
                    //if many parameters are passed then the system requires then this block will get executed.
                    if (split_parameters.Length != 2)
                    {
                        //if invalid parameter is entered then error becomes true
                        error = true;
                        //storing errors in arraylist
                        errors.Add("Invalid parameters at line "+DrawAllShapes.line_number);
                    }
                    else if (isVariable)
                    {
                        if (store_variables.ContainsKey(split_parameters[0]))
                        {
                            xAxis = Convert.ToInt32(store_variables[split_parameters[0]]);
                            //MessageBox.Show("" + firstParameter);
                        }

                        if (store_variables.ContainsKey(split_parameters[1]))
                        {
                            yAxis = Convert.ToInt32(store_variables[split_parameters[1]]);
                            //MessageBox.Show("" + secondParameter);
                        }
                    }
                    else
                    {
                      
                        try
                        {
                            //converting the parameters from string to integer
                            xAxis = Convert.ToInt32(split_parameters[0]);
                            yAxis = Convert.ToInt32(split_parameters[1]);
                            //creates a pen
                            Pen p = new Pen(new SolidBrush(Color.Red), 2);
                        }
                        //catch the exception thrown by the try block
                        catch (FormatException e)
                        {
                            //if non numberic values is entered then error becomes true
                            error = true;
                            //storing errors in arraylist
                            errors.Add("Non numeric values at line " + DrawAllShapes.line_number);
                        }
                    }
                }
                //if user type a drawto command this block will get executed
                else if (commands.Equals("drawto"))
                {
                    //if many parameters are passed then the system requires then this block will get executed.
                    if (split_parameters.Length != 2)
                    {
                        //if invalid parameter is entered then error becomes true
                        error = true;
                        //storing errors in arraylist
                        errors.Add("Invalid parameters at line " + DrawAllShapes.line_number);
                    }
                    else if (isVariable)
                    {
                        if (store_variables.ContainsKey(split_parameters[0]))
                        {
                            firstParameter = Convert.ToInt32(store_variables[split_parameters[0]]);
                            //MessageBox.Show("" + firstParameter);
                        }

                        if (store_variables.ContainsKey(split_parameters[1]))
                        {
                            secondParameter = Convert.ToInt32(store_variables[split_parameters[1]]);
                            //MessageBox.Show("" + secondParameter);
                        }
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
                            //if Non nummeric values  is entered then error becomes true
                            error = true;
                            //storing errors in arraylist
                            errors.Add("Non nummeric values at line " + DrawAllShapes.line_number);
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
                    colour = colors.getPenColor(parameters);
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
                        //if invalid parameter is entered then error becomes true
                        error = true;
                        //storing errors in arraylist
                        errors.Add("Invalid parameters at line " + DrawAllShapes.line_number);
                    }
                    else if (isVariable)
                    {
                        
                        if (store_variables.ContainsKey(split_parameters[0]))
                        {
                            firstParameter = Convert.ToInt32(store_variables[split_parameters[0]]);
                            //MessageBox.Show("" + firstParameter);
                        }

                        if (store_variables.ContainsKey(split_parameters[1]))
                        {
                            secondParameter = Convert.ToInt32(store_variables[split_parameters[1]]);
                            //MessageBox.Show("" + secondParameter);
                        }
                        
                        
                       canvas.drawRectangle(colour, xAxis, yAxis, fillOn, isPen, firstParameter, secondParameter, g);
                    }
                    
                    else if (isIf == true)
                    {
                        if (equal||greater_equals||smaller_equals||not_equals||greater||greater_equals)
                            canvas.drawRectangle(colour, xAxis, yAxis, fillOn, isPen, value1, value2, g);
                                
                        else
                            MessageBox.Show("Condition doesn't meet");
                    }
                    else
                    {
                        try
                        {
                            //converts the width and height to integer from string.
                            int firstParameter = Convert.ToInt32(split_parameters[0]);
                            int secondParameter = Convert.ToInt32(split_parameters[1]);
                           
                            //passing the parameters to draw a  rectangle
                            canvas.drawRectangle(colour, xAxis, yAxis, fillOn,isPen, firstParameter, secondParameter, g);                           
                        }
                        catch (FormatException e)
                        {
                            //if Non nummeric values  is entered then error becomes true
                            error = true;
                            //storing errors in arraylist
                            errors.Add("Non nummeric values at line " + DrawAllShapes.line_number);
                        }
                    }
                }
        
                //if user type a circle command this block will get executed
                else if (commands.Equals("circle"))
                {
                    if (isVariable)
                    {
                        if (store_variables.ContainsKey(parameters))
                        {
                            int radius = Convert.ToInt32(store_variables[parameters]);
                            canvas.drawCircle(colour, xAxis, yAxis, fillOn, isPen, radius, g);
                        }

                       
                      
                    }
                    else
                    {
                        try
                        {
                            //converting from string to integer
                            int radius = Convert.ToInt32(parameters);
                            //draws the cricle shape
                            canvas.drawCircle(colour, xAxis, yAxis, fillOn, isPen, radius, g);
                        }
                        catch (FormatException e)
                        {
                            //if Non nummeric values  is entered then error becomes true
                            error = true;
                            //storing errors in arraylist
                            errors.Add("Non nummeric values at line " + DrawAllShapes.line_number);
                        }
                    }

                }            
                //if user type a command which is not valid then this block executes
                else
                {
                    //if invalid command is entered then error becomes true
                    error = true;
                    //storing errors in arraylist
                    errors.Add("Invalid command at line " + DrawAllShapes.line_number);
                }
            }
            catch (IndexOutOfRangeException e)
            {
                //if commands is trianlge then make error to false
                if (commands.Equals("triangle"))
                {
                    error = false;
                }              
                else
                {
                    //if invalid command is entered then error becomes true
                    error = true;
                    //storing errors in arraylist
                    errors.Add("Invalid command at line " + DrawAllShapes.line_number); 
                }
            }
        }

        /// <summary>
        /// If reset command is get executed from the commandline then this method will be called to reset
        /// the position of xAxis and yAxis to 0,0
        /// </summary>
        public void Reset()
        {
            //sets xAxis to zero
            xAxis = 0;
            //sets yAxis to zero
            yAxis = 0;
        }
    }
}
