using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace public_api_interface.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly HttpClient _httpClient;
        public ProductController(ILogger<ProductController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpGet]
        [Route("GetProducts")]
        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            Uri requestUri = new("http://host.docker.internal:9091/Product/GetProducts");
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            var response = await _httpClient.GetAsync(requestUri);
            var content = await response.Content.ReadAsStringAsync();
            var products = JsonSerializer.Deserialize<List<Product>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return products;
        }
    }
}
