using PostCodeAPI.Service.Interface;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PostCodeAPI.Service.Impl
{
    public class HttpClientRepository : IHttpClientRepository
    {
        private readonly HttpClient client;
        public HttpClientRepository(IHttpClientFactory clientFactory)
        {
            client = clientFactory.CreateClient("PostCodesAPI");
        }
        public virtual async Task<string> HttpGet(string url)
        {
            var response = await client.GetAsync(url);
            var stringResponse = string.Empty;
            if (response.IsSuccessStatusCode)
            {
                stringResponse = response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }
            return stringResponse;
        }
    }
}
