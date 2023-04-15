using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTrader.Services.Localisation
{
    public interface ITranslationSource
    {
        CultureInfo CurrentCulture { get; set; }
    }
}
