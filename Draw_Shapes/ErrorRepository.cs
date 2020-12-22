using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draw_Shapes
{
    public class ErrorRepository : Container
    {
        public static List<String> errorsList = new List<String>();
        public Iterator getIterator()
        {
            return new ErrorIterator();
        }
        private class ErrorIterator : Iterator
        {
            int index;
            public bool hasNext()
            {
             
                if (index < errorsList.Count)
                {
                    return true;
                }
                return false;
            }

            public object Next()
            {
                if (this.hasNext())
                {
                    return errorsList[index++];
                }
                return null;
            }
        }
    }
}
