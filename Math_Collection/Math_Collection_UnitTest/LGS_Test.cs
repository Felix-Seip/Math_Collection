using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Math_Collection.LinearAlgebra.Matrices;
using Math_Collection.LinearAlgebra.Vectors;
using Math_Collection.LGS;

namespace Math_Collection_UnitTest
{
	[TestClass]
	public class LGS_Test
	{
		[TestMethod]
		public void Solve_Test()
		{
			Matrix m = new Matrix(new double[4,4] { { 1,1,1,1},{ 1,1,-1, -1},{ 1,-1,1, 1}, { 1, -1, -1, 1}});
			Vector v = new Vector(new double[4] { 0,1,3, -1});

			LGS lgs = new LGS(m,v);
			Vector result = lgs.Solve(LGS.ESolveAlgorithm.eGauß);

			Vector expected = new Vector(new double[6] { 1,-2,3,4,2,-1});

			Assert.AreEqual(expected,result);

		}
	}
}
