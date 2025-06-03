namespace MVCDatabaseDemo.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateUpdated { get; set; }
    }

}
