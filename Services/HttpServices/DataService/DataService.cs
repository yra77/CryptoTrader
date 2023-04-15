

using CryptoTrader.Services.Repository;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;


namespace CryptoTrader.Services.HttpServices.DataService
{
    internal class DataService : IDataService
    {


        private readonly IRepository _repository;


        public DataService(IRepository repository)
        {
            _repository = repository;
        }


        public async Task<ObservableCollection<T>> GetData<T>(HttpMethod method, string path) where T : class, new()
        {
            string res = await _repository.GetData(method, path);

            return (res != null)
                    ? JsonConvert.DeserializeObject<ObservableCollection<T>>(res)// JObject.Parse(res)["data"].ToString())
                    : null;
        }


        public async Task<T> GetDataCoinInfo<T>(HttpMethod method, string path) where T : class, new()
        {
            string res = await _repository.GetData(method, path);

            return (res != null)
                    ? JsonConvert.DeserializeObject<T>(res)//JObject.Parse(res)["data"].ToString())
                    : null;
        }


        public async Task<List<T>> GetHistory<T>(HttpMethod method, string path) where T : class, new()
        {
            string res = await _repository.GetData(method, path);

            return (res != null)
                    ? JsonConvert.DeserializeObject<List<T>>(JObject.Parse(res)["data"].ToString())
                    : null;
        }

        public async Task<byte[]> GetImage(string coinName)
        {
            return await _repository.GetImage("https://assets.coingecko.com/coins/images/1/small/bitcoin.png");
        }
    }
}
