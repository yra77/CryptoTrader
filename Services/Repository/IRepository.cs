

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace CryptoTrader.Services.Repository
{
    internal interface IRepository
    {
        Task<string> GetData(HttpMethod method, string path);
        Task<byte[]> GetImage(string url);
    }
}
