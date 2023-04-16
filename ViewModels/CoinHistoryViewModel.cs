

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
using System.Windows;


namespace CryptoTrader.ViewModels
{
    internal class CoinHistoryViewModel : BindableBase, INavigationAware
    {


        private readonly IRegionManager _regionManager;
        private readonly IDataService _dataService;
        private double _minvalue;
        private string _interval;


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
        public DelegateCommand<string> IntervalCommand => new DelegateCommand<string>(Interval_Click);

        #endregion



        private void Interval_Click(string interval)
        {
            _interval = interval;
            GraphicView();
        }

        private void GoBack()
        {
            _regionManager.RequestNavigate("ContentRegion", "CoinInfo");
        }

        private void MinMax(double val)
        {
            if (val > MaxValue)
            {
                MaxValue = val + val / 10;
            }
            if (val < _minvalue)
            {
                MinValue = val - val/10;
                _minvalue = val;
            }
        }

        private async void GraphicView()
        {
            var result = await _dataService.GetHistory(HttpMethod.Get,
                             Constants.Path_Constants.HISTORY_PATH1 
                             + Name 
                             + Constants.Path_Constants.HISTORY_PATH2
                             +_interval
                             +Constants.Path_Constants.HISTORY_PATH3);

            Points = new ObservableCollection<ToolkPoint>();

            if (result != null)
            {

                foreach (var item in result)
                {
                    var val = Double.Parse(item[1], System.Globalization.CultureInfo.InvariantCulture);

                    val = Math.Round(val, 7, MidpointRounding.AwayFromZero);
                    MinMax(val);

                    Points.Add(new ToolkPoint(val, Convert.ToInt64(item[0])));
                }
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
                
                default:
                    break;
            }
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
           
            Name = navigationContext.Parameters["name"].ToString().ToLower();
          
            if (Name != null)
            {
                _minvalue = 100000.0; 
                _interval = "30";
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
