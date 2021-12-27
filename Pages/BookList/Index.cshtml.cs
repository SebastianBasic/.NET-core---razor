using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace _NET_core___razor.Pages.BookList
{

    public class IndexModel : PageModel
    {
        private readonly MariaDbContext _db;
        public IEnumerable<Book> Books { get; set; }

        public IndexModel(MariaDbContext db)
        {
            _db = db;
        }

        public async Task OnGet()
        {
            Books = await _db.Book.ToListAsync();
        }

        public async Task<IActionResult> OnDelete(int id)
        {
            var book = await _db.Book.FindAsync(id);

            if(book == null){
                return NotFound();
            }
            
            _db.Book.Remove(book);
            await _db.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    
    }

}