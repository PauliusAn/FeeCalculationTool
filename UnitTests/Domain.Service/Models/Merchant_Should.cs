using System;
using Domain.Service.Models;
using Xunit;

namespace UnitTests.Domain.Service.Models
{
    public class Merchant_Should
    {

        [Theory]
        [InlineData(2019, 11, 10)]
        [InlineData(2020, 11, 10)]
        [InlineData(2015, 1, 17)]
        [InlineData(2045, 12, 10)]
        public void ReturnTrue_WhenMonthlyFeeWasPaidInSameMonth(int year, int month, int day)
        {
            // Arrange
            var sut = new Merchant(null, "");
            var date = new DateTime(year, month, day);

            sut.LastDateMonthlyFeeWasPaid = date;

            // Act
            var result = sut.IsMonthlyFeePaid(date);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData(2019, 11, 10)]
        [InlineData(2020, 11, 10)]
        [InlineData(2015, 1, 17)]
        [InlineData(2045, 12, 10)]
        public void ReturnFalse_WhenDateIsNotInCollection(int year, int month, int day)
        {
            // Arrange
            var sut = new Merchant(null, "");
            var date = new DateTime(year, month, day);

            // Act
            var result = sut.IsMonthlyFeePaid(date);

            // Assert
            Assert.False(result);
        }
    }
}
