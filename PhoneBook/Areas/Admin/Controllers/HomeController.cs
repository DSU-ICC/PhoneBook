using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PhoneBook.Models;
using PhoneBook.ViewModels;

namespace PhoneBook.Areas.Admin.Controllers
{
    [Area("admin")]
    public class HomeController : Controller
    {
        PhoneBookContext db;
        public HomeController(PhoneBookContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Login", "Account");

            AdminViewModel adminViewModel = new AdminViewModel 
            {
                Structures = db.Structures.ToList(),
                Users = db.Users.ToList()
            };

            return View(adminViewModel);
        }
    }
}
