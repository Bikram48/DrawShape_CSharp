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
        CommandChecker checker = new CommandChecker();
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
            //makes boolean value to true if this button is clicked.
            syntaxCheckerClicked = true;
            //clears the error from the arraylist
            CommandChecker.errors.Clear();
            //resets the value of linenumber to zero
            line_number = 0;
            //clears  the richtextbox
            errorBox.Clear();
            //reads the lines of text from RichTextBox
            string[] RichTextBoxLines = multiLineCommandBox.Lines;
            //takes the commands from the singleline command box and it removes the whitespace and transform the letter into small alphabet
            String singleLineCommand = singleLineCommandBox.Text.Trim().ToLower();
            //Taking all the lines of text from richtextbox
            foreach (string line in RichTextBoxLines)
            {
                //increments the value of line number when lines are found in richtextbox
                line_number++;           
                //passing the line of text into the parseCommands method to check the commands are valid or invalid
                checker.parseCommands(line, g);
            }
            //if textbox is not empty then this block will get executed
            if (!singleLineCommandBox.Text.Equals(""))
            {
                line_number++;
                //passing the line of text into the parseCommands method to check the commands are valid or invalid
                checker.parseCommands(singleLineCommandBox.Text, g);
            }
           
            
            
            //checker.parseCommands(textBox2.Text, g);
            if (CommandChecker.error == true)
            {
                //counts the total number of errors detected
                int totalerrors = CommandChecker.errors.Count;
                //printing the total number of errors
                textBox1.Text = totalerrors + " Errors";
                //showing all the errors in error box
                for (int i = 0; i < CommandChecker.errors.Count; i++)
                {
                    errorBox.Text += "\n"+CommandChecker.errors[i]+"\n";
                    //Thread.Sleep(1000);
                }
                
            }
            //if there are no errors detected then this block will get executed
           if(CommandChecker.error==false)
            {
                //set total error to zero
                int totalerrors = CommandChecker.errors.Count;
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
