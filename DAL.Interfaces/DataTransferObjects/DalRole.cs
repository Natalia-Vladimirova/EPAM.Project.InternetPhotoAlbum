namespace DAL.Interfaces.DataTransferObjects
{
    public class DalRole : IEntity
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
    }
}
