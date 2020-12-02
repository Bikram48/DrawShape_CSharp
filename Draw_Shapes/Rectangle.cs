using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Draw_Shapes
{
    public class Rectangle : Shapes
    {
        private int width;
        private int height;


        public Rectangle(Color colour,int xAxis, int yAxis,Boolean fillOn,int width,int height) : base(colour,xAxis,yAxis,fillOn)
        {
            this.width = width;
            this.height = height;
        }

        public override void draw(Graphics g)
        {
            if (fillOn==true)
            {
                if (CommandChecker.isPen == true)
                {
                    SolidBrush sb = new SolidBrush(colour);
                    g.FillRectangle(sb, xAxis, yAxis, width, height);
                }
                else
                {
                    SolidBrush sb = new SolidBrush(Color.Black);
                    g.FillRectangle(sb, xAxis, yAxis, width, height);
                }
                
            }
            else
            {
                if (CommandChecker.isPen == true)
                {
                    Pen p = new Pen(colour, 2);
                    g.DrawRectangle(p, xAxis, yAxis, width, height);
                }
                else
                {
                    Pen p = new Pen(Color.Black, 2);
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
