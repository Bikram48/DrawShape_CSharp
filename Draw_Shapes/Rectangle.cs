using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Draw_Shapes
{
    /// <summary>
    /// This class is extended from the Shape abstract class.
    /// It implements the abstract method of base class Shapes.
    /// It also contains the additional properties which will be required to draw a rectangle.
    /// </summary>
    public class Rectangle : Shapes
    {
        /// <summary>
        /// Uses private modifier to give access inside this class only.
        /// Stores the width of rectangle.
        /// </summary>
        private int width;
        /// <summary>
        /// Uses private modifier to give access inside this class only.
        /// Stores the height of the rectangle.
        /// </summary>
        private int height;
        /// <summary>
        /// Default constructor declaired.
        /// </summary>
        public Rectangle():base()
        {
          
        }
        /// <summary>
        /// Parameterized constructor is declaired to the set the value of properties.
        /// </summary>
        /// <param name="colour">Pen color</param>
        /// <param name="xAxis">xAxis of moveTo command</param>
        /// <param name="yAxis">yAxis of moveTo command</param>
        /// <param name="fillOn">color fill on/off</param>
        /// <param name="width">Rectangle width</param>
        /// <param name="height">Rectangle height</param>
        public Rectangle(Color colour,int xAxis, int yAxis,Boolean fillOn,int width,int height) : base(colour,xAxis,yAxis,fillOn)
        {
            this.width = width;
            this.height = height;
        }
        /// <summary>
        /// Overrides the set method of base class.
        /// It sets the value of properties.
        /// </summary>
        /// <param name="colour">Pen color</param>
        /// <param name="fillOn">color fill on/off</param>
        /// <param name="list">xAxis,yAxis,width and height</param>
        public override void set(Color colour,Boolean fillOn,params int[] list)
        {
            base.set(colour, fillOn,list[0],list[1]);
            this.width = list[2];
            this.height = list[3];
        }
        /// <summary>
        /// Uses the public visibility modifier to give access to other classes also.
        /// This method draws the rectangle in canvas.
        /// </summary>
        /// <param name="g">Graphics reference</param>
        public override void draw(Graphics g)
        {
            //if fill command is executed then this block of code will get executed.
            if (fillOn==true)
            {
                //checks if pen command is entered. If entered then the back color will be set with the same color of pen.
                if (CommandChecker.isPen == true)
                {
                    SolidBrush sb = new SolidBrush(colour);
                    //fills the rectangle with color sent by the user.
                    g.FillRectangle(sb, xAxis, yAxis, width, height);
                }
                else
                {
                    SolidBrush sb = new SolidBrush(Color.Black);
                    //fills the rectangle with black color.
                    g.FillRectangle(sb, xAxis, yAxis, width, height);
                }
                
            }
            //if fill command isn't entered then this code will get executed.
            else
            {
                //if pen has a color then it gets executed.
                if (CommandChecker.isPen == true)
                {
                    //makes the pen
                    Pen p = new Pen(colour, 2);
                    //draws the rectangle in canvas
                    g.DrawRectangle(p, xAxis, yAxis, width, height);
                }
                //if pen doesn't have a color then the default color of pen will be black.
                else
                {
                    Pen p = new Pen(Color.Black, 2);
                    //draws the rectangle in canvas.
                    g.DrawRectangle(p, xAxis, yAxis, width, height);
                }
            }
        }

       
        public override void drawLine(Graphics g)
        {
            
            Pen p = new Pen(colour, 2);
            SolidBrush b = new SolidBrush(colour);
            g.DrawLine(p, xAxis,xAxis,yAxis,yAxis);
           
        }

    }
}
