using System;
using System.Threading.Tasks;
using Card.IO;
using Foundation;
using MvvmCross.Platform;
using MvvmCross.Platform.iOS.Views;
using MvvmCross.Platform.Platform;
using UIKit;

namespace CreditCardScan.iOS
{
	public class CreditCardScanService : NSObject, ICardIOPaymentViewControllerDelegate, ICreditCardScanService
	{
		internal static void Initialize()
		{
			Mvx.RegisterSingleton<ICreditCardScanService>(new CreditCardScanService());
			Mvx.Trace(MvxTraceLevel.Diagnostic, "ICreditCardScanService registered");
		}

		protected CreditCardScanService() 
		{
			_modalHost = Mvx.Resolve<IMvxIosModalHost>();
		}

		private readonly IMvxIosModalHost _modalHost;

		private Action<CreditCard> _callback;
		private CardIOPaymentViewController _paymentViewController;

		public void ScanCardInfo(Action<CreditCard> callback, CreditCardScanOptions creditCardScanOptions = null)
		{
			if (creditCardScanOptions == null)
			{
				creditCardScanOptions = new CreditCardScanOptions();
			}

			_callback = callback;
			_paymentViewController = new CardIOPaymentViewController(this)
			{
				GuideColor = ColorFromHex(creditCardScanOptions.GuideColor)
			};

			_modalHost.PresentModalViewController(_paymentViewController, true);
		}

		public Task<CreditCard> ScanCardInfoAsync(CreditCardScanOptions creditCardScanOptions = null)
		{
			var tcs = new TaskCompletionSource<CreditCard>();
			ScanCardInfo(tcs.SetResult, creditCardScanOptions);
			return tcs.Task;
		}

		public void UserDidCancelPaymentViewController(CardIOPaymentViewController paymentViewController)
		{
			_callback?.Invoke(CreditCard.Empty);
			_paymentViewController.DismissViewController(true, null);
			_modalHost.NativeModalViewControllerDisappearedOnItsOwn();
		}

		public void UserDidProvideCreditCardInfo(CreditCardInfo cardInfo, CardIOPaymentViewController paymentViewController)
		{
			var creditCard = cardInfo == null ? CreditCard.Empty : new CreditCard
			{
				CardNumber = cardInfo.CardNumber,
				Cvv = cardInfo.Cvv,
				ExpirationMonth = (int)cardInfo.ExpiryMonth,
				ExpirationYear = (int)cardInfo.ExpiryYear
			};

			_callback?.Invoke(creditCard);
			_paymentViewController.DismissViewController(true, null);
			_modalHost.NativeModalViewControllerDisappearedOnItsOwn();
		}

		private UIColor ColorFromHex(string hex)
		{
			var rgbValue = Convert.ToInt32(hex.Substring(1, hex.Length), 16);
			var red = (nfloat)(((rgbValue & 0xFF0000) >> 16) / 255.0);
			var green = (nfloat)(((rgbValue & 0xFF00) >> 8) / 255.0);
			var blue = (nfloat)((rgbValue & 0xFF) / 255.0);

			return new UIColor(red, green, blue, 1.0f);
		}
	}
}