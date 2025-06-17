using Microsoft.AspNetCore.Identity;

namespace MVCDatabaseDemo.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Occupation { get; set; }
        public string MarritalStatus { get; set; }
    }
}
