using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Math_Collection_Interface
{
    /// <summary>
    /// Interaction logic for MatrixMultiplicationWithScalar.xaml
    /// </summary>
    public partial class MatrixMultiplicationWithScalar : UserControl
    {
        public MatrixMultiplicationWithScalar()
        {
            InitializeComponent();
            CommonOperations.AddTextToComboBox(comboBoxMatrixSize, Properties.Resources.Matrix_Combo_Box_Values);
        }

        private void MatrixDimensionsChanged(object sender, SelectionChangedEventArgs e)
        {
            CommonOperations.AddTextBoxesToGrid(matrixGrid, ComboBoxParser.MatrixComboBoxCount((string)comboBoxMatrixSize.SelectedValue), true);
            CommonOperations.AddTextBoxesToGrid(resultMatrixGrid, ComboBoxParser.MatrixComboBoxCount((string)comboBoxMatrixSize.SelectedValue), false);
        }

        private void btnMultiplication_Click(object sender, RoutedEventArgs e)
        {
            Math_Collection.LinearAlgebra.Matrices.Matrix inputMatrix = CommonOperations.GetMatrixTextBoxValues(matrixGrid, ComboBoxParser.MatrixComboBoxCount((string)comboBoxMatrixSize.SelectedValue));
            CommonOperations.SetMatrixResultTextBoxes(resultMatrixGrid, Math_Collection.LinearAlgebra.LinearAlgebraOperations.MultiplyMatrixWithScalar(inputMatrix, double.Parse(txtBoxScalar.Text)));
        }
    }
}
