using Math_Collection.Basics;
using Math_Collection.LinearAlgebra.Vectors;
using RuntimeFunctionParser;
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
		public static double Derivation_Approximation(Function f, int x, double h, bool arimeticMiddle = false)
		{
			double fFromXPlusH = f.Solve(x + h, 0);
			double fFromX = f.Solve(x, 0);
			if (arimeticMiddle)
			{
				return Math.Round((fFromXPlusH - fFromX) / (2 * h), 4);
			}
			else
			{
				return Math.Round(((fFromXPlusH - fFromX) / h), 4);
			}
		}
		
		/// <summary>
		/// Returns the minimum or maximum approximated point of a function with the fibonacci method
		/// </summary>
		/// <param name="function">Function for which the point should be found</param>
		/// <param name="intervall">Interval in which the point should be</param>
		/// <param name="n">Number of iterations</param>
		/// <param name="extremaToFind">Minimum or Maximum</param>
		/// <returns>A Vector with the x and y value of the founded point</returns>
		public static Vector ExtremaApproximatedWithFibonacciMethod(Function function, Interval intervall, int n, Enums.EExtrema extremaToFind)
		{
			// n can only begin with 2 or higher
			if (n < 2)
				n = 2;
			int intervalPartsCount = Basics.Basics.FibonacciSequence(n + 2);
			int untereGrenze = 0;
			int obereGrenze = intervalPartsCount;
			int currentNumber = Basics.Basics.FibonacciSequence(n);
			double intervalStep = intervall.Range / (double)intervalPartsCount;

			double functionValueForCurrentNumber = 0;
			for (int i = 0; i < n; i++)
			{
				functionValueForCurrentNumber = function.Solve(currentNumber * intervalStep + intervall.MinValue, 0);

				int numberForSymmetricPoint = obereGrenze - (currentNumber - untereGrenze);
				// check for same x values
				if (numberForSymmetricPoint == currentNumber)
					break;

				double functionValueForSymmetricPoint = function.Solve(numberForSymmetricPoint * intervalStep + intervall.MinValue, 0);

				Enums.ECompareResult compareResultCurrentWithSymmetricFunctionValue = Basics.Basics.Compare(functionValueForSymmetricPoint, functionValueForCurrentNumber);

				// Check for same y values
				if (compareResultCurrentWithSymmetricFunctionValue == Enums.ECompareResult.eSame)
				{
					if (numberForSymmetricPoint < currentNumber)
						obereGrenze = currentNumber;
					else
						untereGrenze = currentNumber;

					continue;
				}

				bool isSymmetricPointBigger = compareResultCurrentWithSymmetricFunctionValue == Enums.ECompareResult.eBigger;

				if (extremaToFind == Enums.EExtrema.eMinimum && !isSymmetricPointBigger)
				{
					if (numberForSymmetricPoint < currentNumber)
						obereGrenze = currentNumber;
					else
						untereGrenze = currentNumber;

					currentNumber = numberForSymmetricPoint;
				}
				else if (extremaToFind == Enums.EExtrema.eMinimum && isSymmetricPointBigger)
				{
					if (numberForSymmetricPoint < currentNumber)
						untereGrenze = numberForSymmetricPoint;
					else
						obereGrenze = numberForSymmetricPoint;
				}
				else if (extremaToFind == Enums.EExtrema.eMaximum && !isSymmetricPointBigger)
				{
					if (numberForSymmetricPoint < currentNumber)
						untereGrenze = numberForSymmetricPoint;
					else
						obereGrenze = numberForSymmetricPoint;

				}
				else if (extremaToFind == Enums.EExtrema.eMaximum && isSymmetricPointBigger)
				{
					if (numberForSymmetricPoint < currentNumber)
						obereGrenze = currentNumber;
					else
						untereGrenze = currentNumber;

					currentNumber = numberForSymmetricPoint;
				}
			}

			double x = currentNumber * intervalStep + intervall.MinValue;
			double y = function.Solve(x, 0);

			// Check lower limit
			if (currentNumber == 1)
			{
				double lowestLimitFunctionValue = function.Solve(0 * intervalStep + intervall.MinValue, 0);
				if (y.CompareTo(lowestLimitFunctionValue) > 0 && extremaToFind == Enums.EExtrema.eMinimum)
				{
					y = lowestLimitFunctionValue;
					x = 0 * intervalStep + intervall.MinValue;
				}
			}

			// check highest limit
			if (currentNumber == intervalPartsCount - 1)
			{
				double highestLimitFunctionValue = function.Solve(intervalPartsCount * intervalStep + intervall.MinValue, 0);
				if (y.CompareTo(highestLimitFunctionValue) < 0 && extremaToFind == Enums.EExtrema.eMaximum)
				{
					y = highestLimitFunctionValue;
					x = intervalPartsCount * intervalStep + intervall.MinValue;
				}
			}

			return new Vector(new double[] { x, y });
		}
	}
}
