

using CryptoTrader.Models;
using CryptoTrader.Services.HttpServices.DataService;
using CryptoTrader.Services.Localisation;
using CryptoTrader.Views;

using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Navigation;
using System.Xml.Linq;


namespace CryptoTrader.ViewModels
{
    internal class MainWindowViewModel:BindableBase
    {

        private readonly ITranslationSource _translationSource;


        public MainWindowViewModel(IRegionManager regionManager,
                                   ITranslationSource translationSource)
        {
            _translationSource = translationSource;
            _translationSource.CurrentCulture = CultureInfo.GetCultureInfo("en");
            regionManager.RegisterViewWithRegion("ContentRegion", typeof(CoinList));
        }

    }
}

