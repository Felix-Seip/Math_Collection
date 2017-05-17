using Math_Collection.LinearAlgebra.Matrices;
using Math_Collection.LinearAlgebra.Vectors;
using System;
using static Math_Collection.Enums;

namespace Math_Collection.LinearAlgebra
{
	public class LGS
	{

		private ELGSType _resultType;
		public ELGSType ResultType
		{
			get { return _resultType; }
			private set { _resultType = value; }
		}

		private Matrix _koeffizientenMatrix;
		public Matrix KoeffizientenMatrix
		{
			get { return _koeffizientenMatrix; }
			set { _koeffizientenMatrix = value; }
		}

		private Vector _expansionVector;
		public Vector ExpansionVector
		{
			get { return _expansionVector; }
			set { _expansionVector = value; }
		}

		public LGS(Matrix input, Vector outcome)
		{
			KoeffizientenMatrix = input;
			ExpansionVector = outcome;
			ResultType = ELGSType.eNotSolved;
		}

		/// <summary>
		/// Solves the LGS 
		/// </summary>
		/// <param name="usedAlgorithm">Algorithm that should be used
		/// With the Automatic option a algorithm is automaticallly picked</param>
		/// <param name="iterations">Optional Parameter and is used for the Approximated Algorithm</param>
		/// <param name="startVectorForJacobiMethod">Start value for the Jacobi Algorithm</param>
		/// <param name="epsilon">Precision that is used in the Jacobi Algorithm</param>
		/// <returns></returns>
		public Vector Solve(ESolveAlgorithm usedAlgorithm = ESolveAlgorithm.eAutomatic, int iterations = 50, 
			Vector startVectorForJacobiMethod = null , double epsilon = 0.001)
		{
			if (!KoeffizientenMatrix.IsSqaureMatrix)
				return null;

			if (usedAlgorithm != ESolveAlgorithm.eAutomatic)
			{
				switch (usedAlgorithm)
				{
					case ESolveAlgorithm.eApproximated:
						return SolveLGSApproximated(iterations);
					case ESolveAlgorithm.eDeterminant:
						return SolveLGSDeterminant();
					case ESolveAlgorithm.eGaussianElimination:
						return SolveLGSGauß();
					case ESolveAlgorithm.eJacobi:
						return SolveLGSJacobi(startVectorForJacobiMethod, epsilon);
					default:
						return null;
				}
			}

			Vector result = new Vector(new double[KoeffizientenMatrix.ColumnCount]);

			//Until a dimension of 8 we can solve it with the Gramersche Rule
			if (KoeffizientenMatrix.ColumnCount <= 8)
			{
				result = SolveLGSDeterminant();
			}
			else
			{
				result = SolveLGSGauß();
			}

			if (result == null)
				result = SolveLGSApproximated(50);

			return result;
		}

		/// <summary>
		/// Calculates a approximated result of an LGS with the JacobiMethod
		/// </summary>
		/// <param name="inputMatrix"></param>
		/// <param name="expectedOutcome"></param>
		/// <param name="iterations"></param>
		/// <returns></returns>
		private Vector SolveLGSApproximated(int iterations)
		{
			if (KoeffizientenMatrix == null || ExpansionVector == null)
				return null;

			Vector solvedVector = new Vector(new double[ExpansionVector.Size]);

			for (int p = 0; p < iterations; p++)
			{
				for (int i = 0; i < KoeffizientenMatrix.RowCount; i++)
				{
					double sigma = 0;
					for (int j = 0; j < KoeffizientenMatrix.ColumnCount; j++)
					{
						if (j != i)
							sigma += KoeffizientenMatrix[i, j] * solvedVector[j];
					}
					solvedVector.Values[i] = (ExpansionVector[i] - sigma) / KoeffizientenMatrix[i, i];
				}
			}
			ResultType = ELGSType.eUnique;
			return solvedVector;
		}

		/// <summary>
		/// Calculates the result of an LGS with the Determinant Algorithm (Cramersche Regel)
		/// </summary>
		/// <param name="inputMatrix"></param>
		/// <param name="outcome"></param>
		/// <returns>The result as an Vector or null if it fails</returns>
		private Vector SolveLGSDeterminant()
		{
			if (KoeffizientenMatrix == null || ExpansionVector == null)
				return null;

			if (KoeffizientenMatrix.Determinant == 0)
				return null;

			if (KoeffizientenMatrix.ColumnCount != ExpansionVector.Size)
				return null;

			Vector result = new Vector(new double[ExpansionVector.Size]);
			double inputDeterminante = KoeffizientenMatrix.Determinant;

			for (int i = 0; i < KoeffizientenMatrix.ColumnCount; i++)
			{
				Matrix xi = LinearAlgebra.LinearAlgebraOperations.ChangeColumnInMatrix(KoeffizientenMatrix, ExpansionVector, i);
				result[i] = xi.Determinant / inputDeterminante;
			}

			return result;
		}

		private Vector SolveLGSGauß()
		{
			if (KoeffizientenMatrix == null || ExpansionVector == null)
				return null;

			if (KoeffizientenMatrix.ColumnCount != ExpansionVector.Size)
				return null;

			Matrix calcMatrix = new Matrix(KoeffizientenMatrix.Values);
			Vector calcVector = new Vector(ExpansionVector.Values);

			// Elimination
			for (int i = 0; i < calcMatrix.RowCount - 1; i++)
			{
				int actualRow = i;
				int pivotRow = calcMatrix.NextPivotRow(i, i);

				if (actualRow != pivotRow)
					SwitchRows(calcMatrix, calcVector, pivotRow, actualRow);

				EleminatePivotColumn(calcMatrix, calcVector, i, i);
			}

			return ReversePlugIn(calcMatrix, calcVector);
		}

