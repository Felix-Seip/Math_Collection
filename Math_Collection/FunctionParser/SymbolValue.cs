using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionParser
{
    public class SymbolValue : Value
    {
        public SymbolValue(Enums.eOperationSymbol eOperationSymbol) : base(eOperationSymbol)
        {
        }

        public double Evaluate(double val1, double val2)
        {
            switch (m_eSymbol)
            {
                case Enums.eOperationSymbol.Multiplication:
                    return val1 * val2;
                case Enums.eOperationSymbol.Division:
                    return val1 / val2;
                case Enums.eOperationSymbol.Addition:
                    return val1 + val2;
                case Enums.eOperationSymbol.Subtraction:
                    return val1 - val2;
                case Enums.eOperationSymbol.Exponent:
                    return Math.Pow(val1, val2);
                case Enums.eOperationSymbol.sine:
                    return Math.Sin(val2);
                case Enums.eOperationSymbol.cosine:
                    return Math.Cos(val2);
                case Enums.eOperationSymbol.tangent:
                    return Math.Tan(val2);
                case Enums.eOperationSymbol.cotangent:
                    return 1 / Math.Tan(val2);
                case Enums.eOperationSymbol.SquareRoot:
                    return Math.Sqrt(val2);
            }
            return val2;
        }

        public override string ToString()
        {
            switch(m_eSymbol)
            {
                case Enums.eOperationSymbol.ParenthesisOpening:
                    return "(";
                case Enums.eOperationSymbol.ParenthesisClosing:
                    return ")";
                case Enums.eOperationSymbol.Exponent:
                    return "^";
                case Enums.eOperationSymbol.Multiplication:
                    return "*";
                case Enums.eOperationSymbol.Division:
                    return "/";
                case Enums.eOperationSymbol.Addition:
                    return "+";
                case Enums.eOperationSymbol.Subtraction:
                    return "-";
                case Enums.eOperationSymbol.SquareRoot:
                    return "sqrt";
                case Enums.eOperationSymbol.sine:
                    return "sin";
                case Enums.eOperationSymbol.cosine:
                    return "cos";
            }
            return "";
        }
    }
}
