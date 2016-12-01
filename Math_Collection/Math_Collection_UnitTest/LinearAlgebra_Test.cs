using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Math_Collection;
using Math_Collection.LinearAlgebra.Matrices;
using Math_Collection.LinearAlgebra;

namespace Math_Collection_UnitTest
{
    [TestClass]
    public class LinearAlgebra_Test
    {
        [TestMethod]
        public void SwitchRows_Test()
        {
            double[,] elements = new double[3, 3] { { 1, 1, 1 }, { 2, 2, 2 }, { 3, 3, 3 } };
            Matrix m = new Matrix(elements);

            Matrix actual = LinearAlgebraOperations.SwitchRows(m, 2, 1);
            Assert.Inconclusive();
        }

        [TestMethod]
        public void LR_Partition_Test()
        {
            Matrix input = new Matrix(new double[3, 3] { { 1, 2, 3 }, { 1, 1, 1 }, { 3, 3, 1 } });
            Matrix l = new Matrix();
            Matrix r = new Matrix();

            LinearAlgebraOperations.LR_Partition(input, out l, out r);

            int i = 0;

        }


    }
}
