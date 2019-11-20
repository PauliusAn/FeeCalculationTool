using System;
using Domain.Service.Calculators.Decorators;
using Persistence.Read_Models;
using UnitTests.TestModels;
using Xunit;

namespace UnitTests.Domain.Service.FeeCalculator.Decorators
{
    public class MonthlyFeeCalculator_Should
    {
        [Theory]
        [InlineData(0)]
        [InlineData(42)]
        [InlineData(0.2)]
        public void ReturnBaseFee_IfMonthlyFeeAlreadyPaid(decimal fee)
        {
            // Arrange
            var sut = new MonthlyFeeCalculator(new BaseTestCalculator());

            var date = new DateTime(2010, 10, 10);
            var testTransaction = new Transaction(date, "testMerchant", 0)
            {
                Fee = fee
            };

            sut.CalculateFee(testTransaction);

            // Act
            var result = sut.CalculateFee(testTransaction);

            // Assert
            Assert.Equal(fee, result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(42)]
        [InlineData(0.2)]
        public void AddMonthlyFee_IfItWasNotAlreadyPaid(decimal fee)
        {
            // Arrange
            var sut = new MonthlyFeeCalculator(new BaseTestCalculator());

            var date = new DateTime(2010, 10, 10);
            var testTransaction = new Transaction(date, "testMerchant", 0)
            {
                Fee = fee
            };

            var expectedResult = fee + sut.GetMonthlyFeeAmount();

            // Act
            var result = sut.CalculateFee(testTransaction);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(2010, 11, 30)]
        [InlineData(2011, 10, 10)]
        public void SetNewMonthWithFeePaid_WhenMonthlyFeeIsPaid(int year, int month, int day)
        {
            // Arrange
            var sut = new MonthlyFeeCalculator(new BaseTestCalculator());

            var date = new DateTime(2010, 10, 10);
            var testTransaction = new Transaction(date, "testMerchant", 0)
            {
                Fee = 0
            };

            sut.CalculateFee(testTransaction);
            var oldDate = sut.GetLastPaidMonth();

            var newDate = new DateTime(year, month, day);
            var newTestTransaction = new Transaction(newDate, "testMerchant", 0)
            {
                Fee = 0
            };

            // Act
            sut.CalculateFee(newTestTransaction);

            //
            var lastPaidMonth = sut.GetLastPaidMonth();
            newDate = newDate.AddDays(-newDate.Day + 1);
            date = date.AddDays(-date.Day + 1);

            // Assert
            Assert.Equal(newDate, lastPaidMonth);
            Assert.Equal(oldDate, date);
            Assert.NotEqual(oldDate, lastPaidMonth);
        }

        [Theory]
        [InlineData(2010, 11, 30)]
        [InlineData(2011, 10, 10)]
        public void NotSetNewMonth_WhenMonthlyFeeIsNotPaid(int year, int month, int day)
        {
            // Arrange
            var sut = new MonthlyFeeCalculator(new BaseTestCalculator());

            var date = new DateTime(year, month, day);
            var testTransaction = new Transaction(date, "testMerchant", 0)
            {
                Fee = 0
            };

            sut.CalculateFee(testTransaction);
            var oldDate = sut.GetLastPaidMonth();

            var newDate = new DateTime(year, month, day);
            var newTestTransaction = new Transaction(newDate, "testMerchant", 0)
            {
                Fee = 0
            };

            // Act
            sut.CalculateFee(newTestTransaction);

            //
            var lastPaidMonth = sut.GetLastPaidMonth();
            newDate = newDate.AddDays(-newDate.Day + 1);

            // Assert
            Assert.Equal(newDate, lastPaidMonth);
            Assert.Equal(oldDate, lastPaidMonth);
        }
    }
}
