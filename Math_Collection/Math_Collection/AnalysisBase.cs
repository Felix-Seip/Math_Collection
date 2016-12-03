using Math_Collection.LinearAlgebra.Vectors;
using System;

namespace Math_Collection.Analysis
{
    public static class AnalysisBase
    {
        /// <summary>
        /// Calculates the derivation of a simple polynomial function.
        /// </summary>
        public static string FunctionDerivation(Vector coefficients, Vector exponents, int derivationLevel)
        {
            Vector derivation = new Vector(new double[coefficients.Size]);
            string derivedExpression = "";

            for (int i = 0; i < derivationLevel; i++)
            {
                for (int j = 0; j < coefficients.Size; j++)
                {
                    if (i == 0)
                        derivation.Values[j] += exponents[j] * coefficients[j];
                    else
                        derivation.Values[j] = exponents[j] * derivation[j];

                    //Decrease the exponent by one after the multiplication.
                    if (exponents[j] != 0)
                        exponents.Values[j]--;
                }
            }

            for (int i = 0; i < coefficients.Values.Length; i++)
            {
                //if (exponents.vectorValues[i] == 0)
                //    derivation.vectorValues[i] = 1;
                if (i != coefficients.Values.Length - 1)
                    derivedExpression += derivation[i] + "x^" + exponents[i] + " + ";
                else
                    derivedExpression += derivation[i] + "x^" + exponents[i];
            }
            return derivedExpression;
        }

        /// <summary>
        /// Calculated the approximated derivation at a point with the h-method
        /// </summary>
        /// <param name="functionValues"></param>
        /// <param name="x">x value of the function</param>
        /// <param name="h"></param>
        /// <param name="arimeticMiddle">use arimeticMiddle?</param>
        /// <returns>the approximated value of the derivation or NaN if it fails</returns>
        public static double Derivation_Approximation(double[] functionValues, int x, double h, bool arimeticMiddle = false)
        {
            if (functionValues.Length < 3 || x < 1)
                return double.NaN;

            if (arimeticMiddle)
            {
                return Math.Round((functionValues[x + 1] - functionValues[x - 1]) / (2 * h),4);
            }else
            {
                return Math.Round(((functionValues[x + 1] - functionValues[x]) / h), 4);
            }
        }
    }
}
