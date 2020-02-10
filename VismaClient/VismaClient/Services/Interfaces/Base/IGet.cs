using System.Threading.Tasks;

namespace VismaClient.Services.Interfaces.Base
{
    public interface IGet<T> where T: class
    {
        public Task<T> GetAsync(string id);
    }
}
