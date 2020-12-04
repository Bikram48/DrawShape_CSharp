using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draw_Shapes
{
    /// <summary>
    /// This is the design patterns which is used to create the objects of the shapes
    /// which are passed as a parameter in the method.
    /// </summary>
    public class ShapeFactory
    {
        /// <summary>
        /// Checks if the shapes are existed and creates the object of the shapes if they are existed.
        /// </summary>
        /// <param name="shapeType">Shapes</param>
        /// <returns></returns>
        public Shapes checkShapes(String shapeType)
        {
            //makes the shapetype passed as a parameter into the uppercase letter and also removes the whitespace using trim method
            shapeType = shapeType.ToUpper().Trim(); 

            //if shapeType is cricle then returns the object of it
            if (shapeType.Equals("CIRCLE"))
            {
                return new Circle();

            }
            //if shapeType is rectangle then returns the object of it
            else if (shapeType.Equals("RECTANGLE"))
            {
                return new Rectangle();

            }
            //if shapeType is triangle then returns the object of it
            else if (shapeType.Equals("TRIANGLE"))
            {
                return new Triangle();
            }
            //throws an exception if invalid shape are found.
            else
            {
                //if we get here then what has been passed in is inkown so throw an appropriate exception
                System.ArgumentException argEx = new System.ArgumentException("Factory error: " + shapeType + " does not exist");
                throw argEx;
            }            
            return null;
        }
    }
}
