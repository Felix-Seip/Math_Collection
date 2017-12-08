using static Math_Collection.Enums;
using RuntimeFunctionParser;
using Math_Collection.Basics;
using System;

namespace Math_Collection.Analysis
{

	public class Integration
	{
		public Function Function { get; private set; }

		public Integration(Function f)
		{
			Function = f;
		}

		public double Integrate(Interval i)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Caluclates the integral of the function with a numeric method
		/// </summary>
		/// <param name="i">Interval in which the integral should be calculated</param>
		/// <param name="n">Count of pieces</param>
		/// <param name="type">Specifies the numeric method</param>
		/// <returns>result of the integration</returns>
		public double IntegrateNumeric(Interval i, int n, ENumericIntegrationMethod type)
		{
			if (type == ENumericIntegrationMethod.None)
				throw new ArgumentException("Numeric method type is not set", nameof(type));

			bool isStraightAmount = n % 2 == 0;

			if (!isStraightAmount && (type == ENumericIntegrationMethod.SimpsonRule || type == ENumericIntegrationMethod.TangentialTrapezoidalRule))
				throw new ArgumentException("n must be straight in order to use simpson or tangetial method", nameof(n));

			double result = 0;

			switch (type)
			{
				case ENumericIntegrationMethod.TrapezoidalRule:
					result = CalculateNumericIntegralWithTrapeziol(i, n);
					break;
				case ENumericIntegrationMethod.TangentialTrapezoidalRule:
					result = CalculateNumericIntegralWithTangentialTrapeziol(i, n);
					break;
				case ENumericIntegrationMethod.SimpsonRule:
					result = CaluclateNumericIntegralWithSimpson(i, n);
					break;
			}
			return result;
		}

		private double CalculateNumericIntegralWithTrapeziol(Interval i, int n)
		{
			double widthOfOneTrapeziol = i.Range / n;
			double constMultiplier = i.Range / (2 * n);

			double result = Function.Solve(i.MinValue, 0) + Function.Solve(i.MaxValue, 0);
			for (double x = i.MinValue + widthOfOneTrapeziol; x <= i.MaxValue - widthOfOneTrapeziol; x += widthOfOneTrapeziol)
			{
				result += 2 * Function.Solve(x, 0);
			}
			return constMultiplier * result;
		}

		private double CalculateNumericIntegralWithTangentialTrapeziol(Interval i, int n)
		{
			double halfWidthOfOneTrapeziol = (i.Range / n);
			double widthOfOneTrapeziol = 2 * halfWidthOfOneTrapeziol;

			double result = 0;
			for (double x = i.MinValue + halfWidthOfOneTrapeziol; x <= i.MaxValue - halfWidthOfOneTrapeziol; x += widthOfOneTrapeziol)
			{
				result += Function.Solve(x, 0);
			}
			return widthOfOneTrapeziol * result;
		}

		private double CaluclateNumericIntegralWithSimpson(Interval i, int n)
		{
			double halfWidthOfOneTrapeziol = (i.Range / n);
			double widthOfOneTrapeziol = 2 * halfWidthOfOneTrapeziol;
			double constMultiplier = i.Range / (3 * n);

			double result = Function.Solve(i.MinValue, 0) + Function.Solve(i.MaxValue, 0);
			for (double x = i.MinValue + halfWidthOfOneTrapeziol; x <= i.MaxValue - halfWidthOfOneTrapeziol; x += widthOfOneTrapeziol)
			{
				result += 4 * Function.Solve(x, 0);
			}

			for (double x = i.MinValue + widthOfOneTrapeziol; x <= i.MaxValue - widthOfOneTrapeziol; x += widthOfOneTrapeziol)
			{
				result += 2 * Function.Solve(x, 0);
			}

			return constMultiplier * result;
		}
	}
}
