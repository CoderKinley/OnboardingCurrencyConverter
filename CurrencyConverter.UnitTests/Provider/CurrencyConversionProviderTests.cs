using CurrencyConverter.Provider;
using NSubstitute;
using System.Net;
using System.Reflection;

namespace CurrencyConverter.UnitTests.Provider
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    internal class CurrencyConversionProviderTests
    {
        #region
        [Test]
        public async Task GetConversionRatio_ShouldCalculateCorrectRatio_FromMockedApi()
        {
            // 1. ARRANGE
            var apiKey = "test_key";
            var fakeJson = "{\"success\": true, \"quotes\": {\"USDAUD\": 1.5, \"USDEUR\": 0.9}}";

            // Create a substitute for the handler
            var handler = Substitute.For<HttpMessageHandler>();

            // NSubstitute to mock the protected SendAsync method
            var method = typeof(HttpMessageHandler).GetMethod("SendAsync",
                BindingFlags.NonPublic | BindingFlags.Instance);

            method.Invoke(handler, new object[] { Arg.Any<HttpRequestMessage>(), Arg.Any<CancellationToken>() })
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
        #endregion
    }
}
