using Math_Collection.LinearAlgebra.Vectors;

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
    }
}
