using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draw_Shapes
{
    /// <summary>
    /// This class contains the properties and methods to draw a triangle in the canvas.
    /// Triangle class is extends the Shapes abstract class and it implements all the
    /// abstract methods here.
    /// </summary>
    class Triangle:Shapes
    {
        /// <summary>
        /// Local variables are declaired to store the points to draw a triangle
        /// </summary>
        Point point1,point2,point3;
       
        //A default construtor
        public Triangle() : base()
        {

        }

      /// <summary>
      /// Sets the properties on parent abstract class Shapes.
      /// </summary>
      /// <param name="colour">Pen color</param>
      /// <param name="fillOn">To fill the shapes with color</param>
      /// <param name="list">xAxis,yAxis</param>      

        public override void set(Color colour, Boolean fillOn, params int[] list)
        {
            base.set(colour, fillOn, list[0], list[1]);
        }

        /// <summary>
        /// Draws the triangle in canvas.
        /// Overrides the draw method of base abstract class Shapes.
        /// </summary>
        /// <param name="g">Graphics</param>
        public override void draw(Graphics g)
        {
            //checks if fill command is on
            if (fillOn == true)
            {
                if (CommandChecker.isPen == true)
                {
                    SolidBrush sb = new SolidBrush(colour);
                   // g.FillRectangle(sb, xAxis, yAxis, width, height);
                }
                else
                {
                    SolidBrush sb = new SolidBrush(Color.Black);
                    //g.FillRectangle(sb, xAxis, yAxis, width, height);
                }

            }
            //if fill is off then this command executes
            else
            {
                //if pen command is get executed
                if (CommandChecker.isPen == true)
                {
                    //makes a pen with the color passed by the user 
                    Pen p = new Pen(colour, 4);
                    //making the points for a trianlgle
                    point1 = new Point(xAxis,yAxis);
                    point2 = new Point(xAxis, xAxis + 90);
                    point3 = new Point(xAxis + 90, yAxis + 90);
                    //storing the points into an array
                    Point[] points = { point1, point2, point3 };
                    //draws the polygon in canvas
                    g.DrawPolygon(p, points);
                  
                }
                //if pen command isn't executed
                else
                {
                    //makes a pen with the black color
                    Pen p = new Pen(Color.Black, 4);
                    //making the points for a trianlgle
                    point1 = new Point(xAxis, yAxis);
                    point2 = new Point(xAxis, xAxis + 90);
                    point3 = new Point(xAxis + 90, yAxis + 90);
                    //storing the points into an array
                    Point[] points = { point1, point2, point3 };
                    //draws the polygon in canvas
                    g.DrawPolygon(p, points);
                    // g.DrawRectangle(p, xAxis, yAxis, width, height);
                }
            }
        }

        public override void drawLine(Graphics g)
        {
            throw new NotImplementedException();
        }
    }
}
