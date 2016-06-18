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

        public static void AddTextBoxesToGrid(Grid grid, int[] textboxCount, bool allowTextBoxInput)
        {
            grid.Children.Clear();
            for (int i = 0; i < textboxCount[0]; i++)
            {
                for (int j = 0; j < textboxCount[1]; j++)
                {
                    TextBox valueInputTextBox = new TextBox();
                    valueInputTextBox.SetValue(Grid.RowProperty, i);
                    valueInputTextBox.SetValue(Grid.RowSpanProperty, 1);

                    valueInputTextBox.SetValue(Grid.ColumnProperty, j);
                    valueInputTextBox.SetValue(Grid.ColumnSpanProperty, 1);
                    valueInputTextBox.Margin = new Thickness(10);

                    valueInputTextBox.IsEnabled = allowTextBoxInput;

                    if (allowTextBoxInput)
                        valueInputTextBox.SetValue(FrameworkElement.NameProperty, "MatrixInputValueTextBox" + i + "" + j);
                    else
                        valueInputTextBox.SetValue(FrameworkElement.NameProperty, "MatrixOutputValueTextBox" + i + "" + j);
                    grid.Children.Add(valueInputTextBox);
                }
            }
        }

        public static void SetMatrixResultTextBoxes(Grid grid, int[] textboxCount)
        {
            //for (int i = 0; i < textboxCount[0]; i++)
            //{
            //    for (int j = 0; j < textboxCount[1]; j++)
            //    {


            //       // grid.Children.Add(valueInputTextBox);
            //    }
            //}
        }

        public static Matrix GetMatrixTextBoxValues(Grid grid, int[] textboxCount)
        {
            double[,] matrixValues = new double[textboxCount[0], textboxCount[1]];
            for (int i = 0; i < textboxCount[0]; i++)
            {
                for (int j = 0; j < textboxCount[1]; j++)
                {
                    string bla1 = "MatrixInputValueTextBox" + i + "" + j;
                    object textBox = grid.FindName("MatrixInputValueTextBox" + i + "" + j);
                    string bla = ((TextBox)textBox).Text;
                }
            }
            return new Matrix(matrixValues);
        }
    }
}
