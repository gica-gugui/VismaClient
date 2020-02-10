using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using VismaClient.Converters;
using VismaClient.Models;
using VismaClient.Services.Interfaces;

namespace VismaClient.Services
{
    public class DocumentService: IDocumentService
    {
        private const string RESOURCE = "/api/v1/document/";

        private readonly HttpClient _httpClient;

        public DocumentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Document> GetAsync(string id)
        {
            var response = await _httpClient.GetAsync($"{RESOURCE}/{id}");

            response.EnsureSuccessStatusCode();

            using (var responseStream = await response.Content.ReadAsStreamAsync())
            {
                return await JsonSerializer.DeserializeAsync<Document>(responseStream, new JsonSerializerOptions { PropertyNamingPolicy = SnakeCaseNamingPolicy.Instance });
            }            
        }

        public async Task<string> PostAsync(Document model)
        {
            var json = JsonSerializer.Serialize(model, new JsonSerializerOptions { IgnoreNullValues = true, PropertyNamingPolicy = SnakeCaseNamingPolicy.Instance });
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(RESOURCE, content);

            response.EnsureSuccessStatusCode();

            return response.Headers.Location.ToString();
        }
    }
}
