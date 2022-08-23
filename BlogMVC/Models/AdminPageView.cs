
namespace BlogMVC.Models
{
    public class AdminPageView
    {
        public IEnumerable<Blog> blogValues { get; set; }
        public IEnumerable<Category> categoryValues { get; set; }
    }
}
