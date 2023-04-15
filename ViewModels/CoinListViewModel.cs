

using CryptoTrader.Models;
using CryptoTrader.Services.HttpServices.DataService;

using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Windows.Data;


namespace CryptoTrader.ViewModels
{
    internal class CoinListViewModel : BindableBase, INavigationAware
    {

        private readonly IRegionManager _regionManager;
        private readonly IDataService _dataService;
        private List<DataMarketModel> _staticList;
        private ICollectionView _lists;

        public CoinListViewModel(IDataService dataService,
                                   IRegionManager regionManager)
        {
            _regionManager = regionManager;
            _dataService = dataService;

            SearchInput = "";
            _staticList = new List<DataMarketModel>(); 
            GetItems();
        }



        #region Public Property


        private string _searchInput;
        public string SearchInput
        {
            get => _searchInput;
            set => SetProperty(ref _searchInput, value);
        }


        public List<DataMarketModel> CoinsList { get; private set; } =
            new List<DataMarketModel>();


        private DataMarketModel _selectedCoin = null;
        public DataMarketModel SelectedCoin
        {
            get => _selectedCoin;
            set => SetProperty(ref _selectedCoin, value);
        }


        public DelegateCommand CommandSettings => new DelegateCommand(CommandSettingsClick);

        #endregion



        private void CommandSettingsClick()
        {
            var parameters = new NavigationParameters();
            parameters.Add("pathToHome", "CoinList");

            _regionManager.RequestNavigate("ContentRegion", "Settings", parameters);
        }

        private async void GetItems()
        {
            var list = await _dataService.GetData<DataMarketModel>(HttpMethod.Get, Constants.Path_Constants.LIST_COIN_PATH);

            if (list != null)
            {
                CoinsList.Clear();

                for (int i = 0; i < list.Count; i++)
                {
                    list[i].image = list[i].image.Replace("large", "thumb");

                    CoinsList.Add(list[i]);
                }

                _staticList = CoinsList;
                _lists = CollectionViewSource.GetDefaultView(CoinsList);
                _lists.Refresh();
            }
            else
            {

            }
        }


        #region implement Interfaces

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            switch (args.PropertyName)
            {
                case "SelectedCoin":
                    var parameters = new NavigationParameters();
                    parameters.Add("name", SelectedCoin.id);
                    parameters.Add("price", SelectedCoin.current_price);

                    _regionManager.RequestNavigate("ContentRegion", "CoinInfo", parameters);
                    break;

                case "SearchInput":
                    if (SearchInput.Length > 0)
                    {
                        CoinsList = _staticList.Where(item => item.name.ToLower().Contains(SearchInput.ToLower())
                                   || item.symbol.ToLower().Contains(SearchInput.ToLower())).ToList();

                        _lists.Refresh();
                        RaisePropertyChanged("CoinsList");
                    }
                    else if(_staticList != null)
                    {
                       CoinsList = _staticList; 
                        _lists.Refresh();
                        RaisePropertyChanged("CoinsList");
                    }
                    break;

                default:
                    break;
            }
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            Debug.WriteLine("to");
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
