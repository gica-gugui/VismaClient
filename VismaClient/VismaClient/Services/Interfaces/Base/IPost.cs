using System.Threading.Tasks;

namespace VismaClient.Services.Interfaces.Base
{
    public interface IPost<T> where T: class
    {
        public Task<string> PostAsync(T model);
    }
}
