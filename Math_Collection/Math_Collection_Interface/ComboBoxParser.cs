using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math_Collection_Interface
{
    static class ComboBoxParser
    {
        public static int ComboBoxCount(string s)
        {
            int comboBoxCount = -1;
            if (s.ToLower().Contains("vector"))
            {
                try
                {
                    string numberOfComboBoxes = s.Substring(s.IndexOf('x') + 1, 1);
                    int.TryParse(numberOfComboBoxes, out comboBoxCount);
                }
                catch (ArgumentException ex)
                {
                    comboBoxCount = -1;
                }
            }
            else if (s.ToLower().Contains("matrix"))
            {
                try
                {
                    string numberOfComboBoxes = s.Substring(s.IndexOf(' ') + 1);
                    char[] values = numberOfComboBoxes.ToCharArray();
                    int firstValue, secoundValue;
                    int.TryParse(values[0].ToString(), out firstValue);
                    int.TryParse(values[2].ToString(), out secoundValue);
                    comboBoxCount = firstValue * secoundValue;
                }catch(ArgumentException ex)
                {
                    comboBoxCount = -1;
                }
            }
            return comboBoxCount;
        }
    }
}
