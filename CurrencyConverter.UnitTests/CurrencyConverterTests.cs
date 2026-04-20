using NUnit.Framework;
using System.Threading;

namespace CurrencyConverter.UnitTests
{
    [TestFixture]
    internal class CurrencyConverterTests
    {
        [Test]
        [Apartment(ApartmentState.STA)]
        public void SourceCurrency_WithDefaultConstructor_ReturnsDefaultValue()
        {
            // Arrange
            CurrencyConverter curConverter = new CurrencyConverter();

            // Assert
            Assert.That(curConverter.SourceCurrency, Is.EqualTo("USD"));
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void TargetCurrency_WithDefaultConstructor_ReturnsDefaultValue()
        {
            // Arrange
            CurrencyConverter curConverter = new CurrencyConverter();

            // Assert
            Assert.That(curConverter.TargetCurrency, Is.EqualTo("EUR"));
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void SourceValue_WithDefaultConstructor_ReturnsDefaultValue()
        {
            // Arrange
            CurrencyConverter curConverter = new CurrencyConverter();

            // Assert 
            Assert.That(curConverter.SourceValue, Is.EqualTo(1.00m));
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void TargetValue_WithDefaultConstructor_ReturnsDefaultValue()
        {
            // Arrange
            CurrencyConverter curConverter = new CurrencyConverter();

            // Assert 
            Assert.That(curConverter.TargetValue, Is.EqualTo(1.00m));
        }

        #region Property Value Tests

        [Test]
        [Apartment(ApartmentState.STA)]
        public void SourceCurrency_WithNewValue_ReturnsNewValue()
        {
            // Arrange
            CurrencyConverter curConverter = new CurrencyConverter();

            // Act
            curConverter.SourceCurrency = "BTN";

            // Assert
            Assert.That(curConverter.SourceCurrency, Is.EqualTo("BTN"));
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void TargetCurrency_WithNewValue_ReturnsNewValue()
        {
            // Arrange
            CurrencyConverter curConverter = new CurrencyConverter();

            // Act
            curConverter.TargetCurrency = "JPY";

            // Assert
            Assert.That(curConverter.TargetCurrency, Is.EqualTo("JPY"));
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void SourceValue_WithNewValue_ReturnsNewValue()
        {
            // Arrange
            CurrencyConverter curConverter = new CurrencyConverter();

            // Act
            curConverter.SourceValue = 50.0m;

            // Assert
            Assert.That(curConverter.SourceValue, Is.EqualTo(50.0m));
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void TargetValue_WithNewSourceValue_ReturnsDoubleReflectedValue()
        {
            // Arrange
            CurrencyConverter curConverter = new CurrencyConverter();

            // Act
            curConverter.SourceValue = 75.0m;

            // Assert
            Assert.That(curConverter.TargetValue, Is.EqualTo(75.0m * 2));
        }

        #endregion

        #region Event Raising Tests

        [Test]
        [Apartment(ApartmentState.STA)]
        public void SourceCurrency_WithNewValue_RaisesSourceCurrencyChangedEvent()
        {
            // Arrange
            CurrencyConverter curConverter = new CurrencyConverter();
            bool eventRaised = false;
            curConverter.SourceCurrencyChanged += (s, e) => eventRaised = true;

            // Act
            curConverter.SourceCurrency = "GBP";

            // Assert
            Assert.That(eventRaised, Is.True);
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void TargetCurrency_WithNewValue_RaisesTargetCurrencyChangedEvent()
        {
            // Arrange
            CurrencyConverter curConverter = new CurrencyConverter();
            bool eventRaised = false;
            curConverter.TargetCurrencyChanged += (s, e) => eventRaised = true;

            // Act
            curConverter.TargetCurrency = "INR";

            // Assert
            Assert.That(eventRaised, Is.True);
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void SourceValue_WithNewValue_RaisesSourceValueChangedEvent()
        {
            // Arrange
            CurrencyConverter curConverter = new CurrencyConverter();
            bool eventRaised = false;
            curConverter.SourceValueChanged += (s, e) => eventRaised = true;

            // Act
            curConverter.SourceValue = 10.0m;

            // Assert
            Assert.That(eventRaised, Is.True);
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void TargetValue_WithNewValue_RaisesTargetValueChangedEvent()
        {
            // Arrange
            CurrencyConverter curConverter = new CurrencyConverter();
            bool eventRaised = false;
            curConverter.TargetValueChanged += (s, e) => eventRaised = true;

            // Act
            curConverter.SourceValue = 20.0m;

            // Assert
            Assert.That(eventRaised, Is.True);
        }

        #endregion
    }
}