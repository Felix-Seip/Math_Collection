using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math_Collection_Interface
{
    static class ComboBoxParser
    {
        public static int VectorComboBoxCount(string s)
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
            return comboBoxCount;
        }

        public static int[] MatrixComboBoxCount(string matrixPossibilityString)
        {
            int[] matrixDimensions = new int[2];
            if (matrixPossibilityString.ToLower().Contains("matrix"))
            {
                try
                {
                    string numberOfComboBoxes = matrixPossibilityString.Substring(matrixPossibilityString.IndexOf(' ') + 1);
                    char[] values = numberOfComboBoxes.ToCharArray();

                    int firstValue, secoundValue;
                    int.TryParse(values[0].ToString(), out firstValue);
                    int.TryParse(values[2].ToString(), out secoundValue);

                    matrixDimensions[0] = firstValue;
                    matrixDimensions[1] = secoundValue;
                }
                catch (ArgumentException ex)
                {
                    matrixDimensions = null;
                }
            }
            return matrixDimensions;
        }
    }
}
