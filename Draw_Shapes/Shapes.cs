using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draw_Shapes
{
    public abstract class Shapes
    {
        protected Color colour;
        protected int xAxis, yAxis;
        protected Boolean fillOn;

        public Shapes(Color colour,int xAxis,int yAxis,Boolean fillOn)
        {
            this.colour = colour;
            this.xAxis = xAxis;
            this.yAxis = yAxis;
            this.fillOn = fillOn;
        }

        public abstract void draw(Graphics g);
        public abstract void drawLine(Graphics g);
      
    }
}
