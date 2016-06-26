using Math_Collection.LinearAlgebra;
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
    /// Interaction logic for VectorSubtraction.xaml
    /// </summary>
    public partial class VectorSubtraction : UserControl
    {
        private bool selectionChanged = false;
        public VectorSubtraction()
        {
            InitializeComponent();
            CommonOperations.AddTextToComboBox(firstVectorSizeComboBox, Properties.Resources.Vector_Combo_Box_Values);
            CommonOperations.AddTextToComboBox(secondVectorSizeComboBox, Properties.Resources.Vector_Combo_Box_Values);
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            if (!CommonOperations.CheckGridTextBoxValues(firstVectorValuesGrid))
                return;
            if (!CommonOperations.CheckGridTextBoxValues(secondVectorValuesGrid))
                return;

            List<double> firstVectorValues = new List<double>();
            List<double> secondVectorValues = new List<double>();

            for (int i = 0; i < firstVectorValuesGrid.Children.Count; i++)
                firstVectorValues.Add(double.Parse(((TextBox)firstVectorValuesGrid.Children[i]).Text));
            for (int i = 0; i < secondVectorValuesGrid.Children.Count; i++)
                secondVectorValues.Add(double.Parse(((TextBox)secondVectorValuesGrid.Children[i]).Text));

            Math_Collection.LinearAlgebra.Vectors.Vector result = LinearAlgebraOperations.SubtractVectorFromVector(new Math_Collection.LinearAlgebra.Vectors.Vector(firstVectorValues.ToArray()), new Math_Collection.LinearAlgebra.Vectors.Vector(secondVectorValues.ToArray()));
            resultLabel.Content = "Result: \n" + result;
            resultLabel.FontSize = 20;
        }

        private void SecondVectorSizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (selectionChanged)
                return;

            CommonOperations.AddTextBoxesToGrid(secondVectorValuesGrid, ComboBoxParser.VectorComboBoxCount((string)secondVectorSizeComboBox.SelectedValue));
            selectionChanged = true;
            firstVectorSizeComboBox.SelectedValue = secondVectorSizeComboBox.SelectedValue;
            selectionChanged = false;
            CommonOperations.AddTextBoxesToGrid(firstVectorValuesGrid, ComboBoxParser.VectorComboBoxCount((string)firstVectorSizeComboBox.SelectedValue));

        }

        private void FirstVectorSizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (selectionChanged)
                return;

            CommonOperations.AddTextBoxesToGrid(firstVectorValuesGrid, ComboBoxParser.VectorComboBoxCount((string)firstVectorSizeComboBox.SelectedValue));
            selectionChanged = true;
            secondVectorSizeComboBox.SelectedValue = firstVectorSizeComboBox.SelectedValue;
            selectionChanged = false;
            CommonOperations.AddTextBoxesToGrid(secondVectorValuesGrid, ComboBoxParser.VectorComboBoxCount((string)secondVectorSizeComboBox.SelectedValue));
        }
    }
}
