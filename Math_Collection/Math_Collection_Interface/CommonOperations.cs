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

    }
}
