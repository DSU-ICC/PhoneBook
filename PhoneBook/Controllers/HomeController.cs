using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PhoneBook.Models;
using PhoneBook.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PhoneBook.Controllers
{
    public class HomeController : Controller
    {
        PhoneBookContext db;
        public HomeController(PhoneBookContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            ViewBag.Structures = db.Structures.ToList();

            string returnUrl = Request.Headers["Referer"].ToString();
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }      
        
        public async Task<IActionResult> GetCustomers(int? id, string? text)
        {
            SqlParameter param = new SqlParameter("@text", $"%{text}%");
            List<Customer> allcustomers = db.Customers.Where(p => p.StructureId == id).ToList();
            if (id == null)
            {
                allcustomers = db.Customers.FromSqlRaw("SELECT * FROM Customers WHERE FIO LIKE @text OR Position LIKE @text OR ATS LIKE @text OR CTS LIKE @text", param).ToList();
            }
            foreach (Customer customer in allcustomers)
            {
                customer.Structure = db.Structures.FirstOrDefault(p => customer.StructureId == p.Id);
            }
                      
            return PartialView(allcustomers);
        }
    }
}