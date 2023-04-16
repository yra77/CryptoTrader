

using CryptoTrader.Services.Localisation;
using CryptoTrader.Services.Settings;

using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

using System.ComponentModel;
using System.Globalization;
using System.Windows;


namespace CryptoTrader.ViewModels
{
    internal class SettingsViewModel : BindableBase, INavigationAware
    {


        private readonly IRegionManager _regionManager;
        private readonly ISettings _settings;
        private readonly ITranslationSource _translationSource;
        private string _pathGoBack;


        public SettingsViewModel(ISettings settings,
                                   IRegionManager regionManager,
                                   ITranslationSource translationSource)
        {
            _translationSource = translationSource;
            _regionManager = regionManager;
            _settings = settings;

            //App.LanguageChanged += LanguageChanged;
        }



        #region Public Property


        private bool _isUkrainian;
        public bool IsUkrainian
        {
            get => _isUkrainian;
            set => SetProperty(ref _isUkrainian, value);
        }

        private bool _isEnglish;
        public bool IsEnglish
        {
            get => _isEnglish;
            set => SetProperty(ref _isEnglish, value);
        }


        public DelegateCommand BackBtn => new DelegateCommand(GoBack);

        #endregion region



        private void GoBack()
        {
            _regionManager.RequestNavigate("ContentRegion", _pathGoBack);
        }


        #region implement interface

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);


            switch (args.PropertyName)
            {
                case "IsEnglish":
                    IsUkrainian = false;
                    _translationSource.CurrentCulture = CultureInfo.CreateSpecificCulture("en");
                    break;
                case "IsUkrainian":
                    IsEnglish = false;
                    _translationSource.CurrentCulture = CultureInfo.CreateSpecificCulture("uk");
                   
                    break;
                default:
                    break;
            }

            _settings.SaveProperty(language:_translationSource.CurrentCulture);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
           
            if (navigationContext.Parameters["pathToHome"] != null)
            {
                _pathGoBack = navigationContext.Parameters["pathToHome"].ToString();
               
                if(_translationSource.CurrentCulture.Name == "uk")
                {
                    IsUkrainian = true;
                    IsEnglish = false;
                }
                else
                {
                    IsEnglish = true;
                    IsUkrainian = false;
                }
            }
            else
            {
                MessageBox.Show("Error!");
            }
        }

        #endregion
    }
}
