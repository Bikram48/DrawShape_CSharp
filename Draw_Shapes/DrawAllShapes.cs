using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Draw_Shapes
{
    /// <summary>
    /// This class contains all the gui parts requires for the frontend design.
    /// This class is extended from the Form class to inherits all the properties and methods from base class.
    /// </summary>
    public partial class DrawAllShapes : Form
    {
        /// <summary>
        //Reference of the Graphics class which will be used to create a graphical images in canvas.
        /// </summary>
        Graphics g;
        /// <summary>
        /// Static variable is created to stores the line number on which the error is generated.
        /// </summary>
        public static int line_number;
        /// <summary>
        /// Checks if syntax Check button is clicked.
        /// </summary>
        public static bool syntaxCheckerClicked = false;
        /// <summary>
        /// Instantiating an object of CommandChekcer class
        /// /// The methods and properties of CommandChecker will be called by using the reference variable 
        /// of this CommandChecker class.
        /// </summary>

        /// <summary>
        /// Instantiating an object of CommandLine class.
        /// The methods and properties of CommandLine will be called by using the reference variable 
        /// of this CommandLine class.
        /// </summary>
        CommandLine commands = new CommandLine();

        /// <summary>
        /// Constructor of this class.
        /// </summary>
        public DrawAllShapes()
        {
            InitializeComponent();
            g = canvasBox.CreateGraphics();
            errorBox.Text += "\nNo errors found";
        }
       
        /// <summary>
        /// This event will occurs when a key is pressed inside the textBox while the control has focus.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            //if enter key is entered this block will get executed
            if (e.KeyCode == Keys.Enter)
            {
                //sends the parameter to the CommandLine class to get the commands and parameters from the respective textBox and richTextBox.
                commands.commandLineCommands(singleLineCommandBox, multiLineCommandBox, canvasBox, g,errorBox,textBox1,errorBox); 
            }
        }

        /// <summary>
        /// When user clicks on the openfile menu then this event will occured.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //creates an object of LoadFile class
            LoadFile file = new LoadFile();
            //passing richTextBox into the fileLoading to load the commands from file into richTextBox
            file.fileLoading(multiLineCommandBox);
        }

        /// <summary>
        /// When user clicks on the savefile menu then this event will occured.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //creates an object of SaveFile class
            SaveFile savefile = new SaveFile();
            //passing richTextBox into the fileSave method to save the commands of richTextBox into textFile.
            savefile.fileSave(multiLineCommandBox);
        }

      

       
        /// <summary>
        /// When user clicks a syntax check button to validate the commands then this event will occur.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            CommandChecker check_cmd = new CommandChecker();
            CommandChecker.store_variables.Clear();
            //checks if complex command like while,if,var are found
            bool complex_command = false;
            //counts the lines of richtextbox
            int count_line = 0;
            //Storing the lines of richtextbox line by line
            String[] richTextBoxLines = multiLineCommandBox.Text.Trim().ToLower().Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            //clearing the arraylist
            ErrorRepository.errorsList.Clear();
            //resetting the line number to zero
            DrawAllShapes.line_number = 0;
            //clears the richtextbox
            errorBox.Clear();
            //if syntaxCheck button is clicked then setting bollean value to false.
            syntaxCheckerClicked = true;
            //clears the textBox
            singleLineCommandBox.Clear();
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
                if (command_type.Equals("variable") || command_type.Equals("if") || command_type.Equals("end_tag") || command_type.Equals("while") || command_type.Equals("method") || command_type.Equals("variableoperation"))
                {
                    //if commandtype is equals to variable then this block get executed
                    if (command_type.Equals("variable"))
                    {
                        //if command is variable making isvar to true
                        CommandLine.isvar = true;
                        //passing the line containing variable 
                        check_cmd.CheckVariables(draw);
                    }
                    //if commandtype is equals to variable then this block get executed
                    if (command_type.Equals("variableoperation"))
                    {
                        //if command is variableoperation making isvar to true
                        CommandLine.isvar = true;
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
                                throw new ErrorInCommandsException("Sorry the condition didn't met at line number " + line_number);
                            }
                        }
                        //catches the thrown exception
                        catch (ErrorInCommandsException a)
                        {
                            //makes error is true
                            CommandLine.error = true;
                            //adds errors into the arraylist
                            ErrorRepository.errorsList.Add(a.Message);
                        }
                    }
                    //if commandtype is equals to while then this block get executed
                    if (command_type.Equals("while"))
                    {
                        try
                        {
                            //if command is while then make complex command true
                            complex_command = true;
                            //if condition is meet in while command
                            if (check_cmd.CheckWhileCondition(draw))
                            {
                                //runs the lines inside the while and endloop
                                check_cmd.RunWhileCommand(draw, richTextBoxLines, count_line, g);
                            }
                            else
                            {
                                throw new ErrorInCommandsException("Sorry condition doesn't meet at line " + line_number);
                            }
                        }
                        //catches the thrown exception
                        catch (ErrorInCommandsException a)
                        {
                            //makes error is true
                            CommandLine.error = true;
                            //adds errors into the arraylist
                            ErrorRepository.errorsList.Add(a.Message);
                        }
                    }

                    //if commandtype is equals to variable then this block get executed
                    if (command_type.Equals("method"))
                    {
                        try
                        {
                            //if command is method then make complex command true
                            complex_command = true;
                            //checks if the valid method is declaired
                            if (check_cmd.RunMethod(draw, richTextBoxLines, count_line, g))
                            {
                                //checks if the method is called
                                if (check_cmd.methodcall(richTextBoxLines, count_line, g))
                                {
                                    //makes complex command to true
                                    complex_command = true;
                                    //retrieving the lines of commands from the arraylist
                                    foreach (String lines in CommandChecker.line_of_commands)
                                    {
                                        //passing commands into the drawcommands method
                                        commands.draw_commands(lines, g);
                                    }
                                }
                                else
                                {
                                    throw new ErrorInCommandsException("Please call the method first ");
                                }
                            }
                        }
                        //catches the thrown exception
                        catch (ErrorInCommandsException a)
                        {
                            //makes error is true
                            CommandLine.error = true;
                            //adds errors into the arraylist
                            ErrorRepository.errorsList.Add(a.Message);
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
                            commands.draw_commands(lines, g);
                        }
                    }

                }

                //if there are no any complex commands then pass simple commands into draw commands method 
                if (!complex_command)
                {
                    commands.draw_commands(draw, g);
                }

            }

            //if any errors found then this block get executed
            if (CommandLine.error == true)
            {
                //Creating the object of ErrorRepository class
                ErrorRepository errors = new ErrorRepository();
                //iterating the commands
                for (Iterator iterator = errors.getIterator(); iterator.hasNext();)
                {
                    String name = (String)iterator.Next();
                    //showing errors into the error box
                    errorBox.Text += "\n" + name + "\n";
                }
                //set total error to zero
                int totalerrors = ErrorRepository.errorsList.Count;
                //showing zero errors in textbox
                textBox1.Text = totalerrors + " Errors";
            }
            //if there are no errors detected then this block will get executed
            if (CommandLine.error == false)
            {
                //set total error to zero
                int totalerrors = ErrorRepository.errorsList.Count;
                //showing zero errors in textbox
                textBox1.Text = totalerrors + " Errors";
                //shows no errors found message in richtextbox
                errorBox.Text += "\nNo Errors Found";
            }
            
        
        }

        /// <summary>
        /// If mouse is hovered into the button then this event will occur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_MouseHover(object sender, EventArgs e)
        {
            //changing the background color of button to blue
            syntaxChecker.BackColor =Color.Blue;
        }

        /// <summary>
        /// If mouse leaves from the  button then this event will occur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_MouseLeave(object sender, EventArgs e)
        {
            //changing the background coclor of button to black
            syntaxChecker.BackColor = Color.Black;
        }

        /// <summary>
        /// If user click on the exit button then this event will occur.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //dialog box result appear with some message
            DialogResult dialog = MessageBox.Show("Do you really want to exit?", "Exit", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                //exits the application
                Application.Exit();
            }
           
        }

        
    }
}
