using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionParser
{
    public class Enums
    {
        public enum eOperationSymbol
        {
            Division = 1,
            Multiplication = 2,
            Addition = 3,
            sine = 4,
            cosine = 5,
            tangent = 6,
            cotangent = 7,
            secant = 8,
            cosecant = 9,
            Power = 10,
            ParenthesisOpening = 11,
            ParenthesisClosing = 12,
            Subtraction = 13,
            SquareRoot = 14,
            Number = 15,
            Exponent = 16
        }

        public enum eRecursionStep
        {
            eRecursionStepMain = 1,
            eRecursionStepFurther = 2
        }

        public enum eValueType
        {
            eConstant = 1,
            eVariable = 2
        }
    }
}
