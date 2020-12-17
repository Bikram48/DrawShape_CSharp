using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Draw_Shapes
{
    class CheckCommands
    {

        public void checkCommands(String command)
        {
            if (command.Contains("rectangle"))
            {
                MessageBox.Show("rectangle");
            }
        }
    }
}
