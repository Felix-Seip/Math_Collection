using Microsoft.VisualStudio.TestTools.UnitTesting;
using RuntimeFunctionParser;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math_Collection_UnitTest
{
	[TestClass]
	public class Statistic_Test
	{
		private static List<Point> _points;

		[ClassInitialize]
		public static void ClassInit(TestContext tc)
		{
			_points = new List<Point>();
		}

		[TestInitialize]
		public void TestInit()
		{
			_points.Clear();
		}

		[TestMethod]
		public void LinearRegression_Test()
		{
			_points.Add(new Point(1, 3));
			_points.Add(new Point(2, 1));
			_points.Add(new Point(3, 4));
			_points.Add(new Point(4, 6));
			_points.Add(new Point(5, 5));

			Math_Collection.LinearAlgebra.Matrices.Matrix regMatrix = Math_Collection.Statistic.LinearRegression.CreateRegressionMatrix(_points, 2);
			Math_Collection.LinearAlgebra.Vectors.Vector regVector = Math_Collection.Statistic.LinearRegression.CreateRegressionVector(_points, 2);
			Function actual = Math_Collection.Statistic.LinearRegression.CreateRegression(regMatrix, regVector, 2);

			Assert.AreEqual("0.9*Math.Pow(x,1)+1.1", actual.OriginalFunction);
		}
	}
}
