

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;


namespace CryptoTrader.Services.HttpServices.DataService
{
    internal interface IDataService
    {
        Task<ObservableCollection<T>> GetData<T>(HttpMethod method, string path) where T : class, new();
        Task<T> GetDataCoinInfo<T>(HttpMethod method, string path) where T : class, new();
        Task<List<string[]>> GetHistory(HttpMethod method, string path);
        Task<byte[]> GetImage(string coinName);
    }
}
