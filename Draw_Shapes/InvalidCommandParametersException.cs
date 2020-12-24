using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draw_Shapes
{
    /// <summary>
    /// This is the user defined exception class which is extended from the base class called Exception.
    /// This exception will be thrown when invalid parameters are passed into the commands.
    /// </summary>
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
