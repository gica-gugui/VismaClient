using VismaClient.Models;
using VismaClient.Services.Interfaces.Base;

namespace VismaClient.Services.Interfaces
{
    public interface IDocumentService : IGet<Document>, IPost<Document>
    {
    }
}
