using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace dwalesskaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerifyController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;        
        private readonly string _RecaptchaKey;
        public VerifyController(IHttpClientFactory clientFactory, string RecaptchaKey)
        {
            _clientFactory = clientFactory;
            _RecaptchaKey = RecaptchaKey;
        }

        //POST api/verify        
        [HttpPost]
        public async Task<IActionResult> Post([FromQuery] string token)
        {
            string recaptchaKey = _RecaptchaKey;            
            if(token.Length < 1)
            {
                return NotFound("Missing Recaptcha token.");
            }
            
            var url = "https://www.google.com/recaptcha/api/siteverify";
            var parameters = new Dictionary<string, string> { { "secret", recaptchaKey }, { "response", token } };
            var encodedContent = new FormUrlEncodedContent(parameters);

            var client = _clientFactory.CreateClient();           

            var response = await client.PostAsync(url, encodedContent).ConfigureAwait(false);
            
            if (response.StatusCode == HttpStatusCode.OK)
            {                
                var responseContent = await response.Content.ReadAsStringAsync ().ConfigureAwait (false);                            
                return Ok(responseContent);
            }            
            
            return BadRequest(response.StatusCode);
        }

        // *** BOILERPLATE FROM FRAMEWORK BELOW *** 

        // GET: api/<VerifyController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<VerifyController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // PUT api/<VerifyController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<VerifyController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
