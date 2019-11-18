using System;
using System.Collections.Generic;
using System.Text;
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
            var sut = new Transaction(DateTime.Now, "testMerchant", 100);
            Action setInvalidFee = () => sut.Fee = fee;

            // Act & Assert
            setInvalidFee.Should().Throw<ArgumentException>();
        }
    }
}
