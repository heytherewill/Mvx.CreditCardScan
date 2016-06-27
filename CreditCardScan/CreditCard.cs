using System;

namespace CreditCardScan
{
	public class CreditCard
	{
		public string CardNumber { get; set; }

		public string Holder { get; set; }

		public string Cvv { get; set; }

		public DateTime ExpirationDate => new DateTime(ExpirationMonth, ExpirationYear, DateTime.DaysInMonth(ExpirationYear, ExpirationMonth));

		public int ExpirationMonth { get; set; } = 1;

		public int ExpirationYear { get; set; } = 1;

		public static readonly CreditCard Empty = new CreditCard();
	}
}