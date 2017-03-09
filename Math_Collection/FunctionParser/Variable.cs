using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionParser
{
    class Variable : NumberValue
    {
        public string m_sVariableName;
        public Variable(string sVariableName, double value) : base(value)
        {
            m_sVariableName = sVariableName;
        }

        public void ParseVariable(double valueToReplace = 0, bool eulerConstant = false)
        {
            if (!eulerConstant)
            {
                string[] splitValues;
                if (m_sVariableName.Contains("^"))
                {
                    splitValues = m_sVariableName.Split('^');
                    m_dValue = Math.Pow(valueToReplace, double.Parse(splitValues[1]));
                }
                else
                {
                    m_dValue = valueToReplace;
                }
            }
            else
            {
                string[] splitValues;
                if (m_sVariableName.Contains("^"))
                {
                    splitValues = m_sVariableName.Split('^');
                    if (splitValues[1].Equals("x", StringComparison.InvariantCultureIgnoreCase) ||
                        splitValues[1].Equals("y", StringComparison.InvariantCultureIgnoreCase))
                    {
                        m_dValue = Math.Pow(Math.E, valueToReplace);
                    }
                    else
                    {
                        m_dValue = Math.Pow(Math.E, double.Parse(splitValues[1]));
                    }
                }
                else
                {
                    m_dValue = Math.E;
                }
            }
        }

        public override string ToString()
        {
            return m_sVariableName;
        }
    }
}
