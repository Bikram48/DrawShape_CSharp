using Draw_Shapes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest2
    {
       
        [TestMethod]
        public void CheckIfCondition()
        {
            CommandChecker checker = new CommandChecker();
            String valid_condition = "if 2<3";
            Assert.IsTrue(checker.check_if_command(valid_condition));
            String invalid_condition = "if 2>3";
            Assert.IsFalse(checker.check_if_command(invalid_condition));
        }

        [TestMethod]
        public void CheckWhileCondition()
        {
            CommandChecker checker = new CommandChecker();
            String valid_condition = "while 2<3";
            Assert.IsTrue(checker.check_while_command(valid_condition));
            String invalid_condition = "while 2>3";
            Assert.IsFalse(checker.check_while_command(invalid_condition));
        }

        [TestMethod]
        public void TestCheckCommandType()
        {
            CommandChecker checker = new CommandChecker();
            String expected1 = "if";
            Assert.AreEqual(checker.check_command_type("if 2>3"),expected1);
            String expected2 = "while";
            Assert.AreEqual(checker.check_command_type("while x>y"), expected2);
            String expected3 = "method";
            Assert.AreEqual(checker.check_command_type("method mymethod()"), expected3);
        }

        [TestMethod]
        public void TestVariables()
        {
            CommandChecker checker = new CommandChecker();
            String variable = "x=2";
            String[] splitter = variable.Split('=');
            String variable_name = splitter[0];
            String variable_value = splitter[1];
            checker.check_variable(variable);
            Assert.IsTrue(CommandChecker.store_variables.ContainsKey(variable_name));
            Assert.AreEqual(CommandChecker.store_variables[variable_name], variable_value);
        }

        [TestMethod]
        public void TestVariableOperation()
        {
            CommandChecker checker = new CommandChecker();
            String command = "y=100+100";
            String expected = "200";
            String[] operations = { "+", "-", "*", "/" };
            String[] cmd = command.Split('=');
            String variable_name = cmd[0];
            String variable_value = cmd[1];
            String[] splitbysigns = variable_value.Split(operations, StringSplitOptions.RemoveEmptyEntries).Select(p => p.Trim()).ToArray();
            checker.run_variable_operation(command);
            Assert.AreEqual(CommandChecker.store_variables[variable_name], expected);
        }

       

        /// <summary>
        /// This is the method from VariableChecker class which will be later implemented on the component2.
        /// </summary>
        [TestMethod]
        //[ExpectedException(typeof(InvalidVariableException), "Please declare the variable first. Error at line")]
        public void TestCheckALlVariables()
        {
            CommandChecker checker = new CommandChecker();
            String input = "x$100";
            checker.check_variable("x=x");
            var ex = Assert.ThrowsException<InvalidVariableException>(() => checker.check_variable(input));
            Assert.AreSame(ex.Message, "Please declare the variable first. Error at line");
            /*VariableChecker check = new VariableChecker();
            String expectedVariable = "ifelse";
            
            Assert.AreEqual(check.checkAllVariables("else"), expectedVariable);
            */
        }

       
    }
}
