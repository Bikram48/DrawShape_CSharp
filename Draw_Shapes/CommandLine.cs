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

        public static int lineNumber;
        VariableChecker variable_check = new VariableChecker();
        /// <summary>
        /// Instantiates an object of CommandChecker class.
        /// </summary>
        CommandChecker checker = new CommandChecker();
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
            //It removes the whitespace from the commands using Trim method
            String run_commands = textBox2.Text.Trim();
            //it transforms the text into small alphabets
            String lower = run_commands.ToLower();
            //reads the lines of text from RichTextBox
            string[] RichTextBoxLines = richTextBox1.Lines;
            //Takes the commands from command line and removes the whitespace and also transform letter into small alphabet.
            String singleLineCommand = textBox2.Text.Trim().ToLower();
         
            //If run command entered then this block get executed
            if (singleLineCommand.Equals("run"))
            {
                
                //if syntaxCheck button is clicked then setting bollean value to false.
                DrawAllShapes.syntaxCheckerClicked = false;
                //clears the textBox
                textBox2.Clear();
                //Taking all the lines of text from richtextbox
                foreach (string line in RichTextBoxLines)
                {
                    
                    lineNumber++;
                        //passed the line of text into the parseCommands method to check the commands are valid or invalid
                        checker.parseCommands(line, g);
                    
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
                checker.Reset();
            }
            //if commands are entered in a single commandline box then this code get executed
            else
            {
                //passed the text of TextBox into the parseCommands method to check the commands are valid or invalid
                checker.parseCommands(textBox2.Text, g);
            }
        }
    }
}
