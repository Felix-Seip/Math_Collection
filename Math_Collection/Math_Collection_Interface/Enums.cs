using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math_Collection_Interface
{
    public class Enums
    {
        public enum MathOperations 
        {
            [Description("Degrees To Radians")]
            BasicsDegreesToRadians = 0,

            [Description("Dot Product")]
            VectorDotProduct = 1,
            [Description("Magnitude")]
            VectorMagnitude = 2,
            [Description("Addition With Vector")]
            VectorAdditionWithVector = 3,
            [Description("Subtraction With Vector")]
            VectorSubtractionWithVector = 4,
            [Description("Multiplication With Vector")]
            VectorMultiplicationWithVector = 5,
            [Description("Multiplication With Scalar")]
            VectorMultiplicationWithScalar = 6,

            [Description("Transpose Matrix")]
            MatrixTranspose = 7,
            [Description("Multiply With Scalar")]
            MatrixMultiplicationWithScalar = 8,
            [Description("Multiply With Vector")]
            MatrixMultiplicationWithVector = 9,
            [Description("Multiply With Matrix")]
            MatrixMultiplicationWithMatrix = 10,
            [Description("Potentiate Matrix")]
            MatrixPotentiation = 11,
            [Description("Lower Left Pyramid Form")]
            MatrixLowerLeftPyramidForm = 12,
            [Description("Upper Right Pyramid Form")]
            MatrixUpperRightPyramidForm = 13,

            [Description("Derivation of a Function")]
            AnalysisDerivationOfFunction = 14
        }
    }
}
