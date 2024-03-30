using Microsoft.AspNetCore.Identity;

namespace MRMS.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public DateTime CretedAt { get; set; }
    }
}