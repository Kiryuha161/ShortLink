using Microsoft.AspNetCore.Mvc;
using ShortLink.Data;
using ShortLink.Models;

namespace ShortLink.Controllers
{
    public class GenerateController : Controller
    {
        private readonly ShortLinkDbContext _database;
        
        private Dictionary<string, string> _links = new Dictionary<string, string>();

        public GenerateController(ShortLinkDbContext database)
        {
            _database = database;
        }
        
        public IActionResult Index()
        {
            return View();
        }

       [HttpPost]
        public IActionResult CreateShortLink(string originalUrl)
        {
            if (string.IsNullOrEmpty(originalUrl))
            {
                return BadRequest("Оригинальная ссылка не должна быть пустой");
            }

            string shortUrl = GenerateShortLink();
            _database.Links.Add(new Link
            {
                OriginalUrl = originalUrl,
                ShortUrl = shortUrl
            });

            _database.SaveChanges();

            return Json(shortUrl);
        }

        [HttpGet]
        public IActionResult GetAllLinks()
        {
            var links = _database.Links.ToList();
            return View(links);
        }
        
        [HttpGet]
        [Route("r/{shortUrl}")]
        public IActionResult RedirectShortLink(string shortUrl)
        {
            var link = _database.Links.FirstOrDefault(l => l.ShortUrl == shortUrl);

            if (link == null)
            {
                return NotFound();
            }

            // Проверить, что запрос происходит по HTTPS
            if (!Request.IsHttps)
            {
                var httpsUrl = $"https://{Request.Host}/r/{shortUrl}";
                return Redirect(httpsUrl);
            }

            return Redirect(link.OriginalUrl);
        }

        private string GenerateShortLink()
        {
            return Guid.NewGuid().ToString().Substring(0, 6);
        }

    }
}
