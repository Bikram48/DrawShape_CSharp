using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draw_Shapes
{
    /// <summary>
    /// This class is created for the component2. THe methods in this class are not implemeneted now but
    /// will be implemented in componenet2.
    /// </summary>
    public class VariableChecker
    {
       
        /// <summary>
        /// Uses public access modifier to give access to other classes also.
        /// It reads all the variables passed by the user.
        /// It is unimplemented now but wll be later implemented on component2.
        /// </summary>
        /// <param name="variables"></param>
        /// <returns></returns>
        public String readAllVairables(String variables)
        {
            return variables;
        }

        /// <summary>
        /// Uses public access modifier to give access to other classes also.
        /// It checks all the variables passed by the user.
        /// It is unimplemented now but wll be later implemented on component2.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public String checkAllVariables(String line)
        {
            String txt = line.ToLower().Trim();
            String[] splitter = txt.Split(' ');
            String variables = splitter[1];
            String[] split_parameter = variables.Split('=');
            String variable_name = split_parameter[0];
            String variable_value = split_parameter[1];
            return null;
        }
    }
}
