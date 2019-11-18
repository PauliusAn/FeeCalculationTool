using System;
using FluentAssertions;
using Persistence.Read_Models;
using Xunit;

namespace UnitTests.Persistence.Read_Models
{
    public class Transaction_Should
    {
        [Theory]
        [InlineData(-0.0001)]
        [InlineData(-5)]
        [InlineData(-42)]
        public void ThrowException_WhenTryingToSetNegativeFee(decimal fee)
        {
            // Arrange
            var sut = new Transaction(new DateTime(2019, 10, 10), "testMerchant", 100);
            Action setInvalidFee = () => sut.Fee = fee;

            // Act & Assert
            setInvalidFee.Should().Throw<ArgumentException>();
        }
    }
}
