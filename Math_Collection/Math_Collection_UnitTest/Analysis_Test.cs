using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Analysis = Math_Collection.Analysis.AnalysisBase;
using Math_Collection.LinearAlgebra.Vectors;
using Math_Collection;
using RuntimeFunctionParser;

namespace Math_Collection_UnitTest
{
	[TestClass]
	public class Analysis_Test
	{
		[TestMethod]
		public void DerivationApproximation_Test()
		{
			double actual = 0.0;
			double expected = 0.0;
			double h = 0.001;
			Parser p = new Parser();

			//Function f(x) = 3
			string function = "3";
			Function f = p.ParseFunction(function);

			expected = 0.0;
			actual = Analysis.Derivation_Approximation(f, 2, h);
			Assert.IsTrue(IsNearlyEqual(expected, actual,0));

			// f(x) = x^2
			function = "x^2";
			f = p.ParseFunction(function);
			expected = 2.0;
			actual = Analysis.Derivation_Approximation(f, 1, h);
			Assert.IsTrue(IsNearlyEqual(expected, actual,h));
		}
		[TestMethod]
		public void ExtremaApproximationWithFibonacciMethod_Test()
		{
			string function = "(x-2)*(x-2)+1";
			Function f = new Parser().ParseFunction(function);
			int n = 4;
			Math_Collection.Basics.Interval intervall = new Math_Collection.Basics.Interval(0, 4);
			// TODO: Epsilon umgebung mit (min-max) / Nn, damit Test nicht fehlschlägt
			Vector expected = new Vector(new double[] { 2, 1 });
			Vector actual = Analysis.ExtremaApproximatedWithFibonacciMethod(f, intervall, n, Enums.EExtrema.eMinimum);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void ApproximatedRoot_Test()
		{
			string function = "x^2 - 2";
			Function f = new Parser().ParseFunction(function);

			double startValue = 2;
			double epsilon = 0.001;

			double expectedRoot = Math.Round(Math.Sqrt(2), 4);
			double actualRoot = Math.Round(Analysis.CalulateApproximatedRoot(f, startValue,epsilon), 4);

			Assert.IsTrue(IsNearlyEqual(expectedRoot, actualRoot,epsilon));
		}

		#region Helper methods

		private bool IsNearlyEqual(double a, double b, double deviation)
		{
			return Math.Abs(a - b) <= deviation;
		}

		#endregion
	}
}
