using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draw_Shapes
{
    class CheckCommands
    {
        String commands = null;
        public String checkCommands(String command)
        {
            if (command.Contains("rectangle"))
            {
                commands = "rectangle";
            }
            return commands;
        }
    }
}
