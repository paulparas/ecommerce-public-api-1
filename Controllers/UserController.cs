using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace public_api_interface.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly HttpClient _httpClient;
        private readonly HostSettings _hostSettings;
        public UserController(ILogger<UserController> logger, IHttpClientFactory httpClientFactory, IOptions<HostSettings> options)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
            _hostSettings = options.Value;
        }

        [HttpGet]
        [Route("GetUsers")]
        public async Task<IEnumerable<User>> GetUsers()
        {
            Uri requestUri = new($"http://{_hostSettings.UserApiUrl}/User/GetUsers");
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            var response = await _httpClient.GetAsync(requestUri);
            var content = await response.Content.ReadAsStringAsync();
            var users = JsonSerializer.Deserialize<List<User>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return users;
        }
    }
}
