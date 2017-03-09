using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionParser
{
    public class Value
    {
        public Enums.eOperationSymbol m_eSymbol { private set; get; }

        public Value(Enums.eOperationSymbol operation)
        {
            m_eSymbol = operation;
        }

    }
}
