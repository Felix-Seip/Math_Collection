using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Analysis = Math_Collection.Analysis.AnalysisBase;
using Math_Collection.LinearAlgebra.Vectors;
using Math_Collection;

namespace Math_Collection_UnitTest
{
    [TestClass]
    public class Analysis_Test
    {
        private static List<double> values;

        [ClassInitialize]
        public static void initClass(TestContext context)
        {
            values = new List<double>();
        }

        [TestInitialize]
        public void initTest()
        {
            values.Clear();   
        }

        [TestMethod]
        public void DerivationApproximation_Test()
        {
            double actual = 0.0;
            double expected = 0.0;
            double h = 0.001;

            //not enough values
            values.Add(3);
            expected = double.NaN;
            //actual = Analysis.Derivation_Approximation(values.ToArray(), 1, h);
            Assert.AreEqual(expected, actual);
            values.Clear();

            //Function f(x) = 3
            for (int i = 0; i < 10; i++)
                values.Add(3.0);

            expected = 0.0;
            //actual = Analysis.Derivation_Approximation(values.ToArray(), 2, h);
            Assert.AreEqual(expected, actual);
            values.Clear();

            values.Add(0);
            values.Add(1);
            values.Add(4);
            expected = 2.0;
            //actual = Analysis.Derivation_Approximation(values.ToArray(), 1, h, true);
            Assert.AreEqual(expected, actual);
            
        }
        [TestMethod]
        public void ExtremaApproximationWithFibonacciMethod_Test()
        {
            string function = "(x-2)*(x-2)+1";
            int n = 4;
            Math_Collection.Basics.Interval intervall = new Math_Collection.Basics.Interval(0, 4);
			// TODO: Epsilon umgebung mit (min-max) / Nn, damit Test nicht fehlschlägt
            Vector expected = new Vector(new double[] { 2, 1 });
            Vector actual = Analysis.ExtremaApproximatedWithFibonacciMethod(function, intervall, n, Enums.EExtrema.eMinimum);
            Assert.AreEqual(expected, actual);
        }
    }
}
