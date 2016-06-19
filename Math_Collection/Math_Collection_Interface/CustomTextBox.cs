using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Math_Collection_Interface
{
    class CustomTextBox : TextBox
    {
        public string TextBoxName { get; private set; }
        public CustomTextBox(string name)
        {
            TextBoxName = name;
        }
    }
}
