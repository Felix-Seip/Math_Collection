using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for ValidTextBox.xaml
    /// </summary>
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(System.ComponentModel.Design.IDesigner))]
    public partial class ValidTextBox : UserControl
    {
        private bool _isValid;
        public bool IsValid
        {
            get { return _isValid; }
            set
            {
                _isValid = value;

                txtBox.BorderThickness = new Thickness(2);
                if (_isValid)
                    txtBox.BorderBrush = Brushes.Green;
                else
                    txtBox.BorderBrush = Brushes.Red;
            }
        }

        private string _header;
        public string Header
        {
            get { return _header; }
            set
            {
                if(String.IsNullOrEmpty(_header))
                    _header = value;

                lblHeader.Content = _header;
            }
        }
        public ValidTextBox(string header = "")
        {
            InitializeComponent();
            IsValid = true;
            Header = header;
        }

        private void txtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsValid = ValueValidator.isValidNumber(txtBox.Text);
        }
    }
}
