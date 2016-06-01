﻿using System;
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
            AddTextBoxes(0, 5);
        }

        private void SecondVectorSizeComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AddTextBoxes(3, 2);
        }

        private void AddTextBoxes(int column, int textboxCount)
        {
            for (int i = 0; i < textboxCount; i++)
            {
                TextBox valueInputTextBox = new TextBox();
                valueInputTextBox.SetValue(Grid.RowProperty, i);
                valueInputTextBox.SetValue(Grid.RowSpanProperty, 1);

                valueInputTextBox.SetValue(Grid.ColumnProperty, column);
                valueInputTextBox.SetValue(Grid.ColumnSpanProperty, 1);
                valueInputTextBox.Margin = new Thickness(10);
                textBoxGrid.Children.Add(valueInputTextBox);
            }
        }
    }
}
