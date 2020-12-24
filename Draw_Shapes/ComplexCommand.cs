using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Draw_Shapes
{
    /// <summary>
    /// This class checks if the parameters are variable or simple integer values passed by the user.
    /// If the parameters are variable then the values are passed into the parameter variable from the dictionary.
    /// If there are no variable in parameters then the parameters are simply stored into the index of parameter.
    /// </summary>
    class ComplexCommand
    {
        //stores the parameters into integer array
        int[] parameters = new int[2];
        /// <summary>
        /// checks if the values are variables or simple integer type.
        /// </summary>
        /// <param name="parameter">parameters</param>
        /// <returns></returns>
        public int[] check_values(String[] parameter)
        {
            try
            {
                //stores the first parameter
                String parameter1 = parameter[0];
                //stores the second parameter
                String parameter2 = parameter[1];
                //checks if both parameters are variables and exists in the dictonary as a key 
                if (CommandChecker.store_variables.ContainsKey(parameter1) && CommandChecker.store_variables.ContainsKey(parameter2))
                {
                    //storing the values of variables into the parameters
                    parameters[0] = Convert.ToInt32(CommandChecker.store_variables[parameter1]); ;
                    parameters[1] = Convert.ToInt32(CommandChecker.store_variables[parameter2]);
                }
                //checks if first parameters is variable and exists in the dictonary as a key 
                else if (CommandChecker.store_variables.ContainsKey(parameter1) && !CommandChecker.store_variables.ContainsKey(parameter2))
                {
                    //storing the values of variable into the firstparameter
                    parameters[0] = Convert.ToInt32(CommandChecker.store_variables[parameter1]);
                    parameters[1] = Convert.ToInt32(parameter2);

                }
                //checks if second parameter are variables and exists in the dictonary as a key 
                else if (!CommandChecker.store_variables.ContainsKey(parameter1) && CommandChecker.store_variables.ContainsKey(parameter2))
                {
                    //storing the values of variable into the secondparameter
                    parameters[0] = Convert.ToInt32(parameter1);
                    parameters[1] = Convert.ToInt32(CommandChecker.store_variables[parameter2]);

                }
                //check if both parameters are not variables
                else
                {
                    //stores the values of parameter entered by the user
                    parameters[0] = Convert.ToInt32(parameter1);
                    parameters[1] = Convert.ToInt32(parameter2);
                }
            }
            catch(FormatException e)
            {
                CommandLine.error = true;
                CommandLine.errors.Add("Non nummeric values at line " + DrawAllShapes.line_number);
            }
            catch (System.IndexOutOfRangeException e)
            {

            }

            return parameters;
        }

        /// <summary>
        /// Uses public access modifier.
        /// Checks if the variable stores the string value. 
        /// This method is created mainly for storing the values of pen and fill commands. 
        /// </summary>
        /// <param name="value">values for pen and fill command</param>
        /// <returns></returns>
        public String checkStringVariables(String value)
        {
            String command = null;
            //if the values passed through pen and fill command contains the key in dictionary then this block will get executed
            if (CommandChecker.store_variables.ContainsKey(value))
            {
                //stores the values
                command = CommandChecker.store_variables[value];
            }
            //if the value is not variable then this block will get executed
            else
            {
                //stores the values
                command = value;
            }
            return command;
        }
    }
}
