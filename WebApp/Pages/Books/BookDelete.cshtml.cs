using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryProject.Data.IServices;
using LibraryProject.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp
{
    public class BookDeleteModel : PageModel
    {
        private readonly IBookService bookService;

        public BookDeleteModel(IBookService bookService)
        {
            this.bookService = bookService;
        }
        [BindProperty]
        public Book Book { get; set; }
        public IActionResult OnGet(int id)
        {
            Book = bookService.GetBookById(id);
            if (Book == null)
            {
                return RedirectToPage("NotFound");
            }

            return Page();
        }
        public IActionResult OnPost()
        {
            var temp = bookService.Delete(Book.Id);
            if(temp == null)
            {
                return RedirectToPage("NotFound");
            }

            bookService.Commit();
            TempData["Message"] = "BookDeleted!";

            return RedirectToPage("BookList");
        }
    }
}