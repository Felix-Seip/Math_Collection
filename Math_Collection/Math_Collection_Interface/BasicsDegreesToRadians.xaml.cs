using Math_Collection.Basics;
using System;
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
    /// Interaction logic for BasicsDegreesToRadians.xaml
    /// </summary>
    public partial class BasicsDegreesToRadians : Page
    {
        public BasicsDegreesToRadians()
        {
            InitializeComponent();
        }

        private void calculateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double radians = Basics.DegreesToRadians(double.Parse(degreesInputTextBox.Text));
                radiansOutputTextBox.Text = "" + radians + "";
            }
            catch(Exception ex)
            {
                MessageBox.Show("Please enter a valid degree value.");
            }
        }
    }
}
