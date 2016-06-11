namespace DAL.Interfaces.DataTransferObjects
{
    public class DalRating : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PhotoId { get; set; }
        public int UserRate { get; set; }
    }
}
