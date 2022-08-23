namespace BlogMVC.Models
{
    public class Blog
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public string ImagePath { get; set; }
    }
}
