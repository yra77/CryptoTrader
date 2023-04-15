

using CryptoTrader.Models;
using CryptoTrader.Services.HttpServices.DataService;
using CryptoTrader.Services.Localisation;
using CryptoTrader.Services.Settings;
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
        private readonly ISettings _settings;

        public MainWindowViewModel(ISettings settings,
                                   IRegionManager regionManager,
                                   ITranslationSource translationSource)
        {
            _settings = settings;
            _translationSource = translationSource;
            _translationSource.CurrentCulture = _settings.Load().Language;
            regionManager.RegisterViewWithRegion("ContentRegion", typeof(CoinList));
        }

    }
}

