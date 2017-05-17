using Microsoft.VisualStudio.TestTools.UnitTesting;
using Math_Collection.LinearAlgebra.Matrices;
using Math_Collection.LinearAlgebra.Vectors;
using Math_Collection;
using Math_Collection.LinearAlgebra;

namespace Math_Collection_UnitTest
{
	[TestClass]
	public class LGS_Test
	{
		[TestMethod]
		public void SolveWithGaussianElimination_Test()
		{
			Matrix m = new Matrix(new double[4,4] { { 1,1,1,1},{ 1,1,-1, -1},{ 1,-1,1, 1}, { 1, -1, -1, 1}});
			Vector v = new Vector(new double[4] { 0,1,3, -1});

			LGS lgs = new LGS(m,v);
			Vector result = lgs.Solve(Enums.ESolveAlgorithm.eGaussianElimination);

			Vector expected = new Vector(new double[6] { 1,-2,3,4,2,-1});

			Assert.AreEqual(expected,result);

		}

        [TestMethod]
        public void SolveWithGaussSeidel_Test()
        {
            Matrix inputMatrix = new Matrix(new double[3, 3]
            {
                {10,-4,-2 },
                {-4,10,-4 },
                { -6,-2,12 }
            });

            Vector rightSideVector = new Vector(new double[3]
            {
                2,
                3,
                1
            });
            Vector startValue = new Vector(new double[3] { 0, 0, 0 });


            LGS lgs = new LGS(inputMatrix, rightSideVector);
            Vector result = lgs.Solve(Enums.ESolveAlgorithm.eGaußSeidel, 50, startValue, 0.0001);

            Vector expected = new Vector(new double[3] { 0.598, 0.741, 0.506 });

            Assert.AreEqual(expected, result);

        }

        [TestMethod]
		public void SolveWithJacobiMethod_Test()
		{
			Matrix inputMatrix = new Matrix(new double[3, 3]
			{
				{10,-4,-2 },
				{-4,10,-4 },
				{ -6,-2,12 }
			});

			Vector rightSideVector = new Vector(new double[3]
			{
				2,
				3,
				1
			});

			Vector startValue = new Vector(new double[3]
			{
				0,
				0,
				0
			});

			LGS lgs = new LGS(inputMatrix, rightSideVector);
			Vector actual = lgs.Solve(Enums.ESolveAlgorithm.eJacobi, 0, startValue, 0.0001);
			actual.Round(3);

			Vector expected = new Vector(new double[3]
			{
				0.598,
				0.741,
				0.506
			});

			Assert.AreEqual(expected, actual);
		}
	}
}
