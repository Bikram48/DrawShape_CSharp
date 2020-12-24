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
    /// If user typed a valid commands then the commands get executed and  calls the appropriate methods of other classes.
    /// This class <c>Draw_Shapes</c> is also useful when complex commands like while loop,variables,method and if get executed in the commandbox.
    /// </summary>
    public class CommandChecker
    {
        /// <summary>
        /// <c>singleParameter</c> is a string array which splits the text by the brackets and holds the single parameter passed in the method signature.
        /// </summary>
        String[] singleParameter;
        /// <summary>
        /// String array which splits the commands by commas and holds the number of parameters passed in the parameterized method signature.
        /// </summary>
        String[] numberOfParameters;
        /// <summary>
        /// Uses a static keyword to give access it by using a class name.
        /// Stores the name of the method passed inside the commandbox.
        /// It will be useful when calling method because we need to match the name of the method declaration to the name of the method called.
        /// </summary>
        public static String methodName;
        /// <summary>
        /// Uses a static keyword to give access it by using a class name.
        /// ArrayList datastructure is called to store the lines between the  method and endmethod commands and if and endif commands.
        /// </summary>
        public static ArrayList line_of_commands = new ArrayList();
        ArrayList Errors = new ArrayList();
        /// <summary>
        /// Boolean datatype is used which returns true and false only.
        /// This is a local variable which has a defaul value false.
        /// This boolean variable returns true when the expression meet on if and while conditions and it return false when condition doesn't meet.
        /// </summary>
        bool expression;
        /// <summary>
        /// Creates an object of <c>CommandLine</c> class.
        /// The reference of the CommandLine object will be used to access all the methods and properties inside the CommandLine class who has a public access modifier.
        /// </summary>
        CommandLine commands = new CommandLine();
        /// <summary>
        /// A dictionary called store_variabels is declaired to store the variables which are passed in the commandline box.
        /// In c# dictionary is used to store the values in a key,value pair. A key cannot be duplicated but value can be duplicate.
        ///To get the value of variable we need to use the key of dictionary.
        ///See<see cref="IDictionary"/> to know more about dictionary
        /// </summary>
        public static IDictionary<String, String> store_variables = new Dictionary<String, String>();

        /// <summary>
        /// Uses a public access modifier.
        /// This method takes a command as a parameter and check which command is this and if the command matches to any of the if condition then
        /// it returns the types of the commands. 
        /// It returns error commandtype when command didn't match on any of the condition.
        /// </summary>
        /// <param name="command">Lines from the richTextBox</param>
        /// <returns>
        ///     Type of the commands
        /// </returns>
        ///<exception cref="InvalidVariableException">Thrown when command syntax didn't match</exception>
        public String CheckCommandTypes(String command)
        {
            //commandType is null initially
            String commandType = null;
            try
            {
                //if line contains if command then commandtype will set to if 
                if (command.Contains("if") && !command.Contains("endif"))
                {
                    //checking if the if signature is a total of length 2
                    if (command.Split(' ').Length == 2)
                    {
                        //sets commandtype to if
                        commandType = "if";
                    }
                    //if signature doesn't have a length 2 then the exception will be thrown
                    else
                    {
                        //sets commantype to error
                        commandType = "error";
                        //throws the exception
                        throw new ErrorInCommandsException("If syntax you entered is not valid");
                    }
                }
                //if line contains then command then commandtype will set to singleif
                else if (command.Contains("then"))
                {
                    //sets commandtype to singleif
                    commandType = "singleif";
                }
                //if line contains while command then commandtype will set to while
                else if (command.Contains("while"))
                {
                    //sets commandtype to while
                    commandType = "while";
                }
                //if line contains method command then commandtype will set to method
                else if (command.Contains("method"))
                {
                    //sets commandtype to method
                    commandType = "method";
                }
                //if line contains drawing commands like rectangle or pen or moveto then commandtype will set to drawing_commands
                else if (command.Contains("drawto") || command.Contains("moveto") || command.Contains("pen") || command.Contains("rectangle") || command.Contains("triangle") || command.Contains("circle") || command.Contains("fill")||command.Contains("polygon"))
                {
                    //sets commandtype to drawing_commands
                    commandType = "drawing_commands";
                }
                //if line contains endtag like endif or endloop command then commandtype will set to end_tag
                else if (command.Contains("endif") || command.Contains("endloop") || command.Contains("endmethod"))
                {
                    //sets commandtype to end_tag
                    commandType = "end_tag";
                }
                //if line contains equals sign then commandtype will set to variable and if line contains operator like +,-,*,/ then commandtype will set to variable operation
                else if (command.Contains("="))
                {
                    //checks if the variable is length of 2
                    if (command.Split('=').Length == 2)
                    {
                        //sets commandtype to variable
                        commandType = "variable";
                    }
                    //if variable length is not equals to 2 then exception will be thrown
                    if (command.Split('=').Length != 2)
                    {
                        //sets commandtype to error
                        commandType = "error";
                        //throwing exception
                        throw new InvalidVariableException("Invalid variable syntax at line " + DrawAllShapes.line_number);
                    }
                    if (command.Contains("+") || command.Contains("-") || command.Contains("*") || command.Contains("/"))
                    {
                        //sets commandtype to variable operation
                        commandType = "variableoperation";
                    }
                }
                //if line contains invalid command then commandtype will set to error
                else
                {
                    //sets commandtype to error
                    commandType = "error";
                }
            }
            //catching thrown exceptions
            catch (InvalidVariableException e)
            {
                //sets error boolean to true
                CommandLine.error = true;
                //adding errors into the arraylist
                CommandLine.errors.Add(e.Message);
            }
            //catching thrown exceptions
            catch (ErrorInCommandsException e)
            {
                //sets error boolean to true
                CommandLine.error = true;
                //adding errors into the arraylist
                CommandLine.errors.Add(e.Message);
            }
            //returning commandtype
            return commandType;
        }

        /// <summary>
        /// Uses a public access modifier.
        /// Checks the condition on if signature and returns true when condition matches and false when condition didn't match.
        /// Datatable has been used to check the condition of if command.
        /// </summary>
        /// <param name="command">The line where if found</param>
        /// <returns>
        /// true when condition did match.
        /// </returns>
        /// <exception cref="ErrorInCommandsException">Thrown when invalid if syntax is get executed</exception>
        public bool CheckIfCondition(String command)
        {
            bool equals=false;
            //Datable is created to check the if condition using the compute method
            DataTable table = new DataTable();
            //first parameter of if condition
            String firstParameter;
            //second parameter of if condition
            String secondParameter;
            //putting operator into a signs array to split the value from these operators
            String[] signs = { "<=", ">=", "<", ">", "==", "!=" };
            //splitting line by spaces
            String[] cmd = command.Split(' ').Select(p=>p.Trim()).ToArray();
            //Storing parameters 
            String parameters = cmd[1];
            //Splitting parameters by operators
            String[] splitbysign = parameters.Split(signs, StringSplitOptions.RemoveEmptyEntries).Select(p=>p.Trim()).ToArray();
            try
            {
                //if invalid operator are found then the exception will be thrown
                if (!signs.Any(parameters.Contains))
                {
                    //throwing exception
                    throw new ErrorInCommandsException("Invalid operator at if condition line number " + DrawAllShapes.line_number);
                }
                //if valid operators are entered
                else
                {
                    //checks if command is equals to if
                    if (cmd[0].Equals("if"))
                    {
                        if (command.Contains("=="))
                        {
                            equals = true;
                            //if firstaparameter and secondparameter are stored as a key in variable dictionary then setting the values into them
                            if (store_variables.ContainsKey(splitbysign[0]) && store_variables.ContainsKey(splitbysign[1]))
                            {
                                //sets the values of firstParameter stored in the variable
                                firstParameter = store_variables[splitbysign[0]].ToString();
                                //sets the values of secondParameter stored in the variable
                                secondParameter = store_variables[splitbysign[1]].ToString();
                                if (firstParameter == secondParameter)
                                {
                                    expression = true;
                                }
                            }
                            //if firstaparameter  is stored as a key in variable dictionary then setting the values into it
                            else if (store_variables.ContainsKey(splitbysign[0]) && !store_variables.ContainsKey(splitbysign[1]))
                            {
                                //sets the values of firstParameter stored in the variable
                                firstParameter = store_variables[splitbysign[0]].ToString();
                                //sets the values of secondParameter from the splitted array
                                secondParameter = splitbysign[1];
                                if (firstParameter == secondParameter)
                                {
                                    expression = true;
                                }
                            }
                            //if secondparameter  is stored as a key in variable dictionary then setting the values into it
                            else if (!store_variables.ContainsKey(splitbysign[0]) && store_variables.ContainsKey(splitbysign[1]))
                            {
                                //sets the values  of firstParameter from the splitted array
                                firstParameter = splitbysign[0];
                                //sets the values of firstParameter stored in the variable
                                secondParameter = store_variables[splitbysign[1]].ToString();
                                if (firstParameter == secondParameter)
                                {
                                    expression = true;
                                }
                            }
                            else
                            {
                                //sets the values  of firstParameter from the splitted array
                                firstParameter = splitbysign[0].ToString();
                                //sets the values  of secondParameter from the splitted array
                                secondParameter = splitbysign[1].ToString();
                                if (firstParameter == secondParameter)
                                {
                                    expression = true;
                                }
                            }
                        }
                        if (command.Contains("!="))
                        {
                            equals = true;
                            //if firstaparameter and secondparameter are stored as a key in variable dictionary then setting the values into them
                            if (store_variables.ContainsKey(splitbysign[0]) && store_variables.ContainsKey(splitbysign[1]))
                            {
                                //sets the values of firstParameter stored in the variable
                                firstParameter = store_variables[splitbysign[0]].ToString();
                                //sets the values of secondParameter stored in the variable
                                secondParameter = store_variables[splitbysign[1]].ToString();
                                if (firstParameter != secondParameter)
                                {
                                    expression = true;
                                }
                            }
                            //if firstaparameter  is stored as a key in variable dictionary then setting the values into it
                            else if (store_variables.ContainsKey(splitbysign[0]) && !store_variables.ContainsKey(splitbysign[1]))
                            {
                                //sets the values of firstParameter stored in the variable
                                firstParameter = store_variables[splitbysign[0]].ToString();
                                //sets the values of secondParameter from the splitted array
                                secondParameter = splitbysign[1];
                                if (firstParameter != secondParameter)
                                {
                                    expression = true;
                                }

                            }
                            //if secondparameter  is stored as a key in variable dictionary then setting the values into it
                            else if (!store_variables.ContainsKey(splitbysign[0]) && store_variables.ContainsKey(splitbysign[1]))
                            {
                                //sets the values  of firstParameter from the splitted array
                                firstParameter = splitbysign[0];
                                //sets the values of firstParameter stored in the variable
                                secondParameter = store_variables[splitbysign[1]].ToString();
                                if (firstParameter != secondParameter)
                                {
                                    expression = true;
                                }

                            }
                            else
                            {
                                //sets the values  of firstParameter from the splitted array
                                firstParameter = splitbysign[0].ToString();
                                //sets the values  of secondParameter from the splitted array
                                secondParameter = splitbysign[1].ToString();
                                if (firstParameter != secondParameter)
                                {
                                    expression = true;
                                }
                            }
                        }
                        //if firstaparameter and secondparameter are stored as a key in variable dictionary then setting the values into them
                        if (store_variables.ContainsKey(splitbysign[0]) && store_variables.ContainsKey(splitbysign[1]))
                        {
                            //sets the values of firstParameter stored in the variable
                            firstParameter = store_variables[splitbysign[0]].ToString();
                            //sets the values of secondParameter stored in the variable
                            secondParameter = store_variables[splitbysign[1]].ToString();
                        }
                        //if firstaparameter  is stored as a key in variable dictionary then setting the values into it
                        else if (store_variables.ContainsKey(splitbysign[0]) && !store_variables.ContainsKey(splitbysign[1]))
                        {
                            //sets the values of firstParameter stored in the variable
                            firstParameter = store_variables[splitbysign[0]].ToString();
                            //sets the values of secondParameter from the splitted array
                            secondParameter = splitbysign[1];

                        }
                        //if secondparameter  is stored as a key in variable dictionary then setting the values into it
                        else if (!store_variables.ContainsKey(splitbysign[0]) && store_variables.ContainsKey(splitbysign[1]))
                        {
                            //sets the values  of firstParameter from the splitted array
                            firstParameter = splitbysign[0];
                            //sets the values of firstParameter stored in the variable
                            secondParameter = store_variables[splitbysign[1]].ToString();

                        }
                        //if firstaparameter and secondparameter are not stored as a key in variable dictionary then setting the values from the splitted array
                        else
                        {
                            //sets the values  of firstParameter from the splitted array
                            firstParameter = splitbysign[0].ToString();
                            //sets the values  of secondParameter from the splitted array
                            secondParameter = splitbysign[1].ToString();
                        }
                        if (!equals)
                        {
                            //replacing the splitted parameters by firstparameter and secondparameter
                            String replaced = parameters.Replace(splitbysign[0], firstParameter).Replace(splitbysign[1], secondParameter);
                            //checking if the condition meet between the firstParameter and secondParameter using compute method
                            expression = Convert.ToBoolean(table.Compute(replaced, String.Empty));
                        }
                        
                    }
                   //when if command didn't find then exception will be thrown
                    else
                    {
                        //throwing exception
                        throw new ErrorInCommandsException("Invalid command at line " + DrawAllShapes.line_number);
                    }
                }
              
            }
            //catching the thrown exception
            catch(ErrorInCommandsException e)
            {
                //sets error boolean to true
                CommandLine.error = true;
                //adding errors to the arraylist
                ErrorRepository.errorsList.Add(e.Message);
            }
            //catching the thrown exception
            catch (IndexOutOfRangeException e)
            {

            }
            //return boolean true or false
            return expression;
        }

        /// <summary>
        /// Uses a public access modifier.
        /// Runs the lines between the if command and endif command when condition meet on if command.
        /// If then command found after the if command then single line of will only get executed and if endif found then the commands between if and endif will get executed.
        /// </summary>
        /// <param name="lines">lines of richtextbox</param>
        /// <param name="count_line">Line counter</param>
        /// <param name="g">reference of Graphics to draw into the panel.</param>
        public void RunIfCommand(String[] lines, int count_line, Graphics g)
        {
            //if then command is found on the nextline of if command
            if (lines[count_line].Equals("then"))
            {
                //checking the command types by passing a line next to the then command
                string command_type = CheckCommandTypes(lines[count_line + 1]);
                //if drawing_command are returned by command_type
                if (command_type.Equals("drawing_commands"))
                {
                    //drawing the shapes in canvas
                    commands.draw_commands(lines[count_line + 1], g);
                }

            }
            //if then command is not found on the nextline of if command
            else
            {
                //starts for loop to loop the all the lines inside the richtexbox
                for (int i = count_line; i < lines.Length; i++)
                {
                    //run the loop untill the endif command found in the line
                    if (!(lines[i].Equals("endif")))
                    {
                        //checking a commandtype
                        string command_types = CheckCommandTypes(lines[i]);
                        //if drawing_command are returned by command_type then the commands will be added into the arraylist
                        if (command_types.Equals("drawing_commands"))
                        {
                            //adding lines of command into arraylist
                            line_of_commands.Add(lines[i]);
                            //commands.draw_commands(lines[i], g);
                        }

                    }
                }
            }
        }

        /// <summary>
        /// Uses a public access modifier.
        /// Runs the lines between the while command and endloop command when condition meet on while command.
        /// Datatable class compute method has been used to check the condition of while command.
        /// </summary>
        /// <param name="command">Line where while condition found</param>
        /// <returns>true if condition meet</returns>
        /// <exception cref="ErrorInCommandsException">
        ///     throw when invalid operator or invalid while syntax get found
        /// </exception>
        public bool CheckWhileCondition(string command)
        {
            bool equals = true;
            DataTable table = new DataTable();
            //first parameter of while condition
            String firstParameter;
            //second parameter of while condition
            String secondParameter;
            //putting operator into a signs array to split the value from these operators
            String[] signs = { "<=", ">=", "<", ">", "==", "!=" };
            //splitting line by spaces
            String[] splitter = command.Split(' ');
            //Storing parameters 
            String parameters = splitter[1];
            //Splitting parameters by operators
            String[] splitbysign = parameters.Split(signs, StringSplitOptions.RemoveEmptyEntries).Select(p => p.Trim()).ToArray();
            try
            {
                //if invalid operator are found then the exception will be thrown
                if (!signs.Any(parameters.Contains))
                {
                    //throwing exception
                    throw new ErrorInCommandsException("Invalid operator at if condition line number " + DrawAllShapes.line_number);
                }
                //if valid operators are entered
                else
                {

                    //checks if command is equals to while
                    if (splitter[0].Equals("while"))
                    {
                        if (command.Contains("=="))
                        {
                            equals = true;
                            //if firstaparameter and secondparameter are stored as a key in variable dictionary then setting the values into them
                            if (store_variables.ContainsKey(splitbysign[0]) && store_variables.ContainsKey(splitbysign[1]))
                            {
                                //sets the values of firstParameter stored in the variable
                                firstParameter = store_variables[splitbysign[0]].ToString();
                                //sets the values of secondParameter stored in the variable
                                secondParameter = store_variables[splitbysign[1]].ToString();
                                if (firstParameter == secondParameter)
                                {
                                    expression = true;
                                }
                            }
                            //if firstaparameter  is stored as a key in variable dictionary then setting the values into it
                            else if (store_variables.ContainsKey(splitbysign[0]) && !store_variables.ContainsKey(splitbysign[1]))
                            {
                                //sets the values of firstParameter stored in the variable
                                firstParameter = store_variables[splitbysign[0]].ToString();
                                //sets the values of secondParameter from the splitted array
                                secondParameter = splitbysign[1];
                                if (firstParameter == secondParameter)
                                {
                                    expression = true;
                                }
                            }
                            //if secondparameter  is stored as a key in variable dictionary then setting the values into it
                            else if (!store_variables.ContainsKey(splitbysign[0]) && store_variables.ContainsKey(splitbysign[1]))
                            {
                                //sets the values  of firstParameter from the splitted array
                                firstParameter = splitbysign[0];
                                //sets the values of firstParameter stored in the variable
                                secondParameter = store_variables[splitbysign[1]].ToString();
                                if (firstParameter == secondParameter)
                                {
                                    expression = true;
                                }
                            }
                            else
                            {
                                //sets the values  of firstParameter from the splitted array
                                firstParameter = splitbysign[0].ToString();
                                //sets the values  of secondParameter from the splitted array
                                secondParameter = splitbysign[1].ToString();
                                if (firstParameter == secondParameter)
                                {
                                    expression = true;
                                }
                            }
                        }
                        if (command.Contains("!="))
                        {
                            equals = true;
                            //if firstaparameter and secondparameter are stored as a key in variable dictionary then setting the values into them
                            if (store_variables.ContainsKey(splitbysign[0]) && store_variables.ContainsKey(splitbysign[1]))
                            {
                                //sets the values of firstParameter stored in the variable
                                firstParameter = store_variables[splitbysign[0]].ToString();
                                //sets the values of secondParameter stored in the variable
                                secondParameter = store_variables[splitbysign[1]].ToString();
                                if (firstParameter != secondParameter)
                                {
                                    expression = true;
                                }
                            }
                            //if firstaparameter  is stored as a key in variable dictionary then setting the values into it
                            else if (store_variables.ContainsKey(splitbysign[0]) && !store_variables.ContainsKey(splitbysign[1]))
                            {
                                //sets the values of firstParameter stored in the variable
                                firstParameter = store_variables[splitbysign[0]].ToString();
                                //sets the values of secondParameter from the splitted array
                                secondParameter = splitbysign[1];
                                if (firstParameter != secondParameter)
                                {
                                    expression = true;
                                }

                            }
                            //if secondparameter  is stored as a key in variable dictionary then setting the values into it
                            else if (!store_variables.ContainsKey(splitbysign[0]) && store_variables.ContainsKey(splitbysign[1]))
                            {
                                //sets the values  of firstParameter from the splitted array
                                firstParameter = splitbysign[0];
                                //sets the values of firstParameter stored in the variable
                                secondParameter = store_variables[splitbysign[1]].ToString();
                                if (firstParameter != secondParameter)
                                {
                                    expression = true;
                                }

                            }
                            else
                            {
                                //sets the values  of firstParameter from the splitted array
                                firstParameter = splitbysign[0].ToString();
                                //sets the values  of secondParameter from the splitted array
                                secondParameter = splitbysign[1].ToString();
                                if (firstParameter != secondParameter)
                                {
                                    expression = true;
                                }
                            }
                        }
                        //if firstaparameter and secondparameter are stored as a key in variable dictionary then setting the values into them
                        if (store_variables.ContainsKey(splitbysign[0]) && store_variables.ContainsKey(splitbysign[1]))
                        {
                            //sets the values of firstParameter stored in the variable
                            firstParameter = store_variables[splitbysign[0]].ToString();
                            //sets the values of secondParameter stored in the variable
                            secondParameter = store_variables[splitbysign[1]].ToString();
                        }
                        //if firstaparameter  is stored as a key in variable dictionary then setting the values into it
                        else if (store_variables.ContainsKey(splitbysign[0]) && !store_variables.ContainsKey(splitbysign[1]))
                        {
                            //sets the values of firstParameter stored in the variable
                            firstParameter = store_variables[splitbysign[0]].ToString();
                            //sets the values of secondParameter from the splitted array
                            secondParameter = splitbysign[1];
                        }
                        //if secondparameter  is stored as a key in variable dictionary then setting the values into it
                        else if (!store_variables.ContainsKey(splitbysign[0]) && store_variables.ContainsKey(splitbysign[1]))
                        {
                            //sets the values  of firstParameter from the splitted array
                            firstParameter = splitbysign[0];
                            //sets the values of secondParameter stored in the variable
                            secondParameter = store_variables[splitbysign[1]].ToString();

                        }
                        //if firstaparameter and secondparameter are not stored as a key in variable dictionary then setting the values from the splitted array
                        else
                        {
                            //sets the values  of firstParameter from the splitted array
                            firstParameter = splitbysign[0].ToString();
                            //sets the values  of secondParameter from the splitted array
                            secondParameter = splitbysign[1].ToString();
                        }
                        //replacing the splitted parameters by firstparameter and secondparameter
                        String replaced = parameters.Replace(splitbysign[0], firstParameter).Replace(splitbysign[1], secondParameter);
                        if (!equals)
                        {
                            //checking if the condition meet between the firstParameter and secondParameter using compute method
                            expression = Convert.ToBoolean(table.Compute(replaced, String.Empty));
                        }
                    }
                    //when while command didn't find then exception will be thrown
                    else
                    {
                        //throwing exception
                        throw new ErrorInCommandsException("Invalid command at line " + DrawAllShapes.line_number);
                    }
                }
            }
            //catching the thrown exception
            catch (ErrorInCommandsException e)
            {
                //sets error boolean to true
                CommandLine.error = true;
                //adding errors to the arraylist
                ErrorRepository.errorsList.Add(e.Message);
            }
            //catching the thrown exception
            catch (IndexOutOfRangeException e)
            {
               
            }
            //return boolean true or false
            return expression;
        }

        /// <summary>
        /// Uses a public access modifier.
        /// Runs the lines starting from the while command till the endLoop command.
        /// It repeats the lines between while and enloop command till the condition didn't get false.
        /// </summary>
        /// <param name="command">line where while found</param>
        /// <param name="lines">lines of richtextbox</param>
        /// <param name="count_line">Line counter</param>
        /// <param name="g">reference of Graphics to draw into the panel.</param>
        public void RunWhileCommand(String command,String[] lines, int count_line, Graphics g)
        {
            //putting operator into a signs array to split the value from these operators
            String[] signs = { "<=", ">=", "<", ">", "==", "!=" };
            //splitting line by spaces
            String[] splitter = command.Split(' ');
            //storing parameters
            String parameters = splitter[1];
            //Splitting parameters by operators
            String[] splitbysign = parameters.Split(signs, StringSplitOptions.RemoveEmptyEntries).Select(p => p.Trim()).ToArray();
            //initializing loop variable to zero while will be used later on to loop the lines
            int loop = 0;
            //initializing loop_count to zero while will be used later on to loop the lines
            int loop_count = 0;
            //if while firstparameter is existed in the dictionary as a key then setting the values of loop with the value of the variable name
            if (store_variables.ContainsKey(splitbysign[0]))
            {
                //sets the values of loop
                loop = Convert.ToInt32(store_variables[splitbysign[0]]);
                //sets the values of loop count
                loop_count = Convert.ToInt32(splitbysign[1]);
            }
            //for loop to loop the lines 
            for (int count = loop; count <= loop_count; count++)
            {
                //for loop to get all the lines after the while command found
                for (int i = count_line; i < lines.Length; i++)
                {
                    //looping until the endloop command found
                    if (!(lines[i].Equals("endloop")))
                    {
                        //checking a command type
                        string command_types = CheckCommandTypes(lines[i]);
                        //if drawing_commands is returned by the command_type then the shape will be drawn untill the while condition get false
                        if (command_types.Equals("drawing_commands"))
                        {
                            //passing lines to draw shapes
                            commands.draw_commands(lines[i], g);
                        }
                        //if variableoperation is returned then the operation will be done in variable
                        if (command_types.Equals("variableoperation"))
                        {
                            //calling a method for a operation
                            RunVariableOperations(lines[i]);
                        }

                    }
                    //if endloop found then break it
                    else
                    {
                        break;
                    }

                }
                //incrementing the value of count
                count = Convert.ToInt32(store_variables[splitbysign[0]]);
            }
        }

        /// <summary>
        /// Uses a public access modifier.
        /// Takes the line where equals operator get found as a parameter and checks if the command is actually a variable.\
        /// If command is actually a variable then it adds the variable name and variable value into the dictionary as a ke,value pair
        /// </summary>
        /// <param name="command">Line where equals sign found</param>
        /// <exception cref="InvalidVariableException">
        ///     Throw when invalid variables are found
        /// </exception>
        public void CheckVariables(string command)
        {
            try
            {
                //Splitting the line by equals 
                String[] cmd = command.Split('=').Select(p => p.Trim()).ToArray();
                //Stroring the variable name
                String variable_name = cmd[0];
                //Stroring the variable value
                String variable_value = cmd[1];
                //making a isVar to true
                CommandLine.isvar = true;
                //checks if invalid operator are found using Regex
                if (Regex.IsMatch(variable_value, @"([<>\?\*\\\""/\|!()@%^*+,$])+"))
                {
                    //throws the exception
                    throw new InvalidVariableException("Invalid operator at line number " + DrawAllShapes.line_number);
                }
                //checks if invalid variable value is entered
                else if (variable_name.Equals(variable_value))
                {
                    //throws the exception
                    throw new InvalidVariableException("Invalid variable value at line number " + DrawAllShapes.line_number);
                }
                //checks if variable value is not passed
                else if (variable_value.Equals(""))
                {
                    //throws the exception
                    throw new InvalidVariableException("Please add value of the variable at line number " + DrawAllShapes.line_number);
                }
                //if there are no errors then this block will get executed
                else
                {
                    //if variable name is not already existed into the dictionary then only add the variable name as a key in dictionary
                    if (!store_variables.ContainsKey(variable_name))
                    {
                        //checks if variable value is stored as a key in dictionary
                        if (store_variables.ContainsKey(variable_value))
                        {
                            //sets the values of variable_value from dictionary
                            variable_value = store_variables[variable_value];
                            //adding into the dictionary
                            store_variables.Add(variable_name, variable_value);
                        }
                        //if variable  is not stored as a key in dictionary
                        else
                        {
                            //checks if variable value contains letter
                            if (Regex.IsMatch(variable_value, @"^[a-zA-Z]+$"))
                            {
                                //if variable_value is not stored as a key in dictionary then exception will be thrown 
                                if (!store_variables.ContainsKey(variable_value))
                                {
                                    //throws exception
                                    throw new InvalidVariableException("Please declare the variable first. Error at line ");
                                }
                            }
                            //if variable value contains number then this block will get executed
                            else
                            {
                                //adding variable name and variable value into the dictionary
                                store_variables.Add(variable_name, variable_value);
                            }
                        }
                       
                    }
                }
            }
            //catching the thrown exception
            catch (InvalidVariableException e)
            {
                //sets error boolean to true
                CommandLine.error = true;
                //adding errors to the arraylist
                ErrorRepository.errorsList.Add(e.Message);
            }

        }

        /// <summary>
        /// Uses a public access modifier.
        /// Performs the operation when operators  are found.
        /// If the operation is done then the dictionary key will be updated with the new value.
        /// </summary>
        /// <remarks>
        ///     Valid operators +,-,* and /
        /// </remarks>
        /// <param name="command">the line where valid operators like +,- are found</param>
        /// <exception cref="InvalidVariableException">
        ///     throw when invalid operators are found.
        /// </exception>
        public void RunVariableOperations(String command)
        {
            //make a variable true
            CommandLine.isvar = true;
            DataTable table = new DataTable();
            //putting operator into a signs array to split the value from these operators
            String[] operations = { "+", "-", "*", "/" };
            //splitting line by equals sign
            String[] cmd = command.Split('=');
            //Storing variable name
            String variable_name = cmd[0];
            //Storing variable value
            String variable_value = cmd[1];
            //Splitting value by operators
            String[] splitbysigns = variable_value.Split(operations, StringSplitOptions.RemoveEmptyEntries).Select(p=>p.Trim()).ToArray();
            try
            {
                //if variable value contains any of the signs then run this block
                if (operations.Any(variable_value.Contains))
                {
                    //if line contains + then perform addition
                    if (command.Contains("+"))
                    {
                        //if first variable existed in dictionary then taking a value from the dictionary with the existed key
                        if (store_variables.ContainsKey(splitbysigns[0]) && !store_variables.ContainsKey(splitbysigns[1]))
                        {
                            //performing addition operation on values
                            int total = Convert.ToInt32(store_variables[splitbysigns[0]]) + Convert.ToInt32(splitbysigns[1]);
                            store_variables.Remove(splitbysigns[0]);
                            store_variables.Add(variable_name, total.ToString());
                        }
                        //if second variable existed in dictionary then taking a value from the dictionary with the existed key
                        if (!store_variables.ContainsKey(splitbysigns[0]) && store_variables.ContainsKey(splitbysigns[1]))
                        {
                            //performing addition operation on values
                            int total = Convert.ToInt32(splitbysigns[0]) + Convert.ToInt32(store_variables[splitbysigns[1]]);
                            store_variables.Remove(splitbysigns[0]);
                            store_variables.Add(variable_name, total.ToString());
                        }
                    }
                    //if line contains - then perform subtraction
                    else if (command.Contains("-"))
                    {
                        //if first variable existed in dictionary then taking a value from the dictionary with the existed key
                        if (store_variables.ContainsKey(splitbysigns[0]) && !store_variables.ContainsKey(splitbysigns[1]))
                        {
                            //performing subtraction operation on values
                            int total = Convert.ToInt32(store_variables[splitbysigns[0]]) - Convert.ToInt32(splitbysigns[1]);
                            store_variables.Remove(splitbysigns[0]);
                            store_variables.Add(variable_name, total.ToString());
                        }
                        //if second variable existed in dictionary then taking a value from the dictionary with the existed key
                        if (!store_variables.ContainsKey(splitbysigns[0]) && store_variables.ContainsKey(splitbysigns[1]))
                        {
                            //performing subtraction operation on values
                            int total = Convert.ToInt32(splitbysigns[0]) - Convert.ToInt32(store_variables[splitbysigns[1]]);
                            store_variables.Remove(splitbysigns[0]);
                            store_variables.Add(variable_name, total.ToString());
                        }
                    }
                    //if line contains * then perform multiplication
                    else if (command.Contains("*"))
                    {
                        //if first variable existed in dictionary then taking a value from the dictionary with the existed key
                        if (store_variables.ContainsKey(splitbysigns[0]) && !store_variables.ContainsKey(splitbysigns[1]))
                        {
                            //performing multiplication operation on values
                            int total = Convert.ToInt32(store_variables[splitbysigns[0]]) * Convert.ToInt32(splitbysigns[1]);
                            store_variables.Remove(splitbysigns[0]);
                            store_variables.Add(variable_name, total.ToString());
                        }
                        //if second variable existed in dictionary then taking a value from the dictionary with the existed key
                        if (!store_variables.ContainsKey(splitbysigns[0]) && store_variables.ContainsKey(splitbysigns[1]))
                        {
                            //performing multiplication operation on values
                            int total = Convert.ToInt32(splitbysigns[0]) * Convert.ToInt32(store_variables[splitbysigns[1]]);
                            store_variables.Remove(splitbysigns[0]);
                            store_variables.Add(variable_name, total.ToString());
                        }
                    }
                    //if line contains / then perform divison
                    else if (command.Contains("/"))
                    {
                        //if first variable existed in dictionary then taking a value from the dictionary with the existed key
                        if (store_variables.ContainsKey(splitbysigns[0]) && !store_variables.ContainsKey(splitbysigns[1]))
                        {
                            //performing divison operation on values
                            int total = (Convert.ToInt32(store_variables[splitbysigns[0]])) / (Convert.ToInt32(splitbysigns[1]));
                            store_variables.Remove(splitbysigns[0]);
                            store_variables.Add(variable_name, total.ToString());
                        }
                        //if second variable existed in dictionary then taking a value from the dictionary with the existed key
                        if (!store_variables.ContainsKey(splitbysigns[0]) && store_variables.ContainsKey(splitbysigns[1]))
                        {
                            //performing divison operation on values
                            int total = Convert.ToInt32(splitbysigns[0]) / Convert.ToInt32(store_variables[splitbysigns[1]]);
                            store_variables.Remove(splitbysigns[0]);
                            store_variables.Add(variable_name, total.ToString());
                        }
                    }
                }
                //if invalid operator found then throw exception
                else
                {
                    //throws exception if invalid opartor found
                    throw new InvalidVariableException("Invalid operator at line " + DrawAllShapes.line_number);
                }
                //if key is not already existed then adding key and values in dictionary
                if (!store_variables.ContainsKey(variable_name))
                {
                    //if numbers operation are made then compute will automataically perform actions into them
                    variable_value = table.Compute(variable_value, String.Empty).ToString();
                    //adding variable_name and value into the storvariable dictionary
                    store_variables.Add(variable_name, variable_value);
                }
                
            }
            //catching the thrown exception
            catch (InvalidVariableException e)
            {
                //sets error boolean to true
                CommandLine.error = true;
                //adding errors to the arraylist
                ErrorRepository.errorsList.Add(e.Message);
            }

        }

        /// <summary>
        /// Uses a public access modifier.
        /// Checks if the method is a parameterized or parameterless command using a Regex. If method is one of them then the code will run appropriately.
        /// If all required things get matched then the lines starting from method command to the endmethod will be added into the arraylist and will 
        /// get executed when method is called inside the command box.
        /// This method returns true when condition for the parameterless or parameterized methods are meet.
        /// </summary>
        /// <param name="command">line where method signature found</param>
        /// <param name="lines">lines of richtextbox</param>
        /// <param name="count_line">Line counter</param>
        /// <param name="g">reference of Graphics to draw into the panel.</param>
        /// <returns>true when parameterless or parameterized method are get matched.</returns>
        public bool RunMethod(String command, String[] lines, int count_line, Graphics g)
        {
            //storing brackets into the array for the splitting purpose
            char[] brackets = { '(', ')' };
            //pattern for methow without parameter
            string method_signature_without_parameter = @"([a-z]\s)([a-z]\w{3,10}\S)([(][)])";
            //Regex to match the pattern
            Regex regex = new Regex(method_signature_without_parameter);
            //if pattern matched then run this block
            if (regex.IsMatch(command))
            {
                //Splitting line by spaces
                String[] splitter = command.Split(' ');
                //splitting the parameter by brackets
                String[] parameter = splitter[1].Split(brackets, StringSplitOptions.RemoveEmptyEntries).Select(p => p.Trim()).ToArray();
                //storing the methodname
                methodName = parameter[0];
                //if commandname is method then this block will get executed
                if (splitter[0].Equals("method"))
                {
                    //for loop starts from the line where method command found
                    for (int i = count_line; i < lines.Length; i++)
                    {
                        //runs the loop untill the endmethod command is found into the lines
                        if (!lines[i].Equals("endmethod"))
                        {
                            //checking a commandtype by passing a lines 
                            string command_types = CheckCommandTypes(lines[i]);
                            //if commandtype return drawing_command then adding the block of lines in ArrayList
                            if (command_types.Equals("drawing_commands"))
                            {
                                //adding lines in arraylist
                                line_of_commands.Add(lines[i]);
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                //returns true
                return true;
            }
            //this is the parameterized method block
            else
            {
                //splitting a line by space
                String[] splitter = command.Split(' ');
                //if command is a method then run this block
                if (splitter[0].Equals("method"))
                {
                    //splitting parameters by brackets
                    singleParameter = splitter[1].Split(new String[] { "(", ")" }, StringSplitOptions.RemoveEmptyEntries).Select(p => p.Trim()).ToArray();
                    String[] parameter = splitter[1].Split('(');
                    //Part for regex
                    string methodPlaceholder = "method";
                    string parametersPlaceholder = "parameters";
                    //pattern for parameterized method
                    string pattern = string.Format(@"^(?<{0}>\w+)(\((?<{1}>[^)]*)\))?$", methodPlaceholder, parametersPlaceholder);
                    //Regex to match the parameterized method
                    Regex check = new Regex(pattern, RegexOptions.Compiled);
                    //if pattern matched then this block will get executed
                    if (check.IsMatch(splitter[1]))
                    {
                        //stroing method name
                        methodName = parameter[0];
                        //first checks if method contains ,
                        if (command.Contains(','))
                        {
                            //if comma found then split parameters by comma
                            numberOfParameters = parameter[1].Split(new string[] { ",", ")" }, StringSplitOptions.RemoveEmptyEntries);
                            //foreach to iterate the parameters existed in method
                            foreach (String parameters in numberOfParameters)
                            {
                                //passing paramters to a method to add them into the dictionary
                                ParameterizedMethod(parameters);
                            }
                            //for loop starts from the line where method command found
                            for (int i = count_line; i < lines.Length; i++)
                            {
                                //runs the loop untill the endmethod command is found into the lines
                                if (!lines[i].Equals("endmethod"))
                                {
                                    //checking a commandtype by passing a lines 
                                    string command_types = CheckCommandTypes(lines[i]);
                                    //if commandtype return drawing_command then adding the block of lines in ArrayList
                                    if (command_types.Equals("drawing_commands"))
                                    {
                                        //adding lines in arraylist
                                        line_of_commands.Add(lines[i]);
                                    }

                                }
                                else
                                {
                                    continue;
                                }
                            }

                        }
                        //checks if method contains only one parameter 
                        else if (singleParameter[1].Length == 1)
                        {
                            //passing a sing paramter to a parameterizedMethod to add them into the dictionary
                            ParameterizedMethod(singleParameter[1]);
                            //for loop starts from the line where method command found
                            for (int i = count_line; i < lines.Length; i++)
                            {
                                //runs the loop untill the endmethod command is found into the lines
                                if (!lines[i].Equals("endmethod"))
                                {
                                    //checking a commandtype by passing a lines 
                                    string command_types = CheckCommandTypes(lines[i]);
                                    //if commandtype return drawing_command then adding the block of lines in ArrayList
                                    if (command_types.Equals("drawing_commands"))
                                    {
                                        //adding lines in arraylist
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

        /// <summary>
        /// Uses a public access modifier.
        /// This method adds the parameters passed in the method to the dictionary as a key and the value will be null initially. The values will only be added to the key
        /// when values are passed from the method call.
        /// params keyword is used in a parameter are when we don't know how many actually parameters are there.
        /// </summary>
        /// <param name="parameters">parameters passed in parameterized method signature</param>
        public void ParameterizedMethod(params String[] parameters)
        {
            //for loop to add the parameters into the dictionary
            for (int i = 0; i < parameters.Length; i++)
            {
                //storing parameters into the dictionary
                if (!store_variables.ContainsKey(parameters[i]))
                {
                    store_variables.Add(parameters[i], null);
                }
            }
        }

        /// <summary>
        /// Uses a public access modifier.
        /// This is a method where actually method call part are done.
        /// This method first checks which method is called using a Regex and if method founds with the actuall name and parameter then that method will be called and lines 
        /// inside those method will get executed.
        /// If the parameterized method gets called then the value inside the parameter field will be added into the key of parameters passed in the method declaration.
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="count_line"></param>
        /// <param name="g"></param>
        /// <returns>true if method called signature are matched to the method declaired signature</returns>
        public bool methodcall(String[] lines, int count_line, Graphics g)
        {
            try
            {
                //Regex part
                string methodPlaceholder = "method";
                string parametersPlaceholder = "parameters";
                //pattern for the calling method
                string pattern = string.Format(@"^(?<{0}>\w+)(\((?<{1}>[^)]*)\))?$", methodPlaceholder, parametersPlaceholder);
                //Regex for matching method
                Regex check = new Regex(pattern, RegexOptions.Compiled);
                //storing brackets into the character array for the splitting purpose
                char[] brackets = { '(', ')' };
                //patterns for method calling
                string syntax = @"([a-z]\w{3,10}\S)([(][)])";
                //regex to match the pattern
                Regex regex = new Regex(syntax);
                lines[count_line - 1] = "";
                //for loop for the lines in richtextbox
                for (int i = 0; i < lines.Length; i++)
                {
                    //String command_type = check_command_type(lines[i]);
                    if (regex.IsMatch(lines[i]))
                    {
                        //if lines contains the method signature then returns true
                        if (lines[i].Contains(methodName + "()"))
                        {
                            //return true;
                            return true;
                        }

                    }
                    //if parameterized method matched then this block will get executed
                    else if (check.IsMatch(lines[i]))
                    {
                        bool validParameter = false;

                        //checks if lines contains comma
                        if (lines[i].Contains(","))
                        {
                            //split the line by bracket and comma
                            String[] splitters = lines[i].Split('(');
                            String[] splitter = splitters[1].Split(new String[] { "(", ",", ")" }, StringSplitOptions.RemoveEmptyEntries);

                            //for loop starts to store the variables into the key of dictionary
                            for (int index = 0; index < splitter.Length; index++)
                            {
                                if (!Regex.IsMatch(splitter[index], @"^[a-zA-Z]+$"))
                                {
                                    validParameter = true;
                                    //storing values into the dictionary value
                                    store_variables[numberOfParameters[index]] = splitter[index];
                                    if (store_variables.ContainsKey(numberOfParameters[index]))
                                    {
                                        numberOfParameters[index] = store_variables[numberOfParameters[index]];
                                    }
                                }

                            }
                            if (!validParameter)
                            {
                                throw new InvalidCommandParametersException("Invalid parameter passed at method calling. Error at line " + DrawAllShapes.line_number);
                            }
                            return true;
                        }
                        //checks if lines contains the method signature
                        if (lines[i].Contains(methodName))
                        {
                            //splitting by brackets
                            String[] splitter = lines[i].Split('(');
                            String[] parameter = splitter[1].Split(new String[] { ")" }, StringSplitOptions.RemoveEmptyEntries);
                            // String[] parameter = lines[i].Split(new String[] { "(", ")" }, StringSplitOptions.RemoveEmptyEntries);
                            //if single parameter is passed then this block will get executed
                            if (parameter.Length == 1)
                            {
                                if (!Regex.IsMatch(parameter[0], @"^[a-zA-Z]+$"))
                                {
                                    //storing the variables
                                    store_variables[singleParameter[1]] = parameter[0];
                                    //if key existed in the dictionary then store values into the key
                                    if (store_variables.ContainsKey(singleParameter[1]))
                                    {
                                        //storing values into the key
                                        singleParameter[1] = store_variables[singleParameter[1]];
                                    }
                                }
                                else
                                {
                                    throw new InvalidCommandParametersException("Invalid parameter passed at method calling. Error at line " + DrawAllShapes.line_number);
                                }
                                return true;
                            }
                        }
                    }

                }
            }
            catch(InvalidCommandParametersException e)
            {
                CommandLine.error = true;
                ErrorRepository.errorsList.Add(e.Message);
            }

            return false;
        }


    }
}
