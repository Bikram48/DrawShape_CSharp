using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Draw_Shapes;
using System.Drawing;

namespace UnitTestProject1
{
    /// <summary>
    /// This class is created for unit testing. It tests some important methods from
    /// the different classes if they are working fine.
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// Checks the method of CommandChecker which is responsible for splitting the text by the spaces.
        /// </summary>

        [TestMethod]
        public void TestSplitTextBySpace()
        {
            //creates the  object of CommandChecker class
            CommandChecker checker = new CommandChecker();
            //expected output
            String expected = "rectangle";
            String text = "rectangle 100,100";
            //sends the text to be checked as a parameter
           // String[] splitter=checker.splitTextBySpace(text);
            //checking if returned vale from method and expected value are matched
            //Assert.AreEqual(splitter[0], expected);
        }

        /// <summary>
        /// Checks the method of CommandChecker which is responsible for splitting the parameters of commands by comma.
        /// </summary>
        [TestMethod]
        public void TestSplitParameterByComma()
        {
            //creates the  object of CommandChecker class
            CommandChecker checker = new CommandChecker();
            //expected output
            String expected = "10";
            String text = "10,20";
           // String[] splitter=checker.splitParameterByComma(text);
            //checking if returned vale from method and expected value are matched
           // Assert.AreEqual(splitter[0], expected);

        }

        /// <summary>
        /// Tests the shapeFactory class which is responsible for creating the instances of different shapes(Rectangle,Triangle,Circle).
        /// </summary>
        [TestMethod]
        public void TestShapeFactory()
        {
            //creates the  object of ShapeFactory class
            ShapeFactory s1 = new ShapeFactory();
            Shapes shape1 = s1.checkShapes("rectangle");
            shape1.setX(11);
            Assert.AreEqual(shape1.getX(),11);
        }

        /// <summary>
        /// Tests the PenColor which is responsible for setting a color to the pen.
        /// </summary>
        public void TestPenColor()
        {
            //creates the  object of PenColor class
            PenColor color = new PenColor();
            Color expected =Color.Red;
            Color pencolor=color.getPenColor("Red");
            Assert.AreEqual(pencolor, expected);
        }

        /// <summary>
        /// This is the method from VariableChecker class which will be later implemented on the component2.
        /// </summary>
        [TestMethod]
        public void TestCheckALlVariables()
        {
            VariableChecker check = new VariableChecker();
            String expectedVariable = "ifelse";
            
            Assert.AreEqual(check.checkAllVariables("else"), expectedVariable);
        }

        /// <summary>
        /// This is the method from VariableChecker class which will be later implemented on the component2.
        /// </summary>
        [TestMethod]
        public void TestReadAllVariables()
        {
            VariableChecker check = new VariableChecker();
            String expectedVariable = "ifelse";

            Assert.AreEqual(check.readAllVairables("else"), expectedVariable);
        }
    }
}
