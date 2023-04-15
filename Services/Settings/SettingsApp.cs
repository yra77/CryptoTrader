

using Newtonsoft.Json;
using System;
using System.Globalization;
using System.IO;


namespace CryptoTrader.Services.Settings
{
    internal class SettingsApp : ISettings
    {


        public CultureInfo Language { get; set; }
        public string Theme { get; set; }


        public bool SaveProperty(CultureInfo language, string theme = "Light")
        {

            Language = language;
            Theme = theme;

            try
            {
                File.WriteAllText(GetLocalFilePath("AppSettings.json"), JsonConvert.SerializeObject(this));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public SettingsApp Load()
        {
            try
            {
                var data = JsonConvert.DeserializeObject<SettingsApp>(File.ReadAllText(GetLocalFilePath("AppSettings.json")));

                return data;
            }
            catch
            {
                return new SettingsApp() { Language = new CultureInfo("en"), Theme = "Light"};
            }
        }

        private string GetLocalFilePath(string fileName)
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return Path.Combine(appData, fileName);
        }

    }
}
