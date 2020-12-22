using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draw_Shapes
{
    public class ErrorInCommandsException:Exception
    {
        public ErrorInCommandsException()
        {

        }

        public ErrorInCommandsException(String message) : base(message)
        {

        }
    }
}
