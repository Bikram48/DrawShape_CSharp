using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Draw_Shapes
{
    class LoadFile
    {
        public void fileLoading(RichTextBox richTextBox1)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    String line;
                    String filePath = openFileDialog.FileName;
                    StreamReader s = File.OpenText(filePath);
                    CommandChecker obj = new CommandChecker();
                    while ((line = s.ReadLine()) != null)
                    {

                        richTextBox1.Text += line + "\n";
                        // Note, we could have used
                        // textBox1 += s.ReadToEnd();
                    }
                }
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
