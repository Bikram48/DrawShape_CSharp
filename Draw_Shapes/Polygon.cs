using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draw_Shapes
{
    public class Polygon : Shapes
    {
        int[] parameters;
        /// <summary>
        /// Local variables are declaired to store the points to draw a triangle
        /// </summary>
        Point point1, point2, point3;

        //A default construtor
        public Polygon() : base()
        {

        }

        /// <summary>
        /// Sets the properties on parent abstract class Shapes.
        /// </summary>
        /// <param name="colour">Pen color</param>
        /// <param name="fillOn">To fill the shapes with color</param>
        /// <param name="list">xAxis,yAxis</param>      

        public override void set(Color colour, Boolean fillOn, Boolean isPen, params int[] list)
        {
            base.set(colour, fillOn, isPen, list[0], list[1]);
            parameters = list;
        }

        public override void draw(Graphics g)
        {
          
            Point[] polygonPoints = new Point[parameters.Length / 2];
            int index = 0;
            for(int i = 0; i < parameters.Length; i += 2)
            {
                polygonPoints[index] = new Point(parameters[i], parameters[i + 1]);
                index++;
            }
        
            //checks if fill command is on
            if (fillOn == true)
            {
                if (isPen == true)
                {
                    //makes a pen with the color passed by the user 
                    Pen p = new Pen(colour, 4);
                    //making the points for a trianlgle

                    SolidBrush sb = new SolidBrush(colour);
                    g.FillPolygon(sb, polygonPoints);
                }
                else
                {
                    SolidBrush sb = new SolidBrush(Color.Black);
                    //makes a pen with the color passed by the user 
                    Pen p = new Pen(colour, 4);
                   
                    g.FillPolygon(sb, polygonPoints);
                }

            }
            //if fill is off then this command executes
            else
            {
                //if pen command is get executed
                if (isPen == true)
                {
                    //makes a pen with the color passed by the user 
                    Pen p = new Pen(colour, 4);
                   
                    g.DrawPolygon(p, polygonPoints);

                }
                //if pen command isn't executed
                else
                {
                    //makes a pen with the black color
                    Pen p = new Pen(Color.Black, 4);
                  
                    g.DrawPolygon(p, polygonPoints);
                    // g.DrawRectangle(p, xAxis, yAxis, width, height);
                }
            }
        }
    }
    
}
