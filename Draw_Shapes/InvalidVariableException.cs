using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draw_Shapes
{
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
