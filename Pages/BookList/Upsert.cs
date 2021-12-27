using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace _NET_core___razor.Pages.BookList
{

    public class UpsertModel : PageModel
    {
        private readonly MariaDbContext _db;

        [BindProperty]        
        public Book Book { get; set; }

        public UpsertModel(MariaDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> OnGet(int? id)
        {
            Book = new Book();

            if(id == null){
                return Page();
            }else{
                Book = await _db.Book.FirstOrDefaultAsync(u => u.id == id);

                if(Book == null) {
                    return NotFound();
                }else{
                    return Page();
                }
            }
        }
        

        public async Task<IActionResult> OnPost()
        {
            if(ModelState.IsValid)
            {
                if(Book.id == 0)
                {
                    _db.Book.Add(Book);
                }else{
                    _db.Book.Update(Book);
                }

                await _db.SaveChangesAsync();

                return RedirectToPage("Index");
            }else{
                return RedirectToPage();
            }
        }
        
    
    }

}