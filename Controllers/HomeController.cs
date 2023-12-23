using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using startup.Models;
using System.Diagnostics;

namespace startup.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _context;

        public HomeController(ILogger<HomeController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("/{slug}.html")]
        public IActionResult PrDetails(string slug)
        {
            if (slug == null) return NotFound();

            var p = _context.products.FirstOrDefault(x => x.Slug == slug);
            return View(p);
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [Route("/post-{slug}-{id:long}.html", Name = "Details")]
        [HttpGet]
        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound(id);
            }
            var p = _context.Posts.Find(id);

            Post post = _context.Posts
                .FirstOrDefault(m => (m.PostID ==id) && (m.IsActive == true));
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        [Route("/list-{slug}-{id:int}.html", Name = "List")]
        [HttpGet]
        public IActionResult List(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var list = _context.PostMenus
                .Where(m =>  (m.IsActive == true))
                .Take(6).ToList();
            if (list == null)
            {
                return NotFound();
            }
            return View(list);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet("/Blog/post")]
        public IActionResult Blog()
        {
            return View();
        }
    }
}