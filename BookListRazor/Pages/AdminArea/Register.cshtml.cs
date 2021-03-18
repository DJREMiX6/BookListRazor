using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookListRazor.Pages.AdminArea
{
    public class RegisterModel : PageModel
    {

        private readonly ApplicationDbContext _db;

        public RegisterModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public new User User { get; set; }

        public async Task<IActionResult> OnPost()
        {
            if(ModelState.IsValid)
            {
                await _db.User.AddAsync(User);
                await _db.SaveChangesAsync();
                return RedirectToPage("Login", new {username = User.Username});
            }
            else
            {
                return Page();
            }
        }
    }
}
