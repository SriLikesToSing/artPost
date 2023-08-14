using artPost_.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace artPost_.Controllers
{
    public class HomeController : Controller
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

        [Authorize]
        public IActionResult Profile()
        {
            return View(new artPost.Models.user());

        }

        [Authorize]
        public IActionResult createPost()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> createPost(string title, string description, IFormFile postImage)
        {

            if(title == null || description == null || postImage == null)
            {
                return View();

            }

            //create new "Image" and store it in db.
            //create new table Image with specific ID's
            //store that image inside the user with the specific userId

             return RedirectToAction("Profile");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}