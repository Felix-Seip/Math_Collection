using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RuntimeFunctionParser;
using Math_Collection.Analysis;
using Math_Collection.Basics;

namespace Math_Collection_UnitTest
{
	[TestClass]
	public class Integration_Test
	{
		private static Parser _parser;
		private Integration _intergration;

		[ClassInitialize]
		public static void MainInit(TestContext tc)
		{
			_parser = new Parser();
		}

		[TestMethod]
		public void Basic_Tests()
		{
			var f = _parser.ParseFunction("x");
			_intergration = new Integration(f);
			var i = new Interval(0, 1);

			try
			{
				_intergration.IntegrateNumeric(i, 4, Math_Collection.Enums.ENumericIntegrationMethod.None);
				Assert.Fail("Integration should thrown an exception");
			}catch(ArgumentException ae)
			{
				Assert.IsTrue(true, "Correct excpetion occured");
			}

			try
			{
				_intergration.IntegrateNumeric(i, 13, Math_Collection.Enums.ENumericIntegrationMethod.SimpsonRule);
				Assert.Fail("Integration should thrown an exception");
			}
			catch (ArgumentException ae)
			{
				Assert.IsTrue(true, "Correct excpetion occured");
			}
		}

		[TestMethod]
		public void Integrate_Test()
		{
			var f = _parser.ParseFunction("1 / (1+x^2)");
			_intergration = new Integration(f);
			double actual = _intergration.Integrate(new Interval(1.0, 3.0));
			double expected = 0.4636;

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void Trapeziol_Test()
		{
			var f = _parser.ParseFunction("1 / (1+x^2)");
			_intergration = new Integration(f);

			double actual = _intergration.IntegrateNumeric(new Interval(1.0, 3.0), 4, Math_Collection.Enums.ENumericIntegrationMethod.TrapezoidalRule);
			double expected = 0.4728;

			Assert.AreEqual(expected, Math.Round(actual, 4),0.01, "f(x) = 1 / (1+x^2)");

			f = _parser.ParseFunction("x^3 / (2+x)");
			_intergration = new Integration(f);

			actual = _intergration.IntegrateNumeric(new Interval(-1.0, 3.0), 20, Math_Collection.Enums.ENumericIntegrationMethod.TrapezoidalRule);
			expected = 4.458;

			Assert.AreEqual(expected, Math.Round(actual, 3),0.01, "f(x) = x^3 / (2+x)");


		}

		[TestMethod]
		public void TangentialTrapeziol_Test()
		{
			var f = _parser.ParseFunction("1 / (1+x^2)");
			_intergration = new Integration(f);

			double actual = _intergration.IntegrateNumeric(new Interval(1.0, 3.0), 4, Math_Collection.Enums.ENumericIntegrationMethod.TangentialTrapezoidalRule);
			double expected = 0.4456;

			Assert.AreEqual(expected, Math.Round(actual,4),0.01, "f(x) = 1 / (1 + x ^ 2)");

			f = _parser.ParseFunction("x^3 / (2+x)");
			_intergration = new Integration(f);

			actual = _intergration.IntegrateNumeric(new Interval(-1.0, 3.0), 20, Math_Collection.Enums.ENumericIntegrationMethod.TangentialTrapezoidalRule);
			expected = 4.458;

			Assert.AreEqual(expected, Math.Round(actual, 3),0.01, "f(x) = x^3 / (2+x)");
			
		}

		[TestMethod]
		public void Simpson_Test()
		{
			var f = _parser.ParseFunction("1 / (1+x^2)");
			_intergration = new Integration(f);

			double actual = _intergration.IntegrateNumeric(new Interval(1.0, 3.0), 4, Math_Collection.Enums.ENumericIntegrationMethod.SimpsonRule);
			double expected = 0.4637;

			Assert.AreEqual(expected, Math.Round(actual,4), "f(x) = 1 / (1+x^2) failed");

			f = _parser.ParseFunction("sin(x) / (1+x^2)");
			_intergration = new Integration(f);

			actual = _intergration.IntegrateNumeric(new Interval(0, Math.PI / 2), 6, Math_Collection.Enums.ENumericIntegrationMethod.SimpsonRule);
			expected = 0.5269;

			Assert.AreEqual(expected, Math.Round(actual, 4), 0.01, "f(x) = sin(x) / (1+x^2)");
		}
	}
}
