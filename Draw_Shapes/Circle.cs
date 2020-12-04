using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draw_Shapes
{
    /// <summary>
    /// This class is extended from the Shape abstract class.
    /// It implements the abstract method of base class Shapes.
    /// It also contains the additional properties which will be required to draw a circle.
    /// </summary>
    class Circle :Shapes
    {
        /// <summary>
        ///  Uses private modifier to give access inside this class only.
        ///  It stores the radius of the circle.
        /// </summary>
        private int radius;
        /// <summary>
        /// Default constructor
        /// </summary>
        public Circle() : base()
        {

        }

        /// <summary>
        /// Parameterized constructor is declaired to the set the value of properties.
        /// </summary>
        /// <param name="colour">Pen color</param>
        /// <param name="xAxis">xAxis of moveTo command</param>
        /// <param name="yAxis">yAxis of moveTo command</param>
        /// <param name="fillOn">color fill on/off</param>
        /// <param name="radius">Circle radius</param>
        public Circle(Color colour, int xAxis, int yAxis, Boolean fillOn,int radius):base(colour,xAxis,yAxis,fillOn)
        {
            //sets the radius of circle
            this.radius = radius;
        }

        /// <summary>
        /// Overrides the set method of base class.
        /// It sets the values for the properties
        /// </summary>
        /// <param name="colour">Pen color</param>
        /// <param name="fillOn">color fill on/off</param>
        /// <param name="list">xAxis,yAxis and Radius</param>
        public override void set(Color colour, Boolean fillOn,Boolean isPen, params int[] list)
        {
            //passing values into the base class method set
            base.set(colour, fillOn,isPen, list[0], list[1]);
            //sets the radius of the circle
            this.radius = list[2];
        }

        /// <summary>
        /// Uses the public visibility modifier to give access to other classes also.
        /// It draws the circle in canvas using DrawEllipse method
        /// </summary>
        /// <param name="g"></param>
        public override void draw(Graphics g)
        {
            //if fill command is executed then this block of code will get executed.
            if (fillOn == true)
            {
                //checks if pen command is entered. If entered then the back color will be set with the same color of pen.
                if (CommandChecker.isPen == true)
                {
                    //brush is created to fill the circle by color.
                    SolidBrush sb = new SolidBrush(colour);
                    //fills the rectangle with color sent by the user.
                    g.FillEllipse(sb, xAxis - radius, yAxis - radius, radius + radius, radius + radius);
                }
                else
                {
                    //brush is created to fill the circle by color.
                    SolidBrush sb = new SolidBrush(Color.Black);
                    //fills the rectangle with black color.
                    g.FillEllipse(sb, xAxis - radius, yAxis - radius, radius + radius, radius + radius);
                }
            }
            else
            {
                //if pen has a color then it gets executed.
                if (CommandChecker.isPen == true)
                {
                    //makes the pen
                    Pen p = new Pen(colour, 2);
                    //draws the rectangle in canvas
                    g.DrawEllipse(p, xAxis - radius, yAxis - radius, radius + radius, radius + radius);
                }
                //if pen doesn't have a color then the default color of pen will be black.
                else
                {
                    //creates the pen
                    Pen p = new Pen(Color.Black, 2);
                    //draws the rectangle in canvas.
                    g.DrawEllipse(p, xAxis - radius, yAxis - radius, radius + radius, radius + radius);
                }
            }         
        }
    }
}
