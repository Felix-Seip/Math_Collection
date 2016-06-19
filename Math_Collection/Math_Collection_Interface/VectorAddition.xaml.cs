using Math_Collection.LinearAlgebra;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Math_Collection_Interface
{
    /// <summary>
    /// Interaction logic for VectorAddition.xaml
    /// </summary>
    public partial class VectorAddition : UserControl
    {
        public VectorAddition()
        {
            InitializeComponent();
            CommonOperations.AddTextToComboBox(firstVectorSizeComboBox, Properties.Resources.Vector_Combo_Box_Values);
            CommonOperations.AddTextToComboBox(secondVectorSizeComboBox, Properties.Resources.Vector_Combo_Box_Values);
        }

        private void FirstVectorSizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object temp = firstVectorSizeComboBox.SelectedValue;
            CommonOperations.AddTextBoxesToGrid(firstVectorValuesGrid, ComboBoxParser.VectorComboBoxCount((string)temp));
        }

        private void SecondVectorSizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object temp = secondVectorSizeComboBox.SelectedValue;
            CommonOperations.AddTextBoxesToGrid(secondVectorValuesGrid, ComboBoxParser.VectorComboBoxCount((string)temp));
        }

        private void CalculateButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if(CommonOperations.CheckGridTextBoxValues(firstVectorValuesGrid) && CommonOperations.CheckGridTextBoxValues(secondVectorValuesGrid))
            {
                List<double> firstVectorValues = new List<double>();
                List<double> secondVectorValues = new List<double>();

                for (int i = 0; i < firstVectorValuesGrid.Children.Count; i++)
                    firstVectorValues.Add(double.Parse(((TextBox)firstVectorValuesGrid.Children[i]).Text));
                for (int i = 0; i < secondVectorValuesGrid.Children.Count; i++)
                    secondVectorValues.Add(double.Parse(((TextBox)secondVectorValuesGrid.Children[i]).Text));

                Math_Collection.LinearAlgebra.Vectors.Vector result = LinearAlgebraOperations.AddVectorToVector(new Math_Collection.LinearAlgebra.Vectors.Vector(firstVectorValues.ToArray()), new Math_Collection.LinearAlgebra.Vectors.Vector(secondVectorValues.ToArray()));
                resultLabel.Content = "Result: \n" + result;
                resultLabel.FontSize = 20;
            }
        }
    }
}
