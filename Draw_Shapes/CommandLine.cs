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
        private static int line;
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
        public void commandLineCommands(TextBox textBox2,RichTextBox richTextBox1,Panel panel1,Graphics g,RichTextBox richTextBox2,TextBox textBox1)
        {
            String run_commands = textBox2.Text.Trim();
            String lower = run_commands.ToLower();
            //reads the lines of text from RichTextBox
            string[] RichTextBoxLines = richTextBox1.Lines;
            String singleLineCommand = textBox2.Text.Trim().ToLower();

            //If run command entered then this block get executed
            if (singleLineCommand.Equals("run"))
            {
                textBox2.Clear();
                DrawAllShapes.syntaxCheckerClicked = false;
                foreach (string line in RichTextBoxLines)
                {
                  
                    //passed the line of text into the parseCommands method to check the commands are valid or invalid
                    checker.parseCommands(line, g);
                }

            }
            //If clear command entered then this block get executed
            else if (singleLineCommand.Equals("clear"))
            {
                //clears the panel and RichTextBox
                richTextBox1.Clear();
                panel1.Refresh();
            }
            else if (singleLineCommand.Equals("reset"))
            {
                checker.Reset();
            }
            else
            {
              
                //passed the text of TextBox into the parseCommands method to check the commands are valid or invalid
                checker.parseCommands(textBox2.Text, g);
               
            }
        }
    }
}
