using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Draw_Shapes
{
    /// <summary>
    /// This class is used to load the commands from the text file.
    /// </summary>
    class LoadFile
    {
        /// <summary>
        /// THis method loads the files containing commands into the program and it also
        /// read those commands line by line and embed it into the richTextBox.
        /// </summary>
        /// <param name="richTextBox1"></param>
        public void fileLoading(RichTextBox richTextBox1)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    //lines from the textFile.
                    String line;
                    //path of the file
                    String filePath = openFileDialog.FileName;
                    StreamReader s = File.OpenText(filePath);
                    //instantiates an object of CommandChecker class
                    CommandChecker obj = new CommandChecker();
                    //reads the line untill the end of line comes.
                    while ((line = s.ReadLine()) != null)
                    {
                        //embeds the line into the richTextBox.
                        richTextBox1.Text += line + "\n";
                    }
                }   
                //if file doesn't exist then this exception will be thrown
                catch (FileNotFoundException)
                {
                    MessageBox.Show("Error", "Cannot find limerick.txt");
                }
                catch (IOException ie)
                {
                    MessageBox.Show("Error", "IO exception");
                }
            }
        }
    }
}
