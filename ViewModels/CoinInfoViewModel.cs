

using CryptoTrader.Models;
using CryptoTrader.Services.HttpServices.DataService;

using Prism.Commands;
using Prism.Regions;
using Prism.Mvvm;

using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;


namespace CryptoTrader.ViewModels
{
    internal class CoinInfoViewModel : BindableBase, INavigationAware
    {


        private readonly IDataService _dataService;
        private readonly IRegionManager _regionManager;



        public CoinInfoViewModel(IDataService dataService, IRegionManager regionManager)
        {
            _regionManager = regionManager;
            _dataService = dataService;
        }



        #region Public Property


        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value); 
        }

        private string _price;
        public string Price
        {
            get => _price;
            set => SetProperty(ref _price, value);
        }


        private CoinInfoModel _coininfo;
        public CoinInfoModel CoinInfo 
        {
            get => _coininfo;
            set => SetProperty(ref _coininfo, value);
        }


        public DelegateCommand BackBtn => new DelegateCommand(GoBack);
        public DelegateCommand HistoryBtn => new DelegateCommand(GoHistory);

        #endregion



        private void GoBack()
        {
            _regionManager.RequestNavigate("ContentRegion", "CoinList");
        }

        private void GoHistory()
        {
            var parameters = new NavigationParameters();
            parameters.Add("name", CoinInfo.id);

            _regionManager.RequestNavigate("ContentRegion", "CoinHistory", parameters);
        }

        private async void GetCoinInfo()
        {
            CoinInfo = await _dataService.GetDataCoinInfo<CoinInfoModel>(HttpMethod.Get,
                             Constants.Path_Constants.COIN_INFO_PATH1 + Name.ToLower()
                             + Constants.Path_Constants.COIN_INFO_PATH2);

        }





        #region implement Interfaces

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            switch (args.PropertyName)
            {
                case "Name":
                    Debug.WriteLine(Name);
                    break;
                default:
                    break;
            }
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {

            if (navigationContext.Parameters["name"] != null 
                && navigationContext.Parameters["price"] != null)
            {
                Name = navigationContext.Parameters["name"].ToString().ToUpper();
                Price = navigationContext.Parameters["price"].ToString();

                if (Name != null & Price != null)
                {
                    GetCoinInfo();
                }
                else
                {

                }
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            Debug.WriteLine("from");
        }

        #endregion
    }
}
