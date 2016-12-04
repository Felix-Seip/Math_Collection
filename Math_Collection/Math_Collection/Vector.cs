using System;

namespace Math_Collection.LinearAlgebra.Vectors
{
	public class Vector
	{
		/// <summary>
		/// Values in the vector.
		/// </summary>
		public double[] Values;

		/// <summary>
		/// The number of elements in the vector.
		/// </summary>
		public int Size
		{
			get
			{
				return Values.Length;
			}

			private set { }
		}

		public double Magnitude
		{
			get
			{
				return CalculateMagnitude();
			}

			private set { }
		}

		public double this[int i]
		{
			get
			{
				return Values[i];
			}
			set
			{
				Values[i] = value;
			}
		}

		public Vector(double[] values)
		{
			Values = (double[])values.Clone();
		}

		public Vector(Vector v)
		{
			Values = v.Values;
		}

		/// <summary>
		/// Returns the length of a vector.
		/// </summary>
		private double CalculateMagnitude()
		{
			double squaredValues = 0;

			for (int i = 0; i < Size; i++)
				squaredValues += Math.Pow(this[i],2);

			return Math.Sqrt(squaredValues);
		}

		public override string ToString()
		{
			string vectorAsString = "";

			for (int i = 0; i < Values.Length; i++)
				vectorAsString += "|" + Values[i] + "|\n";

			return vectorAsString;
		}

		public override bool Equals(object obj)
		{
			Vector tmp = obj as Vector;
			if (tmp == null)
				return false;

			bool equal = true;

			for (int k = 0; k < Size; k++)
			{
				if (Values[k] != tmp[k])
					equal = false;
			}

			return equal;
		}
	}
}