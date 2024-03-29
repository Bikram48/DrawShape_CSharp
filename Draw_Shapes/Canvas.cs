﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Draw_Shapes
{
    /// <summary>
    /// This <c>Draw_Shapes</c> class contains a various types of methods to draw a shapes in a canvas.
    /// This class first checks if the shape exists on the system through ShapeFactory class. If exist then it makes
    /// the object of that shape and calls the draw method to draw the shapes.
    /// The methods in these class takes the parameters which will be needed to draw the
    /// shapes and it passed those parameter to shape classes (rectangle,Triangle,Circle) to draw the shapes.
    /// </summary>
    ///<remarks>
    ///This class get the parameters like colour,xAxsis,yAxis,g which will be appropriate for all the shapes.
    /// </remarks>
    class Canvas
    { 
        /// <summary>
        /// Instantiates an object of ShapeFacotry class.
        /// ShapeFactory is a factory design pattern which returns the object of the shape if existed.
        /// </summary>
        ShapeFactory shape = new ShapeFactory();
        /// <summary>
        /// If the shape is rectangle then this method helps to get the parameters requires 
        /// for the rectangle class.
        /// </summary>
        /// <param name="colour">Pen color</param>
        /// <param name="xAxis">xAxis from moveTo command</param>
        /// <param name="yAxis">yAxis from moveTo command</param>
        /// <param name="fillOn">color fill on/off</param>
        /// <param name="width">Rectangle widht</param>
        /// <param name="height">Rectangle height</param>
        /// <param name="g">Graphics reference</param>
        public void drawRectangle(Color colour, int xAxis, int yAxis, Boolean fillOn,Boolean isPen, int width, int height, Graphics g)
        {
            if (!DrawAllShapes.syntaxCheckerClicked)
            {
                //Creates the object of rectangle class by passing a rectangle text as a parameter into the ShapeFactory class
                Shapes s1 = shape.checkShapes("rectangle");
                //passing the parameter into Shapes abstract class to draw the rectangle
                s1.set(colour, fillOn,isPen, xAxis, yAxis, width, height);
                //sends the reference of Graphcs to draw the rectangle in panel
                s1.draw(g);
            }
        }

        /// <summary>
        /// If the shape is circle then this method helps to get the parameters requires 
        /// for the circle class.
        /// </summary>
        /// <param name="colour">Pen color</param>
        /// <param name="xAxis">xAxis from moveTo command</param>
        /// <param name="yAxis">yAxis from moveTo command</param>
        /// <param name="fillOn">color fill on/off</param>
        /// <param name="radius">Circle radius</param>
        /// <param name="g">Graphics reference</param>
        public void drawCircle(Color colour,int xAxis,int yAxis,Boolean fillOn,Boolean isPen,int radius,Graphics g)
        {
            if (!DrawAllShapes.syntaxCheckerClicked)
            {
                 //Creates the object of circle class by passing a circle text as a parameter into the ShapeFactory class
                Shapes s2 = shape.checkShapes("circle");
                //passing the parameter into Shapes abstract class to draw the circle
                s2.set(colour, fillOn,isPen, xAxis, yAxis, radius);
                //sends the reference of Graphcs to draw the rectangle in panel
                s2.draw(g);
            }  
        }

        /// <summary>
        /// If the shape is triangle then this method helps to get the parameters requires 
        /// for the triangle class.
        /// </summary>
        /// <param name="colour">Pen color</param>
        /// <param name="xAxis">xAxis from moveTo command</param>
        /// <param name="yAxis">yAxis from moveTo command</param>
        /// <param name="fillOn">color fill on/off</param>
        /// <param name="g">Graphics reference</param>
        public void drawTriangle(Color colour, int xAxis, int yAxis, Boolean fillOn,Boolean isPen, Graphics g)
        {
            if (!DrawAllShapes.syntaxCheckerClicked)
            {
                //Creates the object of triangle class by passing a triangle text as a parameter into the ShapeFactory class
                Shapes s3 = shape.checkShapes("triangle");
                //passing the parameter into Shapes abstract class to draw the circle
                s3.set(colour, fillOn,isPen, xAxis, yAxis);
                //sends the reference of Graphcs to draw the rectangle in panel
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
        public void drawLine(Color colour, int xAxis, int yAxis, Boolean isPen, int point1, int point2, Graphics g)
        {
            if (!DrawAllShapes.syntaxCheckerClicked)
            {
                //if pen is set from the commandline then this block will get executed
                if (isPen == true)
                {
                    //creates the pen
                    Pen p = new Pen(colour, 2);
                    //draws the line with the color passed by the user.
                    g.DrawLine(p, point1, point2, xAxis, yAxis);
                }
                //if pen is not set then this block will get executed
                else
                {
                    //creates the pen
                    Pen p = new Pen(Color.Black, 2);
                    //draws the line with the black color.
                    g.DrawLine(p, point1, point2, xAxis, yAxis);
                }
            }
            //end of if block
        }

        public void drawPolygon(Color colour, int xAxis, int yAxis, Boolean fillOn, Boolean isPen, Graphics g,String[] points)
        {
            int[] point=new int[points.Length];
            int index = 0;
            foreach(String item in points)
            {
                point[index] = Convert.ToInt32(item);
                index++;
            }
            Shapes s3 = shape.checkShapes("polygon");
            s3.set(colour, fillOn, isPen, point);
            s3.draw(g);
        }
    }
}
