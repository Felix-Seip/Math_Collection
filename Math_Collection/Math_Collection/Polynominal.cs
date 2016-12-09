using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math_Collection.Functions
{
	/// <summary>
	/// Represents a polynominal function with the type of
	/// f(x) = a + bx + cx^2 + dx^3 + ex^4 + .... + nx^m
	/// </summary>
	public class Polynominal
	{

		private double[] _parameters;
		/// <summary>
		/// Parameters values of the polynominal
		/// Begins with a
		/// </summary>
		public double[] Parameters
		{
			get { return _parameters; }
			set { _parameters = value; }
		}

		private int _order;
		/// <summary>
		/// Represents the order of the function
		/// </summary>
		public int Order
		{
			get { return _order; }
			private set { _order = value; }
		}

		private double[] _exponents;
		public double[] Exponents
		{
			get { return _exponents; }
			set { _exponents = value; }
		}

		public Polynominal(double[] parameters,double[] exponets,int order)
		{
			Parameters = parameters;
			Exponents = exponets;
			Order = order;
		}

		public Polynominal(double[] parameters,int order)
		{
			Parameters = parameters;
			Exponents = new double[parameters.Length];
			for (int i = 0; i < Exponents.Length; i++)
			{
				Exponents[i] = i;
			}
			Order = order;
		}

		public Polynominal(int order)
		{
			Parameters = Enumerable.Repeat(1.0,order + 1).ToArray();
			Exponents = new double[Parameters.Length];
			for (int i = 0; i < Exponents.Length; i++)
			{
				Exponents[i] = i;
			}
			Order = order;
		}

		public double FunctionValue(double x)
		{
			double value = 0;
			for (int i = 0; i < Parameters.Length; i++)
			{
				value += Parameters[i] * Math.Pow(x,i);
			}
			return value;
		}

		public Polynominal Derive()
		{
			double[] derivation = new double[Parameters.Length];
			double[] expo = (double[])Exponents.Clone();

			for (int j = 0; j < Parameters.Length; j++)
			{

				derivation[j] += Exponents[j] * Parameters[j];

				//Decrease the exponent by one after the multiplication.
				if (expo[j] != 0)
					expo[j]--;
			}

			return new Polynominal(derivation,expo,Order - 1);
		}

		public override string ToString()
		{
			string function = "";
			for (int i = 0; i < Parameters.Length; i++)
			{
				if (Parameters[i] == 0)
					continue;

				function += Parameters[i];
				if (Exponents[i] == 0.0)
				{
					function += " + ";
				}
				else if (Exponents[i] == 1.0)
				{
					function += "x + ";
				}
				else
				{
					if (i != Parameters.Length - 1)
						function += "x^" + Exponents[i] + "+ ";
					else
						function += "x^" + Exponents[i];
				}
			}
			return function;
		}
	}
}
