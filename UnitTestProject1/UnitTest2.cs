using Draw_Shapes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace UnitTestProject1
{
    /// <summary>
    /// This class tests the important methods for the component2.
    /// </summary>
    [TestClass]
    public class UnitTest2
    {
        /// <summary>
        /// Checksif the condition of if command is true.
        /// </summary>
        [TestMethod]
        public void CheckIfCondition()
        {
            //creates the object of CommandChecker class
            CommandChecker checker = new CommandChecker();
            //storing the if signature in a variable
            String valid_condition = "if 2<3";
            //returns true if the condition meet
            Assert.IsTrue(checker.CheckIfCondition(valid_condition));
            //passing a invalid condition to check
            String invalid_condition = "if 2>3";
            //returns false because invalid condtion is passed
            Assert.IsFalse(checker.CheckIfCondition(invalid_condition));
        }
        /// <summary>
        /// Check the condition of while command
        /// </summary>
        [TestMethod]
        public void CheckWhileCondition()
        {
            //creates the object of CommandChecker class
            CommandChecker checker = new CommandChecker();
            //passing valid while condition to check
            String valid_condition = "while 2<3";
            //returns true when condition matched
            Assert.IsTrue(checker.CheckWhileCondition(valid_condition));
            String invalid_condition = "while 2>3";
            Assert.IsFalse(checker.CheckWhileCondition(invalid_condition));
        }
        /// <summary>
        /// Tests the command type
        /// </summary>
        [TestMethod]
        public void TestCheckCommandType()
        {
            //creates the object of CommandChecker class
            CommandChecker checker = new CommandChecker();
            //expected command is if
            String expected1 = "if";
            //checking if the returned value and expected value met
            Assert.AreEqual(checker.CheckCommandTypes("if 2>3"),expected1);
            //expected command is while
            String expected2 = "while";
            //checking if the returned value and expected value met
            Assert.AreEqual(checker.CheckCommandTypes("while x>y"), expected2);
            String expected3 = "method";
            Assert.AreEqual(checker.CheckCommandTypes("method mymethod()"), expected3);
        }

        /// <summary>
        /// Test if variable is valid or invalid
        /// </summary>
        [TestMethod]
        public void TestVariables()
        {
            //creates the object of CommandChecker class
            CommandChecker checker = new CommandChecker();
            String variable = "x=2";
            String[] splitter = variable.Split('=');
            String variable_name = splitter[0];
            String variable_value = splitter[1];
            checker.CheckVariables(variable);
            Assert.IsTrue(CommandChecker.store_variables.ContainsKey(variable_name));
            Assert.AreEqual(CommandChecker.store_variables[variable_name], variable_value);
        }

        /// <summary>
        /// Test if the operation are working fine on variables
        /// </summary>
        [TestMethod]
        public void TestVariableOperation()
        {
            //creates the object of CommandChecker class
            CommandChecker checker = new CommandChecker();
            //passing a command for the addition operation
            String command = "y=100+100";
            //expected value 
            String expected = "200";
            //valid operation signs
            String[] operations = { "+", "-", "*", "/" };
            //splitting the command by equals
            String[] cmd = command.Split('=');
            //stores the variable name
            String variable_name = cmd[0];
            //stores the variable value
            String variable_value = cmd[1];
            //splitting text by the signs
            String[] splitbysigns = variable_value.Split(operations, StringSplitOptions.RemoveEmptyEntries).Select(p => p.Trim()).ToArray();
            //passing command for the variable operation
            checker.RunVariableOperations(command);
            //checks expected value is matched
            Assert.AreEqual(CommandChecker.store_variables[variable_name], expected);
        }

    }
}
