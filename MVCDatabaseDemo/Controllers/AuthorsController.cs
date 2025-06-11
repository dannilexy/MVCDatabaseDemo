using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCDatabaseDemo.Data;
using MVCDatabaseDemo.Models;

namespace MVCDatabaseDemo.Controllers
{
    public class AuthorsController(BookStoreContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var authors = await _context.Authors.Include(x => x.Books).ToListAsync();
            return View(authors);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Author author)
        {

            var authorExists = await _context.Authors.AnyAsync(x => x.Name == author.Name);
            if (authorExists)
            {
                ModelState.AddModelError("Name", "Author with name already exists.");
                ViewBag.ErrorMessage = "Author with name already exists.";
                return View(author);
            }

            if (ModelState.IsValid)
            {
                await _context.Authors.AddAsync(author);
                //await _context.AddAsync(author);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.ErrorMessage = "Invalid data. Please check the form.";


            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            var authors = await _context.Authors.Include(x => x.Books).FirstOrDefaultAsync(x => x.Id == id);
            return View(authors);
        }
    }
}
