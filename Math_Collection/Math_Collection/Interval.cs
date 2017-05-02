using System;

namespace Math_Collection.Basics
{
	public class Interval
	{
		private double _minValue;
		/// <summary>
		/// Represents the lower boundary of the intervall
		/// </summary>
		public double MinValue
		{
			get
			{
				if (Feature == Enums.eIntervalFeature.eOpen || Feature == Enums.eIntervalFeature.eLeftOpenRightClosed)
					return _minValue + Step;

				return _minValue;
			}
			set
			{
				_minValue = value;
			}
		}

		private double _maxValue;
		/// <summary>
		/// Represents the higher boundary of the invervall
		/// </summary>
		public double MaxValue
		{
			get
			{
				if (Feature == Enums.eIntervalFeature.eOpen || Feature == Enums.eIntervalFeature.eLeftClosedRightOpen)
					return _maxValue - Step;

				return _maxValue;
			}
			set
			{
				_maxValue = value;
			}
		}

		/// <summary>
		/// Represents the increment value
		/// </summary>
		public double Step { get; private set; }

		/// <summary>
		/// Represents the option of the intervall
		/// </summary>
		public Enums.eIntervalFeature Feature { get; private set; }

		public double Range
		{
			get
			{
				return MaxValue - MinValue;
			}
		}

		public Interval() : this(0.0, 0.0, 1.0, Enums.eIntervalFeature.eClosed)
		{ }

		public Interval(double min, double max, Enums.eIntervalFeature f = Enums.eIntervalFeature.eClosed) : this(min, max, 1.0, f)
		{ }

		public Interval(double min, double max, double step, Enums.eIntervalFeature feature = Enums.eIntervalFeature.eClosed)
		{
			MinValue = Math.Min(min, max);
			MaxValue = Math.Max(min, max);
			Step = step;
			Feature = feature;
		}
	}
}
