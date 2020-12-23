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
        public static Boolean error = false;
        public static ArrayList errors = new ArrayList();
        public static bool isvar = false;
        public static Boolean isPen = false;
        Boolean fillOn = false;
        Color pen;
        PenColor colour = new PenColor();
        //  ComplexCommand complex = new ComplexCommand();
        Canvas canvas = new Canvas();
        private int xAxis;
        private int yAxis;
        int count_line;
        

        /// <summary>
        /// It checks for the commands of commandLine and if commands exists then it 
        /// performs the actions based on the codes.
        /// </summary>
        /// <param name="textBox2">CommandLine texts</param>
        /// <param name="richTextBox1">RichTextBox texts</param>
        /// <param name="panel1">Panel</param>
        /// <param name="g">Graphics reference</param>
        public void commandLineCommands(TextBox textBox2,RichTextBox richTextBox1,Panel panel1,Graphics g,RichTextBox richTextBox2,TextBox textBox1,RichTextBox richTextBox3)
        {
            bool end_command = false;
            ComplexCommand complex = new ComplexCommand();
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
                bool complex_command = false;
                int count_line = 0;
                String[] richTextBoxLines = richTextBox1.Text.Trim().ToLower().Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                CommandLine.errors.Clear();
                DrawAllShapes.line_number = 0;
                richTextBox3.Clear();
                //if syntaxCheck button is clicked then setting bollean value to false.
                DrawAllShapes.syntaxCheckerClicked = false;
                //clears the textBox
                textBox2.Clear();
                //Taking all the lines of text from richtextbox
                for (int i = 0; i < richTextBoxLines.Length; i++)
                {
                    DrawAllShapes.line_number++;
                    count_line++;
                    String draw = richTextBoxLines[i];
                    String command_type = check_cmd.check_command_type(draw);

                    if (command_type.Equals("variable") || command_type.Equals("if") || command_type.Equals("end_tag")|| command_type.Equals("while")||command_type.Equals("method")|| command_type.Equals("variableoperation"))
                    {
                        if (command_type.Equals("variable"))
                        {
                            isvar = true;
                            check_cmd.check_variable(draw);
                        }
                        if (command_type.Equals("variableoperation"))
                        {
                            isvar = true;
                            check_cmd.run_variable_operation(draw);
                        }
                        if (command_type.Equals("if"))
                        {
                            try
                            {
                                complex_command = true;
                                if (check_cmd.check_if_command(draw))
                                {
                                    check_cmd.run_if_command(richTextBoxLines, count_line, g);
                                }
                                else
                                {
                                    throw new ErrorInCommandsException("Sorry the condition didn't met at line number " + DrawAllShapes.line_number);
                                }
                            }
                            catch (ErrorInCommandsException e)
                            {
                                CommandLine.error = true;
                                ErrorRepository.errorsList.Add(e.Message);
                            }
                        }

                        if (command_type.Equals("while"))
                        {
                            complex_command = true;
                            if(check_cmd.check_while_command(draw))
                            {
                                check_cmd.run_while_command(draw,richTextBoxLines, count_line, g);
                            }
                        }

                        if (command_type.Equals("method"))
                        {
                            complex_command = true;
                            if (check_cmd.checkMethod(draw, richTextBoxLines, count_line, g))
                            {
                                if (check_cmd.methodcall(richTextBoxLines, count_line,g))
                                {
                                   complex_command = true;
                                   foreach(String lines in CommandChecker.line_of_commands)
                                    {
                                        draw_commands(lines,g);
                                    }
                                }
                            }
                          
                        }
                        if (command_type.Equals("end_tag"))
                        {
                            complex_command = false;
                            foreach (String lines in CommandChecker.line_of_commands)
                            {
                                draw_commands(lines, g);
                            } 
                        }
                    }


                    if (!complex_command)
                    {
                           draw_commands(draw, g);
                    }

                }

                //checker.parseCommands(textBox2.Text, g);
                if (CommandLine.error == true)
                {
                    ErrorRepository errors = new ErrorRepository();
                    for(Iterator iterator = errors.getIterator(); iterator.hasNext();)
                    {
                       String name = (String)iterator.Next();
                        richTextBox3.Text += "\n" + name + "\n";
                    }

                   /* //counts the total number of errors detected
                    int totalerrors = CommandLine.errors.Count;
                    //printing the total number of errors
                    textBox1.Text = totalerrors + " Errors";
                    //showing all the errors in error box
                     for (int i = 0; i < CommandLine.errors.Count; i++)
                     {
                         richTextBox3.Text += "\n" + CommandLine.errors[i] + "\n";
                         //Thread.Sleep(1000);
                     }
                   */
                    
                  
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
                //clears the panel and RichTextBox
                richTextBox1.Clear();
                //clearing panel
                panel1.Refresh();
                textBox2.Clear();
                textBox1.Text = 0 + " Errors";
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
       
        public void draw_commands(String command, Graphics g)
        {
            try { 
                int radius;
                ComplexCommand complex = new ComplexCommand();
                String[] line = command.ToLower().Trim().Split(' ');
                String commands = line[0];
                if (commands.Equals("triangle"))
                {
                    if (line.Length != 1)
                    {
                        error = true;
                        ErrorRepository.errorsList.Add("Invalid parameters at line " + DrawAllShapes.line_number);
                    }
                    else
                    {
                        canvas.drawTriangle(pen, xAxis, yAxis, fillOn, isPen, g);
                    }
                }

                String value = line[1];
                String[] parameter = line[1].Split(',');
                int[] parameters = complex.check_values(parameter);
            //if(commands)
           
                if (commands.Equals("moveto"))
                {
                    if (parameter.Length != 2)
                    {
                        error = true;
                        ErrorRepository.errorsList.Add("Invalid parameters at line " + DrawAllShapes.line_number);
                    }
                    xAxis = parameters[0];
                    yAxis = parameters[1];
                }
                else if (commands.Equals("drawto"))
                {
                    if (parameter.Length != 2)
                    {
                        error = true;
                        ErrorRepository.errorsList.Add("Invalid parameters at line " + DrawAllShapes.line_number);
                    }
                    canvas.drawLine(pen, xAxis, yAxis, isPen, parameters[0], parameters[1], g);
                    xAxis = parameters[0];
                    yAxis = parameters[1];
                }
                else if (commands.Equals("pen"))
                {
                    String check = @"^[a-zA-Z]+$";
                    Regex regex = new Regex(check);
                    if (regex.IsMatch(value))
                    {
                        isPen = true;
                        String pen_color = complex.checkStringVariables(value);
                        pen = colour.getPenColor(pen_color);
                    }
                    else
                    {
                        error = true;
                        ErrorRepository.errorsList.Add("Please add string values in line number " + DrawAllShapes.line_number);
                    }
                }
                else if (commands.Equals("fill"))
                {
                    String check = @"^[a-zA-Z]+$";
                    Regex regex = new Regex(check);
                    if (regex.IsMatch(value))
                    {
                        if (complex.checkStringVariables(value).Equals("on"))
                        {
                            fillOn = true;
                        }
                        else
                        {
                            fillOn = false;
                        }
                    }
                    else
                    {
                        error = true;
                        ErrorRepository.errorsList.Add("Please add string values in line number " + DrawAllShapes.line_number);
                    }
                }
                else if (commands.Equals("rectangle"))
                {
                    if (parameter.Length != 2)
                    {
                        error = true;
                        ErrorRepository.errorsList.Add("Invalid parameters at line " + DrawAllShapes.line_number);
                    }
                    canvas.drawRectangle(pen, xAxis, yAxis, fillOn, isPen, parameters[0], parameters[1], g);
                }
               
                /*else if (commands.Equals("triangle"))
                {
                    MessageBox.Show("triangle");
                    canvas.drawTriangle(Color.Red, xAxis, yAxis, true, true, g);
                }
                */
                else if (commands.Equals("circle"))
                {
                   
                    try
                    {
                        if (CommandChecker.store_variables.ContainsKey(value))
                        {
                            radius = Convert.ToInt32(CommandChecker.store_variables[value]);
                        }
                        else
                        {
                            radius = Convert.ToInt32(value);
                        }
                        canvas.drawCircle(pen, xAxis, yAxis, fillOn, isPen, radius, g);
                    }
                    catch (FormatException)
                    {
                        error = true;
                        ErrorRepository.errorsList.Add("Invalid parameters at line " + DrawAllShapes.line_number);
                    }
                }
                else
                {

                }
            }
            catch (System.IndexOutOfRangeException e)
            {
                if (isvar==false)
                {
                    error = true;
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
