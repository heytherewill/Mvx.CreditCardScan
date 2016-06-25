# Mvx.CreditCardScan
:credit_card: MvvmCross Credit Card Scan Plugin

This plugins wraps [card.io](https://www.card.io/) to make it usable from any [MvvmCross](https://github.com/MvvmCross/MvvmCross). Currently only available for Android

# Installation

Install via [NuGet](https://www.nuget.org/packages/Mvx.CreditCardScan/) using:

``PM> Install-Package Mvx.CreditCardScan``

# Usage

Resolve it:

``var creditCardScanService = Mvx.Resolve<ICreditCardScanService>();``

Configure it, if needed (this step is optional):

```
var options = new CreditCardScanOptions
{
  GuideColor = "#1E1E1E",
  UsePaypalActionbarIcon = true
};
```

Call the scan screen asynchronously:

``var creditCard = await creditCardScanService.ScanCardInfoAsync(options);``

or using a callback:

```
creditCardScanService.ScanCardInfo(creditCard => { /* Magic goes here */ }, options);
```

The service never returns null, so you can compare the returned card with ``CreditCard.Empty`` to check if the call failed:

``if (creditCard == CreditCard.Empty) return;``


#Thanks

Credit Card Icon by Zlatko Najdenovski from the Noun Project
