using System;

namespace DAL.Interfaces.DataTransferObjects
{
    public class DalPhoto : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public int TotalRate { get; set; }
        public DateTime CreationDate { get; set; }
        public int UserId { get; set; }
    }
}
