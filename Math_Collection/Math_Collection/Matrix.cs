using System;

namespace Math_Collection.LinearAlgebra.Matrices
{
    public class Matrix
    {
        /// <summary>
        /// Values in the matrix.
        /// </summary>
        public double[,] Values { get; protected set; }
        /// <summary>
        /// Row count of the matrix.
        /// </summary>
        public int RowCount
        {
            get
            {
                return Values.GetLength(0);
            }
            private set
            { }
        }
        /// <summary>
        /// Column count of the matrix.
        /// </summary>
        public int ColumnCount
        {
            get
            {
                return Values.GetLength(1);
            }
            private set
            { }
        }

        public double Determinant
        {
            get
            {
                //TODO: Implement calculate determinant
                throw new NotImplementedException();
            }
            private set { }
        }

        public bool IsDiagonallyDominant
        {
            get
            {
                //TODO: Implement Getter
                throw new NotImplementedException();
            }
        }

        public bool IsSqaureMatrix
        {
            get
            {
                return RowCount == ColumnCount;
            }
        }

        public double this[int row, int column]
        {
            get
            {
                return Values[row, column];
            }
            set
            {
                Values[row, column] = value;
            }
        }

        public Matrix() { }
        public Matrix(double[,] matrix)
        {
            Values = matrix;
        }

        public override string ToString()
        {
            string matrixAsString = "";
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    matrixAsString += Values[i, j] + "\t";
                }
                matrixAsString += "\n";
            }
            return matrixAsString;
        }
    }
}
