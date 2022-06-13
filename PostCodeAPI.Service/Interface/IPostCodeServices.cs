using PostCodeAPI.DataTransferModel;
using System.Threading.Tasks;

namespace PostCodeAPI.Service.Interface
{
    public interface IPostCodeServices
    {
        Task<PostCodeAutoCompleteDataTransferModel> GetPostCodes(string countryCode);
        Task<PostCodeLookUpDataTransferModel> PostCodeLookup(string postCode);
    }
}
