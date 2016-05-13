using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            FillTreeView();
        }

        /// <summary>
        /// Fills the operations tree view.
        /// </summary>
        private void FillTreeView()
        {
            string[] functions = ReadOperationsFile();
            for (int i = 0; i < functions.Length; i++)
            {
                mathOperationsTreeView.Items.Add(CreateTreeViewItem(functions[i]));
            }
        }

        private CustomTreeViewItem CreateTreeViewItem(string header)
        {
            string[] bla = header.Split('-');
            if (header.Contains("Degrees To Radians"))
                return new CustomTreeViewItem(Enums.MathOperations.BasicsDegreesToRadians, header);
            return null;
        }

        private string[] ReadOperationsFile()
        {
            return Properties.Resources.Functions.Split('\n');
        }

    }
}
