

using CryptoTrader.Services.HttpServices.DataService;
using CryptoTrader.Models;

using Prism.Mvvm;
using Prism.Regions;
using Prism.Commands;

using System;
using System.Net.Http;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;


namespace CryptoTrader.ViewModels
{
    internal class CoinHistoryViewModel : BindableBase, INavigationAware
    {


        private readonly IRegionManager _regionManager;
        private readonly IDataService _dataService;


        public CoinHistoryViewModel(IDataService dataService,
                                   IRegionManager regionManager)
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


        private double _maxValue;
        public double MaxValue 
        {
            get => _maxValue;
            set => SetProperty(ref _maxValue, value);
        }


        private double _minValue;
        public double MinValue
        {
            get => _minValue;
            set => SetProperty(ref _minValue, value);
        }


        private ObservableCollection<ToolkPoint> _points;
        public ObservableCollection<ToolkPoint> Points 
        {
            get => _points;
            set => SetProperty(ref _points, value);
        }


        public DelegateCommand BackBtn => new DelegateCommand(GoBack);

        #endregion



        private void GoBack()
        {
            _regionManager.RequestNavigate("ContentRegion", "CoinInfo");
        }

        private void MinMax(double val)
        {
            if (val > MaxValue)
            {
                MaxValue = Math.Round(val, 5, MidpointRounding.AwayFromZero);
            }
            if (val < MinValue)
            {
                MinValue = Math.Round(val, 5, MidpointRounding.AwayFromZero);
            }
        }

        private async void GraphicView()
        {
            //bitcoin/history?interval=d1
            var result = await _dataService.GetHistory<HistoryModel>(HttpMethod.Get, 
                             Constants.Path_Constants.HISTORY_PATH + Name + "/history?interval=d1");//&start=1681126967000&end=1681472567000");


            Points = new ObservableCollection<ToolkPoint>();

            if (result != null)
            {

                foreach (var item in result)
                {

                    var val = Double.Parse(item.priceUsd, System.Globalization.CultureInfo.InvariantCulture);

                    MinMax(val);

                    Points.Add(new ToolkPoint(val, Convert.ToInt64(item.time)));
                }
            }
        }



        #region implement Interfaces

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            switch (args.PropertyName)
            {
                
                default:
                    break;
            }
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
           
            Name = navigationContext.Parameters["name"].ToString().ToLower();
          
            if (Name != null)
            {
                MinValue = 0;
                MaxValue = 0;
                GraphicView();
            }
            else
            {

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
