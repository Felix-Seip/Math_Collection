using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Math_Collection.Functions;

namespace Math_Collection_UnitTest
{
	[TestClass]
	public class Polynominal_Test
	{
		[TestMethod]
		public void ToString_Test()
		{
			// f(x) = 2x^2
			Polynominal p = new Polynominal(new double[1] { 2.0 },new double[1] { 2.0 },2);

			string expected = "2x^2";
			string actual = p.ToString();

			Assert.AreEqual(expected,actual);
		}

		[TestMethod]
		public void Derive_Test()
		{
			// f(x) = 2x^2
			Polynominal p = new Polynominal(new double[1] { 2.0 },new double[1] { 2.0 },2);
			Polynominal derive = p.Derive();

			string expected = "4x";

			Assert.AreEqual(expected,derive.ToString());
		}

		[TestMethod]
		public void FunctionValue_Test()
		{
			// f(x) = 2x^2
			Polynominal p = new Polynominal(new double[1] { 2.0 },new double[1] { 2 },2);
			double expected = 8;
			double actual = p.FunctionValue(2);

			Assert.AreEqual(expected,actual);

		}
	}
}
