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
            this.radius = radius;
        }
        /// <summary>
        /// Overrides the set method of base class.
        /// It sets the values for the properties
        /// </summary>
        /// <param name="colour">Pen color</param>
        /// <param name="fillOn">color fill on/off</param>
        /// <param name="list">xAxis,yAxis and Radius</param>
        public override void set(Color colour, Boolean fillOn, params int[] list)
        {
            base.set(colour, fillOn, list[0], list[1]);
            this.radius = list[2];
        }
        /// <summary>
        /// Uses the public visibility modifier to give access to other classes also.
        /// It draws the circle in canvas using DrawEllipse method
        /// </summary>
        /// <param name="g"></param>
        public override void draw(Graphics g)
        {
            //creates the pen 
            Pen p = new Pen(Color.Red, 2);
            //draw the circle
            g.DrawEllipse(p, xAxis-radius, yAxis-radius,radius+radius, radius+radius);
        }

        public override void drawLine(Graphics g)
        {
            throw new NotImplementedException();
        }
    }
}
