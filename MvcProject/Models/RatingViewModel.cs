namespace MvcProject.Models
{
    public class RatingViewModel
    {
        public int Id { get; set; }
        public int UserRate { get; set; }
        public int PhotoId { get; set; }
        public int UserId { get; set; }
        public UserViewModel User { get; set; }
    }
}