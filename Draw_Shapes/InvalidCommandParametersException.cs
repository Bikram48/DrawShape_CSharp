using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draw_Shapes
{
    class InvalidCommandParametersException:Exception
    {
        public InvalidCommandParametersException()
        {

        }

        public InvalidCommandParametersException(String message) : base(message)
        {
            
        }
    }
}
