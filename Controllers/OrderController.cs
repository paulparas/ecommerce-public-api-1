using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace public_api_interface.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly HttpClient _httpClient;
        private readonly HostSettings _hostSettings;
        public OrderController(ILogger<OrderController> logger, IHttpClientFactory httpClientFactory, IOptions<HostSettings> options)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
            _hostSettings = options.Value;
        }

        [HttpGet]
        [Route("GetOrders")]
        public async Task<IEnumerable<Order>> GetOrders()
        {
            Uri requestUri = new($"http://{_hostSettings.OrderApiUrl}/Order/GetOrders");
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