		private Vector SolveLGSJacobi(Vector startValue, double epsilon)
		{
			if (KoeffizientenMatrix == null || ExpansionVector == null)
				return null;

			if (KoeffizientenMatrix.ColumnCount != ExpansionVector.Size)
				return null;

			if (KoeffizientenMatrix.ColumnCount != startValue.Size)
				return null;

			Vector prevVector = null;
			Vector resultVector = startValue.Clone();
			while(prevVector == null || System.Math.Abs(resultVector.Magnitude-prevVector.Magnitude) > epsilon)
			{
				prevVector = resultVector.Clone();
				for (int i = 0; i < ExpansionVector.Size; i++)
				{
					double sigma = 0;
					for(int j = 0; j < ExpansionVector.Size; j++)
					{
						if (i == j)
							continue;

						sigma += KoeffizientenMatrix[i, j] * resultVector[j];
					}
					resultVector[i] = (1 / KoeffizientenMatrix[i, i]) * (ExpansionVector[i] - sigma); 
				}
			}
			return resultVector;
		}

		private double FindVariableValue(double vectorValue, double matrixValue)
		{
			return vectorValue / matrixValue;
		}

		private Vector ReversePlugIn(Matrix calcMatrix, Vector calcVector)
		{
			Vector parameter = new Vector(calcVector.Values);
			parameter.FlushVectorValues();
			for (int k = calcMatrix.RowCount - 1; k >= 0; k--)
			{
				for (int m = 0; m < calcMatrix.ColumnCount; m++)
				{
					if (calcMatrix[k, m] != 0 && k == calcMatrix.RowCount - 1)
					{
						parameter[k] = FindVariableValue(calcVector[k], calcMatrix[k, m]);
					}
					else if (calcMatrix[k, m] != 0)
					{
						for (int i = 0; i < parameter.Size; i++)
						{
							if (parameter[i] != 0)
							{
								double bla = calcMatrix[k, i];
								calcVector[k] -= calcMatrix[k, i] * parameter[i];
							}
						}
						parameter[k] = FindVariableValue(calcVector[k], calcMatrix[k, m]);
						break;
					}
				}
			}
			return parameter;
		}

		/// <summary>
		/// Turns every value in the column under the pivotRow to zero#
		/// Uses for Gauß Algorithm
		/// </summary>
		/// <param name="mSource"></param>
		/// <param name="pivotRow"></param>
		/// <param name="pivotColumn"></param>
		/// <returns></returns>
		private void EleminatePivotColumn(Matrix mSource, Vector vSource, int pivotRow, int pivotColumn)
		{
			if (pivotRow < 0 || pivotRow > mSource.RowCount || pivotColumn < 0 || pivotColumn > mSource.ColumnCount)
				return;

			// Elemination
			for (int i = pivotRow + 1; i < mSource.RowCount; i++)
			{
				if (mSource[i, pivotColumn] == 0)
					continue;

				double lampda = -(mSource[i, pivotColumn] / mSource[pivotRow, pivotColumn]);
				// Eliminate values of Matrix
				for (int k = 0; k < mSource.ColumnCount; k++)
				{
					mSource[i, k] = mSource[pivotRow, k] * lampda + mSource[i, k];
				}
				// Eleminate values of Vector
				vSource[i] = vSource[pivotRow] * lampda + vSource[i];
			}
		}

		/// <summary>
		/// Switch two rows within the matrix
		/// </summary>
		/// <param name="input">Matrix within the rows should switch</param>
		/// <param name="rowToSwitch">index of row that switch</param>
		/// <param name="rowToSwitchWith">index of row to switch with</param>
		/// <returns>Changed matrix or null if it fails</returns>
		private void SwitchRows(Matrix input, Vector v, int rowToSwitch, int rowToSwitchWith)
		{
			if (input == null)
				return;

			if (rowToSwitch < 0 || rowToSwitch > input.RowCount)
				return;

			if (rowToSwitchWith < 0 || rowToSwitchWith > input.RowCount)
				return;

			if (rowToSwitch == rowToSwitchWith)
				return;

			for (int i = 0; i < input.ColumnCount; i++)
			{
				double tempMatrix = input[rowToSwitch, i];
				input[rowToSwitch, i] = input[rowToSwitchWith, i];
				input[rowToSwitchWith, i] = tempMatrix;
			} // end of for

			double tempVector = v[rowToSwitch];
			v[rowToSwitch] = v[rowToSwitchWith];
			v[rowToSwitchWith] = tempVector;
		}

        public Vector GaussSeidel(Matrix matrix, Vector outcome)
        {
            https://www.felixseip.com/gauss-seidel
            Vector expected = new Vector(new double[outcome.Size]);

            double prevMagnitude = expected.Magnitude - 1;
            while (prevMagnitude != expected.Magnitude)
            {
                prevMagnitude = expected.Magnitude;
                for (int i = 0; i < outcome.Size; i++)
                {
                    double sigma = 0;
                    for(int j = 0; j < outcome.Size; j++)
                    {
                        if(j != i)
                        {
                            sigma = sigma + (matrix[i, j] * expected[j]);
                        }
                    }
                    expected[i] = (1 / matrix[i, i]) * (outcome[i] - sigma);
                }
            }

            for(int i = 0; i < expected.Size; i++)
            {
                expected[i] = Math.Round(expected[i], 3);
            }

            return expected;
        }
	}
}
