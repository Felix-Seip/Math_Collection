using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionParser
{
    public class NewFunctionParser
    {
        private Function func;
        public Function Parse(string stringToParse, Enums.eRecursionStep recursionStep, Function firstDerivative = null)
        {
            if (recursionStep == Enums.eRecursionStep.eRecursionStepMain)
            {
                func = new Function(stringToParse, firstDerivative);
            }

            Enums.eOperationSymbol eOperationToPerformWithX = Enums.eOperationSymbol.Multiplication;
            string[] values = new string[1] { stringToParse };

            if (stringToParse.Contains("x") || (stringToParse.Contains("+") ||
                stringToParse.Contains("*") || stringToParse.Contains("/") ||
                stringToParse.Contains("-")))
            {
                eOperationToPerformWithX = DetermineOperation(stringToParse, out values);
            }

            for (int i = 0; i < values.Length; i++)
            {
                if (i > 0 && (func.valueList[func.valueList.Count - 1].m_eSymbol != Enums.eOperationSymbol.ParenthesisClosing || 
                    (eOperationToPerformWithX == Enums.eOperationSymbol.Addition || eOperationToPerformWithX == Enums.eOperationSymbol.Division||
                    eOperationToPerformWithX == Enums.eOperationSymbol.Multiplication || eOperationToPerformWithX == Enums.eOperationSymbol.Subtraction)))
                {
                    func.valueList.Add(new SymbolValue(eOperationToPerformWithX));
                }
                if ((!values[i].Contains("x") && !values[i].Contains("y")) && (!values[i].Contains("+") &&
                    !values[i].Contains("*") && !values[i].Contains("/") &&
                    !values[i].Contains("-") && !values[i].Equals("")))
                {
                    if (values[i].Contains(")"))
                    {
                        values[i] = values[i].Replace(")", "");
                        if(!values[i].Equals(""))
                        {
                            func.valueList.Add(new NumberValue(double.Parse(values[i])));
                        }
                        func.valueList.Add(new SymbolValue(Enums.eOperationSymbol.ParenthesisClosing));
                    }
                    else
                    {
                        if (!values[i].Equals("cos", StringComparison.InvariantCultureIgnoreCase) &&
                            !values[i].Equals("sin", StringComparison.InvariantCultureIgnoreCase) &&
                            !values[i].Equals("tan", StringComparison.InvariantCultureIgnoreCase) &&
                            !values[i].Equals("cot", StringComparison.InvariantCultureIgnoreCase) &&
                            !values[i].Equals("sqrt", StringComparison.InvariantCultureIgnoreCase))
                        {
                            func.valueList.Add(new NumberValue(double.Parse(values[i])));
                        }
                    }
                }
                else if ((values[i].StartsWith("x") || values[i].StartsWith("y") || values[i].StartsWith("e")) && !(values[i].Contains("+") ||
                         values[i].Contains("*") || values[i].Contains("/") ||
                         values[i].Contains("-")))
                {
                    if (values[i].Contains(")"))
                    {
                        values[i] = values[i].Replace(")", "");
                        func.valueList.Add(new Variable(values[i], 0));
                        func.valueList.Add(new SymbolValue(Enums.eOperationSymbol.ParenthesisClosing));
                    }
                    else
                    {
                        func.valueList.Add(new Variable(values[i], 0));
                    }
                }
                else
                {
                    if (!values[i].Equals(""))
                    {
                        Parse(values[i], Enums.eRecursionStep.eRecursionStepFurther);
                    }
                }
            }
            return func;
        }
        private static string ReplaceVariableWithValue(string stringWithVariable, double value)
        {
            return stringWithVariable.Replace("x", "" + value);
        }

        private static double RaiseStringValueToPower(string stringToRaise)
        {
            string[] vals = stringToRaise.Split('^');
            double exponent = double.Parse(vals[1]);
            double xValue = double.Parse(vals[0]);
            double xReplacedValue = xValue;
            for (int j = 1; j < exponent; j++)
            {
                xReplacedValue *= xValue;
            }
            return xReplacedValue;
        }

        private Enums.eOperationSymbol DetermineOperation(string stringToParse, out string[] values)
        {
            if (stringToParse.StartsWith("sin") || stringToParse.StartsWith("cos"))
            {
                Enums.eOperationSymbol trigonometricFuntion = Enums.eOperationSymbol.sine;
                values = stringToParse.Split('(');
                for (int i = 0; i < values.Length; i++)
                {
                    if (values[i].Contains("sin"))
                    {
                        func.valueList.Add(new SymbolValue(Enums.eOperationSymbol.sine));
                        return Enums.eOperationSymbol.ParenthesisOpening;
                    }
                    if (values[i].Contains("cos"))
                    {
                        func.valueList.Add(new SymbolValue(Enums.eOperationSymbol.cosine));
                        return Enums.eOperationSymbol.ParenthesisOpening;
                    }
                }
                return trigonometricFuntion;
            }
            else if (stringToParse.StartsWith("tan") || stringToParse.StartsWith("cot"))
            {
                Enums.eOperationSymbol trigonometricFuntion = Enums.eOperationSymbol.sine;
                values = stringToParse.Split('(');
                for (int i = 0; i < values.Length; i++)
                {
                    if (values[i].Contains("tan"))
                    {
                        func.valueList.Add(new SymbolValue(Enums.eOperationSymbol.tangent));
                        return Enums.eOperationSymbol.ParenthesisOpening;
                    }
                    if (values[i].Contains("cot"))
                    {
                        func.valueList.Add(new SymbolValue(Enums.eOperationSymbol.cotangent));
                        return Enums.eOperationSymbol.ParenthesisOpening;
                    }
                }
                return trigonometricFuntion;
            }
            else if (stringToParse.StartsWith("sqrt"))
            {
                Enums.eOperationSymbol function = Enums.eOperationSymbol.SquareRoot;
                values = stringToParse.Split(new char[] { '(' }, 2);
                for (int i = 0; i < values.Length; i++)
                {
                    if (values[i].Contains("sqrt"))
                    {
                        func.valueList.Add(new SymbolValue(Enums.eOperationSymbol.SquareRoot));
                        return Enums.eOperationSymbol.ParenthesisOpening;
                    }
                }
                return function;
            }

            char[] stringAsArray = stringToParse.ToArray<char>();
            if (!(stringToParse.Contains(")*(") || stringToParse.Contains(")/(") ||
                  stringToParse.Contains(")+(") || stringToParse.Contains(")*(")))
            {

                for (int i = 0; i < stringAsArray.Length; i++)
                {
                    if (stringAsArray[i] == '(' || stringAsArray[i] == ')' ||
                        stringAsArray[i] == '/' || stringAsArray[i] == '+' ||
                        stringAsArray[i] == '*' || stringAsArray[i] == '-' ||
                        stringAsArray[i] == '^')
                    {
                        if (stringAsArray[i] == '*')
                        {
                            values = stringToParse.Split(new char[] { '*' }, 2);
                            return Enums.eOperationSymbol.Multiplication;
                        }
                        else if (stringAsArray[i] == '+')
                        {
                            values = stringToParse.Split('+');
                            return Enums.eOperationSymbol.Addition;
                        }
                        else if (stringAsArray[i] == '/')
                        {
                            values = stringToParse.Split('/');
                            return Enums.eOperationSymbol.Division;
                        }
                        else if (stringAsArray[i] == '-')
                        {
                            values = stringToParse.Split('-');
                            return Enums.eOperationSymbol.Subtraction;
                        }
                        else if (stringAsArray[i] == '(')
                        {
                            func.valueList.Add(new SymbolValue(Enums.eOperationSymbol.ParenthesisOpening));
                            values = new string[] { stringToParse.Substring(stringToParse.IndexOf("(") + 1, stringToParse.IndexOf(")") - stringToParse.IndexOf("(")), stringToParse.Substring(stringToParse.IndexOf(")") - stringToParse.IndexOf("(") + 1) };
                            return Enums.eOperationSymbol.ParenthesisOpening;
                        }
                    }
                }
            }
            else
            {
                if (stringToParse.Contains(")*("))
                {
                    int iIndexToSplit = stringToParse.IndexOf(")*(");
                    values = SplitAt(stringToParse, iIndexToSplit + 2);
                    for (int i = 0; i < values.Length; i++)
                    {
                        values[i] = values[i].Replace("(", "");
                        values[i] = values[i].Replace(")", "");
                    }
                    func.valueList.Add(new SymbolValue(Enums.eOperationSymbol.ParenthesisOpening));
                    return Enums.eOperationSymbol.Multiplication;
                }

                else if (stringToParse.Contains(")/("))
                {
                    int iIndexToSplit = stringToParse.IndexOf(")/(");
                    values = SplitAt(stringToParse, iIndexToSplit + 2);
                    for (int i = 0; i < values.Length; i++)
                    {
                        values[i] = values[i].Replace("(", "");
                        values[i] = values[i].Replace(")", "");
                    }
                    func.valueList.Add(new SymbolValue(Enums.eOperationSymbol.ParenthesisOpening));
                    return Enums.eOperationSymbol.Division;
                }
                else if (stringToParse.Contains(")+("))
                {
                    int iIndexToSplit = stringToParse.IndexOf(")+(");
                    values = SplitAt(stringToParse, iIndexToSplit + 2);
                    for (int i = 0; i < values.Length; i++)
                    {
                        values[i] = values[i].Replace("(", "");
                        values[i] = values[i].Replace(")", "");
                    }
                    func.valueList.Add(new SymbolValue(Enums.eOperationSymbol.ParenthesisOpening));
                    return Enums.eOperationSymbol.Addition;
                }
                else if (stringToParse.Contains(")-("))
                {
                    int iIndexToSplit = stringToParse.IndexOf(")-(");
                    values = SplitAt(stringToParse, iIndexToSplit + 2);
                    for (int i = 0; i < values.Length; i++)
                    {
                        values[i] = values[i].Replace("(", "");
                        values[i] = values[i].Replace(")", "");
                    }
                    func.valueList.Add(new SymbolValue(Enums.eOperationSymbol.ParenthesisOpening));
                    return Enums.eOperationSymbol.Subtraction;
                }
            }
            values = new string[1] { stringToParse };
            return Enums.eOperationSymbol.Addition;

        }

        private string[] SplitAt(string stringToSplit, int iSplitAt)
        {
            string[] values = new string[2];

            values[0] = stringToSplit.Substring(0, stringToSplit.Length - iSplitAt);
            values[1] = stringToSplit.Substring(iSplitAt, stringToSplit.Length - values[0].Length - 1);

            return values;
        }
    }
}