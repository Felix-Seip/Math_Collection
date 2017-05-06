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
    /// Interaction logic for MatrixPotentiate.xaml
    /// </summary>
    public partial class MatrixPotentiate : UserControl
    {
        int matrixPower = 0;
        public MatrixPotentiate()
        {
            InitializeComponent();
            CommonOperations.AddTextToComboBox(matrixSizeComboBox, Properties.Resources.Matrix_Combo_Box_Values);
        }

        private void MatrixPowerChanged(object sender, TextChangedEventArgs e)
        {
            int.TryParse(matrixPowerTextBox.Text, out matrixPower);
        }

        private void PotentiateMatrixButtonClicked(object sender, RoutedEventArgs e)
        {
            Math_Collection.LinearAlgebra.Matrices.Matrix matrix = CommonOperations.GetMatrixTextBoxValues(matrixValuesGrid, ComboBoxParser.MatrixComboBoxCount((string)matrixSizeComboBox.SelectedValue));
            CommonOperations.SetMatrixResultTextBoxes(potentiatedMatrixValuesGrid, Math_Collection.LinearAlgebra.LinearAlgebraOperations.MultiplyMatrixWithItself(matrix, matrixPower));
        }

        private void MatrixDimensionsChanged(object sender, SelectionChangedEventArgs e)
        {
            CommonOperations.AddTextBoxesToGrid(matrixValuesGrid, ComboBoxParser.MatrixComboBoxCount((string)matrixSizeComboBox.SelectedValue), true);
            CommonOperations.AddTextBoxesToGrid(potentiatedMatrixValuesGrid, ComboBoxParser.MatrixComboBoxCount((string)matrixSizeComboBox.SelectedValue), false);
        }
    }
}
