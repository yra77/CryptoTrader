

using CryptoTrader.Services.Localisation;
using CryptoTrader.Services.Settings;
using CryptoTrader.Views;

using Prism.Mvvm;
using Prism.Regions;


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

