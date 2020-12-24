using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draw_Shapes
{
    /// <summary>
    /// This is the interface which methods are implemented in child class called ErrorRepository
    /// </summary>
    public interface Container
    {
        Iterator getIterator();
    }
}
