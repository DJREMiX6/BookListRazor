using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookListRazor.Controllers
{
    [Route("api/Book")]
    [ApiController]
    public class BookController : Controller
    {

        private readonly ApplicationDbContext _db;

        public BookController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.Book.ToListAsync() });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            Book bookFromDb = await _db.Book.FirstOrDefaultAsync(x => x.Id == id);
            if(bookFromDb == null)
            {
                return Json(new { success = false, message = "Error! Book not found!" });
            }
            _db.Remove(bookFromDb);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Book Deleted succesfully!" });
        }
    }
}
