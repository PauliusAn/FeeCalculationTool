using System;
using System.Collections.Generic;
using System.Text;
using Domain.Service.Calculators.Decorators;
using FluentAssertions;
using Persistence.Read_Models;
using UnitTests.TestModels;
using Xunit;

namespace UnitTests.Domain.Service.FeeCalculator.Decorators
{
    public class FeeDiscountCalculator_Should
    {
        [Theory]
        [InlineData(-0.1)]
        [InlineData(-99)]
        [InlineData(1.1)]
        [InlineData(99)]
        public void ThrowException_IfDiscountAmountIsInvalid(decimal discount)
        {
            // Arrange
            Action createCalculator = () => new FeeDiscountCalculator(new BaseTestCalculator(), discount);

            // Act & Assert
            createCalculator.Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(42)]
        [InlineData(0)]
        public void ReturnFeeWithDiscount_WhenCalculateFeeIsCalled(decimal fee)
        {
            // Arrange
            var sut = new FeeDiscountCalculator(new BaseTestCalculator(), 0.2m);

            var date = new DateTime(2010, 10, 10);
            var testTransaction = new Transaction(date, "testMerchant", 0)
            {
                Fee = fee
            };

            var expectedResult = fee * (1 - 0.2m);
            
            // Act
            var result = sut.CalculateFee(testTransaction);

            // Assert
            Assert.Equal(expectedResult, result);
        }
    }
}
