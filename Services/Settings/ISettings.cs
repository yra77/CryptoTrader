using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTrader.Services.Settings
{
    internal interface ISettings
    {
        bool SaveProperty(CultureInfo language, string theme = "Light");
        SettingsApp Load();
    }
}
