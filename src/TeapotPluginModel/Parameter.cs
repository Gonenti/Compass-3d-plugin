namespace TeapotPlugin.Model
{
    using System;
    public class Parameter
    {
        /// <summary>
        /// Max value.
        /// </summary>
        public double MaxValue { get; set; }

        /// <summary>
        /// Min value.
        /// </summary>
        public double MinValue { get; set; }

        /// <summary>
        /// Value.
        /// </summary>
        public double Value
        {
            get => _value;
            set
            {
                if (!Validate(value))
                {
                    throw new ArgumentException( "Value is out of the range" );
                }

                _value = value;
            }
        }

        /// <summary>
        /// Validate parameter
        /// </summary>
        /// <param name="value">
        /// The value to validate
        /// </param>
        /// <returns>
        /// True if value is right, otherwise returns false
        /// </returns>
        private bool Validate(double value)
        {
            return value >= MinValue && value <= MaxValue;
        }

        /// <summary>
        /// Текущее значение параметра.
        /// </summary>
        private double _value;
    }
}