namespace TeapotPlugin.Model
{
    using System.Collections.Generic;

    /// <summary>
    /// Teapot parameters.
    /// </summary>
    public class TeapotParameters
    {
        /// <summary>
        /// Set value in the dictionary
        /// </summary>
        /// <param name="type">key for dictionary</param>
        /// <param name="parameter">value for dictionary</param>
        public void SetParameter(ParameterType type, double value)
        {
            _parameters[type].Value = value;
            _changeDependentParameter(type);
        }

        /// <summary>
        /// Get method to recieve parameter from dictionary
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public Parameter getParameterByType(ParameterType type)
        {
          return _parameters[type];
        }

        /// <summary>
        /// Parameters.
        /// </summary>
        private readonly Dictionary<ParameterType, Parameter> _parameters =
            new Dictionary<ParameterType, Parameter>
            {
                {ParameterType.Height, new Parameter{ MaxValue = 200, MinValue = 100, Value = 180 }},
                {ParameterType.BaseCircle, new Parameter{ MaxValue = 100, MinValue = 80, Value = 90 }},
                {ParameterType.OuterSpoutCircle, new Parameter{ MaxValue = 20, MinValue = 10, Value = 10 }},
                {ParameterType.InnerSpoutCircle, new Parameter{ MaxValue = 9, MinValue = 5, Value = 8 }},
                {ParameterType.SpoutLength, new Parameter{ MaxValue = 150, MinValue = 50, Value = 136.4 }},
                {ParameterType.HandleThickness, new Parameter{ MaxValue = 11.7, MinValue = 5.4, Value = 10 }},
                {ParameterType.HandleType, new Parameter{ MaxValue = 1, MinValue = 0, Value = 0 }},
            };

        /// <summary>
        /// Change the dependent parameter, if it exists
        /// </summary>
        /// <param name="type">
        /// Type of the base parameter
        /// </param>
        private void _changeDependentParameter(ParameterType type)
        {
            switch (type)
            {
                case ParameterType.Height:
                    _parameters[ParameterType.HandleThickness].MinValue = getParameterByType(type).Value * 0.03;
                    _parameters[ParameterType.HandleThickness].MaxValue = getParameterByType(type).Value * 0.065;
                    break;

                case ParameterType.OuterSpoutCircle:
                    _parameters[ParameterType.InnerSpoutCircle].MinValue = getParameterByType(type).Value * 0.5;
                    _parameters[ParameterType.InnerSpoutCircle].MaxValue = getParameterByType(type).Value - 1;
                    break;
            }
        }
    }
}