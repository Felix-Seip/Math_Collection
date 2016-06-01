using System;
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
    /// Interaktionslogik für VectorMagnitude.xaml
    /// </summary>
    public partial class VectorMagnitude : Page
    {
        private List<TextBox> textBoxes;

        public VectorMagnitude()
        {
            textBoxes = new List<TextBox>();
            InitializeComponent();
            FillComboBox();  
        }

        private void FillComboBox()
        {
            string[] vectorPossibilities = ReadVectorComboBoxValues();
            for (int i = 0; i < vectorPossibilities.Length; i++)
            {
                comboBoxVector.Items.Add(vectorPossibilities[i]);
            }
            comboBoxVector.SelectedIndex = 0;
        }

        private string[] ReadVectorComboBoxValues()
        {
            return Properties.Resources.Vector_Combo_Box_Values.Split('\n');
        }

        private void comboBoxVector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object temp = comboBoxVector.SelectedValue;
            int count = ComboBoxParser.ComboBoxCount((string)temp);
            AddTextBoxes(0, count);
        }

        private void AddTextBoxes(int column, int textboxCount)
        {
            textBoxGrid.Children.Clear();
            for (int i = 0; i < textboxCount; i++)
            {
                TextBox valueInputTextBox = new TextBox();
                valueInputTextBox.SetValue(Grid.RowProperty, i);
                valueInputTextBox.SetValue(Grid.RowSpanProperty, 1);

                valueInputTextBox.SetValue(Grid.ColumnProperty, column);
                valueInputTextBox.SetValue(Grid.ColumnSpanProperty, 1);
                valueInputTextBox.Margin = new Thickness(10);
                textBoxes.Add(valueInputTextBox);
                textBoxGrid.Children.Add(valueInputTextBox);
            }
        }
        
        private void btnCalcMagnitude_Click(object sender, RoutedEventArgs e)
        {
            bool allValid = true;
            foreach(TextBox tb in textBoxes)
            {
                tb.BorderBrush = Brushes.Gray;
                if (!ValueValidator.isValidNumber(tb.Text))
                {
                    tb.BorderBrush = Brushes.Red;
                    allValid = false;
                }
            }
            if (!allValid)
                return;

            double totalCount = 1.0;
            foreach(TextBox tb in textBoxes)
            {
                totalCount *= double.Parse(tb.Text,CultureInfo.InvariantCulture);
            }
            result.FontSize = 20;
            result.Content = "Result: " + Math.Sqrt(totalCount);
        }
    }
}
