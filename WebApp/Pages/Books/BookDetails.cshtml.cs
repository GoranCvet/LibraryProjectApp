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
    public class BookDetailsModel : PageModel
    {
        private readonly IBookService bookService;

        public BookDetailsModel(IBookService bookService)
        {
            this.bookService = bookService;
        }
        public Book Book { get; set; }
        [TempData]
        public string Message { get; set; }
        public IActionResult OnGet(int id)
        {
            Book = bookService.GetBookById(id);
            if(Book == null)
            {
                return RedirectToPage("NotFound");
            }

            return Page();
        }
    }
}