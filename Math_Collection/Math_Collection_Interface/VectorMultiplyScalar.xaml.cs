﻿using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaktionslogik für VectorMultiplyScalar.xaml
    /// </summary>
    public partial class VectorMultiplyScalar : UserControl
    {
        private List<TextBox> textBoxes;
        public VectorMultiplyScalar()
        {
            textBoxes = new List<TextBox>();
            InitializeComponent();
            CommonOperations.AddTextToComboBox(comboBoxVectorValues, Properties.Resources.Vector_Combo_Box_Values);
        }
        private void AddTextBoxes(int column, int textboxCount)
        {
            TextBoxGrid.Children.Clear();
            textBoxes.Clear();
            lblResult.Content = "";
            for (int i = 0; i < textboxCount; i++)
            {
                TextBox valueInputTextBox = new TextBox();
                valueInputTextBox.SetValue(Grid.RowProperty, i);
                valueInputTextBox.SetValue(Grid.RowSpanProperty, 1);

                valueInputTextBox.SetValue(Grid.ColumnProperty, column);
                valueInputTextBox.SetValue(Grid.ColumnSpanProperty, 1);
                valueInputTextBox.Margin = new Thickness(10);
                textBoxes.Add(valueInputTextBox);
                TextBoxGrid.Children.Add(valueInputTextBox);
            }
        }

        private void comboBoxVectorValues_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object temp = comboBoxVectorValues.SelectedValue;
            int count = ComboBoxParser.VectorComboBoxCount((string)temp);
            AddTextBoxes(0, count);
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            bool allValid = true;
            double[] values = new double[textBoxes.Count];
            for (int i = 0; i < textBoxes.Count; i++)
            {
                textBoxes[i].BorderBrush = Brushes.Gray;
                if (!ValueValidator.isValidNumber(textBoxes[i].Text))
                {
                    textBoxes[i].BorderBrush = Brushes.Red;
                    allValid = false;
                }
                double.TryParse(textBoxes[i].Text, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out values[i]);
            }
            if (!allValid)
                return;

            Math_Collection.LinearAlgebra.Vectors.Vector v = new Math_Collection.LinearAlgebra.Vectors.Vector(values);
            double scalar = double.Parse(txtBoxScalar.Text);
            lblResult.FontSize = 20;
            lblResult.Content = "Result: \r\n" + Math_Collection.LinearAlgebra.LinearAlgebraOperations.MultiplyVectorWithScalar(v, scalar).ToString(); 
        }
    }
}
