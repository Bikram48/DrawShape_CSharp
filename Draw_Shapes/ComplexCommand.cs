using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Draw_Shapes
{
    class ComplexCommand
    {
        int[] parameters = new int[2];
        public int[] check_values(String[] parameter)
        {
            try
            {
                String parameter1 = parameter[0];
                String parameter2 = parameter[1];
                if (CommandChecker.store_variables.ContainsKey(parameter1) && CommandChecker.store_variables.ContainsKey(parameter2))
                {
                    int one = Convert.ToInt32(CommandChecker.store_variables[parameter1]);
                    int two = Convert.ToInt32(CommandChecker.store_variables[parameter2]);
                    parameters[0] = one;
                    parameters[1] = two;
                }
                else if (CommandChecker.store_variables.ContainsKey(parameter1) && !CommandChecker.store_variables.ContainsKey(parameter2))
                {

                    parameters[0] = Convert.ToInt32(CommandChecker.store_variables[parameter1]);
                    parameters[1] = Convert.ToInt32(parameter2);

                }
                else if (!CommandChecker.store_variables.ContainsKey(parameter1) && CommandChecker.store_variables.ContainsKey(parameter2))
                {
                    parameters[0] = Convert.ToInt32(parameter1);
                    parameters[1] = Convert.ToInt32(CommandChecker.store_variables[parameter2]);

                }

                else
                {
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

        public String checkStringVariables(String value)
        {
            String command = null;
            if (CommandChecker.store_variables.ContainsKey(value))
            {
                command = CommandChecker.store_variables[value];
            }
            else
            {
                command = value;
            }
            return command;
        }

        public void run_variables()
        {

        }
    }
}
