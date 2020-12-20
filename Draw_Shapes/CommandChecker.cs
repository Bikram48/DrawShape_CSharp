﻿using System;
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
            if (command.Contains("if") && !command.Contains("endif"))
            {
                type = "if";
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
            else
            {
                if (command.Contains("="))
                {
                    if (command.Split('=').Length == 2)
                    {
                        type = "variable";
                    }
                    if (command.Contains("+") || command.Contains("-") || command.Contains("*") || command.Contains("/"))
                    {
                        type = "variableoperation";
                    }
                }

            }
            return type;
        }

        public void check_if_command(String command, String[] lines, int count_line, Graphics g)
        {
            DataTable table = new DataTable();
            String one;
            String two;
            String[] signs = { "<=", ">=", "<", ">", "==", "!=" };
            String[] cmd = command.Split(' ');
            String parameters = cmd[1];
            String[] splitbysign = parameters.Split(signs, StringSplitOptions.None);

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
                                    commands.draw_commands(lines[i], g);
                                }
                                else
                                {
                                    break;
                                }

                            }
                        }
                    }
                }
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
            String[] splitbysign = parameters.Split(signs, StringSplitOptions.None);
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

                if (expression)
                {
                    int loop = 0;
                    int loop_count = 0;

                    if (store_variables.ContainsKey(splitbysign[0]))
                    {
                        loop = Convert.ToInt32(store_variables[splitbysign[0]]);
                        loop_count = Convert.ToInt32(splitbysign[1]);
                    }

                    for (int count = loop; count < loop_count; count++)
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

                                else if (command_types.Equals("variableoperation"))
                                {
                                    run_variable_operation(lines[i]);
                                }
                                else
                                {
                                    break;
                                }

                            }
                            else
                            {
                                break;
                            }

                        }
                    }
                }
            }

        }


        public void check_variable(string command)
        {
            String one;
            String two;
            char[] operations = { '+', '-', '*', '/' };
            DataTable table = new DataTable();
            String[] cmd = command.Split('=');
            String variable_name = cmd[0];
            String variable_value = cmd[1];

            if (!store_variables.ContainsKey(variable_name))
            {
                store_variables.Add(variable_name, variable_value);
            }

        }

        public void run_variable_operation(String command)
        {
            DataTable table = new DataTable();
            char[] operations = { '+', '-', '*', '/' };
            String[] cmd = command.Split('=');
            String variable_name = cmd[0];
            String variable_value = cmd[1];
            String[] splitbysigns = variable_value.Split(operations, StringSplitOptions.None);
            if (command.Contains("+"))
            {
                if (store_variables.ContainsKey(splitbysigns[0]) && !store_variables.ContainsKey(splitbysigns[1]))
                {
                    int one = Convert.ToInt32(store_variables[splitbysigns[0]]);
                    int two = Convert.ToInt32(splitbysigns[1]);
                    int total = one + two;
                    store_variables.Remove(splitbysigns[0]);
                    store_variables.Add(splitbysigns[0], total.ToString());
                }
                if (!store_variables.ContainsKey(splitbysigns[0]) && store_variables.ContainsKey(splitbysigns[1]))
                {
                    int total = Convert.ToInt32(splitbysigns[0]) + Convert.ToInt32(store_variables[splitbysigns[1]]);
                    store_variables.Remove(splitbysigns[0]);
                    store_variables.Add(splitbysigns[0], total.ToString());
                }
            }
            else if (command.Contains("-"))
            {
                if (store_variables.ContainsKey(splitbysigns[0]) && !store_variables.ContainsKey(splitbysigns[1]))
                {
                    int total = Convert.ToInt32(store_variables[splitbysigns[0]]) - Convert.ToInt32(splitbysigns[1]);
                    store_variables.Remove(splitbysigns[0]);
                    store_variables.Add(splitbysigns[0], total.ToString());
                }
                if (!store_variables.ContainsKey(splitbysigns[0]) && store_variables.ContainsKey(splitbysigns[1]))
                {
                    int total = Convert.ToInt32(splitbysigns[0]) - Convert.ToInt32(store_variables[splitbysigns[1]]);
                    store_variables.Remove(splitbysigns[0]);
                    store_variables.Add(splitbysigns[0], total.ToString());
                }
            }
            else if (command.Contains("*"))
            {
                if (store_variables.ContainsKey(splitbysigns[0]) && !store_variables.ContainsKey(splitbysigns[1]))
                {
                    int total = Convert.ToInt32(store_variables[splitbysigns[0]]) * Convert.ToInt32(splitbysigns[1]);
                    store_variables.Remove(splitbysigns[0]);
                    store_variables.Add(splitbysigns[0], total.ToString());
                }
                if (!store_variables.ContainsKey(splitbysigns[0]) && store_variables.ContainsKey(splitbysigns[1]))
                {
                    int total = Convert.ToInt32(splitbysigns[0]) * Convert.ToInt32(store_variables[splitbysigns[1]]);
                    store_variables.Remove(splitbysigns[0]);
                    store_variables.Add(splitbysigns[0], total.ToString());
                }
            }
            else if (command.Contains("/"))
            {
                if (store_variables.ContainsKey(splitbysigns[0]) && !store_variables.ContainsKey(splitbysigns[1]))
                {
                    int total = (Convert.ToInt32(store_variables[splitbysigns[0]])) / (Convert.ToInt32(splitbysigns[1]));
                    store_variables.Remove(splitbysigns[0]);
                    store_variables.Add(splitbysigns[0], total.ToString());
                }
                if (!store_variables.ContainsKey(splitbysigns[0]) && store_variables.ContainsKey(splitbysigns[1]))
                {
                    int total = Convert.ToInt32(splitbysigns[0]) / Convert.ToInt32(store_variables[splitbysigns[1]]);
                    store_variables.Remove(splitbysigns[0]);
                    store_variables.Add(splitbysigns[0], total.ToString());
                }
            }
            // String values = table.Compute(variable_value, String.Empty).ToString();
            //MessageBox.Show(values);
            if (!store_variables.ContainsKey(variable_name))
            {
                store_variables.Add(variable_name, variable_value);
            }

        }

        public bool checkMethod(String command, String[] lines, int count_line, Graphics g)
        {
           

            char[] brackets = { '(', ')' };

            //[\s\S]*?[)]
            // ([A - Z]\w +)\(int([a - z]\w +)[\s\S]+?, int([a - z]\w +)[\s\S]+?, string([a - z]\w +)
            string method_signature_without_parameter = @"([a-z]\s)([a-z]\w{3,10}\S)([(][)])";
            //string method_signature_with_parameters=
            Regex regex = new Regex(method_signature_without_parameter);
            if (regex.IsMatch(command))
            {
               // MessageBox.Show("matched");
                String[] splitter = command.Split(' ');
                String[] parameter = splitter[1].Split(brackets, StringSplitOptions.None);
                methodName = parameter[0] + "()";

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
                    
                  
                    String[] parameter = splitter[1].Split('(');
                    method_signature = parameter[0];
                    MessageBox.Show(method_signature);
                    string methodPlaceholder = "method";
                    string parametersPlaceholder = "parameters";
                    string pattern = string.Format(@"^(?<{0}>\w+)(\((?<{1}>[^)]*)\))?$", methodPlaceholder, parametersPlaceholder);
                    Regex check = new Regex(pattern, RegexOptions.Compiled);
                    if (check.IsMatch(splitter[1]))
                    {

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

                        return true;  
                    }
                }
            }

            return false;

        }

        public void parameterizedMethod(params String[] parameters)
        {
            /*foreach(String parameter in parameters)
            {
                store_variables.Add(parameter, null);
                if (store_variables.ContainsKey(parameter))
                {
                    parameter = store_variables[parameter];
                }
            }
            */
            for(int i = 0; i < parameters.Length; i++)
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

            for (int i =0; i < lines.Length; i++)
            {
                //String command_type = check_command_type(lines[i]);
                if (regex.IsMatch(lines[i]))
                {
                    if (lines[i].Contains(methodName))
                    {
                        return true;
                    }

                }
                if(check.IsMatch(lines[i]))
                {
                    if (lines[i].Contains(method_signature)&&lines[i].Contains(","))
                    {
                        String[] splitters = lines[i].Split('(');
                        String[] splitter = splitters[1].Split(new String[] { "(", ",", ")" }, StringSplitOptions.RemoveEmptyEntries);
                        for(int index = 0; index < splitter.Length; index++)
                        {
                            store_variables[numberofparameters[index]] = splitter[index];
                            if (store_variables.ContainsKey(numberofparameters[index]))
                            {
                                numberofparameters[index] = store_variables[numberofparameters[index]];
                                MessageBox.Show(numberofparameters[index]);
                            }
                        }
                        return true;
                    }
                }

            }
            
            return false;
        }
    }
}
