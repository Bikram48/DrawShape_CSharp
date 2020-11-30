using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draw_Shapes
{
    class Triangle
    {
       
        Point v1;
        Point v2;
        Point v3;

        bool getv1 = false;
        bool getv2 = false;
        bool getv3 = false;
       
        public void drawTriangle(Graphics g,int xAxis,int yAxis)
        {
            /* Pen drawingPen = new Pen(Brushes.Black, 3);
             Pen p = new Pen(Color.Red, 4);
             int x1 = 100;
             int y1 = 100;
             int x2 = 300;
             int y2 = 100;
             int x3 = 100;
             int y3 = 400;
             g.DrawLine(p, x1, y1, x2, y2);
             g.DrawLine(p, x1, y1, x3, y3);
            */
            /*
            */
            Pen p = new Pen(Color.Red, 4);
           
                v1 = new Point(xAxis, yAxis);
             v2 = new Point(xAxis, xAxis+90);
              v3 = new Point(xAxis+90, yAxis+90);

            Point[] points = { v1, v2, v3 };

            g.DrawPolygon(p,points);
            

        }
    }
}
