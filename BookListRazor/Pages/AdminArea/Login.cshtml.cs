using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Pages.AdminArea
{
    public class LoginModel : PageModel
    {

        private readonly ApplicationDbContext _db;

        public LoginModel(ApplicationDbContext db)
        {
            _db = db;
        }
        
        [BindProperty]
        public User User { get; set; }

        public async Task<IActionResult> OnGet(string username, int? userId)
        {
            User userFromDb;
            if(userId != null)
            {
                userFromDb = await _db.User.FirstOrDefaultAsync(x => x.Id == userId);
            }
            else
            {
                if(username != null)
                {
                    userFromDb = await _db.User.FirstOrDefaultAsync(x => x.Username == username);
                }
                else
                {
                    return Page();
                }
            }
            if(userFromDb != null)
            {
                if(userFromDb.IsLoggedIn)
                {
                    return RedirectToPage("Index", new { userId = userFromDb.Id });
                }
                else
                {
                    User = userFromDb;
                }
            }
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if(ModelState.IsValid)
            {
                User userFromDb = await _db.User.FirstOrDefaultAsync(x => x.Username == User.Username && x.Password == User.Password);
                if(userFromDb == null)
                {
                    return Page();
                }
                else
                {
                    userFromDb.IsLoggedIn = true;
                    _db.User.Update(userFromDb);
                    await _db.SaveChangesAsync();
                    return RedirectToPage("Index", new { userId = userFromDb.Id });
                }
            }
            else
            {
                return Page();
            }
        }
    }
}
