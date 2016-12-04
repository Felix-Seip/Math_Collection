using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Math_Collection.LinearAlgebra.Matrices;
using Math_Collection.LinearAlgebra.Vectors;
using Math_Collection;

namespace Math_Collection_UnitTest
{
	[TestClass]
	public class LGS_Test
	{
		[TestMethod]
		public void Solve_Test()
		{
			Matrix m = new Matrix(new double[3,3] { { 0,3,1 },{ 2,4,6 },{ 3,3,9 } });
			Vector v = new Vector(new double[3] { 0,2,-6 });

			LGS lgs = new LGS(m,v);
			Vector result = lgs.Solve(LGS.SolveAlgorithm.Gauß);

			Vector expected = new Vector(new double[3] { 22,3,-9 });

			Assert.AreEqual(expected,result);

		}
	}
}
