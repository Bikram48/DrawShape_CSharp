using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draw_Shapes
{
    class Circle:Shapes
    {
        private int radius;
        public Circle(Color colour, int xAxis, int yAxis, Boolean fillOn,int radius):base(colour,xAxis,yAxis,fillOn)
        {
            this.radius = radius;
        }
        public override void draw(Graphics g)
        {
            throw new NotImplementedException();
        }

        public override void drawLine(Graphics g)
        {
            throw new NotImplementedException();
        }
    }
}
