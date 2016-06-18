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
    /// Interaction logic for MatrixTranspose.xaml
    /// </summary>
    public partial class MatrixTranspose : Page
    {
        public MatrixTranspose()
        {
            InitializeComponent();
            FillMatrixComboBox();
        }

        private void FillMatrixComboBox()
        {
            string[] matrixPossibilities = ReadMatrixComboBoxValues();
            for (int i = 0; i < matrixPossibilities.Length; i++)
            {
                matrixSizeComboBox.Items.Add(matrixPossibilities[i]);
            }
            matrixSizeComboBox.SelectedIndex = 0;
        }

        private string[] ReadMatrixComboBoxValues()
        {
            return Properties.Resources.Matrix_Combo_Box_Values.Split('\n');
        }

        private void MatrixDimensionsChanged(object sender, SelectionChangedEventArgs e)
        {
            CommonOperations.AddTextBoxesToGrid(matrixValuesGrid, ComboBoxParser.MatrixComboBoxCount((string)matrixSizeComboBox.SelectedValue), true);
            CommonOperations.AddTextBoxesToGrid(transposedMatrixValuesGrid, ComboBoxParser.MatrixComboBoxCount((string)matrixSizeComboBox.SelectedValue), false);
        }

        private void TransposeMatrixButton_Click(object sender, RoutedEventArgs e)
        {
            CommonOperations.GetMatrixTextBoxValues(matrixValuesGrid, ComboBoxParser.MatrixComboBoxCount((string)matrixSizeComboBox.SelectedValue));
            //CommonOperations.SetMatrixResultTextBoxes(transposedMatrixValuesGrid, ComboBoxParser.MatrixComboBoxCount((string)matrixSizeComboBox.SelectedValue));
        }
    }
}
