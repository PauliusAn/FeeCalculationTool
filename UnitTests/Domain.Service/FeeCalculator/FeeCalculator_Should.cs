using System;
using Persistence.Read_Models;
using Xunit;

namespace UnitTests.Domain.Service.FeeCalculator
{
    public class FeeCalculator_Should
    {
        [Theory]
        [InlineData(100)]
        [InlineData(50)]
        [InlineData(0)]
        public void CalculateFee_WhenTransactionIsGiven(decimal amount)
        {
            // Arrange
            var sut = new global::Domain.Service.Calculators.FeeCalculator();

            var date = new DateTime(2010, 10, 10);
            var testTransaction = new Transaction(date, "testMerchant", amount);

            var expectedResult = amount * sut.GetFeeRate();

            // Act
            var result = sut.CalculateFee(testTransaction);

            // Assert
            Assert.Equal(expectedResult, result);
        }
    }
}
