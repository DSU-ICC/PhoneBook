using PhoneBook.Models;

namespace PhoneBook.ViewModels
{
    public class AdminViewModel
    {       
        public IEnumerable<Structure> Structures { get; set; }
        public IEnumerable<User> Users { get; set; }
    }
}
