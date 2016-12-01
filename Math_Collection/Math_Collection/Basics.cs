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

        public static Vector JacobiMethod(Matrix inputMatrix, Vector expectedOutcome, int iterations)
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
                //Console.WriteLine("Step #" + p + ": " + String.Join(", ", solvedVector.Values.Select(v => v.ToString()).ToArray()));
            }
            return solvedVector;
        }
    }
}
