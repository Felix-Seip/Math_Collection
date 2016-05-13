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
        }

        public Vector(double[] values)
        {
            Values = values;
        }

        /// <summary>
        /// Returns the length of a vector.
        /// </summary>
        private double CalculateMagnitude()
        {
            double squaredValues = 0;

            for (int i = 0; i < Size; i++)
                squaredValues += Math.Pow(this[i], 2);

            return Math.Sqrt(squaredValues);
        }

        public override string ToString()
        {
            string vectorAsString = "";

            for (int i = 0; i < Values.Length; i++)
                vectorAsString += "|" + Values[i] + "|\n";

            return vectorAsString;
        }
    }
}