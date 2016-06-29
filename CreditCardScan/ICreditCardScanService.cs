using System;
using System.Threading.Tasks;

namespace CreditCardScan
{
	public interface ICreditCardScanService
	{
		/// <summary>
		/// Scans the card info asynchronously.
		/// </summary>
		/// <returns>The scanned credit card's info.</returns>
		/// <param name="creditCardScanOptions"> Options for the scan screen. </param>
		Task<CreditCard> ScanCardInfoAsync(CreditCardScanOptions creditCardScanOptions = null);

		/// <summary>
		/// Scans the card info.
		/// </summary>
		/// <param name="callback">Callback called once the card has been scanned.</param>
		/// <param name="creditCardScanOptions"> Options for the scan screen.</param>
		void ScanCardInfo(Action<CreditCard> callback, CreditCardScanOptions creditCardScanOptions = null);
	}
}