using System;
using Domain.Service.Calculators;
using Persistence.Read_Models;
using Xunit;

namespace UnitTests.Domain.Service.FeeCalculator
{
    public class Accountant_Should
    {
        [Fact]
        public void AddNewCalculator_IfMerchantIsNotFound()
        {
            // Arrange
            var sut = new Accountant();
            var testTransaction = new Transaction(
                new DateTime(2010, 10, 10), "newMerchant", 100);

            // Act
            sut.CalculateFee(testTransaction);

            // Arrange
            Assert.Single(sut.RegisteredCalculators);
        }
    }
}