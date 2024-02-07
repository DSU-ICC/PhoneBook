namespace PhoneBook.Models
{
    public class Structure
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public List<Customer> Customers { get; set; }
    }
}
