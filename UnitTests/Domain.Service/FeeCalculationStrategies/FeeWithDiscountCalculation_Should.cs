using System;
using Domain.Service.FeeCalculationStrategies;
using FluentAssertions;
using Xunit;

namespace UnitTests.Domain.Service.FeeCalculationStrategies
{
    public class FeeWithDiscountCalculation_Should
    {
        [Theory]
        [InlineData(100, 0.2, 0.8)]
        [InlineData(50, 0.1, 0.45)]
        [InlineData(0, 0.5, 0)]
        [InlineData(-100, 0.1, -0.9)]
        public void ReturnCalculatedFee_WhenAmountAndDiscountIsGiven(decimal amount, decimal discount, decimal expectedFee)
        {
            // Arrange
            var sut = new FeeWithDiscountCalculation(discount);

            // Act
            var result = sut.CalculateFee(amount);

            // Assert
            Assert.Equal(result, expectedFee);
        }

        [Theory]
        [InlineData(1.5)]
        [InlineData(-5)]
        [InlineData(-0.0001)]
        public void ThrowException_WhenInvalidFeeDiscountIsGiven(decimal discount)
        {
            // Arrange
            Action a = () => new FeeWithDiscountCalculation(discount);

            // Act & Assert
            a.Should().Throw<ArgumentException>();
        }
    }
}
