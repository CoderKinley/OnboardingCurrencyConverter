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

            // Act 

            // Assert
            Assert.That(curConverter.SourceCurrency, Is.EqualTo("USD"));
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void TargetCurrency_WithDefaultConstructor_ReturnsDefaultValue()
        {
            // Arrange
            CurrencyConverter curConverter = new CurrencyConverter();

            // Act 

            // Assert
            Assert.That(curConverter.TargetCurrency, Is.EqualTo("EUR"));
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void SourceValue_WithDefaultConstructor_ReturnsDefaultValue()
        {
            // Arrange
            CurrencyConverter curConverter = new CurrencyConverter();

            // Act 

            // Assert 
            Assert.That(curConverter.SourceValue, Is.EqualTo(1.00m));
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void TargetValue_WithDefaultConstructor_ReturnsDefaultValue()
        {
            // Arrange
            CurrencyConverter curConverter = new CurrencyConverter();

            // Act 

            // Assert 
            Assert.That(curConverter.TargetValue, Is.EqualTo(1.00m));
        }
    }
}