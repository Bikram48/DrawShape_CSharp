using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draw_Shapes
{
    /// <summary>
    /// Uses a public visibility modifier
    /// This is a interface class which contains the abstract methods hasNext() and Next() which are unimplemented but
    /// will be later implemented in child class called ErrorIterator.
    /// </summary>
    public interface Iterator
    {
        /// <summary>
        /// This method checks if element has got items in the collection based on an incrementing position number.
        /// This is a abstract method which is implemented in the child class ErrorIterator.
        /// </summary>
        /// <returns>true if collection has items</returns>
        Boolean hasNext();
        /// <summary>
        /// This method returns an object from collection  which can be later casted to an expected type.
        /// This is a abstract method which is implemented in the child class ErrorIterator.
        /// </summary>
        /// <returns>an object from the collection</returns>
        object Next();
    }
}
