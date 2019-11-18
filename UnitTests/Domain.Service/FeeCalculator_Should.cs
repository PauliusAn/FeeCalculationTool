using System;
using Domain.Service;
using Domain.Service.FeeCalculationStrategies;
using Domain.Service.Models;
using Persistence.Read_Models;
using Xunit;

namespace UnitTests.Domain.Service
{
    public class FeeCalculator_Should
    {
        private readonly decimal _fixedMonthlyFee = 29;

        [Fact]
        public void ReturnFixedFeePlusMonthlyFee_WhenMerchantIsNotRegisteredAndHasNoCompletedPaymentInTheSameMonth()
        {
            // Arrange
            var sut = new FeeCalculator();
            var testTransaction = new Transaction(DateTime.Now, "unknownMerchant", 100);
            var expected = 1 + _fixedMonthlyFee;

            // Act
            var result = sut.CalculateFee(testTransaction);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ReturnFixedFee_WhenMerchantIsRegisteredAndHasCompletedPaymentInTheSameMonth()
        {
            // Arrange
            var sut = new FeeCalculator();

            var testMerchant = new Merchant(new BasicFeeCalculation(), "knownMerchant");
            testMerchant.AddPaymentDate(DateTime.Now);

            sut.RegisteredMerchants.Add(testMerchant);

            var testTransaction = new Transaction(DateTime.Now, "knownMerchant", 100);

            // Act
            var result = sut.CalculateFee(testTransaction);

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public void ReturnFeeWithDiscountPlusMonthlyFee_WhenMerchantHasFeeWithDiscountCalculationStrategyAndHasNoCompletedPaymentInTheSameMonth()
        {
            // Arrange
            var sut = new FeeCalculator();

            var testMerchant = new Merchant(new FeeWithDiscountCalculation(0.2m), "knownMerchant");
            sut.RegisteredMerchants.Add(testMerchant);

            var testTransaction = new Transaction(DateTime.Now, "knownMerchant", 100);
            var expected = _fixedMonthlyFee + 0.8m;

            // Act
            var result = sut.CalculateFee(testTransaction);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ReturnFeeWithDiscount_WhenMerchantHasFeeWithDiscountCalculationStrategyAndHasCompletedPaymentInTheSameMonth()
        {
            // Arrange
            var sut = new FeeCalculator();

            var testMerchant = new Merchant(new FeeWithDiscountCalculation(0.2m), "knownMerchant");
            testMerchant.AddPaymentDate(DateTime.Now);

            sut.RegisteredMerchants.Add(testMerchant);

            var testTransaction = new Transaction(DateTime.Now, "knownMerchant", 100);

            // Act
            var result = sut.CalculateFee(testTransaction);

            // Assert
            Assert.Equal(0.8m, result);
        }
    }
}
