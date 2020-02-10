using Microsoft.AspNetCore.WebUtilities;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using VismaClient.Converters;
using VismaClient.Models;
using VismaClient.Services.Interfaces;

namespace VismaClient.Services
{
    public class BearerService: IBearerService
    {
        private const string RESOURCE = "/api/v1/auth/token";

        private readonly HttpClient _httpClient;

        public BearerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Bearer> PostAsync(string clientId, string clientSecret, string scopes)
        {
            var queryParams = new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" },
                { "client_id", clientId },
                { "client_secret", clientSecret },
                { "scope", scopes }
            };

            var uri = QueryHelpers.AddQueryString(RESOURCE, queryParams);

            var response = await _httpClient.PostAsync(uri, new StringContent(""));

            response.EnsureSuccessStatusCode();

            using (var responseStream = await response.Content.ReadAsStreamAsync())
            {
                return await JsonSerializer.DeserializeAsync<Bearer>(responseStream, new JsonSerializerOptions { PropertyNamingPolicy = SnakeCaseNamingPolicy.Instance });
            }            
        }
    }
}
