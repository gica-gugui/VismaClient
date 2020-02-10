using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using VismaClient.Converters;
using VismaClient.Models;
using VismaClient.Services.Interfaces;

namespace VismaClient.Services
{
    public class OrganizationService: IOrganizationService
    {
        private const string RESOURCE = "/api/v1/organization/";

        private readonly HttpClient _httpClient;

        public OrganizationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Organization> GetAsync(string id)
        {
            var response = await _httpClient.GetAsync($"{RESOURCE}/{id}");

            response.EnsureSuccessStatusCode();

            using (var responseStream = await response.Content.ReadAsStreamAsync())
            {
                return await JsonSerializer.DeserializeAsync<Organization>(responseStream, new JsonSerializerOptions { PropertyNamingPolicy = SnakeCaseNamingPolicy.Instance });
            }            
        }
    }
}
