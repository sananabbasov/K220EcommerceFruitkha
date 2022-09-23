using K220EcommerceFruitkha.Models;

namespace K220EcommerceFruitkha.Areas.Dashboard.ViewModels
{
    public class BlogVM
    {
        public List<Blog> Blogs { get; set; }
        public List<Tag> Tags { get; set; }
        public List<BlogTag> BlogTags { get; set; }
    }
}
