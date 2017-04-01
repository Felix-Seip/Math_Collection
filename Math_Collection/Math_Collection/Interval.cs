using System;

namespace Math_Collection.Basics
{
    public class Interval
    {
        /// <summary>
        /// Represents the lower boundary of the intervall
        /// </summary>
        public double MinValue { get; set; }
        /// <summary>
        /// Represents the higher boundary of the invervall
        /// </summary>
        public double MaxValue { get; set; }
        /// <summary>
        /// Represents the option of the intervall
        /// </summary>
        public Enums.eIntervalFeature feature { get; private set; }

		public double Range
		{
			get
			{
				return MaxValue - MinValue;
			}
		}


        public Interval(double a, double b, Enums.eIntervalFeature f = Enums.eIntervalFeature.eClosed)
        {
            MinValue = Math.Min(a,b);
            MaxValue = Math.Max(a,b);
            feature = f;
        }
    }
}
