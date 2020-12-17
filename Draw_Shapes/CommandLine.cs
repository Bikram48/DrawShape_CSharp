using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Draw_Shapes
{
    /// <summary>
    /// It takes the commands from the commandLine and performs some actions on it.
    /// </summary>
    class CommandLine
    {
        bool isvar = false;
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

                //if syntaxCheck button is clicked then setting bollean value to false.
                DrawAllShapes.syntaxCheckerClicked = false;
                //clears the textBox
                textBox2.Clear();
                //Taking all the lines of text from richtextbox
                for (int i = 0; i < richTextBoxLines.Length; i++)
                {
                    count_line++;
                    String draw = richTextBoxLines[i];
                    String command_type = check_cmd.check_command_type(draw);
                    if (command_type.Equals("end_tag"))
                    {
                        complex_command = false;
                    }
                    if (command_type.Equals("endloop"))
                    {
                        complex_command = false;
                    }
                    if (command_type.Equals("variable") || command_type.Equals("if") || command_type.Equals("while"))
                    {
                        if (command_type.Equals("variable"))
                        {

                            check_cmd.check_variable(draw);


                        }
                        if (command_type.Equals("if"))
                        {
                            complex_command = true;
                            check_cmd.check_if_command(draw, richTextBoxLines, count_line, g);
                        }

                        if (command_type.Equals("while"))
                        {

                            complex_command = true;
                            check_cmd.check_while_command(draw, richTextBoxLines, count_line, g);

                        }

                        if (command_type.Equals("method"))
                        {
                            complex_command = true;
                        }
                    }

                    if (!complex_command)
                    {
                        draw_commands(draw, g);
                    }

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
                //checker.Reset();
            }
            //if commands are entered in a single commandline box then this code get executed
            else
            {
                //passed the text of TextBox into the parseCommands method to check the commands are valid or invalid
                //checker.parseCommands(textBox2.Text, g);
            }

          
        }
       
        public void draw_commands(String command, Graphics g)
        {
            try
            {
                int radius;
                ComplexCommand complex = new ComplexCommand();
                String[] line = command.ToLower().Trim().Split(' ');
                String commands = line[0];
                if (commands.Equals("triangle"))
                {
                    canvas.drawTriangle(pen, xAxis, yAxis, fillOn, isPen, g);
                }
                String value = line[1];
                String[] parameter = line[1].Split(',');
                int[] parameters = complex.check_values(parameter);
                //if(commands)

                if (commands.Equals("moveto"))
                {
                    xAxis = parameters[0];
                    yAxis = parameters[1];
                }
                else if (commands.Equals("drawto"))
                {
                    canvas.drawLine(pen, xAxis, yAxis, isPen, parameters[0], parameters[1], g);
                    xAxis = parameters[0];
                    yAxis = parameters[1];
                }
                else if (commands.Equals("pen"))
                {
                    isPen = true;
                    String pen_color = complex.checkStringVariables(value);
                    pen = colour.getPenColor(pen_color);
                }
                else if (commands.Equals("fill"))
                {
                    if (complex.checkStringVariables(value).Equals("on"))
                    {
                        fillOn = true;
                    }
                }
                else if (commands.Equals("rectangle"))
                {
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
                else
                {

                }
            }
            catch (System.IndexOutOfRangeException e)
            {

            }

        }
    }
}
