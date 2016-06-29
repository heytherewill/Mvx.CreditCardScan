using MvvmCross.Platform.Plugins;

namespace CreditCardScan.iOS
{
	public class Plugin : IMvxPlugin
	{
		public void Load()
		{
			CreditCardScanService.Initialize();
		}
	}
}