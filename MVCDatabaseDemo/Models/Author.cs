using System.ComponentModel.DataAnnotations;

namespace MVCDatabaseDemo.Models
{
    public class Author : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public IEnumerable<Book> Books { get; set; } = default;
    }
}
