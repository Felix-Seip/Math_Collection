using System.Collections.Generic;
using System.Linq;
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
    public partial class VectorDotProduct : UserControl
    {
        public VectorDotProduct()
        {
            InitializeComponent();
            FillComboBoxes();
        }

        private void FillComboBoxes()
        {
            string[] vectorPossibilities = ReadVectorComboBoxValues();
            for(int i = 0; i < vectorPossibilities.Length; i++)
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

        }

        private void SecondVectorSizeComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
