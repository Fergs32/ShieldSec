using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShieldSec.Utils
{
    /// <summary>
    ///  This class contains utility methods for comparing objects and more.
    /// </summary>
    public static class ComparisonUtil
    {
        /// <summary>
        ///  Compares two objects of the same type
        /// </summary>
        /// <typeparam name="T"> The type of the objects to compare </typeparam>
        /// <param name="obj1"> The first object to compare </param>
        /// <param name="obj2"> The second object to compare </param>
        /// <returns></returns>
        public static bool Compare<T>(T obj1, T obj2)
        {
            return obj1.Equals(obj2);
        }
    }
}
