using Math_Collection.LinearAlgebra.Vectors;
using RuntimeFunctionParser;
using Math_Collection.Exceptions;
using System;
using System.Collections.Generic;

namespace Math_Collection.Analysis
{
    public class Gradient
    {
        public Dictionary<string, Func<Function, double[], double, bool, double>> Functions;
        private Function func;

        public Func<Function, double[], double, bool, double> this[string variable]
        {
            get
            {
                Func<Function, double[], double, bool, double> delegateFunc;
                Functions.TryGetValue(variable, out delegateFunc);
                return delegateFunc;
            }
        }

        public Gradient()
        {
            Functions = new Dictionary<string, Func<Function, double[], double, bool, double>>();
        }

        public Gradient(Function function)
        {
            Functions = new Dictionary<string, Func<Function, double[], double, bool, double>>();
            func = function;
            for (int i = 0; i < 2; i++)
            {
                if (i == 0)
                {
                    Functions.Add("x", PartialDerivative);
                }
                else
                {
                    Functions.Add("y", PartialDerivative);
                }
            }
        }

        public Gradient(Gradient v)
        {
            Functions = v.Functions;
        }

        public Vector Solve(double[] paramValues)
        {
            if (Functions.Count > 2)
            {
                throw new GradientException("Vector valued functions with more than 2 functions are not supported yet.");
            }

            Vector results = new Vector(new double[Functions.Count]); //Currently only goes until 2 functions
            int i = 0;
            foreach (KeyValuePair<string, Func<Function, double[], double, bool, double>> value in Functions)
            {
                if (i == 0)
                {
                    results[i] = value.Value.Invoke(func, paramValues, 0.00001, true);
                }
                else
                {
                    results[i] = value.Value.Invoke(func, paramValues, 0.00001, false);
                }
                i++;
            }
            return results;
        }

        private double PartialDerivative(Function f, double[] functionValues, double h, bool x)
        {
            double fFromXPlusH = f.Solve(functionValues[0] + h, functionValues[1]);
            double fFromX = f.Solve(functionValues[0] - h, functionValues[1]);

            double fFromYPlusH = f.Solve(functionValues[0], functionValues[1] + h);
            double fFromY = f.Solve(functionValues[0], functionValues[1] - h);

            if (x)
            {
                return Math.Round((fFromXPlusH - fFromX) / (2 * h), 4);
            }
            else
            {
                return Math.Round((fFromYPlusH - fFromY) / (2 * h), 4);
            }
        }
    }
}