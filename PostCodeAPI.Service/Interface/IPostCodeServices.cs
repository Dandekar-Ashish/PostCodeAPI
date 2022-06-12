using PostCodeAPI.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PostCodeAPI.Service.Interface
{
    public interface IPostCodeServices
    {
        Task<PostCodeAutoComplete> GetPostCodes(string countryCode);
        Task<PostCodeLookUp> PostCodeLookup(string postCode);
    }
}
