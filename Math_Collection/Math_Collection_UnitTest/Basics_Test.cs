using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Math_Collection.LinearAlgebra.Matrices;
using Math_Collection.LinearAlgebra.Vectors;
using Math_Collection.Basics;

namespace Math_Collection_UnitTest
{
	[TestClass]
	public class Basics_Test
	{
		[TestMethod]
		public void SolveLGSDeterminant_Test()
		{
			Matrix input = new Matrix(new double[3,3] { { 82,45,9 },{ 27,16,3 },{ 9,5,1 } });
			Vector outcome = new Vector(new double[3] { 1,1,0 });

			Vector result = Basics.SolveLGSDeterminant(input,outcome);
			Vector expected = new Vector(new double[3] { 1,1,-14 });

			Assert.AreEqual(expected,result);
		}
	}
}
