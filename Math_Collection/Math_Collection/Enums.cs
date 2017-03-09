namespace Math_Collection
{
    public class Enums
    {
        public enum eIntervalFeature
        {
            /// <summary>
            /// { x | a < x < b }
            /// </summary>
            eOpen = 0,
            /// <summary>
            /// { x | a <= x <= b }
            /// </summary>
            eClosed,
            /// <summary>
            /// { x | a < x <= b }
            /// </summary>
            eLeftOpenRightClosed,
            /// <summary>
            /// { x | a <= x < b }
            /// </summary>
            eLeftClosedRightOpen
        }

    }
}
