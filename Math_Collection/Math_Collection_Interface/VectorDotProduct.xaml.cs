using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Math_Collection.LinearAlgebra;
using System.Drawing;

namespace Math_Collection_Interface
{
    /// <summary>
    /// Interaction logic for VectorDotProduct.xaml
    /// </summary>
    public partial class VectorDotProduct : Page
    {
        public VectorDotProduct()
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

        private void FirstVectorSizeComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object temp = firstVectorSizeComboBox.SelectedValue;
            int count = ComboBoxParser.ComboBoxCount((string)temp);
            AddTextBoxes(0, count, "first");
        }

        private void SecondVectorSizeComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object temp = secondVectorSizeComboBox.SelectedValue;
            int count = ComboBoxParser.ComboBoxCount((string)temp);
            AddTextBoxes(3, count, "second");
        }

        private void AddTextBoxes(int column, int textboxCount, string firstOrSecondVector)
        {
            for(int i = 0; i < textBoxGrid.Children.Count; i++)
            {
                if (((string)(textBoxGrid.Children[i].GetValue(NameProperty))).Contains(firstOrSecondVector))
                    textBoxGrid.Children.RemoveAt(i);
            }
            for (int i = 0; i < textboxCount; i++)
            {
                TextBox valueInputTextBox = new TextBox();
                valueInputTextBox.SetValue(Grid.RowProperty, i);
                valueInputTextBox.SetValue(Grid.RowSpanProperty, 1);

                valueInputTextBox.SetValue(Grid.ColumnProperty, column);
                valueInputTextBox.SetValue(Grid.ColumnSpanProperty, 1);
                valueInputTextBox.Margin = new Thickness(10);
                valueInputTextBox.SetValue(NameProperty, firstOrSecondVector);
                textBoxGrid.Children.Add(valueInputTextBox);
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (CommonOperations.CheckGridTextBoxValues(textBoxGrid))
            {
                List<double> firstVectorValues = new List<double>();
                List<double> secondVectorValues = new List<double>();

                for (int i = 0; i < textBoxGrid.Children.Count; i++)
                {
                    if (((string)(textBoxGrid.Children[i].GetValue(NameProperty))).Contains("first"))
                        firstVectorValues.Add(double.Parse(((TextBox)textBoxGrid.Children[i]).Text));
                    else
                        secondVectorValues.Add(double.Parse(((TextBox)textBoxGrid.Children[i]).Text));
                }

                double result = LinearAlgebraOperations.DotProduct(new Math_Collection.LinearAlgebra.Vectors.Vector(firstVectorValues.ToArray()), new Math_Collection.LinearAlgebra.Vectors.Vector(secondVectorValues.ToArray()));
                resultLabel.Content = "Result: " + result;
                resultLabel.FontSize = 20;
            }
        }
    }
}
