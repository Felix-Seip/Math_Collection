using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math_Collection.Basics
{
    public class Intervall
    {
        public enum eIntervallFeature
        {
            /// <summary>
            /// { x | a < x < b }
            /// </summary>
            open = 0,
            /// <summary>
            /// { x | a <= x <= b }
            /// </summary>
            closed,
            /// <summary>
            /// { x | a < x <= b }
            /// </summary>
            leftOpenRightClosed,
            /// <summary>
            /// { x | a <= x < b }
            /// </summary>
            leftClosedRightOpen
        }

        /// <summary>
        /// Represents the lower boundary of the intervall
        /// </summary>
        public double minValue { get; set; }
        /// <summary>
        /// Represents the higher boundary of the invervall
        /// </summary>
        public double maxValue { get; set; }
        /// <summary>
        /// Represents the option of the intervall
        /// </summary>
        public eIntervallFeature feature { get; private set; }


        public Intervall(double a, double b, eIntervallFeature f = eIntervallFeature.closed)
        {
            minValue = Math.Min(a,b);
            maxValue = Math.Max(a,b);
            feature = f;
        }
    }
}
