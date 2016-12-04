using Math_Collection.LinearAlgebra.Matrices;
using Math_Collection.LinearAlgebra.Vectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math_Collection
{
	public class LGS
	{

		public enum LGSType
		{
			/// <summary>
			/// Default value. LGS is not solved yet
			/// </summary>
			NotSolved,
			/// <summary>
			/// LGS have no solution
			/// </summary>
			Unsolvable,
			/// <summary>
			/// LGS have just one unique solution
			/// </summary>
			Unique,
			/// <summary>
			/// LGS have infinite solutions
			/// </summary>
			Infinite
		}

		public enum SolveAlgorithm
		{
			/// <summary>
			/// Uses the algorithm that is best for the given input
			/// </summary>
			Automatic,
			/// <summary>
			/// Solves the LGS only approximated
			/// </summary>
			Approximated,
			/// <summary>
			/// Solves the LGS with the Gramersche Rule
			/// https://de.wikipedia.org/wiki/Cramersche_Regel
			/// </summary>
			Determinant,
			/// <summary>
			/// Solves the LGS with the Gauß Algorithm
			/// https://de.wikipedia.org/wiki/Gau%C3%9Fsches_Eliminationsverfahren
			/// </summary>
			Gauß
		}

		private LGSType _resultType;
		public LGSType ResultType
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

		public LGS(Matrix input,Vector outcome)
		{
			KoeffizientenMatrix = input;
			ExpansionVector = outcome;
			ResultType = LGSType.NotSolved;
		}

		public Vector Solve(SolveAlgorithm usedAlgorithm = SolveAlgorithm.Automatic)
		{
			if (!KoeffizientenMatrix.IsSqaureMatrix)
				return null;

			if (usedAlgorithm != SolveAlgorithm.Automatic)
			{
				switch (usedAlgorithm)
				{
					case SolveAlgorithm.Approximated:
					return SolveLGSApproximated(50);
					case SolveAlgorithm.Determinant:
					return SolveLGSDeterminant();
					case SolveAlgorithm.Gauß:
					return SolveLGSGauß();
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
							sigma += KoeffizientenMatrix[i,j] * solvedVector[j];
					}
					solvedVector.Values[i] = (ExpansionVector[i] - sigma) / KoeffizientenMatrix[i,i];
				}
			}
			ResultType = LGSType.Unique;
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
				Matrix xi = LinearAlgebra.LinearAlgebraOperations.ChangeColumnInMatrix(KoeffizientenMatrix,ExpansionVector,i);
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

			// Elemination
			for (int i = 0; i < calcMatrix.RowCount - 1; i++)
			{
				int actualRow = i;

				int pivotRow = calcMatrix.NextPivotRow(i,i);

				if (actualRow != pivotRow)
					SwitchRows(calcMatrix,calcVector,pivotRow,actualRow);

				EleminatePivotColumn(calcMatrix,calcVector,i,i);
			}

			Vector result = new Vector(Enumerable.Repeat(1.0,ExpansionVector.Size).ToArray());

			//TODO: Plug In
			for (int k = KoeffizientenMatrix.RowCount - 1; k >= 0; k--)
			{
				double parameter = 0;
				for (int m = 0; m < KoeffizientenMatrix.ColumnCount; m++)
				{
					parameter += calcMatrix[k,m] * result[m];
				}
				result[k] = calcVector[k] / parameter;
			}

			return result;
		}

		/// <summary>
		/// Turns every value in the column under the pivotRow to zero#
		/// Uses for Gauß Algorithm
		/// </summary>
		/// <param name="mSource"></param>
		/// <param name="pivotRow"></param>
		/// <param name="pivotColumn"></param>
		/// <returns></returns>
		private void EleminatePivotColumn(Matrix mSource,Vector vSource,int pivotRow,int pivotColumn)
		{
			if (pivotRow < 0 || pivotRow > mSource.RowCount || pivotColumn < 0 || pivotColumn > mSource.ColumnCount)
				return;

			// Elemination
			for (int i = pivotRow + 1; i < mSource.RowCount; i++)
			{
				if (mSource[i,pivotColumn] == 0)
					continue;

				double lampda = -(mSource[i,pivotColumn] / mSource[pivotRow,pivotColumn]);
				// Eliminate values of Matrix
				for (int k = 0; k < mSource.ColumnCount; k++)
				{
					mSource[i,k] = mSource[pivotRow,k] * lampda + mSource[i,k];
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
		private void SwitchRows(Matrix input,Vector v,int rowToSwitch,int rowToSwitchWith)
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
				double tempMatrix = input[rowToSwitch,i];
				input[rowToSwitch,i] = input[rowToSwitchWith,i];
				input[rowToSwitchWith,i] = tempMatrix;
			} // end of for

			double tempVector = v[rowToSwitch];
			v[rowToSwitch] = v[rowToSwitchWith];
			v[rowToSwitchWith] = tempVector;
		}


	}
}
