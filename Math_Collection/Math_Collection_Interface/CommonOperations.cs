using Math_Collection.LinearAlgebra.Matrices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Math_Collection_Interface
{
    static class CommonOperations
    {
        public static bool CheckGridTextBoxValues(Grid grid)
        {
            bool allValid = true;
            foreach (TextBox tb in grid.Children)
            {
                tb.BorderBrush = System.Windows.Media.Brushes.Gray;
                if (!ValueValidator.isValidNumber(tb.Text))
                {
                    tb.BorderBrush = System.Windows.Media.Brushes.Red;
                    allValid = false;
                }
            }
            if (!allValid)
                return false;
            return true;
        }

        public static void AddTextBoxesToGrid(Grid grid, int textboxCount)
        {
            grid.Children.Clear();
            for (int i = 0; i < textboxCount; i++)
            {
                TextBox valueInputTextBox = new TextBox();
                valueInputTextBox.SetValue(Grid.RowProperty, i);
                valueInputTextBox.SetValue(Grid.RowSpanProperty, 1);

                valueInputTextBox.SetValue(Grid.ColumnProperty, 0);
                valueInputTextBox.SetValue(Grid.ColumnSpanProperty, 1);
                valueInputTextBox.Margin = new Thickness(10);

                grid.Children.Add(valueInputTextBox);
            }
        }

        public static void AddTextToComboBox(ComboBox comboBox, string file)
        {
            string[] cbValues = ReadFile(file);
            for(int i = 0; i < cbValues.Length; i++)
            {
                comboBox.Items.Add(cbValues[i].Replace("\r", ""));
            }
            comboBox.SelectedIndex = 0;
        }

        private static string[] ReadFile(string file)
        {
            return file.Split('\n');
        }

        public static void AddTextBoxesToGrid(Grid grid, int[] textboxCount, bool allowTextBoxInput, bool transposeTextBoxes = false)
        {
            grid.Children.Clear();
            for (int i = 0; transposeTextBoxes == false ? i < textboxCount[0] : i < textboxCount[1]; i++)
            {
                for (int j = 0; transposeTextBoxes == false ? j < textboxCount[1] : j < textboxCount[0]; j++)
                {
                    CustomTextBox valueInputTextBox;
                    if (allowTextBoxInput)
                        valueInputTextBox = new CustomTextBox("MatrixInputValueTextBox" + i + "" + j);
                    else
                        valueInputTextBox = new CustomTextBox("MatrixOutputValueTextBox" + i + "" + j);

                    valueInputTextBox.SetValue(Grid.RowProperty, i);
                    valueInputTextBox.SetValue(Grid.RowSpanProperty, 1);

                    valueInputTextBox.SetValue(Grid.ColumnProperty, j);
                    valueInputTextBox.SetValue(Grid.ColumnSpanProperty, 1);
                    valueInputTextBox.Margin = new Thickness(10);

                    valueInputTextBox.IsEnabled = allowTextBoxInput;
                    valueInputTextBox.Visibility = Visibility.Visible;

                    grid.Children.Add(valueInputTextBox);
                }
            }
        }

        public static Matrix GetMatrixTextBoxValues(Grid grid, int[] textboxCount)
        {
            double[,] matrixValues = new double[textboxCount[0], textboxCount[1]];
            for (int i = 0; i < textboxCount[0]; i++)
            {
                for (int j = 0; j < textboxCount[1]; j++)
                {
                    for (int k = 0; k < grid.Children.Count; k++)
                    {
                        if (((CustomTextBox)grid.Children[k]).TextBoxName == "MatrixInputValueTextBox" + i + "" + j)
                        {
                            //if(((TextBox)grid.Children[k]).IsValid)
                                double.TryParse(((CustomTextBox)grid.Children[k]).TextBoxName, out matrixValues[i, j]);
                        }
                    }
                }
            }
            return new Matrix(matrixValues);
        }

        public static void SetMatrixResultTextBoxes(Grid grid, Matrix matrix, bool textBoxesAreTransposed = false)
        {
            for (int i = 0; textBoxesAreTransposed == false ? i < matrix.ColumnCount : i < matrix.RowCount; i++)
            {
                for (int j = 0; textBoxesAreTransposed == false ? j < matrix.RowCount : j < matrix.ColumnCount; j++)
                {
                    for (int k = 0; k < grid.Children.Count; k++)
                    {
                        if (((CustomTextBox)grid.Children[k]).TextBoxName.Equals("MatrixOutputValueTextBox" + i + "" + j, StringComparison.InvariantCultureIgnoreCase))
                        {
                            ((CustomTextBox)grid.Children[k]).Text = (matrix[i, j] + "");
                        }
                    }
                }
            }
        }
    }
}
