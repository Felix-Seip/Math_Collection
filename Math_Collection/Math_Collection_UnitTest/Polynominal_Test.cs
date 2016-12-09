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
			Polynominal p = new Polynominal(3);
			string exp = "1 + 1x + 1x^2 + 1x^3";
			string actual = p.ToString();

			Assert.AreEqual(exp,actual);
		}

		[TestMethod]
		public void Derive_Test()
		{
			Polynominal p = new Polynominal(3);
			Polynominal derive = p.Derive();
			int i = 0;
		}
	}
}
