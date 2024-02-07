using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PhoneBook.Models;

namespace PhoneBook.Areas.Admin.Controllers
{
    [Area("admin")]
    public class CustomerController : Controller
    {
        PhoneBookContext db;
        public CustomerController(PhoneBookContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View(db.Structures.ToList());
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

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Structures = new SelectList(db.Structures.ToList(), "Id", "Title");
            ViewBag.StructureId = db.Structures.FirstOrDefault().Id;
            ViewBag.StructureTitle = db.Structures.FirstOrDefault().Title;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            customer.Structure = db.Structures.FirstOrDefault(p => p.Id == customer.StructureId);
            db.Customers.Add(customer);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null) return RedirectToAction("Index");

            ViewBag.Structures = new SelectList(db.Structures.ToList(), "Id", "Title");

            return View(db.Customers.Find(id));
        }

        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            Customer customerEdit = db.Customers.Find(customer.Id);
            customerEdit.Position = customer.Position;
            customerEdit.FIO = customer.FIO;
            customerEdit.ATS = customer.ATS;
            customerEdit.CTS = customer.CTS;
            customerEdit.StructureId = customer.StructureId;
            customerEdit.Structure = db.Structures.Find(customer.StructureId);

            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            return View(db.Customers.Find(id));
        }
        [HttpPost]
        public IActionResult Delete(Customer customer)
        {
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
