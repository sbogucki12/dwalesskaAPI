using Microsoft.AspNetCore.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace dwalesskaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly string _SendGridAPI;
        public EmailController(string SendGridAPI)
        {
            _SendGridAPI = SendGridAPI;
        }
        // GET: api/<EmailController>
        [HttpGet]
        public async Task<Response> Get()
        {

            //GO HERE: https://ilessrg.medium.com/send-emails-using-sendgrid-with-azure-in-net-core-6282c61b4041
            //Start at SendEmailService.cs

            string sendGridAPI = _SendGridAPI;  

            var client = new SendGridClient(sendGridAPI);
            var from = new EmailAddress("test@example.com", "Example User");
            var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress("sbogucki@mail.usf.edu", "Example User");
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);

            return response; 
        }

        // GET api/<EmailController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<EmailController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<EmailController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EmailController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
