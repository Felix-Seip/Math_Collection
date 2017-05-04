using Math_Collection.LinearAlgebra.Matrices;
using System.Collections.Generic;
using System.Drawing;
using System;
using Math_Collection.LinearAlgebra.Vectors;
using RuntimeFunctionParser;
using Math_Collection.LinearAlgebra;

namespace Math_Collection.Statistic
{
    public static class LinearRegression
    {
        public static Function CreateRegression(Matrix regressionMatrix, Vector regressionVector, int numberOfVariables)
        {
            Parser parser = new Parser();

            LGS lgs = new LGS(regressionMatrix, regressionVector);
            string function = CreateRegressionFunction(lgs.Solve(Enums.ESolveAlgorithm.eGauß), numberOfVariables);
            return parser.ParseFunction(function);
        }

        private static string CreateRegressionFunction(Vector lgsResult, int numberOfVariables)
        {
            string function = "";
            int varCount = numberOfVariables;
            for (int i = 0; i < varCount; i++)
            {
                if (numberOfVariables != 1)
                {
                    function += string.Format(lgsResult[i] + "*x^{0}", numberOfVariables - 1) + "+";
                    numberOfVariables--;
                }
                else
                {
                    function += lgsResult[i];
                }
            }
            return function;
        }

        public static Vector CreateRegressionVector(List<Point> points, int power)
        {
            Vector regressionVector = new Vector(new double[power]);

            for (int i = 0; i < power; i++)
            {
                regressionVector[i] = SumXYValues(points, power - (i + 1));
            }

            return regressionVector;
        }

        public static Matrix CreateRegressionMatrix(List<Point> points, int matrixDimension)
        {
            Matrix regressionSumMatrix = new Matrix(new double[matrixDimension, matrixDimension]);

            if (matrixDimension != 2 && matrixDimension % 2 == 0)
            {
                matrixDimension += 2;
            }
            else if (matrixDimension != 2 && matrixDimension % 2 != 0)
            {
                matrixDimension += 1;
            }

            for (int j = 0; j < regressionSumMatrix.RowCount; j++)
            {
                for (int i = 0; i < regressionSumMatrix.ColumnCount; i++)
                {
                    double sum = SumXValuesForMatrix(points, matrixDimension - i);

                    regressionSumMatrix[j, i] = sum;
                }
                matrixDimension--;
            }
            return regressionSumMatrix;
        }

        private static double SumXValuesForMatrix(List<Point> points, int power)
        {
            double sum = 0;
            for (int i = 0; i < points.Count; i++)
            {
                sum += Math.Pow(points[i].X, power);
            }
            return sum;
        }

        private static double SumXYValues(List<Point> points, int power)
        {
            double sum = 0;
            if (power != 0)
            {
                for (int i = 0; i < points.Count; i++)
                {
                    sum += Math.Pow(points[i].X, power) * points[i].Y;
                }
            }
            else
            {
                for (int i = 0; i < points.Count; i++)
                {
                    sum += points[i].Y;
                }
            }
            return sum;
        }
    }
}
