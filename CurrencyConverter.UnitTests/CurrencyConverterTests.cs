using CurrencyConverter.Models;
using CurrencyConverter.Provider;
using NSubstitute;
using System.Windows.Controls;

namespace CurrencyConverter.UnitTests
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    internal class CurrencyConverterTests
    {
        #region DefaultValues 
        [Test]
        public void SourceCurrency_WithDefaultConstructor_ReturnsDefaultValue()
        {
            // Arrange
            var provider = Substitute.For<ICurrencyProvider>();
            CurrencyConverter curConverter = new CurrencyConverter { ConversionProvider = provider };

            // Assert
            Assert.That(curConverter.SourceCurrency, Is.EqualTo("USD"));
        }

        [Test]
        public void TargetCurrency_WithDefaultConstructor_ReturnsDefaultValue()
        {
            // Arrange
            var provider = Substitute.For<ICurrencyProvider>();
            CurrencyConverter curConverter = new CurrencyConverter { ConversionProvider = provider };

            // Assert
            Assert.That(curConverter.TargetCurrency, Is.EqualTo("EUR"));
        }

        [Test]
        public void SourceValue_WithDefaultConstructor_ReturnsDefaultValue()
        {
            // Arrange
            var provider = Substitute.For<ICurrencyProvider>();
            CurrencyConverter curConverter = new CurrencyConverter { ConversionProvider = provider };

            // Assert 
            Assert.That(curConverter.SourceValue, Is.EqualTo(1.00m));
        }

        [Test]
        public void TargetValue_WithDefaultConstructor_ReturnsDefaultValue()
        {
            // Arrange
            var provider = Substitute.For<ICurrencyProvider>();
            CurrencyConverter curConverter = new CurrencyConverter();
            curConverter.ConversionProvider = provider;

            provider
                .GetConversionRatio("AUD", "USD")
                .ReturnsForAnyArgs(Task.FromResult(1m));

            // Assert 
            Assert.That(curConverter.TargetValue, Is.EqualTo(0m));
        }
        #endregion

        #region Property Value Tests

        [Test]
        public void SourceCurrency_WithNewValue_ReturnsNewValue()
        {
            // Arrange
            var provider = Substitute.For<ICurrencyProvider>();
            CurrencyConverter curConverter = new CurrencyConverter { ConversionProvider = provider };

            // Act
            curConverter.SourceCurrency = "BTN";

            // Assert
            Assert.That(curConverter.SourceCurrency, Is.EqualTo("BTN"));
        }

        [Test]
        public void TargetCurrency_WithNewValue_ReturnsNewValue()
        {
            // Arrange
            var provider = Substitute.For<ICurrencyProvider>();
            CurrencyConverter curConverter = new CurrencyConverter { ConversionProvider = provider };

            // Act
            curConverter.TargetCurrency = "JPY";

            // Assert
            Assert.That(curConverter.TargetCurrency, Is.EqualTo("JPY"));
        }

        [Test]
        public void SourceValue_WithNewValue_ReturnsNewValue()
        {
            // Arrange
            var provider = Substitute.For<ICurrencyProvider>();
            CurrencyConverter curConverter = new CurrencyConverter { ConversionProvider = provider };

            // Act
            curConverter.SourceValue = 50.0m;

            // Assert
            Assert.That(curConverter.SourceValue, Is.EqualTo(50.0m));
        }
        #endregion

        #region Event Raising Tests

        [Test]
        public void SourceCurrency_WithNewValue_RaisesSourceCurrencyChangedEvent()
        {
            // Arrange
            var provider = Substitute.For<ICurrencyProvider>();
            CurrencyConverter curConverter = new CurrencyConverter { ConversionProvider = provider };
            bool eventRaised = false;
            curConverter.SourceCurrencyChanged += (s, e) => eventRaised = true;

            // Act
            curConverter.SourceCurrency = "GBP";

            // Assert
            Assert.That(eventRaised, Is.True);
        }

        [Test]
        public void TargetCurrency_WithNewValue_RaisesTargetCurrencyChangedEvent()
        {
            // Arrange
            var provider = Substitute.For<ICurrencyProvider>();
            CurrencyConverter curConverter = new CurrencyConverter { ConversionProvider = provider };
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
            var provider = Substitute.For<ICurrencyProvider>();
            CurrencyConverter curConverter = new CurrencyConverter { ConversionProvider = provider };
            bool eventRaised = false;
            curConverter.SourceValueChanged += (s, e) => eventRaised = true;

            // Act
            curConverter.SourceValue = 10.0m;

            // Assert
            Assert.That(eventRaised, Is.True);
        }
        #endregion

        #region
        [Test]
        public void CurrencyConverter_WithDefaultValues_ReturnsCorrectValues()
        {
            // Arrange
            var provider = Substitute.For<ICurrencyProvider>();
            var control = new CurrencyConverter();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(control.SourceCurrency, Is.EqualTo("USD"));
                Assert.That(control.TargetCurrency, Is.EqualTo("EUR"));
                Assert.That(control.SourceValue, Is.EqualTo(1m));
                Assert.That(control.TargetValue, Is.EqualTo(1m));
                Assert.That(control.SourceValue, Is.EqualTo(1m));
            });
        }
        [Test]
        public async Task CurrencyConverter_WithChangeToSource_ReturnsCorrectValues()
        {
            // Arrange
            var provider = Substitute.For<ICurrencyProvider>();
            CurrencyConverter control = new CurrencyConverter { ConversionProvider = provider };
            var substituteProvider = Substitute.For<ICurrencyProvider>();

            substituteProvider
                .GetConversionRatio("AUD", "USD")
                .Returns(Task.FromResult(0.7931034482758621m));

            control.ConversionProvider = substituteProvider;

            var tcs = new TaskCompletionSource<bool>();
            control.TargetValueChanged += (s, e) => tcs.TrySetResult(true);

            // Act
            control.SourceCurrency = "AUD";
            control.TargetCurrency = "USD";
            control.SourceValue = 29m;
            await Task.WhenAny(tcs.Task, Task.Delay(2000));

            // Assert 
            Assert.That(control.TargetValue, Is.EqualTo(23m).Within(0.0000000001m));
        }
        [Test]
        public async Task CurrencyConverter_SourceValueZero_ReturnsZero()
        {
            // Arrange
            var provider = Substitute.For<ICurrencyProvider>();
            CurrencyConverter control = new CurrencyConverter { ConversionProvider = provider };
            provider.GetConversionRatio(Arg.Any<string>(), Arg.Any<string>()).Returns(0.85m);
            control.ConversionProvider = provider;

            // Act
            control.SourceValue = 0m;
            await Task.Delay(100);

            // Assert
            Assert.That(control.TargetValue, Is.EqualTo(0m));
        }

        [Test]
        public async Task CurrencyConverter_NoProviderSet_DoesNotCrashAndReturnsZero()
        {
            // Arrange
            var provider = Substitute.For<ICurrencyProvider>();
            CurrencyConverter control = new CurrencyConverter { ConversionProvider = provider };
            // Act & Assert
            Assert.DoesNotThrowAsync(async () =>
            {
                var result = await control.ConvertCurrency(100m, "USD", "EUR");
                Assert.That(result, Is.EqualTo(0m));
            });
        }


        [Test]
        public async Task CurrencyConverter_ChangingTargetCurrency_UpdatesValue()
        {
            // Arrange
            var provider = Substitute.For<ICurrencyProvider>();
            CurrencyConverter control = new CurrencyConverter { ConversionProvider = provider };

            provider.GetConversionRatio("USD", "GBP").Returns(0.8m);
            provider.GetConversionRatio("USD", "JPY").Returns(110m);

            control.ConversionProvider = provider;
            control.SourceCurrency = "USD";
            control.SourceValue = 10m;

            // Act 1: Initial conversion to GBP
            control.TargetCurrency = "GBP";
            await Task.Delay(100);
            Assert.That(control.TargetValue, Is.EqualTo(8m));

            // Act 2: Change Target to JPY
            control.TargetCurrency = "JPY";
            await Task.Delay(100);

            // Assert
            Assert.That(control.TargetValue, Is.EqualTo(1100m));
        }


        [Test]
        public async Task CurrencyConverter_WhenProviderSwapped_UpdatesTargetValueImmediately()
        {
            // Arrange
            var control = new CurrencyConverter
            {
                SourceCurrency = "USD",
                TargetCurrency = "GBP",
                SourceValue = 100m,
            };

            var mockProvider = Substitute.For<ICurrencyProvider>();
            mockProvider.GetConversionRatio("USD", "GBP").Returns(0.75m);

            var tcs = new TaskCompletionSource<bool>();
            control.TargetValueChanged += (s, e) => tcs.TrySetResult(true);

            // Act
            control.ConversionProvider = mockProvider;

            // Wait for the async callback triggered by OnConversionProviderChanged
            await Task.WhenAny(tcs.Task, Task.Delay(500));

            // Assert
            Assert.That(control.TargetValue, Is.EqualTo(75m));
        }

        [Test]
        public void SupportedCurrencies_WhenProviderIsNull_ReturnsEmptyList()
        {
            // Arrange
            var control = new CurrencyConverter { ConversionProvider = null };

            // Act & Assert
            Assert.That(control.SupportedCurrencies, Is.Empty);
        }

        [Test]
        public async Task ConvertCurrency_ManualCallWithOverrideProvider_UsesOverrideInsteadOfInternal()
        {
            // Arrange
            var internalProvider = Substitute.For<ICurrencyProvider>();
            var overrideProvider = Substitute.For<ICurrencyProvider>();

            internalProvider.GetConversionRatio("USD", "EUR").Returns(0.9m);
            overrideProvider.GetConversionRatio("USD", "EUR").Returns(0.5m);

            var control = new CurrencyConverter { ConversionProvider = internalProvider };

            await Task.Delay(50);
            internalProvider.ClearReceivedCalls();

            // Act
            var result = await control.ConvertCurrency(100m, "USD", "EUR", overrideProvider);

            // Assert
            Assert.That(result, Is.EqualTo(50m));

            // Now this will correctly verify that ONLY the override was used during the 'Act' phase
            await internalProvider.DidNotReceive().GetConversionRatio(Arg.Any<string>(), Arg.Any<string>());
            await overrideProvider.Received(1).GetConversionRatio("USD", "EUR");
        }
        #endregion
    }
}