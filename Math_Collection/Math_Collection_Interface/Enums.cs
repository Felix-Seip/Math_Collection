using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math_Collection_Interface
{
    public class Enums
    {
        public enum MathOperations
        {
            BasicsDegreesToRadians = 0,

            VectorDotProduct = 1,
            VectorMagnitude = 2, 
            VectorAdditionWithVector = 3,
            VectorSubtractionWithVector = 4,
            VectorMultiplicationWithVector = 5,
            VectorMultiplicationWithScalar = 6,

            MatrixTranspose = 7,
            MatrixMultiplicationWithScalar = 8,
            MatrixMultiplicationWithVector = 9,
            MatrixMultiplicationWithMatrix = 10,
            MatrixLowerLeftPyramidForm = 11,
            MatrixUpperRightPyramidForm = 12,

            AnalysisDerivationOfFunction = 13
        }
    }
}
