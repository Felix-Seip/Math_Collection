using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionParser
{
    class NumberValue : Value
    {
        public double m_dValue;
        public NumberValue(double value) : base(Enums.eOperationSymbol.Number)
        {
            m_dValue = value;
        }

        public override string ToString()
        {
            return "" + m_dValue;
        }
    }
}
