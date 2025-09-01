using System.Diagnostics;
using System.Drawing.Imaging;
using System.Drawing;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Paint_Hub_web.Models;

namespace Paint_Hub_web.Controllers
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

        public IActionResult Auto()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Auto(string repoUrl)
        {

            string RandomName = Guid.NewGuid().ToString().Replace("-", "");

            var listInfo = GetRepoInfo(repoUrl);

            string ImageAvatar = SaveImage(listInfo.AvatarUrl, $"{RandomName}.png");
            using (Bitmap card = CardGenerator.CreateCard(
            ImageAvatar,
            listInfo.Name,
                listInfo.OwnerName,
                listInfo.Description,
                listInfo.Stars,
                listInfo.Forks,
                listInfo.Issues,
                listInfo.Language))
            {
                card.Save($"wwwroot/ImagesResult/Card-{RandomName}.png", ImageFormat.Png);

                ViewBag.ResultImage = $"{RandomName}.png";
            }

            System.IO.File.Delete($"wwwroot/Images/{RandomName}.png");

            return View("Result");
        }

        public IActionResult Manual()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Manual(string repoUrl,
            string bgColor,
            string titleColor,
            string ownerColor,
            string descColor,
            string statColor)
        {

            string RandomName = Guid.NewGuid().ToString().Replace("-", "");

            var listInfo = GetRepoInfo(repoUrl);

            string ImageAvatar = SaveImage(listInfo.AvatarUrl, $"{RandomName}.png");
            using (Bitmap card = CardGenerator.CreateCard(
            ImageAvatar,
            listInfo.Name,
                listInfo.OwnerName,
                listInfo.Description,
                listInfo.Stars,
                listInfo.Forks,
                listInfo.Issues,
                listInfo.Language,
                ColorTranslator.FromHtml(titleColor),
                ColorTranslator.FromHtml(ownerColor),
                ColorTranslator.FromHtml(descColor),
                ColorTranslator.FromHtml(bgColor),
                ColorTranslator.FromHtml(statColor)))
            {
                card.Save($"wwwroot/ImagesResult/Card-{RandomName}.png", ImageFormat.Png);

                ViewBag.ResultImage = $"{RandomName}.png";
            }

            //System.IO.File.Delete($"wwwroot/Images/{RandomName}.png");

            return View("Result");
        }

        public IActionResult Result()
        {
            return View();
        }

        ListCard GetRepoInfo(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    url = url.ToLower().Replace("github.com", "api.github.com/repos");

                    client.DefaultRequestHeaders.Add("User-Agent", "CSharpApp");
                    HttpResponseMessage response = client.GetAsync(url).Result;
                    response.EnsureSuccessStatusCode();

                    string json = response.Content.ReadAsStringAsync().Result;

                    using (JsonDocument doc = JsonDocument.Parse(json))
                    {
                        var root = doc.RootElement;

                        string name = root.GetProperty("name").GetString();
                        string description = root.GetProperty("description").GetString();
                        string language = root.GetProperty("language").GetString();
                        int stars = root.GetProperty("stargazers_count").GetInt32();
                        int forks = root.GetProperty("forks_count").GetInt32();
                        int issues = root.GetProperty("open_issues_count").GetInt32();

                        // اینجا باید دو مرحله‌ای بری
                        string ownerName = root.GetProperty("owner").GetProperty("login").GetString();
                        string avatarUrl = root.GetProperty("owner").GetProperty("avatar_url").GetString();

                        if (description.Length > 50)
                        {
                            description = description.Substring(0, 50) + " ...";
                        }


                        ListCard listCard = new ListCard()
                        {
                            Name = name,
                            Description = description,
                            Language = language,
                            Stars = stars,
                            Forks = forks,
                            Issues = issues,
                            OwnerName = ownerName,
                            AvatarUrl = avatarUrl
                        };

                        return listCard;
                    }
                }
                catch (Exception ex)
                {
                    ListCard listCard = new ListCard()
                    {
                        ErrorMessage = ex.Message,
                    };

                    return listCard;
                }
            }
        }

        string SaveImage(string imageUrl, string fileName)
        {
            // مسیر پوشه bin پروژه
            string savePath = Path.Combine("wwwroot/Images", fileName);

            using (WebClient client = new WebClient())
            {
                client.DownloadFile(imageUrl, savePath);

                return savePath;
            }
        }

        class ListCard
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Language { get; set; }
            public int Stars { get; set; }
            public int Forks { get; set; }
            public int Issues { get; set; }
            public string OwnerName { get; set; }
            public string AvatarUrl { get; set; }
            public string? ErrorMessage { get; set; }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
