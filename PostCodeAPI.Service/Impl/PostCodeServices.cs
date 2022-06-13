using AutoMapper;
using Microsoft.Extensions.Logging;
using PostCodeAPI.Common.Interface;
using PostCodeAPI.DataTransferModel;
using PostCodeAPI.Model;
using PostCodeAPI.Service.Interface;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace PostCodeAPI.Service.Impl
{
    public class PostCodeServices : IPostCodeServices
    {
        private readonly HttpClient client;
        private readonly IHttpClientRepository _httpClientRepository;
        private readonly IEnvironmentConfiguration _environmentConfiguration;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        public PostCodeServices(IHttpClientFactory clientFactory, IHttpClientRepository httpClientRepository, IEnvironmentConfiguration environmentConfiguration, ILogger<PostCodeServices> logger, IMapper mapper)
        {
            client = clientFactory.CreateClient("PostCodesAPI");
            _httpClientRepository = httpClientRepository;
            _environmentConfiguration = environmentConfiguration;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<PostCodeAutoCompleteDataTransferModel> GetPostCodes(string countryCode)
        {
            var url = string.Format(_environmentConfiguration.GetAutoCompleteRoute(), countryCode);
            _logger.LogInformation("GetPostCodes url => " + url);
            string response = await _httpClientRepository.HttpGet(url);
            _logger.LogInformation("GetPostCodes response => " + response);
            PostCodeAutoComplete result = JsonSerializer.Deserialize<PostCodeAutoComplete>(response,
                   new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            PostCodeAutoCompleteDataTransferModel postCodeLookUpDataTransferModel = _mapper.Map<PostCodeAutoCompleteDataTransferModel>(result);


            return postCodeLookUpDataTransferModel;
        }
        public async Task<PostCodeLookUpDataTransferModel> PostCodeLookup(string postCode)
        {
            var url = string.Format(_environmentConfiguration.GetPostCodeLookupRoute(), postCode);
            _logger.LogInformation("PostCodeLookup url => " + url);
            string response = await _httpClientRepository.HttpGet(url);
            _logger.LogInformation("PostCodeLookup response => " + response);
            PostCodeLookUp result = JsonSerializer.Deserialize<PostCodeLookUp>(response,
                new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            PostCodeLookUpDataTransferModel postCodeLookUpDataTransferModel =  _mapper.Map<PostCodeLookUpDataTransferModel>(result);

            return postCodeLookUpDataTransferModel;
        }
    }
}
