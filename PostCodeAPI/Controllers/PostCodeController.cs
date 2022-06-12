using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public PostCodeController(IPostCodeServices iPostCodeServices)
        {
            _iPostCodeServices = iPostCodeServices;
        }
        [HttpGet("autocomplete/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var data = await _iPostCodeServices.GetPostCodes(id);
            return Ok(data);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> PostCodeLookup(string id)
        {
            var data = await _iPostCodeServices.PostCodeLookup(id);
            return Ok(data);
        }
    }
}
