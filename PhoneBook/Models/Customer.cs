using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneBook.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string? Position { get; set; }
        public string? FIO { get; set; }      
        public string? ATS { get; set; }
        public string? CTS { get; set; }
        public int StructureId { get; set; }
        public Structure Structure { get; set; }

    }
}
