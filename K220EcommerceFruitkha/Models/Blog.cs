
namespace K220EcommerceFruitkha.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string PhotoUrl { get; set; }
        public int Hit { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public string UserId { get; set; }
        public K220User User { get; set; }
        public virtual List<Comment> Comments { get; set; }
    }
}
