using System.Threading.Tasks;
using VismaClient.Models;

namespace VismaClient.Services.Interfaces
{
    public interface IBearerService
    {
        public Task<Bearer> PostAsync(string clientId, string clientSecret, string scopes);
    }
}
