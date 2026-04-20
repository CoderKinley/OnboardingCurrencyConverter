namespace CurrencyConverter.UnitTests
{
    internal class CurrencyConverterTests
    {
        [Test]
        [Apartment(ApartmentState.STA)]
        public void SourceCurrency_WithDefaultConstructor_ReturnsDefaultValue()
        {
            // Arrange - create situation
            CurrencyConverter curConverter = new CurrencyConverter();

            // Act - call func/act for esting ,user interfa

            // Assert - write checks
            Assert.That(curConverter.SourceCurrency, Is.EqualTo("USD"));

        }
    }
}
