using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace public_api_interface.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly HttpClient _httpClient;
        public OrderController(ILogger<OrderController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpGet]
        [Route("GetOrders")]
        public async Task<IEnumerable<Order>> GetOrders()
        {
            Uri requestUri = new("http://host.docker.internal:9092/Order/GetOrders");
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            var response = await _httpClient.GetAsync(requestUri);
            var content = await response.Content.ReadAsStringAsync();
            var orders = JsonSerializer.Deserialize<List<Order>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return orders;
        }
    }


}
