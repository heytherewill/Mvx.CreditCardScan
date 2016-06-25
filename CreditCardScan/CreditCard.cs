using System;

namespace CreditCardScan
{
	public class CreditCard
	{
		public string CardNumber { get; set; }

		public string Holder { get; set; }

		public string Cvv { get; set; }

		public string Expiry { get; set; }

		public DateTime ExpirationDate => new DateTime(ExpirationMonth, ExpirationYear, 1);

		public int ExpirationMonth { get; set; }

		public int ExpirationYear { get; set; }

		public static readonly CreditCard Empty = new CreditCard();
	}
}