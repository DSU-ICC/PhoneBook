using Microsoft.AspNetCore.Identity;

namespace PhoneBook.Models
{
    public class User : IdentityUser
    {
        public string FIO { get; set; }
    }
}
