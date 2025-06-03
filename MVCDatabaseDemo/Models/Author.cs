namespace MVCDatabaseDemo.Models
{
    public class Author : BaseEntity
    {
        public string Name { get; set; }
        public IEnumerable<Book> Books { get; set; } = default;
    }
}
