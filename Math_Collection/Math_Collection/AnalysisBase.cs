using Math_Collection.LinearAlgebra.Vectors;
using System;

namespace Math_Collection.Analysis
{
    public static class AnalysisBase
    {
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
