using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Draw_Shapes
{
    class IfCommand
    {
        public String[] commandCheck(String line)
        {
            String[] signs = { "<=", ">=", "<", ">", "==", "!=" };
            String text = line.ToLower().Trim();
            String[] splitter = text.Split(' ');
            String parameters = splitter[1];
           
           String[] splitbysign = parameters.Split(signs,StringSplitOptions.None);
           
            return splitbysign;
        }
    }
}
