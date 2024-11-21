using BestStoreMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace BestStoreMVC.Controllers
{
    public partial class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        /// <summary>
        /// news API
        /// </summary>
        /// <returns></returns>

        public async Task<IActionResult> News()
        {

            Root root = new Root();
            HttpClient client = new HttpClient();
            HttpResponseMessage message = await client.GetAsync("https://newsapi.org/v2/everything?q=tesla&from=2024-10-15&sortBy=publishedAt&apiKey=380d97d4ee3344da84c022ffb2bc66d9");

            if (message.IsSuccessStatusCode)
            {
                var jstring = await message.Content.ReadAsStringAsync();
               root = JsonConverter.DeserializeObject <Root>(jstring);
                return View(root);
            }
            else
            {
                return View(root);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
