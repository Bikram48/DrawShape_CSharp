using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draw_Shapes
{
    /// <summary>
    /// Abstract class which contains abstract methods and non-abstract methods.
    /// Abstract methods are later on implemented on the child classes and also child classes
    /// overrides the method of this base class.
    /// This class also contains some properties which will be used by the child classes.
    /// </summary>
    public abstract class Shapes
    {
        /// <summary>
        /// Uses the protected access modifier to give access to the child classes.
        /// stores the pen color.
        /// </summary>
        protected Color colour;
        /// <summary>
        /// Uses the protected access modifier to give access to the child classes.
        /// It takes the xAxis from the moveto command
        /// </summary>
        protected int xAxis;
        /// <summary>
        /// Uses the protected access modifier to give access to the child classes.
        /// It takes the yAxis from the moveto command
        /// </summary>
        protected int yAxis;
        /// <summary>
        /// Uses the protected access modifier to give access to the child classes.
        /// returns true if fill is on if not then it returns false.
        /// </summary>
        protected Boolean fillOn;
        protected Boolean isPen;

        /// <summary>
        /// A default constructor is declaired
        /// </summary>
        public Shapes()
        {

        }
        /// <summary>
        /// Parameterized constructor is declaired to set the properties.
        /// </summary>
        /// <param name="colour">Pen color</param>
        /// <param name="xAxis">xAxis of moveTo</param>
        /// <param name="yAxis">yAxis of moveTo</param>
        /// <param name="fillOn">color fill on/off</param>
        public Shapes(Color colour, int xAxis, int yAxis, Boolean fillOn)
        {
            this.colour = colour;
            this.xAxis = xAxis;
            this.yAxis = yAxis;
            this.fillOn = fillOn;
        }

        /// <summary>
        /// It is a abstract method which will be used to draw the shapes in canvas.
        /// It will be implemented on the child classes.
        /// </summary>
        /// <param name="g">Graphics reference</param>
        public abstract void draw(Graphics g);

        /// <summary>
        /// It sets the properties of this class.
        /// </summary>
        /// <param name="colour">Pen color</param>
        /// <param name="fillOn">color fill on/off</param>
        /// <param name="list">array which store xAxis and yAxis</param>
        public virtual void set(Color colour, bool fillOn, bool isPen, params int[] list)
        {
            //sets the color property
            this.colour = colour;
            //sets the xAxis property
            this.xAxis = list[0];
            //sets the yAxis property
            this.yAxis = list[1];
            //sets the fillOn property
            this.fillOn = fillOn;
            this.isPen = isPen;
        }

        /// <summary>
        /// This method is created for the testing purpose
        /// </summary>
        /// <param name="xAxis"></param>
        public void setX(int xAxis)
        {
            this.xAxis = xAxis;
        }

        /// <summary>
        /// This method is created for the testing purpose
        /// </summary>
        /// <returns></returns>
        public int getX()
        {
            return this.xAxis;
        }

    }
}
