using Math_Collection.LinearAlgebra.Matrices;
using Math_Collection.LinearAlgebra.Vectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math_Collection.Basics
{
    public class Basics
    {
        public static double DegreesToRadians(double degrees)
        {
            return (degrees / 180) * Math.PI;
        }

        public static int FibonacciSequence(int iRepititions)
        {
            if (iRepititions == 1)
            {
                return 1;
            }
            else if (iRepititions == 0)
            {
                return iRepititions;
            }
            else
            {
                return FibonacciSequence(iRepititions - 1) + FibonacciSequence(iRepititions - 2);
            }
            
        }
    }
}
