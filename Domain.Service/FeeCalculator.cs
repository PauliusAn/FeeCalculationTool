using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Models;

namespace Domain.Service
{
    public static class FeeCalculator
    {
        private const double BasicFee = 0.01;

        public static decimal CalculateFee(Transaction transaction)
        {
            return transaction.TransferAmount * Convert.ToDecimal(BasicFee);
        }
    }
}
