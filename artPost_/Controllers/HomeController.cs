using artPost_.Data;
using artPost_.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace artPost_.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<HomeController> _logger;
        MemoryStream memoryStream = new MemoryStream(); 

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
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
        public async Task<IActionResult> createPost(string title, string description, IFormFile Image )
        {
            if(title == null || description == null || Image == null)
            {
                return View();
            }

            byte [] byteImage; 

            if(Image.Length > 0)
            {
                byte[] p1 = null;
                using (var fs1 = Image.OpenReadStream()) 
                using (var ms1 = new MemoryStream())
                {
                    fs1.CopyTo(ms1);
                    p1 = ms1.ToArray();
                }
                byteImage = p1;
            }
            else
            {
                return View();
            }

            Debug.WriteLine(title + " " + description + " " + Image);
            using var transaction = _db.Database.BeginTransaction();

            var POST = new Image
            {
                Title = title,
                Description = description,
                image = byteImage,
                ownerId = User.Identity.Name
            };
            _db.image.Add(POST);
            _db.SaveChanges();
            transaction.Commit();

             return RedirectToAction("Profile");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}