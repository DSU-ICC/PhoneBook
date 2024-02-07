using PhoneBook.Models;

namespace PhoneBook.ViewModels
{
    public class ListCustomerViewModel
    {
        public IEnumerable<Customer> Customers { get; set; }
        public IEnumerable<Structure> Structures { get; set; }
    }
}
