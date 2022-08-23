using BlogMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace BlogMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            AdminPageView values = new AdminPageView();
            values.blogValues = _context.Blogs.ToList();
            values.categoryValues = _context.Categories.ToList();
            return View(values);
        }

        
        public IActionResult ShowCategoryBlogs(int id)
        {
            var categoryBlogs = _context.Blogs.Where(x => x.CategoryId==id).ToList();
            return View(categoryBlogs);
        }
        
        public IActionResult ContinueReadingBlog(int id)
        { 
            var readBlog = _context.Blogs.Where(x => x.ID == id).ToList();
            return View(readBlog);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult ContactUs()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}