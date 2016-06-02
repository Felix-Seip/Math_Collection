using Math_Collection.LinearAlgebra;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Math_Collection_Interface
{
    /// <summary>
    /// Interaction logic for VectorAddition.xaml
    /// </summary>
    public partial class VectorAddition : Page
    {
        public VectorAddition()
        {
            InitializeComponent();
            FillComboBoxes();
        }

        private void FillComboBoxes()
        {
            string[] vectorPossibilities = ReadVectorComboBoxValues();
            for (int i = 0; i < vectorPossibilities.Length; i++)
            {
                firstVectorSizeComboBox.Items.Add(vectorPossibilities[i]);
                secondVectorSizeComboBox.Items.Add(vectorPossibilities[i]);
            }
            firstVectorSizeComboBox.SelectedIndex = 0;
            secondVectorSizeComboBox.SelectedIndex = 0;
        }

        private string[] ReadVectorComboBoxValues()
        {
            return Properties.Resources.Vector_Combo_Box_Values.Split('\n');
        }

        private void firstVectorSizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object temp = firstVectorSizeComboBox.SelectedValue;
            int count = ComboBoxParser.ComboBoxCount((string)temp);
            CommonOperations.AddTextBoxesToGrid(firstVectorValuesGrid, count);
        }

        private void secondVectorSizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object temp = secondVectorSizeComboBox.SelectedValue;
            int count = ComboBoxParser.ComboBoxCount((string)temp);
            CommonOperations.AddTextBoxesToGrid(secondVectorValuesGrid, count);
        }

        private void calculateButton_Click(object sender, System.Windows.RoutedEventArgs e)
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
                resultLabel.Content = "Result: " + result;
                resultLabel.FontSize = 20;
            }
        }
    }
}
