using PostCodeAPI.Common.Interface;
using PostCodeAPI.Model;
using PostCodeAPI.Service.Interface;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PostCodeAPI.Service.Impl
{
    public class PostCodeServices : IPostCodeServices
    {
        private readonly HttpClient client;
        private readonly IHttpClientRepository _httpClientRepository;
        private readonly IEnvironmentConfiguration _environmentConfiguration;
        public PostCodeServices(IHttpClientFactory clientFactory, IHttpClientRepository httpClientRepository, IEnvironmentConfiguration environmentConfiguration)
        {
            client = clientFactory.CreateClient("PostCodesAPI");
            _httpClientRepository = httpClientRepository;
            _environmentConfiguration = environmentConfiguration;
        }
        public async Task<PostCodeAutoComplete> GetPostCodes(string countryCode)
        {
            var url = string.Format(_environmentConfiguration.GetAutoCompleteRoute(), countryCode);
            string response = await _httpClientRepository.HttpGet(url);
            PostCodeAutoComplete result = JsonSerializer.Deserialize<PostCodeAutoComplete>(response,
                   new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            return result;
        }
        public async Task<PostCodeLookUp> PostCodeLookup(string postCode)
        {
            var url = string.Format(_environmentConfiguration.GetPostCodeLookupRoute(), postCode);
            string response = await _httpClientRepository.HttpGet(url);
            PostCodeLookUp result = JsonSerializer.Deserialize<PostCodeLookUp>(response,
                new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            return result;
        }
    }
}
