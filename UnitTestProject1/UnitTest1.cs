using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Draw_Shapes;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            CommandChecker checker = new CommandChecker();
            String expected = "rectangle";
            String text = "rectangle 100,100";

            String[] splitter=checker.splitTextBySpace(text);
            Assert.AreEqual(splitter[0], expected);
        }
        [TestMethod]
        public void TestMethod2()
        {
            CommandChecker checker = new CommandChecker();
            String expected = "10";
            String text = "10,20";
            String[] splitter=checker.splitParameterByComma(text);
            Assert.AreEqual(splitter[0], expected);

        }

        [TestMethod]
        public void TestMethod3()
        {
            ShapeFactory s1 = new ShapeFactory();
            Shapes shape1 = s1.checkShapes("rectangle");
            shape1.setX(11);
            Assert.AreEqual(shape1.getX(),11);
        }
    }
}
