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
    /// It takes the commands from the commandLine and performs some actions on it.
    /// </summary>
    class CommandLine
    {
        /// <summary>
        /// Uses a private visibility modifier to give access to this class only.
        /// It's a integer type which will store the values of xAxis from the moveTo command.
        /// </summary>
        private int xAxis;
        /// <summary>
        /// Uses a private visibility modifier to give access to this class only.
        /// It's a integer type which will store the values of yAxis from the moveTo command.
        /// </summary>
        private int yAxis;
        /// <summary>
        /// Uses a public modifier to give access to other classes also.
        /// Declaired as a static variable which will get accessed  through the class name.
        /// It returns the true value if errors are detected if not then it returns false.
        /// </summary>
        public static Boolean error = false;
        /// <summary>
        /// ArrayList is created with static keyword to access this arraylist by a classname.
        /// Stores all the errors throws by the commands or program.
        /// </summary>
        public static ArrayList errors = new ArrayList();
        /// <summary>
        ///Uses a public modifier to give access to other classes also.
        /// Declaired as a static variable which will get accessed by through the class name.
        ///It is a boolean type variable to check if the variable command is get executed or not.
        /// </summary>
        public static bool isvar = false;
        /// <summary>
        /// Uses a public modifier to give access to other classes also.
        /// Declaired as a static variable which will get accessed by through the class name.
        /// It is a boolean type variable to check if the pen command is get executed or not.
        /// It returns true if the pen command is executed if not it returns false.
        /// </summary>
        public static Boolean isPen = false;
        /// <summary>
        /// Local variable with the boolean property which will check if the fill command is get executed or not.
        /// It will return true if the fillOn command is executed if not it will return false.
        /// </summary>
        Boolean fillOn = false;
        /// <summary>
        /// It is a reference variable of the Color class.
        /// It store the pen color entered by the user to draw a shapes.
        /// </summary>
        Color pen;
        /// <summary>
        /// Creates the object of PenColor class
        /// </summary>
        PenColor colour = new PenColor();
        /// <summary>
        /// The object of Canvas class is get instantiated.
        /// Will call the method of Canvas class through the canvas reference variable.
        /// </summary>
        Canvas canvas = new Canvas(); 


        /// <summary>
        /// Uses a public access modifier.
        /// It checks for the commands of commandLine and if commands exists then it 
        /// performs the actions based on the codes.
        /// This method is also used to read the lines of richtextbox.
        /// </summary>
        /// <param name="textBox2">CommandLine texts</param>
        /// <param name="richTextBox1">RichTextBox texts</param>
        /// <param name="panel1">Panel</param>
        /// <param name="g">Graphics reference</param>
        public void commandLineCommands(TextBox textBox2,RichTextBox richTextBox1,Panel panel1,Graphics g,RichTextBox richTextBox2,TextBox textBox1,RichTextBox richTextBox3)
        {
            //check if there are any end tags
            bool end_command = false;
            //Creates the object of ComplexCommand class
            ComplexCommand complex = new ComplexCommand();
            //Creates the object of CommandChecker class
            CommandChecker check_cmd = new CommandChecker();
            //It removes the whitespace from the commands using Trim method
            String run_commands = textBox2.Text.Trim();
            //it transforms the text into small alphabets
            String lower = run_commands.ToLower();
            //Takes the commands from command line and removes the whitespace and also transform letter into small alphabet.
            String singleLineCommand = textBox2.Text.Trim().ToLower();
         
            //If run command entered then this block get executed
            if (singleLineCommand.Equals("run"))
            {
                //checks if complex command like while,if,var are found
                bool complex_command = false;
                //counts the lines of richtextbox
                int count_line = 0;
                //Storing the lines of richtextbox line by line
                String[] richTextBoxLines = richTextBox1.Text.Trim().ToLower().Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                //clearing the arraylist
                CommandLine.errors.Clear();
                //resetting the line number to zero
                DrawAllShapes.line_number = 0;
                //clears the richtextbox
                richTextBox3.Clear();
                //if syntaxCheck button is clicked then setting bollean value to false.
                DrawAllShapes.syntaxCheckerClicked = false;
                //clears the textBox
                textBox2.Clear();
                //Looping on to the richtextbox lines
                for (int i = 0; i < richTextBoxLines.Length; i++)
                {
                    //incrementing the line number value
                    DrawAllShapes.line_number++;
                    //incrementing count line variable
                    count_line++;
                    //storing lines into the draw variable
                    String draw = richTextBoxLines[i];
                    //checks the command type by passing a line
                    String command_type = check_cmd.CheckCommandTypes(draw);
                    //if command_type returns value matches these commands then this block get executed
                    if (command_type.Equals("variable") || command_type.Equals("if") || command_type.Equals("end_tag")|| command_type.Equals("while")||command_type.Equals("method")|| command_type.Equals("variableoperation"))
                    {
                        //if commandtype is equals to variable then this block get executed
                        if (command_type.Equals("variable"))
                        {
                            //if command is variable making isvar to true
                            isvar = true;
                            //passing the line containing variable 
                            check_cmd.CheckVariables(draw);
                        }
                        //if commandtype is equals to variable then this block get executed
                        if (command_type.Equals("variableoperation"))
                        {
                            //if command is variableoperation making isvar to true
                            isvar = true;
                            //passing the line containing variableoperation 
                            check_cmd.RunVariableOperations(draw);
                        }
                        //if commandtype is equals to variable then this block get executed
                        if (command_type.Equals("if"))
                        {
                            try
                            {
                                //if command is if then make complex command true
                                complex_command = true;
                                //if condition is meet in if command
                                if (check_cmd.CheckIfCondition(draw))
                                {
                                    //runs the lines inside the if and endif
                                    check_cmd.RunIfCommand(richTextBoxLines, count_line, g);
                                }
                                //if condition doesn't meet
                                else
                                {
                                    //throws the exception
                                    throw new ErrorInCommandsException("Sorry the condition didn't met at line number " + DrawAllShapes.line_number);
                                }
                            }
                            //catches the thrown exception
                            catch (ErrorInCommandsException e)
                            {
                                //makes error is true
                                CommandLine.error = true;
                                //adds errors into the arraylist
                                ErrorRepository.errorsList.Add(e.Message);
                            }
                        }
                        //if commandtype is equals to while then this block get executed
                        if (command_type.Equals("while"))
                        {
                            //if command is while then make complex command true
                            complex_command = true;
                            //if condition is meet in while command
                            if (check_cmd.CheckWhileCondition(draw))
                            {
                                //runs the lines inside the while and endloop
                                check_cmd.RunWhileCommand(draw,richTextBoxLines, count_line, g);
                            }
                        }

                        //if commandtype is equals to variable then this block get executed
                        if (command_type.Equals("method"))
                        {
                            //if command is method then make complex command true
                            complex_command = true;
                            //checks if the valid method is declaired
                            if (check_cmd.RunMethod(draw, richTextBoxLines, count_line, g))
                            {
                                //checks if the method is called
                                if (check_cmd.methodcall(richTextBoxLines, count_line,g))
                                {
                                    //makes complex command to true
                                   complex_command = true;
                                    //retrieving the lines of commands from the arraylist
                                   foreach(String lines in CommandChecker.line_of_commands)
                                    {
                                        //passing commands into the drawcommands method
                                        draw_commands(lines,g);
                                    }
                                }
                            }
                          
                        }
                        //if commandtype is equals to variable then this block get executed
                        if (command_type.Equals("end_tag"))
                        {
                            //if command is end_tag then make complex command true
                            complex_command = false;
                            //retrieving the lines of commands from the arraylist
                            foreach (String lines in CommandChecker.line_of_commands)
                            {
                                //passing commands into the drawcommands method
                                draw_commands(lines, g);
                            } 
                        }
                    }

                    //if there are no any complex commands then pass simple commands into draw commands method 
                    if (!complex_command)
                    {
                        draw_commands(draw, g);
                    }

                }

                //if any errors found then this block get executed
                if (CommandLine.error == true)
                {
                    //Creating the object of ErrorRepository class
                    ErrorRepository errors = new ErrorRepository();
                    //iterating the commands
                    for(Iterator iterator = errors.getIterator(); iterator.hasNext();)
                    {
                       String name = (String)iterator.Next();
                        //showing errors into the error box
                        richTextBox3.Text += "\n" + name + "\n";
                    }
                }
                //if there are no errors detected then this block will get executed
                if (CommandLine.error == false)
                {
                    //set total error to zero
                    int totalerrors = CommandLine.errors.Count;
                    //showing zero errors in textbox
                    textBox1.Text = totalerrors + " Errors";
                    //shows no errors found message in richtextbox
                    richTextBox3.Text += "\nNo Errors Found";
                }
            }
            //If clear command entered then this block get executed
            else if (singleLineCommand.Equals("clear"))
            {
                CommandChecker.line_of_commands.Clear();
                //clears the panel and RichTextBox
                richTextBox1.Clear();
                //clearing panel
                panel1.Refresh();
                //clears the textbox
                textBox2.Clear();
                //sets the textbox 1 text to 0 errors if no errors found
                textBox1.Text = 0 + " Errors";
                //clears the richtextbox
                richTextBox3.Clear();
            }
            //if reset command entered then this block get executed
            else if (singleLineCommand.Equals("reset"))
            {
                //resets the X and Y co-ordinates to 0,0 using reset method from CommandChecker class.
                Reset();
            }
            //if commands are entered in a single commandline box then this code get executed
            else
            {
                //passed the text of TextBox into the parseCommands method to check the commands are valid or invalid
                draw_commands(textBox2.Text, g);
            }

          
        }

        /// <summary>
        /// Use public access modifier.
        /// This method is run when only commandtype returns the drawing Commands type.
        /// It is responsible to get the required parameters to draw the shapes.
        /// If valid commands are entered for the shape then it passes the parameters to the canvas class to draw the shapes.
        /// </summary>
        /// <param name="command">Line with the command</param>
        /// <param name="g">Reference of graphics</param>
        /// <exception cref="IndexOutOfRangeException">
        ///     throw when invaid array index is used
        /// </exception>
        /// 
        public void draw_commands(String command, Graphics g)
        {
            try { 
                //stores the radius value for the circle
                int radius;
                //creating the object of complexCommand class
                ComplexCommand complex = new ComplexCommand();
                //splitting the line by spaces
                String[] line = command.ToLower().Trim().Split(' ');
                //gets the command name
                String commands = line[0];
                //if the triangle command is typed then this block of code will get executed.
                if (commands.Equals("triangle"))
                {
                    //if the length of line is not equals  1 then the exception will be thrown
                    if (line.Length != 1)
                    {
                        //Makes error is true
                        error = true;
                        //Adds errors into the arraylist
                        ErrorRepository.errorsList.Add("Invalid parameters at line " + DrawAllShapes.line_number);
                    }
                    else
                    {
                        //calls the method drawTriangle from canvas class to draw a triangle.
                        canvas.drawTriangle(pen, xAxis, yAxis, fillOn, isPen, g);
                    }
                }
                //takes the value 
                String value = line[1];
                //splitting the parameter by comma
                String[] parameter = line[1].Split(',');
                //stores both variable values or simple values passed by the user
                int[] parameters = complex.check_values(parameter);

                //if moveto command is executed then this block will be called
                if (commands.Equals("moveto"))
                {
                    //if the parameter of moveto commands length is not equals to 2 then exception will be throws
                    if (parameter.Length != 2)
                    {
                        //makes error is true
                        error = true;
                        //Adds errors into the arraylist
                        ErrorRepository.errorsList.Add("Invalid parameters at line " + DrawAllShapes.line_number);
                    }
                    //stores xAxis
                    xAxis = parameters[0];
                    //stores yAxis
                    yAxis = parameters[1];
                }
                //if user type a drawto command this block will get executed
                else if (commands.Equals("drawto"))
                {
                    //if the parameter of drawto commands length is not equals to 2 then exception will be throws
                    if (parameter.Length != 2)
                    {
                        //makes error is true
                        error = true;
                        //Adds errors into the arraylist
                        ErrorRepository.errorsList.Add("Invalid parameters at line " + DrawAllShapes.line_number);
                    }
                    //passed the parameters required to draw a line
                    canvas.drawLine(pen, xAxis, yAxis, isPen, parameters[0], parameters[1], g);
                    xAxis = parameters[0];
                    yAxis = parameters[1];
                }
                //if user type a pen command this block will get executed
                else if (commands.Equals("pen"))
                {
                    //checks if the parameter of pen command are only letters
                    String check = @"^[a-zA-Z]+$";
                    //matching letters using Regex
                    Regex regex = new Regex(check);
                    //if pattern is matched
                    if (regex.IsMatch(value))
                    {
                        //makes pen is true
                        isPen = true;
                        //check if color is stored in the variable
                        String pen_color = complex.checkStringVariables(value);
                        //gets the color of the pen
                        pen = colour.getPenColor(pen_color);
                    }
                    else
                    {
                        //makes error is true
                        error = true;
                        //Adds errors into the arraylist
                        ErrorRepository.errorsList.Add("Please add string values in line number " + DrawAllShapes.line_number);
                    }
                }
                //if user type a fill command this block will get executed
                else if (commands.Equals("fill"))
                {
                    //checks if the parameter of fill command are only letters
                    String check = @"^[a-zA-Z]+$";
                    //matching letters using Regex
                    Regex regex = new Regex(check);
                    //if pattern is matched
                    if (regex.IsMatch(value))
                    {
                        //if fill parameter is on
                        if (complex.checkStringVariables(value).Equals("on"))
                        {
                            //makes fillOn true
                            fillOn = true;
                        }
                        else
                        {
                            //if not makes fillOn false
                            fillOn = false;
                        }
                    }
                    else
                    {
                        //makes error is true
                        error = true;
                        //Adds errors into the arraylist
                        ErrorRepository.errorsList.Add("Please add string values in line number " + DrawAllShapes.line_number);
                    }
                }
                //if user type a rectangle command this block will get executed
                else if (commands.Equals("rectangle"))
                {
                    if (parameter.Length != 2)
                    {
                        //makes error is true
                        error = true;
                        //Adds errors into the arraylist
                        ErrorRepository.errorsList.Add("Invalid parameters at line " + DrawAllShapes.line_number);
                    }
                    //passing the parameters to draw a  rectangle
                    canvas.drawRectangle(pen, xAxis, yAxis, fillOn, isPen, parameters[0], parameters[1], g);
                }
                //if user type a circle command this block will get executed
                else if (commands.Equals("circle"))
                {
                   
                    try
                    {
                        //checks if the value of radius is stored into the vairable
                        if (CommandChecker.store_variables.ContainsKey(value))
                        {
                            //storing value of radius from vairable
                            radius = Convert.ToInt32(CommandChecker.store_variables[value]);
                        }
                        else
                        {
                            //stores the value of radius
                            radius = Convert.ToInt32(value);
                        }
                        //draws the cricle shape
                        canvas.drawCircle(pen, xAxis, yAxis, fillOn, isPen, radius, g);
                    }
                    catch (FormatException)
                    {
                        //makes error is true
                        error = true;
                        //Adds errors into the arraylist
                        ErrorRepository.errorsList.Add("Invalid parameters at line " + DrawAllShapes.line_number);
                    }
                }
                else
                {

                }
            }
            //catchs the IndexOutOfRangeException exception
            catch (System.IndexOutOfRangeException e)
            {
                if (isvar==false)
                {
                    //makes error is true
                    error = true;
                    //Adds errors into the arraylist
                    ErrorRepository.errorsList.Add("Invalid command at line " + DrawAllShapes.line_number);
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
