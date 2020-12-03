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
    /// This class is used to saves the commands entered into the richTextBox by the user in the text based file.
    /// </summary>
    class SaveFile
    {
        /// <summary>
        /// This method helps to save the commands in the textfile.
        /// </summary>
        /// <param name="richTextBox1">RichTextBox</param>
        public void fileSave(RichTextBox richTextBox1)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "*.txt";
            //sets the default extension of the file.
            saveFileDialog.DefaultExt = "txt";
            //sets the filter with txt extensions only
            saveFileDialog.Filter = "txt files (*.txt)|*.txt";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Stream filestream = saveFileDialog.OpenFile();
                StreamWriter sw = new StreamWriter(filestream);
                //writes the commands into the textfile
                sw.Write(richTextBox1.Text);

                sw.Close();
                filestream.Close();
            }
        }
    }
}
