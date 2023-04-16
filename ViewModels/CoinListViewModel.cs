

using CryptoTrader.Models;
using CryptoTrader.Services.HttpServices.DataService;

using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Timers;
using System.Windows;
using System.Windows.Data;


namespace CryptoTrader.ViewModels
{
    internal class CoinListViewModel : BindableBase, INavigationAware
    {

        private static Timer _Timer;

        private readonly IRegionManager _regionManager;
        private readonly IDataService _dataService;
        private List<DataMarketModel> _staticList;


        public CoinListViewModel(IDataService dataService,
                                   IRegionManager regionManager)
        {
            _regionManager = regionManager;
            _dataService = dataService;

            SearchInput = "";

            GetItems(false);

            // Create a timer and set a two second interval.
            _Timer = new System.Timers.Timer();
            _Timer.Interval = 30000;
            _Timer.Elapsed += OnTimedEvent;
            _Timer.AutoReset = true;
            _Timer.Enabled = true;
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


        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            Console.WriteLine(e.SignalTime);
            GetItems(true);
        }

        private void CommandSettingsClick()
        {
            var parameters = new NavigationParameters();
            parameters.Add("pathToHome", "CoinList");

            _regionManager.RequestNavigate("ContentRegion", "Settings", parameters);
        }

        private async void GetItems(bool isTimer)
        {
            var list = await _dataService.GetData<DataMarketModel>(HttpMethod.Get, Constants.Path_Constants.LIST_COIN_PATH);

            if (list != null)
            {
                CoinsList.Clear();

                for (int i = 0; i < list.Count; i++)
                {
                    list[i].image = list[i].image.Replace("large", "thumb");

                    if (list[i].price_change_percentage_24h[0] == '-')
                    {
                        list[i].colorPercentage = "Red";
                    }
                    else
                    {
                        list[i].colorPercentage = "Green";
                    }

                    if (isTimer 
                        && i < _staticList.Count
                        && list[i].name == _staticList[i].name)
                    {
                       
                        var now = Double.Parse(list[i].current_price, CultureInfo.InvariantCulture);
                        var prev = Double.Parse(_staticList[i].current_price, CultureInfo.InvariantCulture);
                       // Console.WriteLine(now + " " + prev);

                        if (now >= prev)
                        {
                            list[i].colorPrice = "Green";
                        }
                        else if (now < prev)
                        {
                            list[i].colorPrice = "Red";
                        }
                    }
                    CoinsList.Add(list[i]);

                }

                _staticList = new List<DataMarketModel>(CoinsList);
                RaisePropertyChanged("CoinsList");
            }
            else
            {
                MessageBox.Show("Error!");
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

                        RaisePropertyChanged("CoinsList");
                    }
                    else if (_staticList != null)
                    {
                        CoinsList = _staticList;
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
