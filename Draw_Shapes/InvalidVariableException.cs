using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draw_Shapes
{
    /// <summary>
    /// This is the user defined exception class which is extended from the base class called Exception.
    /// This exception will be thrown when invalid variables are passed into the commandBox.
    /// </summary>
    public class InvalidVariableException:Exception
    {
        public InvalidVariableException()
        {

        }

        public InvalidVariableException(String message):base(message)
        {
            
        }
    }
}
