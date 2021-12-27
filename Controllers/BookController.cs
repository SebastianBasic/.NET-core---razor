using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _NET_core___razor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Controllers
{
    [Route("api/Book")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly MariaDbContext _db;

        public BookController(MariaDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Json(new {data = await _db.Book.ToListAsync()});
        }

        [HttpDelete]
        public async Task<IActionResult> Destroy(int id)
        {
            var bookFromDb = await _db.Book.FirstOrDefaultAsync(u => u.id == id);

            if(bookFromDb == null){
                return Json(new {success = false, message="Error while Deleting"});
            }
            _db.Book.Remove(bookFromDb);
            await _db.SaveChangesAsync();
            
            return Json(new {success = true, message = "Delete successful"});
        }
    }
}