using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    [Route("kafka")]
    public class KafkaController : Controller
    {
        private readonly IKafkaService _kafkaService;

        public KafkaController(IKafkaService kafkaService)
        {
            _kafkaService = kafkaService;
        }

        // GET: /kafka
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
          
            var messages = await _kafkaService.GetMessagesAsync();
            if (messages == null)
            {
                messages = new List<string>();
            }

            return View(messages);
        }

        // POST: /kafka/send
        [HttpPost("send")]
        public async Task<IActionResult> SendMessage(string message)
        {
          
            if (!string.IsNullOrEmpty(message))
            {
               
                await _kafkaService.SendMessageAsync("web-message", message);

              
                TempData["Status"] = "Message sent successfully!";
            }
            else
            {
               
                TempData["Status"] = "Message cannot be empty.";
            }

          
            return RedirectToAction("Index");
        }
    }
}
