namespace UnitTest
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TeapotPlugin.Model;

    [TestClass]
    public class TeapotParametersUnitTests
    {
        private TeapotParameters _parameters;

        [TestInitialize]
        public void Setup()
        {
            _parameters = new TeapotParameters();
        }

        [TestMethod]
        public void SetParameter_Height_WithinRange_UpdatesValueAndDependentParameters()
        {
            // Arrange
            var newHeightValue = 150;

            // Act
            _parameters.SetParameter(ParameterType.Height, newHeightValue);

            // Assert
            Assert.AreEqual(newHeightValue, _parameters.getParameterByType(ParameterType.Height).Value);
            Assert.AreEqual(newHeightValue * 0.03, _parameters.getParameterByType(ParameterType.HandleThickness).MinValue);
            Assert.AreEqual(newHeightValue * 0.065, _parameters.getParameterByType(ParameterType.HandleThickness).MaxValue);
        }

        [TestMethod]
        public void SetParameter_HeightOutOfRange_ArgumentException()
        {
            // Arrange
            var valuesOutOfRange = new double[] { 99.9, 200.1 };

            // Act & Assert
            foreach (var value in valuesOutOfRange)
            {
                Assert.ThrowsException<ArgumentException>(() => _parameters.SetParameter(ParameterType.Height, value));
            }
        }

        [TestMethod]
        public void SetParameter_OuterSpoutCircleWithinRange_UpdatesValueAndDependentParameters()
        {
            // Arrange
            var newOuterSpoutCircle = 15.0;

            // Act
            _parameters.SetParameter(ParameterType.OuterSpoutCircle, newOuterSpoutCircle);

            // Assert
            Assert.AreEqual(newOuterSpoutCircle, _parameters.getParameterByType(ParameterType.OuterSpoutCircle).Value);
            Assert.AreEqual(newOuterSpoutCircle * 0.5, _parameters.getParameterByType(ParameterType.InnerSpoutCircle).MinValue);
            Assert.AreEqual(newOuterSpoutCircle - 1, _parameters.getParameterByType(ParameterType.InnerSpoutCircle).MaxValue);
        }

        [TestMethod]
        public void SetParameter_OuterSpoutCircle_OutOfRange_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var valuesOutOfRange = new double[] { 9.9, 20.1 };

            // Act & Assert
            foreach (var value in valuesOutOfRange)
            {
                Assert.ThrowsException<ArgumentException>(() => _parameters.SetParameter(ParameterType.OuterSpoutCircle, value));
            }
        }

        [TestMethod]
        public void GetParameterByType_ExistingParameter_ReturnsCorrectValue()
        {
            // Arrange
            var expectedValue = 100.0;

            // Act
            var parameter = _parameters.getParameterByType(ParameterType.Height);

            // Assert
            Assert.AreEqual(expectedValue, parameter.Value);
        }

        [TestMethod]
        public void GetParameterByType_NonexistentParameter_ThrowsArgumentException()
        {
            // Arrange
            var invalidType = (ParameterType)int.MaxValue;

            // Act & Assert
            Assert.ThrowsException<KeyNotFoundException>(() => _parameters.getParameterByType(invalidType));
        }
    }
}
