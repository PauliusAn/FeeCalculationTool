using System;

namespace Persistence.Read_Models
{
    public class Transaction
    {
        public DateTime Date { get; }

        public string MerchantName { get; }

        public decimal TransferAmount { get; }

        private decimal _fee;

        public decimal Fee
        {
            get => _fee;
            set
            {
                if(value < 0)
                    throw new ArgumentException("Transaction fee amount can not be lest than zero!");
                _fee = value;
            }
        }


        public Transaction(DateTime date, string merchantName, decimal transferAmount)
        {
            if (transferAmount < 0)
            {
                throw new ArgumentException("Transaction transfer amount can not be less than zero!");
            }

            Date = date;
            MerchantName = merchantName;
            TransferAmount = transferAmount;
        }

        public override string ToString()
        {
            return $"{Date.ToShortDateString()} {MerchantName,-10} {Fee:0.00}";
        }
    }
}
