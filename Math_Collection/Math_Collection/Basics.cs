using Math_Collection.LinearAlgebra.Matrices;
using Math_Collection.LinearAlgebra.Vectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math_Collection.Basics
{
    public class Basics
    {
        public static double DegreesToRadians(double degrees)
        {
            return (degrees / 180) * Math.PI;
        }

		/// <summary>
		/// Calculates a approximated result of an LGS with the JacobiMethod
		/// </summary>
		/// <param name="inputMatrix"></param>
		/// <param name="expectedOutcome"></param>
		/// <param name="iterations"></param>
		/// <returns></returns>
        public static Vector SolveLGSApproximated(Matrix inputMatrix, Vector expectedOutcome, int iterations)
        {
            Vector solvedVector = new Vector(Enumerable.Repeat(0.0, expectedOutcome.Values.Length).ToArray());
            for (int p = 0; p < iterations; p++)
            {
                for (int i = 0; i < inputMatrix.RowCount; i++)
                {
                    double sigma = 0;
                    for (int j = 0; j < inputMatrix.ColumnCount; j++)
                    {
                        if (j != i)
                            sigma += inputMatrix[i, j] * solvedVector[j];
                    }
                    solvedVector.Values[i] = (expectedOutcome[i] - sigma) / inputMatrix[i, i];
                }
            }
            return solvedVector;
        }

		/// <summary>
		/// Calculates the result of an LGS with the Determinant Algorithm (Cramersche Regel)
		/// </summary>
		/// <param name="inputMatrix"></param>
		/// <param name="outcome"></param>
		/// <returns>The result as an Vector or null if it fails</returns>
		public static Vector SolveLGSDeterminant(Matrix inputMatrix, Vector outcome)
		{
			if (inputMatrix.Determinant == 0)
				return null;

			Vector result = new Vector(new double[outcome.Size]);
			double inputDeterminante = inputMatrix.Determinant;

			for (int i = 0; i<inputMatrix.ColumnCount; i++)
			{
				Matrix xi = LinearAlgebra.LinearAlgebraOperations.ChangeColumnInMatrix(inputMatrix,outcome,i);
				result[i] = xi.Determinant / inputDeterminante;
			}

			return result;
		}
    }
}
