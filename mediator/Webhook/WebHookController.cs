using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace connect.mediator.Webhook
{
    [Route("v1/controller")]
    public class WebHookController : Controller
    {
        private readonly ILogger<WebHookController> _logger;

        public WebHookController(ILogger<WebHookController> logger)
        {
            _logger = logger;
        }

        // GET: /<controller>/
        public ActionResult<string> Index()
        {
            return "WebHookController/index";
        }

        [HttpPost("{agentId}/{topic}/{subtopic}")]
        public async Task<string> AcapyCallback(string agentId, string topic, string subtopic)
        {
            string bodyText = "{}";
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                bodyText = await reader.ReadToEndAsync();
            }

            WebHookModel data = JsonConvert.DeserializeObject<WebHookModel>(bodyText);
            _logger.LogCritical($"\r\nAgent Id: {agentId} / topic : {topic} / subtopic : {subtopic}{data.ToString()}\r\n{bodyText}");


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
