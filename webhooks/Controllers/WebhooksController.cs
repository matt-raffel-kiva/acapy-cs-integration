using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using connect.webhooks.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace connect.webhooks.Controllers
{
    [Route("v1/controller")]
    public class WebhooksController : Controller
    {
        private readonly ILogger<WebhooksController> _logger;

        public WebhooksController(ILogger<WebhooksController> logger)
        {
            _logger = logger;
        }

        // GET: /<controller>/
        public ActionResult<string> Index()
        {
            return "WebhooksController/index";
        }

        [HttpPost("{agentId}/{topic}/{subtopic}")]
        public async Task<string> AcapyCallback(string agentId, string topic, string subtopic)
        {
            string bodyText = "{}";
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                bodyText = await reader.ReadToEndAsync();
            }

            _logger.LogCritical($"**************\r\nv1/controller/{agentId}/{topic}/{subtopic} - {bodyText}\r\n****************");

            return "Ok";
        }

        [HttpPost("{topic}/{subtopic}")]
        public async Task<string> AcapyCallback(string topic, string subtopic)
        {
            string bodyText = "{}";
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                bodyText = await reader.ReadToEndAsync();
            }

            _logger.LogCritical($"**************\r\nv1/controller/{topic}/{subtopic} - {bodyText}\r\n****************");

            return "Ok";
        }
    }
}
