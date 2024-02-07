using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PhoneBook.Models;

namespace PhoneBook.Areas.Admin.Controllers
{
    [Area("admin")]
    public class StructureController : Controller
    {
        PhoneBookContext db;
        public StructureController(PhoneBookContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Structure structure)
        {
            db.Structures.Add(structure);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult GetStructures()
        {
            return PartialView(db.Structures.ToList());
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            return View(db.Structures.Find(id));
        }
        [HttpPost]
        public IActionResult Edit(Structure structure)
        {
            Structure structureEdit = db.Structures.Find(structure.Id); 
            structureEdit.Title = structure.Title;
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            return View(db.Structures.Find(id));
        }

        [HttpPost]
        public IActionResult Delete(Structure structure)
        {
            db.Structures.Remove(structure);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
