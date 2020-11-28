using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draw_Shapes
{
    public class Rectangle : Shapes
    {
        private int width;
        private int height;


        public Rectangle(Color colour,int xAxis, int yAxis,int width,int height) : base(colour,xAxis,yAxis)
        {
            this.width = width;
            this.height = height;
        }

        public override void draw(Graphics g)
        {
            Pen p = new Pen(Color.Black, 2);
            SolidBrush b = new SolidBrush(colour);
            g.DrawRectangle(p, xAxis, yAxis, width, height);
        }

        public override void drawLine(Graphics g)
        {
            
            Pen p = new Pen(colour, 2);
            SolidBrush b = new SolidBrush(colour);
            g.DrawLine(p, xAxis,xAxis,yAxis,yAxis);
           
        }

    }
}
