using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math_Collection_Interface
{
    static class ValueValidator
    {
        private const double cMinValue = -5000.0;
        private const double cMaxValue = 5000.0;

        public static bool isValidNumber(string s)
        {
            if (String.IsNullOrEmpty(s))
                return false;

            try
            {
                double number;
                bool success = Double.TryParse(s, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out number);
                if (success)
                    if (isNumberInRange(number))
                        return true;
                    else
                        return false;
                else
                    return false;

            }
            catch (ArgumentException ex)
            {
                return false;
            }
        }

        public static bool isNumberInRange(double number, double minValue = cMinValue, double maxValue = cMaxValue)
        {
            return number >= minValue && number <= maxValue;
        }

    }
}
