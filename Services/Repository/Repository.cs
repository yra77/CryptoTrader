

using System;
using System.Net.Http;
using System.Threading.Tasks;


namespace CryptoTrader.Services.Repository
{
    internal class Repository:IRepository
    {

        
        private readonly HttpClient _client;


        public Repository()
        {
            _client = new HttpClient();
        }


        public async Task<string> GetData(HttpMethod method, string url) 
        {
            try
            {
                var request = new HttpRequestMessage(method, url);
                _client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.88 Safari/537.36");
                using (var response = await _client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();

                    return await response.Content.ReadAsStringAsync();
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<byte[]> GetImage(string url)
        {
            try
            {
                using (var response = await _client.GetAsync(url))
                {
                    byte[] imageBytes = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
                    return imageBytes;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }


    }
}
