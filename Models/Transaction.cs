using System;

namespace Models
{
    public class Transaction
    {
        public DateTime Date { get; set; }

        public string Name { get; set; }

        public decimal TransferAmount { get; set; }

        public decimal Fee { get; set; }

        public override string ToString()
        {
            return $"{Date.ToShortDateString()} {Name} {Fee}";
        }
    }
}
