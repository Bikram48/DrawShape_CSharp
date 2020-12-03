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
    /// This class contains a various types of methods to draw a shapes in a canvas.
    /// This class first checks if the shape exists on the system through ShapeFactory class. If exist then it makes
    /// the object of that shape and calls the draw method to draw the shapes.
    /// The methods in these class takes the parameters which will be needed to draw the
    /// shapes and it passed those parameter to shape classes (rectangle,Triangle,Circle) to draw the shapes.
    /// </summary>
    class Canvas
    {
        /// <summary>
        /// Instantiates an object of ShapeFacotry shape.
        /// </summary>
        ShapeFactory shape = new ShapeFactory();
        /// <summary>
        /// If the shape is rectangle then this method helps to get the parameters requires 
        /// fro the rectangle class.
        /// </summary>
        /// <param name="colour">Pen color</param>
        /// <param name="xAxis">xAxis from moveTo command</param>
        /// <param name="yAxis">yAxis from moveTo command</param>
        /// <param name="fillOn">color fill on/off</param>
        /// <param name="width">Rectangle widht</param>
        /// <param name="height">Rectangle height</param>
        /// <param name="g">Graphics reference</param>
        public void drawRectangle(Color colour, int xAxis, int yAxis, Boolean fillOn, int width, int height, Graphics g)
        {
            if (DrawAllShapes.syntaxCheckerClicked == false)
            {
                //Creates the object of rectangle class.
                Shapes s1 = shape.checkShapes("rectangle");
                s1.set(colour, fillOn, xAxis, yAxis, width, height);
                s1.draw(g);
            }
           
           
        }

        /// <summary>
        /// If the shape is circle then this method helps to get the parameters requires 
        /// fro the circle class.
        /// </summary>
        /// <param name="colour">Pen color</param>
        /// <param name="xAxis">xAxis from moveTo command</param>
        /// <param name="yAxis">yAxis from moveTo command</param>
        /// <param name="fillOn">color fill on/off</param>
        /// <param name="radius">Circle radius</param>
        /// <param name="g">Graphics reference</param>
        public void drawCircle(Color colour,int xAxis,int yAxis,Boolean fillOn,int radius,Graphics g)
        {
            if (DrawAllShapes.syntaxCheckerClicked == false)
            {
                //Creates the object of circle class.
                Shapes s2 = shape.checkShapes("circle");
                s2.set(colour, fillOn, xAxis, yAxis, radius);
                s2.draw(g);
            }
        }

        /// <summary>
        /// If the shape is triangle then this method helps to get the parameters requires 
        /// fro the triangle class.
        /// </summary>
        /// <param name="colour">Pen color</param>
        /// <param name="xAxis">xAxis from moveTo command</param>
        /// <param name="yAxis">yAxis from moveTo command</param>
        /// <param name="fillOn">color fill on/off</param>
        /// <param name="g">Graphics reference</param>
        public void drawTriangle(Color colour, int xAxis, int yAxis, Boolean fillOn, Graphics g)
        {
            if (DrawAllShapes.syntaxCheckerClicked == false)
            {
                //Creates the object of triangle class.
                Shapes s3 = shape.checkShapes("triangle");
                s3.set(colour, fillOn, xAxis, yAxis);
                s3.draw(g);
            }
        }

        /// <summary>
        /// Uses public visibility modifier to give access to other classes also.
        /// Draws the line when the drawTo command is executed in the commandLine.
        /// </summary>
        /// <param name="colour">Pen color</param>
        /// <param name="xAxis">xAxis from moveTo command</param>
        /// <param name="yAxis">yAxis from moveTo command</param>
        /// <param name="fillOn">color fill on/off</param>
        /// <param name="point1">First point</param>
        /// <param name="point2">Second point</param>
        /// <param name="g">Graphics reference</param>
        public void drawLine(Color colour, int xAxis, int yAxis, Boolean fillOn, int point1, int point2, Graphics g)
        {
            if (DrawAllShapes.syntaxCheckerClicked == false)
            {
                if (CommandChecker.isPen == true)
                {
                    Pen p = new Pen(colour, 2);
                    //draws the line with the color passed by the user.
                    g.DrawLine(p, point1, point2, xAxis, yAxis);
                }
                else
                {
                    Pen p = new Pen(Color.Black, 2);
                    //draws the line with the black color.
                    g.DrawLine(p, point1, point2, xAxis, yAxis);
                }
            }

          
        }

       
    }
}
