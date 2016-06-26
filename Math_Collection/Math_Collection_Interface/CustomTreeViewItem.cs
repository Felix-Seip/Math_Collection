using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Math_Collection_Interface
{
    public class CustomTreeViewItem : TreeViewItem
    {
        public object UserInterface { get; private set; }
        public CustomTreeViewItem(Enums.MathOperations operation, string header)
        {
            UserInterface = CheckOperationType(operation);
            if (header.Contains("\r") || header.Contains("\n"))
            {
                header = header.Replace("\r", "");
                header = header.Replace("\n", "");
            }

            Header = header;
        }

        /// <summary>
        /// Check the operation type to create the corresponding user interface.
        /// </summary>
        private object CheckOperationType(Enums.MathOperations operation)
        {
            switch(operation)
            {
                case Enums.MathOperations.BasicsDegreesToRadians:
                    return new BasicsDegreesToRadians();

                case Enums.MathOperations.VectorDotProduct:
                    return new VectorDotProduct();
                case Enums.MathOperations.VectorMagnitude:
                    return new VectorMagnitude();
                case Enums.MathOperations.VectorAdditionWithVector:
                    return new VectorAddition();
                case Enums.MathOperations.VectorSubtractionWithVector:
                    return new VectorSubtraction();
                case Enums.MathOperations.VectorMultiplicationWithVector:
                    return new VectorMultiplication();
                case Enums.MathOperations.VectorMultiplicationWithScalar:
                    return new VectorMultiplyScalar();

                case Enums.MathOperations.MatrixTranspose:
                    return new MatrixTranspose();
                case Enums.MathOperations.MatrixMultiplicationWithScalar:
                    return null;
                case Enums.MathOperations.MatrixMultiplicationWithVector:
                    return null;
                case Enums.MathOperations.MatrixMultiplicationWithMatrix:
                    return null;
                case Enums.MathOperations.MatrixPotentiation:
                    return new MatrixPotentiate();
                case Enums.MathOperations.MatrixLowerLeftPyramidForm:
                    return null;
                case Enums.MathOperations.MatrixUpperRightPyramidForm:
                    return null;

                case Enums.MathOperations.AnalysisDerivationOfFunction:
                    return null;
            }
            return null;
        }
    }
}
