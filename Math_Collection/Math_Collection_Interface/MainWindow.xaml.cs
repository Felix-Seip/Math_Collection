using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
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

        private object CreateTreeViewItem(string header)
        {
            string[] splitHeader = new string[0];
            if (header.Contains('-'))
                splitHeader = header.Split('-');
            foreach (Enums.MathOperations enumValue in Enum.GetValues(typeof(Enums.MathOperations)))
            {
                foreach (MemberInfo enumMemberInfo in (typeof(Enums.MathOperations)).GetMember(enumValue.ToString()))
                {
                    object[] enumAttributeInfo = enumMemberInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                    for(int i = 0; i < enumAttributeInfo.Length; i++)
                    {
                        if(splitHeader.Length > 1)
                        {
                            for(int j = 0; j < splitHeader.Length; j++)
                            {
                                if (((DescriptionAttribute)enumAttributeInfo[i]).Description.Contains(splitHeader[j]))
                                    return new CustomTreeViewItem(enumValue, header);
                            }
                        }
                        else
                        {
                            return new TreeViewItem().Header = header;
                        }
                    }
                }
            }
            return null;
        }

        private string[] ReadOperationsFile()
        {
            return Properties.Resources.Functions.Split('\n');
        }

        private void SelectedOperationChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if(e.NewValue.GetType() == typeof(CustomTreeViewItem))
            {
                CustomTreeViewItem treeItem = (CustomTreeViewItem)e.NewValue;
                operationContentControl.Content = treeItem.UserInterface;
            }
        }
    }
}
