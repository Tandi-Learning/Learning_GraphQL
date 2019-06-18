using System.Net.Http;
using System.Threading.Tasks;
using CarvedRock.Web.Models;
using Newtonsoft.Json;

namespace CarvedRock.Web.HttpClients
{
    public class ProductHttpClient2
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductHttpClient2(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Response<ProductsContainer>> GetProducts()
        {
            var httpClient = _httpClientFactory.CreateClient("ProductHttpClient");

            var response = await httpClient.GetAsync(@"?query= 
        { products 
            { id name price rating photoFileName } 
        }");
            var stringResult = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Response<ProductsContainer>>(stringResult);
        }
    }
}
