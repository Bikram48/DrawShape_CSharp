using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Draw_Shapes
{
    class SaveFile
    {
        public void fileSave(RichTextBox richTextBox1)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "*.txt";
            saveFileDialog.DefaultExt = "txt";
            saveFileDialog.Filter = "txt files (*.txt)|*.txt";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Stream filestream = saveFileDialog.OpenFile();
                StreamWriter sw = new StreamWriter(filestream);

                sw.Write(richTextBox1.Text);

                sw.Close();
                filestream.Close();
            }
        }
    }
}
