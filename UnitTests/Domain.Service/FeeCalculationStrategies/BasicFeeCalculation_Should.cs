//using Domain.Service.FeeCalculationStrategies;
//using Xunit;
//
//namespace UnitTests.Domain.Service.FeeCalculationStrategies
//{
//    public class BasicFeeCalculation_Should
//    {
//        [Theory]
//        [InlineData(100, 1)]
//        [InlineData(0, 0)]
//        [InlineData(-10, -0.1)]
//        [InlineData(-100, -1)]
//        public void ReturnCalculatedFee_WhenAmountIsGiven(decimal amount, decimal expectedFee)
//        {
//            // Arrange
//            var sut = new BasicFeeCalculation();
//
//            // Act
//            var result = sut.CalculateFee(amount);
//
//            // Assert
//            Assert.Equal(result, expectedFee);
//        }
//    }
//}
