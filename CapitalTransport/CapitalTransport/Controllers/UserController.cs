using CapitalTransport.Model;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;


namespace CapitalTransport.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public UserController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "webAPI");
        }


        [HttpGet("retrieveUsers")]
        public async Task<IActionResult> RetrieveUsers([FromQuery] List<string> usernames)
        {
            usernames = usernames.Distinct().ToList();

            List<UserInfo> usersInfo = new();

            foreach (string username in usernames)
            {
                string apiUrl = $"https://api.github.com/users/{username}";

                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    UserInfo userInfo = JsonSerializer.Deserialize<UserInfo>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    userInfo.CalculateAverageFollowersPerRepository();
                    usersInfo.Add(userInfo);
                }
            }

            usersInfo = usersInfo.OrderBy(u => u.Name).ToList();

            return Ok(usersInfo);
        }
    }
}
 