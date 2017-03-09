using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionParser
{
    public class Function
    {
        public List<Value> valueList; //Contains Following Operation and value
        public Function firstDerivative { protected set; get; }
        protected string m_sOriginalFunctionString { set; get; }

        public Function(string sOriginalFunction, Function firstDerivative = null)
        {
            m_sOriginalFunctionString = sOriginalFunction;
            this.firstDerivative = firstDerivative;
            if (firstDerivative != null)
            {
                firstDerivative.valueList.Remove(firstDerivative.valueList[firstDerivative.valueList.Count - 1]);
            }
            valueList = new List<Value>();
        }

        public double Solve(double x, out int endIndex, double y = 0, int index = 0)
        {
            double result = 0;
            double returnValue = 0;
            double tempReturnValue = 0; 
            bool bNatural0 = false;
            endIndex = 0;

            while (index < valueList.Count)
            {
                if (valueList[index].GetType() == typeof(SymbolValue))
                {
                    if (((SymbolValue)valueList[index]).m_eSymbol == Enums.eOperationSymbol.ParenthesisOpening ||
                        ((SymbolValue)valueList[index]).m_eSymbol == Enums.eOperationSymbol.SquareRoot         )
                    {
                        double recursionResult = Solve(x, out endIndex, y, index + 1);
                        if (returnValue == 0)
                        {
                            returnValue = result;
                        }

                        tempReturnValue = returnValue;

                        if ((((SymbolValue)valueList[index]).m_eSymbol != Enums.eOperationSymbol.ParenthesisOpening ||
                            ((SymbolValue)valueList[index]).m_eSymbol != Enums.eOperationSymbol.ParenthesisClosing))
                        {
                            if (index - 1 != -1)
                            {
                                returnValue = ((SymbolValue)valueList[index - 1]).Evaluate(result, recursionResult);
                            }
                            else
                            {
                                returnValue = ((SymbolValue)valueList[index]).Evaluate(result, recursionResult);
                            }

                            if (index - (endIndex - index) < valueList.Count && index - (endIndex - index) > 0)
                            {
                                if (valueList[index - (endIndex - index)].GetType() == typeof(SymbolValue))
                                {
                                    if( ((SymbolValue)valueList[index - (endIndex - index)]).m_eSymbol == Enums.eOperationSymbol.Addition       ||
                                        ((SymbolValue)valueList[index - (endIndex - index)]).m_eSymbol == Enums.eOperationSymbol.Subtraction    ||
                                        ((SymbolValue)valueList[index - (endIndex - index)]).m_eSymbol == Enums.eOperationSymbol.Multiplication ||
                                        ((SymbolValue)valueList[index - (endIndex - index)]).m_eSymbol == Enums.eOperationSymbol.Division)
                                    {
                                        returnValue = ((SymbolValue)valueList[index - (endIndex - index)]).Evaluate(tempReturnValue, returnValue);
                                    }
                                }
                            }
                        }

                        index += endIndex;
                    }
                    else if (index + 1 != valueList.Count)
                    {
                        if (valueList[index + 1].GetType() == typeof(NumberValue) && ((SymbolValue)valueList[index]).m_eSymbol != Enums.eOperationSymbol.ParenthesisClosing)
                        {
                            returnValue = ((SymbolValue)valueList[index]).Evaluate(returnValue, ((NumberValue)valueList[index + 1]).m_dValue);
                        }
                        else if (valueList[index + 1].GetType() == typeof(Variable))
                        {
                            ParseVariable(((Variable)valueList[index + 1]), x, y);
                            if (returnValue == 0 && result != 0 && !bNatural0)
                            {
                                returnValue = result;
                            }

                            returnValue = ((SymbolValue)valueList[index]).Evaluate(returnValue, ((Variable)valueList[index + 1]).m_dValue);

                            if (returnValue == 0)
                            {
                                bNatural0 = true;
                            }
                        }
                        else if (((SymbolValue)valueList[index]).m_eSymbol == Enums.eOperationSymbol.ParenthesisClosing)
                        {
                            endIndex = index - 1;
                            return returnValue;
                        }

                    }

                }
                else if (valueList[index].GetType() == typeof(NumberValue))
                {
                    result = ((NumberValue)valueList[index]).m_dValue;
                }
                else if (valueList[index].GetType() == typeof(Variable))
                {
                    ParseVariable(((Variable)valueList[index]), x, y);
                    result = ((Variable)valueList[index]).m_dValue;
                    if (returnValue == 0 && result != 0 && !bNatural0)
                    {
                        returnValue = result;
                    }
                }

                index++;
            }
            endIndex = index;
            if (result != 0 && returnValue == 0 && !bNatural0)
            {
                returnValue = result;
            }
            return returnValue;
        }

        private void ParseVariable(Variable variable, double x, double y)
        {
            if (variable.m_sVariableName.Contains("x"))
            {
                variable.ParseVariable(x, variable.m_sVariableName.Contains("e"));
            }
            else if (variable.m_sVariableName.Contains("y"))
            {
                variable.ParseVariable(y, variable.m_sVariableName.Contains("e"));
            }
        }

        public string ToString(int startIndex = 0, int endIndex = 0)
        {
            if (endIndex == 0)
            {
                endIndex = valueList.Count;
            }

            string function = "";
            for (int i = startIndex; i < endIndex; i++)
            {
                if (valueList[i].GetType() == typeof(SymbolValue))
                {
                    function += ((SymbolValue)valueList[i]).ToString();
                }
                else if (valueList[i].GetType() == typeof(NumberValue))
                {
                    function += ((SymbolValue)valueList[i]).ToString();
                }
                else if (valueList[i].GetType() == typeof(Variable))
                {
                    function += ((SymbolValue)valueList[i]).ToString();
                }
            }

            return function;
        }
    }
}
