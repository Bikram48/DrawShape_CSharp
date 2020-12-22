using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draw_Shapes
{
    public interface Iterator
    {
        Boolean hasNext();
        object Next();
    }
}
