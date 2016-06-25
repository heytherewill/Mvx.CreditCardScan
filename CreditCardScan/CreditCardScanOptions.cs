namespace CreditCardScan
{
	public class CreditCardScanOptions
	{
		/// <summary>
		/// Defines whether to use the app theme on the scan screen or not. Default to true.
		/// </summary>
		public bool KeepApplicationTheme { get; set; } = true;

		/// <summary>
		/// Show the Paypal icon. Defaults to false.
		/// </summary>
		public bool UsePaypalActionbarIcon { get; set; } = false;

		/// <summary>
		/// Hides the Card.IO logo. Defaults to true.
		/// </summary>
		public bool HideCardioLogo { get; set; } = true;

		/// <summary>
		/// Color hex for the guide border. Defaults to white.
		/// </summary>
		public string GuideColor { get; set; } = "#FFFFFF";

		/// <summary>
		/// Locale for the scan. Defaults to en_US.
		/// </summary>
		public string LanguageOrLocale { get; set; } = "en_US";

		/// <summary>
		/// Defines whether the scan should try detecting the expiry date. Defaults to true.
		/// </summary>
		public bool ScanExpiry { get; set; } = true;

		/// <summary>
		/// Defines if the user should be prompted to type the expiry date in case the scan fails to recognie it. Defaults to true.
		/// </summary>
		public bool RequireExpiry { get; set; } = true;

		/// <summary>
		/// Prevent the scan from prompting the user to manually enter the card info if the scan fails. Defaults to true.
		/// </summary>
		/// <value>The suppress manual entry.</value>
		public bool SuppressManualEntry { get; set; } = true;
	}
}