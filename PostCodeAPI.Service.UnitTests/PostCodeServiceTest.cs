using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using PostCodeAPI.Common.Impl;
using PostCodeAPI.Common.Interface;
using PostCodeAPI.Model;
using PostCodeAPI.Service.Impl;
using PostCodeAPI.Service.Interface;
using System.Net.Http;
using System.Threading.Tasks;

namespace PostCodeAPI.Service.UnitTests
{
    public class PostCodeServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task TestGetPostCodes()
        {
            #region Mocking Dependancies
            var httpClientFactory = new Mock<IHttpClientFactory>();
            var httpClientRepository = new Mock<IHttpClientRepository>();
            var logger = new Mock<ILogger<PostCodeServices>>();
            var environmentConfiguration = new Mock<IEnvironmentConfiguration>();
            #endregion

            httpClientRepository.Setup(p => p.HttpGet(It.IsAny<string>())).ReturnsAsync(HttpGetautocompleteProxy());
            environmentConfiguration.Setup(p => p.GetAutoCompleteRoute()).Returns("/postcodes/{0}/autocomplete");
            Mock<PostCodeServices> postCodeServices = new Mock<PostCodeServices>(httpClientFactory.Object, httpClientRepository.Object, environmentConfiguration.Object, logger.Object);
            PostCodeAutoComplete data = await postCodeServices.Object.GetPostCodes("PA");

            #region Tests
            Assert.AreEqual(10, data.result.Count);
            Assert.AreEqual(true, data.result.Contains("PA10 2AF"));
            Assert.AreEqual(false, data.result.Contains("DA10 8FS"));
            #endregion
        }

        [Test]
        public async Task TestPostCodeLookup()
        {
            #region Mocking Dependancies
            var httpClientFactory = new Mock<IHttpClientFactory>();
            var httpClientRepository = new Mock<IHttpClientRepository>();
            var logger = new Mock<ILogger<PostCodeServices>>();
            var environmentConfiguration = new Mock<IEnvironmentConfiguration>();
            #endregion

            httpClientRepository.Setup(p => p.HttpGet(It.IsAny<string>())).ReturnsAsync(HttpGetpostcodesProxy());
            environmentConfiguration.Setup(p => p.GetPostCodeLookupRoute()).Returns("/postcodes/{0}");
            Mock<PostCodeServices> postCodeServices = new Mock<PostCodeServices>(httpClientFactory.Object, httpClientRepository.Object, environmentConfiguration.Object, logger.Object);
            PostCodeLookUp data = await postCodeServices.Object.PostCodeLookup("CM8 1EF");

            #region Tests
            Assert.AreEqual("England", data.result.country);
            Assert.AreEqual("East of England", data.result.region);
            Assert.AreEqual("South", data.result.area);
            Assert.AreEqual("E07000067", data.result.codes.admin_district);
            Assert.AreEqual("E14001045", data.result.codes.parliamentary_constituency);
            #endregion
        }

        #region  Proxy Mocked Methods
        private string HttpGetautocompleteProxy()
        {
            return "{\"result\":[\"PA10 2AB\",\"PA10 2AD\",\"PA10 2AE\",\"PA10 2AF\",\"PA10 2AG\",\"PA10 2AH\",\"PA10 2AJ\",\"PA10 2AL\",\"PA10 2AN\",\"PA10 2AP\"]}";
        }
        private string HttpGetpostcodesProxy()
        {
            return "{\"status\":200,\"result\":{\"postcode\":\"CM8 1EF\",\"quality\":1,\"eastings\":581459,\"northings\":213679,\"country\":\"England\",\"nhs_ha\":\"East of England\",\"longitude\":0.629806,\"latitude\":51.792326,\"european_electoral_region\":\"Eastern\",\"primary_care_trust\":\"Mid Essex\",\"region\":\"East of England\",\"lsoa\":\"Braintree 017F\",\"msoa\":\"Braintree 017\",\"incode\":\"1EF\",\"outcode\":\"CM8\",\"parliamentary_constituency\":\"Witham\",\"admin_district\":\"Braintree\",\"parish\":\"Witham\",\"admin_county\":\"Essex\",\"admin_ward\":\"Witham South\",\"ced\":\"Witham Southern\",\"ccg\":\"NHS Mid Essex\",\"nuts\":\"Essex Haven Gateway\",\"codes\":{\"admin_district\":\"E07000067\",\"admin_county\":\"E10000012\",\"admin_ward\":\"E05010388\",\"parish\":\"E04012935\",\"parliamentary_constituency\":\"E14001045\",\"ccg\":\"E38000106\",\"ccg_id\":\"06Q\",\"ced\":\"E58000470\",\"nuts\":\"TLH34\",\"lsoa\":\"E01033460\",\"msoa\":\"E02004462\",\"lau2\":\"E07000067\"}}}";
        }
        #endregion
    }
}