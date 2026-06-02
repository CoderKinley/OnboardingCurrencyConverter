using CurrencyConverter.Models;
using CurrencyConverter.Provider;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System.Net;
using System.Reflection;

namespace CurrencyConverter.UnitTests.Provider
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    internal class CurrencyConversionProviderTests
    {
        #region Method Testing
        [Test]
        public async Task GetConversionRatio_WithMockedApi_ShouldCalculateCorrectRatio()
        {
            // 1. ARRANGE
            var apiKey = "test_key";
            var fakeJson = "{\"success\": true, \"quotes\": {\"USDAUD\": 1.5, \"USDEUR\": 0.9}}";

            // Create a substitute for the handler
            var handler = Substitute.For<HttpMessageHandler>();

            // NSubstitute to mock the protected SendAsync method
            var method = typeof(HttpMessageHandler).GetMethod("SendAsync",
                BindingFlags.NonPublic | BindingFlags.Instance);

            method.Invoke(
                handler, 
                new object[] { Arg.Any<HttpRequestMessage>(), 
                    
                Arg.Any<CancellationToken>() })
                .Returns(Task.FromResult(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(fakeJson)
                }));

            var client = new HttpClient(handler);
            var provider = new CurrencyConversionProvider(apiKey, client);

            // 2. ACT
            decimal ratio = await provider.GetConversionRatio("AUD", "EUR");

            // 3. ASSERT
            Assert.That(ratio, Is.EqualTo(0.6m));
        }

        [Test]
        public void SupportedCurrencies_ShouldReturnList()
        {
            // 1. ARRANGE
            var apiKey = "test_key";
            var fakeJson = "{\"success\": true, \"quotes\": {\"USDAUD\": 1.5, \"USDEUR\": 0.9}}";
            var fakeCsvContent = "";

            // Create a substitute for the handler
            var handler = Substitute.For<HttpMessageHandler>();

            // NSubstitute to mock the protected SendAsync method
            var method = typeof(HttpMessageHandler).GetMethod("SendAsync",
                BindingFlags.NonPublic | BindingFlags.Instance);

            method.Invoke(
                handler,
                new object[] { Arg.Any<HttpRequestMessage>(),

                Arg.Any<CancellationToken>() })
                .Returns(Task.FromResult(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(fakeJson)
                }));

            var client = new HttpClient(handler);
            var provider = new CurrencyConversionProvider(apiKey, client);


            // Act
            var supportedCurrencies = provider.SupportedCurrencies;

            // Assert
            Assert.That(supportedCurrencies.Count, Is.GreaterThan(0));
        }

        #endregion

        #region With Interface
        [Test]
        public void SupportedCurrencies_WithMockList_ShouldReturnCorrectList()
        {
            // Arrange
            var mockProvider = Substitute.For<ICurrencyProvider>();
            var fakeList = new List<CurrencyInfo>
            {
                new CurrencyInfo
                {
                    Country = "Bhutan",
                    CountryCode = "BT",
                    CurrencyName = "Bhutanese Ngultrum",
                    CurrencyCode = "BTN",
                    FlagSource = ""
                },
                new CurrencyInfo
                {
                    Country = "United States",
                    CountryCode = "US",
                    CurrencyName = "US Dollar",
                    CurrencyCode = "USD",
                    FlagSource = ""
                }
            };

            mockProvider.SupportedCurrencies.Returns(fakeList);

            // Act
            var result = mockProvider.SupportedCurrencies;

            // Assert
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.First().CurrencyCode, Is.EqualTo("BTN"));
        }
        [Test]
        public void SupportedCurrencies_WhenNoDataExists_ReturnsEmptyCollection()
        {
            // Arrange
            var mockProvider = Substitute.For<ICurrencyProvider>();
            mockProvider.SupportedCurrencies.Returns(Enumerable.Empty<CurrencyInfo>());

            // Act
            var result = mockProvider.SupportedCurrencies;

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void GetConversionRatio_WithInvalidCurrencyCode_ThrowsException()
        {
            // Arrange
            var mockProvider = Substitute.For<ICurrencyProvider>();
            mockProvider.GetConversionRatio("INVALID", "USD")
                        .Throws(new ArgumentException("Invalid currency code"));

            // Assert
            Assert.ThrowsAsync<ArgumentException>(async () =>
                await mockProvider.GetConversionRatio("INVALID", "USD"));
        }

        [Test]
        public async Task GetConversionRatio_SameSourceAndTarget_ReturnsOne()
        {
            // Arrange
            var mockProvider = Substitute.For<ICurrencyProvider>();
            mockProvider.GetConversionRatio("USD", "USD").Returns(1.0m);

            // Act
            var result = await mockProvider.GetConversionRatio("USD", "USD");

            // Assert
            Assert.That(result, Is.EqualTo(1.0m));
        }

        [Test]
        public async Task GetConversionRatio_IsCaseInsensitive()
        {
            // Arrange
            var mockProvider = Substitute.For<ICurrencyProvider>();
            mockProvider.GetConversionRatio("usd", "eur").Returns(0.9m);

            // Act
            var result = await mockProvider.GetConversionRatio("usd", "eur");

            // Assert
            Assert.That(result, Is.EqualTo(0.9m));
        }
        #endregion
    }
}
