using dwalesskaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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

        private readonly IConfiguration _configuration; 

        public EmailController(IConfiguration Configuration)
        {
            _configuration = Configuration;            
        }
        // GET: api/<EmailController>
        [HttpGet]
        public async Task<Response> Get([FromBody] Email email)
        {

            
            string sendGridAPI = _configuration["SendGridAPI"];
            string sendGridEmail = _configuration["SendGridEmail"];

            var client = new SendGridClient(sendGridAPI);
            var from = new EmailAddress(sendGridEmail, email.VisitorName);
            var subject = "[From website.com] " + email.VisitorComment.Substring(0,20) + "...";
            var to = new EmailAddress(sendGridEmail, "Recipient");
            var content = email.VisitorComment + " \n " + "From: " + email.VisitorEmail; 
            var msg = MailHelper.CreateSingleEmail(from, to, subject, content, null);
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
