using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Draw_Shapes
{
    /// <summary>
    /// This class checks if the user typed a valid commands in the system.
    /// If user typed a valid commands then the commands get executed and 
    /// shows the appropriate output according to the typed commands.
    /// </summary>
    public class CommandChecker
    {
        String[] singleparameter;
        String[] numberofparameters;
        private string method_signature;
        //IDictionary<String, String> method_signature = new Dictionary<String, String>();
        public static String methodName;
        public static ArrayList line_of_commands = new ArrayList();
        ArrayList Errors = new ArrayList();
        public static bool isPen;
        bool expression;
        CommandLine commands = new CommandLine();
        public static IDictionary<String, String> store_variables = new Dictionary<String, String>();
        public String check_command_type(String command)
        {
            String type = null;
            try
            {
                if (command.Contains("if") && !command.Contains("endif"))
                {
                    if (command.Split(' ').Length == 2)
                    {
                        type = "if";
                    }
                    else
                    {
                        type = "error";
                        throw new ErrorInCommandsException("If syntax you entered is not valid");
                    }
                }
                else if (command.Contains("then"))
                {
                    type = "singleif";
                }
                else if (command.Contains("while"))
                {
                    type = "while";
                }
                else if (command.Contains("method"))
                {
                    type = "method";
                }
                else if (command.Contains("drawto") || command.Contains("moveto") || command.Contains("pen") || command.Contains("rectangle") || command.Contains("triangle") || command.Contains("circle") || command.Contains("fill"))
                {
                    type = "drawing_commands";
                }
                else if (command.Contains("endif") || command.Contains("endloop") || command.Contains("endmethod"))
                {
                    type = "end_tag";
                }
                else if (command.Contains("="))
                {
                    if (command.Split('=').Length == 2)
                    {
                        type = "variable";
                    }
                    if (command.Split('=').Length != 2)
                    {
                        type = "error";
                        throw new InvalidVariableException("Invalid variable syntax at line " + DrawAllShapes.line_number);
                    }
                    if (command.Contains("+") || command.Contains("-") || command.Contains("*") || command.Contains("/"))
                    {
                        type = "variableoperation";
                    }
                }
                else
                {
                    type = "error";
                }


          
            }
            catch (InvalidVariableException e)
            {
                CommandLine.error = true;
                CommandLine.errors.Add(e.Message);
            }
            catch(ErrorInCommandsException e)
            {
                CommandLine.error = true;
                CommandLine.errors.Add(e.Message);
            }
            return type;
        }

        public void check_if_command(String command, String[] lines, int count_line, Graphics g)
        {
            DataTable table = new DataTable();
            String one;
            String two;
            String[] signs = { "<=", ">=", "<", ">", "==", "!=" };
            String[] cmd = command.Split(' ').Select(p=>p.Trim()).ToArray();
            String parameters = cmd[1];
            String[] splitbysign = parameters.Split(signs, StringSplitOptions.RemoveEmptyEntries).Select(p=>p.Trim()).ToArray();
            try
            {
                if (!signs.Any(parameters.Contains))
                {
                    throw new ErrorInCommandsException("Invalid operator at if condition line number " + DrawAllShapes.line_number);
                }
                else
                {
                    if (cmd[0].Equals("if"))
                    {
                        if (store_variables.ContainsKey(splitbysign[0]) && store_variables.ContainsKey(splitbysign[1]))
                        {
                            one = store_variables[splitbysign[0]].ToString();
                            two = store_variables[splitbysign[1]].ToString();
                        }
                        else if (store_variables.ContainsKey(splitbysign[0]) && !store_variables.ContainsKey(splitbysign[1]))
                        {

                            one = store_variables[splitbysign[0]].ToString();
                            two = splitbysign[1];

                        }
                        else if (!store_variables.ContainsKey(splitbysign[0]) && store_variables.ContainsKey(splitbysign[1]))
                        {
                            one = splitbysign[0];
                            two = store_variables[splitbysign[1]].ToString();

                        }

                        else
                        {
                            one = splitbysign[0].ToString();
                            two = splitbysign[1].ToString();
                        }

                        String replaced = parameters.Replace(splitbysign[0], one).Replace(splitbysign[1], two);

                        expression = Convert.ToBoolean(table.Compute(replaced, String.Empty));

                        if (expression == true)
                        {
                            if (lines[count_line].Equals("then"))
                            {
                                string command_type = check_command_type(lines[count_line + 1]);
                                if (command_type.Equals("drawing_commands"))
                                {
                                    commands.draw_commands(lines[count_line + 1], g);
                                }

                            }
                            else
                            {
                                for (int i = count_line; i < lines.Length; i++)
                                {
                                    if (!(lines[i].Equals("endif")))
                                    {

                                        string command_types = check_command_type(lines[i]);
                                        if (command_types.Equals("drawing_commands"))
                                        {
                                          
                                            //MessageBox.Show("Drawing commands");
                                            line_of_commands.Add(lines[i]);
                                            //commands.draw_commands(lines[i], g);
                                        }

                                    }

                                  
                                }
                            }
                        }
                        else
                        {
                            throw new ErrorInCommandsException("Sorry the condition didn't met at line number " + DrawAllShapes.line_number);
                        }
                    }
                    else
                    {
                        throw new ErrorInCommandsException("Invalid command at line " + DrawAllShapes.line_number);
                    }
                }
            }
            catch(ErrorInCommandsException e)
            {
                CommandLine.error = true;
                ErrorRepository.errorsList.Add(e.Message);
            }
            catch(IndexOutOfRangeException e)
            {

            }

        }

        public void check_while_command(string command, String[] lines, int count_line, Graphics g)
        {
          
            DataTable table = new DataTable();
            String one;
            String two;
            String[] signs = { "<=", ">=", "<", ">", "==", "!=" };
            String[] splitter = command.Split(' ');
            String parameters = splitter[1];
            String[] splitbysign = parameters.Split(signs, StringSplitOptions.RemoveEmptyEntries).Select(p => p.Trim()).ToArray();
            try
            {
                if (!signs.Any(parameters.Contains))
                {
                    throw new ErrorInCommandsException("Invalid operator at if condition line number " + DrawAllShapes.line_number);
                }
                else
                {
                    if (splitter[0].Equals("while"))
                    {
                        if (store_variables.ContainsKey(splitbysign[0]) && store_variables.ContainsKey(splitbysign[1]))
                        {
                            one = store_variables[splitbysign[0]].ToString();
                            two = store_variables[splitbysign[1]].ToString();
                        }
                        else if (store_variables.ContainsKey(splitbysign[0]) && !store_variables.ContainsKey(splitbysign[1]))
                        {
                            one = store_variables[splitbysign[0]].ToString();
                            two = splitbysign[1];
                        }
                        else if (!store_variables.ContainsKey(splitbysign[0]) && store_variables.ContainsKey(splitbysign[1]))
                        {
                            one = splitbysign[0];
                            two = store_variables[splitbysign[1]].ToString();

                        }

                        else
                        {
                            one = splitbysign[0].ToString();
                            two = splitbysign[1].ToString();
                        }
                        String replaced = parameters.Replace(splitbysign[0], one).Replace(splitbysign[1], two);
                        expression = Convert.ToBoolean(table.Compute(replaced, String.Empty));

                        if (expression==true)
                        {
                            int loop = 0;
                            int loop_count = 0;

                            if (store_variables.ContainsKey(splitbysign[0]))
                            {
                                loop = Convert.ToInt32(store_variables[splitbysign[0]]);
                                loop_count = Convert.ToInt32(splitbysign[1]);
                            }

                            for (int count = loop; count <= loop_count; count++)
                            {
                                for (int i = count_line; i < lines.Length; i++)
                                {
                                    // MessageBox.Show(loop + "");
                                    if (!(lines[i].Equals("endloop")))
                                    {

                                        string command_types = check_command_type(lines[i]);
                                        if (command_types.Equals("drawing_commands"))
                                        {
                                            commands.draw_commands(lines[i], g);
                                        }

                                        if (command_types.Equals("variableoperation"))
                                        {
                                            run_variable_operation(lines[i]);
                                        }
                                        
                                    }
                                    else
                                    {
                                        
                                        break;
                                    }

                                }
                                count = Convert.ToInt32(store_variables[splitbysign[0]]);
                            }
                        }
                        else
                        {
                    
                            throw new ErrorInCommandsException("Sorry the condition didn't met at line number " + DrawAllShapes.line_number);
                        }
                    }
                    else
                    {
                        throw new ErrorInCommandsException("Invalid command at line " + DrawAllShapes.line_number);
                    }
                }
            }
            catch (ErrorInCommandsException e)
            {
                CommandLine.error = true;
                ErrorRepository.errorsList.Add(e.Message);
            }
            catch (IndexOutOfRangeException e)
            {
               
            }

        }


        public void check_variable(string command)
        {
            try
            {
                String one;
                String two;
                String[] cmd = command.Split('=').Select(p => p.Trim()).ToArray();
                String variable_name = cmd[0];
                String variable_value = cmd[1];
                CommandLine.isvar = true;
                if (Regex.IsMatch(variable_value, @"([<>\?\*\\\""/\|!()@%^*+=,$])+"))
                {
                    throw new InvalidVariableException("Invalid operator at line number " + DrawAllShapes.line_number);
                }
                else if (variable_name.Equals(variable_value))
                {
                    throw new InvalidVariableException("Invalid variable value at line number " + DrawAllShapes.line_number);
                }
                else if (variable_value.Equals(""))
                {
                    throw new InvalidVariableException("Please add value of the variable at line number " + DrawAllShapes.line_number);
                }
                else
                {
                    if (!store_variables.ContainsKey(variable_name))
                    {
                        if (store_variables.ContainsKey(variable_value))
                        {
                            variable_value = store_variables[variable_value];
                            store_variables.Add(variable_name, variable_value);
                        }
                        else
                        {
                            if (Regex.IsMatch(variable_value, @"^[a-zA-Z]+$"))
                            {
                                if (!store_variables.ContainsKey(variable_value))
                                {
                                    throw new InvalidVariableException("Please declare the variable first. Error at line " + DrawAllShapes.line_number);
                                }
                            }
                            else
                            {
                                store_variables.Add(variable_name, variable_value);
                            }
                        }
                       
                    }
                }
            }
            catch(InvalidVariableException e)
            {
                CommandLine.error = true;
                ErrorRepository.errorsList.Add(e.Message);
            }

        }

        public void run_variable_operation(String command)
        {
            CommandLine.isvar = true;
            DataTable table = new DataTable();
            String[] operations = { "+", "-", "*", "/" };
            String[] cmd = command.Split('=');
            String variable_name = cmd[0];
            String variable_value = cmd[1];
            String[] splitbysigns = variable_value.Split(operations, StringSplitOptions.RemoveEmptyEntries).Select(p=>p.Trim()).ToArray();
            try
            {
              
                if (operations.Any(variable_value.Contains))
                {
                    if (command.Contains("+"))
                    {
                        if (store_variables.ContainsKey(splitbysigns[0]) && !store_variables.ContainsKey(splitbysigns[1]))
                        {
                            int one = Convert.ToInt32(store_variables[splitbysigns[0]]);
                            int two = Convert.ToInt32(splitbysigns[1]);
                            int total = one + two;
                            store_variables.Remove(splitbysigns[0]);
                            store_variables.Add(variable_name, total.ToString());
                        }
                        if (!store_variables.ContainsKey(splitbysigns[0]) && store_variables.ContainsKey(splitbysigns[1]))
                        {
                            int total = Convert.ToInt32(splitbysigns[0]) + Convert.ToInt32(store_variables[splitbysigns[1]]);
                            store_variables.Remove(splitbysigns[0]);
                            store_variables.Add(variable_name, total.ToString());
                        }
                    }
                    else if (command.Contains("-"))
                    {
                        if (store_variables.ContainsKey(splitbysigns[0]) && !store_variables.ContainsKey(splitbysigns[1]))
                        {
                            int total = Convert.ToInt32(store_variables[splitbysigns[0]]) - Convert.ToInt32(splitbysigns[1]);
                            store_variables.Remove(splitbysigns[0]);
                            store_variables.Add(variable_name, total.ToString());
                        }
                        if (!store_variables.ContainsKey(splitbysigns[0]) && store_variables.ContainsKey(splitbysigns[1]))
                        {
                            int total = Convert.ToInt32(splitbysigns[0]) - Convert.ToInt32(store_variables[splitbysigns[1]]);
                            store_variables.Remove(splitbysigns[0]);
                            store_variables.Add(variable_name, total.ToString());
                        }
                    }
                    else if (command.Contains("*"))
                    {
                        if (store_variables.ContainsKey(splitbysigns[0]) && !store_variables.ContainsKey(splitbysigns[1]))
                        {
                            int total = Convert.ToInt32(store_variables[splitbysigns[0]]) * Convert.ToInt32(splitbysigns[1]);
                            store_variables.Remove(splitbysigns[0]);
                            store_variables.Add(variable_name, total.ToString());
                        }
                        if (!store_variables.ContainsKey(splitbysigns[0]) && store_variables.ContainsKey(splitbysigns[1]))
                        {
                            int total = Convert.ToInt32(splitbysigns[0]) * Convert.ToInt32(store_variables[splitbysigns[1]]);
                            store_variables.Remove(splitbysigns[0]);
                            store_variables.Add(variable_name, total.ToString());
                        }
                    }
                    else if (command.Contains("/"))
                    {
                        if (store_variables.ContainsKey(splitbysigns[0]) && !store_variables.ContainsKey(splitbysigns[1]))
                        {
                            int total = (Convert.ToInt32(store_variables[splitbysigns[0]])) / (Convert.ToInt32(splitbysigns[1]));
                            store_variables.Remove(splitbysigns[0]);
                            store_variables.Add(variable_name, total.ToString());
                        }
                        if (!store_variables.ContainsKey(splitbysigns[0]) && store_variables.ContainsKey(splitbysigns[1]))
                        {
                            int total = Convert.ToInt32(splitbysigns[0]) / Convert.ToInt32(store_variables[splitbysigns[1]]);
                            store_variables.Remove(splitbysigns[0]);
                            store_variables.Add(variable_name, total.ToString());
                        }
                    }
                }
                else
                {
                    throw new InvalidVariableException("Invalid operator at line " + DrawAllShapes.line_number);
                }
                if (!store_variables.ContainsKey(variable_name))
                {
                    store_variables.Add(variable_name, variable_value);
                }
                
            }
            catch(InvalidVariableException e)
            {
                CommandLine.error = true;
                ErrorRepository.errorsList.Add(e.Message);
            }

        }

        public bool checkMethod(String command, String[] lines, int count_line, Graphics g)
        {
            char[] brackets = { '(', ')' };
            string method_signature_without_parameter = @"([a-z]\s)([a-z]\w{3,10}\S)([(][)])";
            Regex regex = new Regex(method_signature_without_parameter);
            if (regex.IsMatch(command))
            {
                String[] splitter = command.Split(' ');
                String[] parameter = splitter[1].Split(brackets, StringSplitOptions.RemoveEmptyEntries).Select(p=>p.Trim()).ToArray();
                methodName = parameter[0];

                if (splitter[0].Equals("method"))
                {

                    for (int i = count_line; i < lines.Length; i++)
                    {
                        if (!lines[i].Equals("endmethod"))
                        {
                            string command_types = check_command_type(lines[i]);
                            if (command_types.Equals("drawing_commands"))
                            {
                                line_of_commands.Add(lines[i]);
                            }

                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                return true;
            }
            else
            {
                String[] splitter = command.Split(' ');
                if (splitter[0].Equals("method"))
                {
                    singleparameter = splitter[1].Split(new String[] { "(", ")" }, StringSplitOptions.RemoveEmptyEntries).Select(p=>p.Trim()).ToArray();
                    String[] parameter = splitter[1].Split('(');
                    method_signature = parameter[0];
                    string methodPlaceholder = "method";
                    string parametersPlaceholder = "parameters";
                    string pattern = string.Format(@"^(?<{0}>\w+)(\((?<{1}>[^)]*)\))?$", methodPlaceholder, parametersPlaceholder);
                    Regex check = new Regex(pattern, RegexOptions.Compiled);
                    if (check.IsMatch(splitter[1]))
                    {
                        methodName = parameter[0];
                        if (command.Contains(','))
                        {

                            numberofparameters = parameter[1].Split(new string[] { ",", ")" }, StringSplitOptions.RemoveEmptyEntries);

                            foreach (String parameters in numberofparameters)
                            {
                                parameterizedMethod(parameters);
                            }
                            for (int i = count_line; i < lines.Length; i++)
                            {
                                if (!lines[i].Equals("endmethod"))
                                {
                                    string command_types = check_command_type(lines[i]);
                                    if (command_types.Equals("drawing_commands"))
                                    {
                                        line_of_commands.Add(lines[i]);
                                    }

                                }
                                else
                                {
                                    continue;
                                }
                            }

                        }
                        else if (singleparameter[1].Length == 1)
                        {
                            parameterizedMethod(singleparameter[1]);
                            for (int i = count_line; i < lines.Length; i++)
                            {
                                if (!lines[i].Equals("endmethod"))
                                {
                                    string command_types = check_command_type(lines[i]);
                                    if (command_types.Equals("drawing_commands"))
                                    {
                                        line_of_commands.Add(lines[i]);
                                    }

                                }
                                else
                                {
                                    continue;
                                }
                            }

                        }

                        return true;
                    }
                }
            }

            return false;

        }

        public void parameterizedMethod(params String[] parameters)
        {
            for (int i = 0; i < parameters.Length; i++)
            {
                store_variables.Add(parameters[i], null);

            }
        }

        public bool methodcall(String[] lines, int count_line, Graphics g)
        {
            string methodPlaceholder = "method";
            string parametersPlaceholder = "parameters";
            string pattern = string.Format(@"^(?<{0}>\w+)(\((?<{1}>[^)]*)\))?$", methodPlaceholder, parametersPlaceholder);
            Regex check = new Regex(pattern, RegexOptions.Compiled);
            char[] brackets = { '(', ')' };
            string syntax = @"([a-z]\w{3,10}\S)([(][)])";
            Regex regex = new Regex(syntax);
            lines[count_line - 1] = "";

            for (int i = 0; i < lines.Length; i++)
            {
                //String command_type = check_command_type(lines[i]);
                if (regex.IsMatch(lines[i]))
                {
                    if (lines[i].Contains(methodName + "()"))
                    {
                        return true;
                    }

                }
                else if (check.IsMatch(lines[i]))
                {
                    if (lines[i].Contains(","))
                    {
                        String[] splitters = lines[i].Split('(');
                        String[] splitter = splitters[1].Split(new String[] { "(", ",", ")" }, StringSplitOptions.RemoveEmptyEntries);
                        for (int index = 0; index < splitter.Length; index++)
                        {
                            store_variables[numberofparameters[index]] = splitter[index];
                            if (store_variables.ContainsKey(numberofparameters[index]))
                            {
                                numberofparameters[index] = store_variables[numberofparameters[index]];
                            }
                        }
                        return true;
                    }

                    if (lines[i].Contains(methodName))
                    {
                        String[] splitter = lines[i].Split('(');
                        String[] parameter = splitter[1].Split(new String[] { ")" }, StringSplitOptions.RemoveEmptyEntries);
                        // String[] parameter = lines[i].Split(new String[] { "(", ")" }, StringSplitOptions.RemoveEmptyEntries);

                        if (parameter.Length == 1)
                        {

                            store_variables[singleparameter[1]] = parameter[0];
                            if (store_variables.ContainsKey(singleparameter[1]))
                            {
                                singleparameter[1] = store_variables[singleparameter[1]];
                            }
                            return true;
                        }
                    }
                }

            }

            return false;
        }
    }
}
