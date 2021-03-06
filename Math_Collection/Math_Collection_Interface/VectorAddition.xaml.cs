﻿using Math_Collection.LinearAlgebra;
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
        private bool selectionChanged = false; //Rekursion verhindern
        public VectorAddition()
        {
            InitializeComponent();
            CommonOperations.AddTextToComboBox(firstVectorSizeComboBox, Properties.Resources.Vector_Combo_Box_Values);
            CommonOperations.AddTextToComboBox(secondVectorSizeComboBox, Properties.Resources.Vector_Combo_Box_Values);
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

        private void CalculateButton_Click(object sender, System.Windows.RoutedEventArgs e)
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

            Math_Collection.LinearAlgebra.Vectors.Vector result = LinearAlgebraOperations.AddVectorToVector(new Math_Collection.LinearAlgebra.Vectors.Vector(firstVectorValues.ToArray()), new Math_Collection.LinearAlgebra.Vectors.Vector(secondVectorValues.ToArray()));
            resultLabel.Content = "Result: \n" + result;
            resultLabel.FontSize = 20;
        }
    }
}

