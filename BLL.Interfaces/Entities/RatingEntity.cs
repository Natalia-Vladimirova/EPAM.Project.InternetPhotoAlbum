namespace BLL.Interfaces.Entities
{
    public class RatingEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PhotoId { get; set; }
        public int UserRate { get; set; }
    }
}
