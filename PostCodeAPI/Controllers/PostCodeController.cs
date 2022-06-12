using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PostCodeAPI.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostCodeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostCodeController : ControllerBase
    {
        IPostCodeServices _iPostCodeServices;
        private readonly ILogger _logger;
        public PostCodeController(IPostCodeServices iPostCodeServices, ILogger<PostCodeController> logger)
        {
            _iPostCodeServices = iPostCodeServices;
            _logger = logger;
        }
        /// <summary>
        /// This API is used to Get Values for Autocomplete Search
        /// </summary>
        /// <param name="searchParams">User input text</param>
        /// <returns></returns>
        [HttpGet("autocomplete/{searchParams}")]
        public async Task<IActionResult> Get(string searchParams)
        {
            _logger.LogInformation("PostCode Autocomplete Called = >" + searchParams);
            var data = await _iPostCodeServices.GetPostCodes(searchParams);
            return Ok(data);
        }

        /// <summary>
        /// This API is used for getting PostCode Details
        /// </summary>
        /// <param name="postCode">User selected text</param>
        /// <returns></returns>
        [HttpGet("{postCode}")]
        public async Task<IActionResult> PostCodeLookup(string postCode)
        {
            _logger.LogInformation("PostCode LookUp Called = >" + postCode);
            var data = await _iPostCodeServices.PostCodeLookup(postCode);
            return Ok(data);
        }
    }
}
