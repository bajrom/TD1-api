
using System.Net.Http;
using System.Net.Http.Json;

namespace Application_Webassembly_Blazor.Models.Services
{
    public class WSService : IService
    {
        private readonly HttpClient _client;
        public WSService()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("http://localhost:7018/api/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Produit>> GetProduitsAsync(string nomControleur)
        {
            try
            {
                return await _client.GetFromJsonAsync<List<Produit>>(nomControleur);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
