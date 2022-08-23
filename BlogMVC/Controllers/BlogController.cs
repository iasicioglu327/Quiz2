using BlogMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.FileProviders;

namespace BlogMVC.Controllers
{
    public class BlogController : Controller
    {
    
        private readonly AppDbContext _context;
        private readonly IFileProvider _fileProvider;
        
        public BlogController(AppDbContext context, IFileProvider fileProvider)
        {
            _fileProvider= fileProvider;
            _context = context;
        }
        public IActionResult Admin()
        {
            AdminPageView values = new AdminPageView();
            values.blogValues = _context.Blogs.ToList();
            values.categoryValues = _context.Categories.ToList();
            return View(values);
        }

        public IActionResult AddCategory(Category category)
        {
            _context.Add(category);
            _context.SaveChanges();
            return RedirectToAction(nameof(Admin));
        }
        public IActionResult AddBlog(Blog blog, IFormFile image)
        {
            if (image != null && image.Length > 0)
            {
                var root = _fileProvider.GetDirectoryContents("wwwroot");
                var picturesDirectory = root.Single(x => x.Name == "pictures");
                var path = Path.Combine(picturesDirectory.PhysicalPath, image.FileName);
                using var stream = new FileStream(path, FileMode.Create);
                image.CopyToAsync(stream);
                blog.ImagePath = image.FileName;
            }
            _context.Add(blog);
            _context.SaveChanges();
            return RedirectToAction(nameof(Admin));
        }
        public IActionResult DeleteCategory(int id)
        {
            var delCategory = _context.Categories.Find(id);
            _context.Categories.Remove(delCategory);
            _context.SaveChanges();
            return RedirectToAction(nameof(Admin));
        }

        public IActionResult UpdateCategoryPage(int id)
        {
            var updCategory = _context.Categories.Find(id);
            Category c =new Category();
            c.CategoryName = updCategory.CategoryName;
            c.ID=updCategory.ID;
            return View(c);
        }

        [HttpPost]
        public IActionResult UpdateCategory(Category category)
        {
            var updCategory = _context.Categories.Find(category.ID);
            updCategory.CategoryName = category.CategoryName;
            _context.Update(updCategory);
            _context.SaveChanges();
            return RedirectToAction(nameof(Admin));
        }

        public IActionResult DeleteBlog(int id)
        {
            var delBlog= _context.Blogs.Find(id);
            _context.Blogs.Remove(delBlog);
            _context.SaveChanges();
            return RedirectToAction(nameof(Admin));
        }
       
        public IActionResult UpdateBlogPage(int id)
        {
            var categoryList = _context.Categories;
            ViewBag.selectList=new SelectList(categoryList,"ID","CategoryName");
            var updBlog = _context.Blogs.Find(id);
            Blog b = new Blog() { ID=updBlog.ID, Title=updBlog.Title, Content=updBlog.Content, CategoryId=updBlog.CategoryId, ImagePath=updBlog.ImagePath, Category=updBlog.Category};
            return View(b);
        }

        [HttpPost]
        public IActionResult UpdateBlog(Blog blog, IFormFile image)
        {
            if (image != null && image.Length > 0)
            {
                var root = _fileProvider.GetDirectoryContents("wwwroot");
                var picturesDirectory = root.Single(x => x.Name == "pictures");
                var path = Path.Combine(picturesDirectory.PhysicalPath, image.FileName);
                using var stream = new FileStream(path, FileMode.Create);
                image.CopyToAsync(stream);
                blog.ImagePath = image.FileName;
            }
            var updBlog = _context.Blogs.Find(blog.ID);
            updBlog.Title = blog.Title;
            updBlog.Content = blog.Content;
            updBlog.CategoryId = blog.CategoryId;
            updBlog.ImagePath= blog.ImagePath;
            _context.Update(updBlog);
            _context.SaveChanges();
            return RedirectToAction(nameof(Admin));
        }

       
    }
}

