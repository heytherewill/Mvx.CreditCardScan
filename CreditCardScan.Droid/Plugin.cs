using MvvmCross.Platform.Plugins;

namespace CreditCardScan.Droid
{
	public class Plugin : IMvxPlugin
	{
		public void Load()
		{
			CreditCardScanService.Initialize();
		}
	}
}