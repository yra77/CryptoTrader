

using CryptoTrader.Services.HttpServices.DataService;
using CryptoTrader.Services.Localisation;
using CryptoTrader.Services.Repository;
using CryptoTrader.Services.Settings;
using CryptoTrader.ViewModels;
using CryptoTrader.Views;

using Prism.Ioc;
using Prism.Regions;
using Prism.Unity;
using System.Windows;


namespace CryptoTrader
{
    public partial class App : PrismApplication
    {


        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("ContentRegion", typeof(CoinList));
            regionManager.RegisterViewWithRegion("CoinInfoRegion", typeof(CoinInfo));
            regionManager.RegisterViewWithRegion("CoinInfoRegion", typeof(CoinHistory));
            regionManager.RegisterViewWithRegion("CoinInfoRegion", typeof(Settings));
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

            // register services
            containerRegistry.Register<IRepository, Repository> ();
            containerRegistry.Register<IDataService, DataService>();
            containerRegistry.RegisterSingleton<ISettings, SettingsApp>();
            containerRegistry.RegisterSingleton<ITranslationSource, TranslationSource>();


            // register pages
            containerRegistry.RegisterForNavigation<CoinInfo, CoinInfoViewModel>();
            containerRegistry.RegisterForNavigation<CoinList, CoinListViewModel>();
            containerRegistry.RegisterForNavigation<CoinHistory, CoinHistoryViewModel>();
            containerRegistry.RegisterForNavigation<Settings, SettingsViewModel>();

        }
        protected override Window CreateShell()
        {
            var w = Container.Resolve<MainWindow>();
            return w;
        }
    }
}
