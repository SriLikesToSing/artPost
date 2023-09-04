using artPost.Models;
using artPost_.Data;
using artPost_.Models;
using Humanizer.Localisation.TimeToClockNotation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Reflection;
using System.Text.Json;

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

        private readonly JsonSerializerOptions _options = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
        };

        public async Task<IActionResult> Index()
        {
            return View(_db.user.ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var specificUser = await _db.user.FirstOrDefaultAsync(x => x.userName == User.Identity.Name);
            Debug.WriteLine(specificUser);
            if(specificUser == null)
            {
                using var transaction = _db.Database.BeginTransaction();
                Random rnd = new Random();
                var tempArray = new List<Image>();

                var POST = new Image
                    {
                    Title = "dummy",
                    Description = "dummy desc",
                    image = new byte[] {0x00}, 
                    ownerId = User.Identity.Name
                   };

                tempArray.Add(POST);
                var newUser = new user
                {
                    userName = User.Identity.Name,
                    followers = 0,
                    description = "",
                    profilePic = Array.Empty<byte>(),
                    imagesJsonString = JsonConvert.SerializeObject(tempArray),
                    images = new List<Image>()
                };

                _db.user.Add(newUser);
                _db.SaveChanges();
                transaction.Commit();
                specificUser = await _db.user.FirstOrDefaultAsync(x => x.userName == User.Identity.Name);
            }
            return View(specificUser);
        }

        [AllowAnonymous]
        public IActionResult viewOtherProfile()
        {
            return View(_db.user.ToList());
        }

        [Authorize]
        public IActionResult createProfile()
        {
            return View();
        }

        [Authorize]
        public IActionResult createPost()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> createProfile(string profileDescription, IFormFile profilePostImage)
        {
            if(profileDescription == null && profilePostImage == null) {
                return View(); 
            }

            byte [] byteImage = Array.Empty<byte>();

            bool isImage = false;

            if(profilePostImage != null)
            {

            if(profilePostImage.Length > 0)
            {
                byte[] p1 = null;
                using (var fs1 = profilePostImage.OpenReadStream()) 
                using (var ms1 = new MemoryStream())
                {
                    fs1.CopyTo(ms1);
                    p1 = ms1.ToArray();
                }
                byteImage = p1;
                isImage = true;
            }
            else
            {
                return View();
            }
           }

            using var transaction = _db.Database.BeginTransaction();
            foreach(var item in _db.user)
            {
                if(item.userName == User.Identity.Name)
                {
                    item.description = profileDescription;

                    if(isImage == true)
                    {
                        item.profilePic = byteImage;
                    }

                }
            }

            _db.SaveChanges();
            transaction.Commit();

            return RedirectToAction("Profile");
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
            Guid guid = Guid.NewGuid();

            var POST = new Image
            {
                Title = title,
                Description = description,
                image = byteImage,
                ownerId = User.Identity.Name,
                likeCount = JsonConvert.SerializeObject(new List<string> { User.Identity.Name }),
                likes = 1,
                iID = guid.ToString()
            };
            
            foreach(var item in _db.user)
            {
                Console.WriteLine(item);
                if(item.userName == User.Identity.Name)
                {
                    item.images.Add(POST);
                    var deserialized = JsonConvert.DeserializeObject<List<Image>>(item.imagesJsonString);
                    deserialized.Add(POST);
                    item.imagesJsonString = JsonConvert.SerializeObject(deserialized);
                    Console.WriteLine(item.images);
                }
            }

            _db.image.Add(POST);
            _db.SaveChanges();
            transaction.Commit();

             return RedirectToAction("Profile");
        }

        [Authorize]
        public async Task<IActionResult> check(string name, string image)
        {

            if(name == User.Identity.Name)
            {
                return Redirect("Home/Profile");
            }

            using var transaction = _db.Database.BeginTransaction();

            var specificUser = await _db.user.FirstOrDefaultAsync(x => x.userName == name);

            List<Image> imageData = JsonConvert.DeserializeObject<List<Image>>(specificUser.imagesJsonString);

            for(int x=0; x<imageData.Count; x++)
            {
                if (imageData[x].iID == image && imageData[x].likeCount != null)
                {
                    List<string> likeList = JsonConvert.DeserializeObject<List<string>>(imageData[x].likeCount);
                    likeList.Add(User.Identity.Name);
                    imageData[x].likeCount = JsonConvert.SerializeObject(likeList.Distinct().ToList());
                    imageData[x].likes = likeList.Distinct().ToList().Count;
                }
            }
            specificUser.imagesJsonString = JsonConvert.SerializeObject(imageData);

            _db.SaveChanges();
            transaction.Commit();

            return RedirectToAction("viewOtherProfile", new { value = name });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}