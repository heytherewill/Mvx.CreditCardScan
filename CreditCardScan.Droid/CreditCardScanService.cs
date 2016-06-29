using System;
using System.Threading.Tasks;
using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Card.IO;
using MvvmCross.Platform;
using MvvmCross.Platform.Droid.Platform;
using MvvmCross.Platform.Droid.Views;
using MvvmCross.Platform.Platform;

namespace CreditCardScan.Droid
{
	public class CreditCardScanService : MvxAndroidTask, ICreditCardScanService
	{
		internal static void Initialize()
		{
			Mvx.RegisterSingleton<ICreditCardScanService>(new CreditCardScanService());
			Mvx.Trace(MvxTraceLevel.Diagnostic, "ICreditCardScanService registered");
		}

		protected CreditCardScanService() { }

		private const int CreditCardScanRequestCode = 101;

		private Action<CreditCard> _callback;

		public void ScanCardInfo(Action<CreditCard> callback, CreditCardScanOptions creditCardScanOptions = null)
		{
			if (creditCardScanOptions == null)
			{
				creditCardScanOptions = new CreditCardScanOptions();
			}

			_callback = callback;

			var context = Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity;

			var intent = new Intent(context, typeof(CardIOActivity));
			intent.PutExtra(CardIOActivity.ExtraKeepApplicationTheme, creditCardScanOptions.KeepApplicationTheme);
			intent.PutExtra(CardIOActivity.ExtraGuideColor, Color.ParseColor(creditCardScanOptions.GuideColor));
			intent.PutExtra(CardIOActivity.ExtraUsePaypalActionbarIcon, creditCardScanOptions.UsePaypalActionbarIcon);
			intent.PutExtra(CardIOActivity.ExtraHideCardioLogo, creditCardScanOptions.HideCardioLogo);
			intent.PutExtra(CardIOActivity.ExtraLanguageOrLocale, creditCardScanOptions.LanguageOrLocale);
			intent.PutExtra(CardIOActivity.ExtraScanExpiry, creditCardScanOptions.ScanExpiry);
			intent.PutExtra(CardIOActivity.ExtraRequireExpiry, creditCardScanOptions.RequireExpiry);
			intent.PutExtra(CardIOActivity.ExtraSuppressManualEntry, creditCardScanOptions.SuppressManualEntry);

			StartActivityForResult(CreditCardScanRequestCode, intent);
		}

		public Task<CreditCard> ScanCardInfoAsync(CreditCardScanOptions creditCardScanOptions = null)
		{
			var tcs = new TaskCompletionSource<CreditCard>();
			ScanCardInfo(tcs.SetResult);
			return tcs.Task;
		}

		protected override void ProcessMvxIntentResult(MvxIntentResultEventArgs result)
		{
			var creditCard = CreditCard.Empty;
			var card = result?
						.Data?
						.GetParcelableExtra(CardIOActivity.ExtraScanResult)?
						.JavaCast<Card.IO.CreditCard>();

			if (card != null)
			{
				creditCard = new CreditCard
				{
					Cvv = card.Cvv,
					CardNumber = card.CardNumber,
					ExpirationYear = card.ExpiryYear,
					ExpirationMonth = card.ExpiryMonth
				};
			}

			_callback?.Invoke(creditCard);
		}
	}
}