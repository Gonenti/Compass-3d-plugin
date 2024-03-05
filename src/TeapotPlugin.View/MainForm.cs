namespace TeapotPlugin.View
{
    using System;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;
    using TeapotPlugin.Model;
    using TeapotPlugin.Wrapper;

    /// <summary>
    /// Describes the main form.
    /// </summary>
    internal partial class MainForm : Form
    {
        /// <summary>
        /// Error message
        /// </summary>
        private String _errorMessage;

        /// <summary>
        /// Error message
        /// </summary>
        private String _heightErrorMessage;
        private String _baseCircleRadiusErrorMessage;
        private String _outerCircleSpoutRadiusErrorMessage;
        private String _spoutLengthErrorMessage;
        private String _innerCircleSpoutErrorMessage;
        private String _handleThicknessErrorMessage;

        /// <summary>
        /// The color of the field, if the data is correct 
        /// </summary>
        private readonly Color _rightColor = Color.White;

        /// <summary>
        /// The color of the field, if the data is wrong
        /// </summary>
        private readonly Color _errorColor = Color.LightPink;

        /// <summary>
        /// Instance of the class TeapotBuilder.
        /// </summary>
        private readonly TeapotBuilder _builder = new TeapotBuilder();

        /// <summary>
        /// Instance of the class TeapotPluginParameters.
        /// </summary>
        private readonly TeapotParameters _parameters = new TeapotParameters();

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            СomboBoxHandleType.SelectedIndex = 0;
        }

        private bool checkFormOnErros()
        {
            int errorCounter = 0;
            if (!string.IsNullOrEmpty(_heightErrorMessage))
            {
                _handleThicknessErrorMessage = "It is not possible to set the value of the handle thickness parameter " +
                                                "because the value of the height is set incorrectly!";
                TextBoxTeapotHeight.BackColor = _errorColor;
                errorCounter++;
                _errorMessage += errorCounter.ToString() + ")" + _heightErrorMessage + "\n\n";
            }
            else
            {
                TextBoxTeapotHeight.BackColor = _rightColor;
            }

            if (!string.IsNullOrEmpty(_baseCircleRadiusErrorMessage))
            {
                TextBoxBaseCircleRadius.BackColor = _errorColor;
                errorCounter++;
                _errorMessage += errorCounter.ToString() + ")" + _baseCircleRadiusErrorMessage + "\n\n";
            }
            else
            {
                TextBoxBaseCircleRadius.BackColor = _rightColor;
            }

            if (!string.IsNullOrEmpty(_outerCircleSpoutRadiusErrorMessage))
            {
                TextBoxOuterCircleSpout.BackColor = _errorColor;
                _innerCircleSpoutErrorMessage = "It is not possible to set the value of the inner circle of the " +
                                                "spout because the value of the outer circle is set incorrectly!\n";
                errorCounter++;
                _errorMessage += errorCounter.ToString() + ")" + _outerCircleSpoutRadiusErrorMessage + "\n\n";
            }
            else
            {
                TextBoxOuterCircleSpout.BackColor = _rightColor;
            }

            if (!string.IsNullOrEmpty(_spoutLengthErrorMessage))
            {
                TextBoxLabelSpoutLength.BackColor = _errorColor;
                errorCounter++;
                _errorMessage += errorCounter.ToString() + ")" + _spoutLengthErrorMessage + "\n\n";
            }
            else
            {
                TextBoxLabelSpoutLength.BackColor = _rightColor;
            }

            if (!string.IsNullOrEmpty(_innerCircleSpoutErrorMessage))
            {
                TextBoxInnerCircleSpout.BackColor = _errorColor;
                errorCounter++;
                _errorMessage += errorCounter.ToString() + ")" +  _innerCircleSpoutErrorMessage + "\n\n";
            }
            else
            {
                TextBoxInnerCircleSpout.BackColor = _rightColor;
            }

            if (!string.IsNullOrEmpty(_handleThicknessErrorMessage))
            {
                TextBoxHandleThickness.BackColor = _errorColor;
                errorCounter++;
                _errorMessage += errorCounter.ToString() + ")" + _handleThicknessErrorMessage + "\n\n";
            }
            else
            {
                TextBoxHandleThickness.BackColor = _rightColor;
            }

            if (!string.IsNullOrEmpty(_errorMessage))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Build teapot.
        /// </summary>
        private void BuildTeapot() 
        {
            if (checkFormOnErros())
            {
                MessageBox.Show("Errors:\n\n" + _errorMessage);
                _errorMessage = "";
                return;
            }

            _builder.BuildTeapot(_parameters);
        }

        private void checkHeightFieldOnError()
        {
            try
            {
                _parameters.SetParameter(ParameterType.Height,
                                         Convert.ToDouble(TextBoxTeapotHeight.Text, CultureInfo.InvariantCulture));

                LabelHandleThicknessLimits.Text = "from " +
                                                  Convert.ToString(_parameters.getParameterByType(ParameterType.HandleThickness).MinValue)
                                                  + " to " +
                                                  Convert.ToString(_parameters.getParameterByType(ParameterType.HandleThickness).MaxValue)
                                                  + " mm";
                _heightErrorMessage = "";
            }

            catch (FormatException)
            {
                if (string.IsNullOrEmpty(TextBoxTeapotHeight.Text))
                {
                    _heightErrorMessage = "Hieght field can't be emty!";
                }
                else
                {
                    _heightErrorMessage = "Invalid format in the height field!";
                }
            }

            catch (ArgumentException exception)
            {
                _heightErrorMessage = exception.Message + " in the hieght field!";
            }
        }

        private void checkBaseCircleFieldOnError()
        {
            try
            {
                _parameters.SetParameter(ParameterType.BaseCircle,
                                         Convert.ToDouble(TextBoxBaseCircleRadius.Text, CultureInfo.InvariantCulture));
                _baseCircleRadiusErrorMessage = "";
            }

            catch (FormatException)
            {
                if (string.IsNullOrEmpty(TextBoxBaseCircleRadius.Text))
                {
                    _baseCircleRadiusErrorMessage = "The base circle field can't be emty!";
                }
                else
                {
                    _baseCircleRadiusErrorMessage = "Invalid format in the base circle field!";
                }
            }

            catch (ArgumentException exception)
            {
                _baseCircleRadiusErrorMessage = exception.Message + " in the base circle field!";
            }
        }

        private void checkOuterCircleSpoutFieldOnError()
        {
            try
            {
                _parameters.SetParameter(ParameterType.OuterSpoutCircle,
                                        Convert.ToDouble(TextBoxOuterCircleSpout.Text, CultureInfo.InvariantCulture));
                LabelInnerCircleSpoutLimists.Text = "from " +
                                                    Convert.ToString(_parameters.getParameterByType(ParameterType.InnerSpoutCircle).MinValue) +
                                                    " to " +
                                                    Convert.ToString(_parameters.getParameterByType(ParameterType.InnerSpoutCircle).MaxValue) + " mm";
                _outerCircleSpoutRadiusErrorMessage = "";
            }

            catch (FormatException)
            {
                if (string.IsNullOrEmpty(TextBoxTeapotHeight.Text))
                {
                    _outerCircleSpoutRadiusErrorMessage = "The field of the outer circle cannot be empty!";
                }
                else
                {
                    _outerCircleSpoutRadiusErrorMessage = "Invalid format in the field of the outer circle of the spout field!";
                }
            }

            catch (ArgumentException exception)
            {
                _outerCircleSpoutRadiusErrorMessage = exception.Message + " in the field of the outer circle!";
            }
        }

        private void checkSpoutLengthFieldOnError()
        {
            try
            {
                _parameters.SetParameter(ParameterType.SpoutLength,
                                         Convert.ToDouble(TextBoxLabelSpoutLength.Text, CultureInfo.InvariantCulture));
                _spoutLengthErrorMessage = "";
            }

            catch (FormatException)
            {
                if (string.IsNullOrEmpty(TextBoxLabelSpoutLength.Text))
                {
                    _spoutLengthErrorMessage = "Spout length field can't be emty!";
                }
                else
                {
                    _spoutLengthErrorMessage = "Invalid format in the spout length field!";
                }
            }

            catch (ArgumentException exception)
            {
                _spoutLengthErrorMessage = exception.Message + " in the spout length field!";
            }
        }

        private void checkInnerCircleSpoutFieldOnError()
        {
            try
            {
                _parameters.SetParameter(ParameterType.InnerSpoutCircle,
                                         Convert.ToDouble(TextBoxInnerCircleSpout.Text, CultureInfo.InvariantCulture));
                _innerCircleSpoutErrorMessage = "";
            }

            catch (FormatException)
            {
                if (string.IsNullOrEmpty(TextBoxInnerCircleSpout.Text))
                {
                    _innerCircleSpoutErrorMessage = "The field of the inner circle cannot be empty!";
                }
                else
                {
                    _innerCircleSpoutErrorMessage = "Invalid format in the field of the inner circle of the spout!";
                }
            }

            catch (ArgumentException exception)
            {
                _innerCircleSpoutErrorMessage = exception.Message + " in the field of the inner circle!";
            }
        }

        private void checkHandleThicknessFieldOnError()
        {
            try
            {
                _parameters.SetParameter(ParameterType.HandleThickness,
                                         Convert.ToDouble(TextBoxHandleThickness.Text, CultureInfo.InvariantCulture));
                _handleThicknessErrorMessage = "";
            }

            catch (FormatException)
            {
                if (string.IsNullOrEmpty(TextBoxHandleThickness.Text))
                {
                    _handleThicknessErrorMessage = "The handle thickness field cannot be empty!";
                }
                else
                {
                    _handleThicknessErrorMessage = "Invalid format in the handle thickness field!";
                }
            }

            catch (ArgumentException exception)
            {
                _handleThicknessErrorMessage = exception.Message + " in the handle thickness field!";
            }
        }

        /// <summary>
        /// Change the limits in the field of the handle thickness.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxTeapotHeight_TextChanged(object sender, EventArgs e)
        {
            checkHeightFieldOnError();
            checkHandleThicknessFieldOnError();
        }

        private void TextBoxBaseCircleRadius_TextChanged(object sender, EventArgs e)
        {
            checkBaseCircleFieldOnError();
        }

        /// <summary>
        /// Change the limits in the field of the inner circle of the spout.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxOuterCircleSpoutCircleRadius_TextChanged(object sender, EventArgs e)
        {
            checkOuterCircleSpoutFieldOnError();
            checkInnerCircleSpoutFieldOnError();
        }

        private void TextBoxLabelSpoutLength_TextChanged(object sender, EventArgs e)
        {
            checkSpoutLengthFieldOnError();
        }

        private void TextBoxInnerCircleSpout_TextChanged(object sender, EventArgs e)
        {
            checkInnerCircleSpoutFieldOnError();
        }

        private void TextHandleThickness_TextChanged(object sender, EventArgs e)
        {
            checkHandleThicknessFieldOnError();
        }

        private void СomboBoxHandleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            _parameters.SetParameter(ParameterType.HandleType, СomboBoxHandleType.SelectedIndex); 
        }

        /// <summary>
        /// Button for building teapot.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonBuildFigure_Click(object sender, EventArgs e)
        {
            BuildTeapot();
        }
    }
}