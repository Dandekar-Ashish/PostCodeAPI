using System.Threading.Tasks;

namespace PostCodeAPI.Service.Interface
{
    public interface IHttpClientRepository
    {
        Task<string> HttpGet(string url);
    }
}
