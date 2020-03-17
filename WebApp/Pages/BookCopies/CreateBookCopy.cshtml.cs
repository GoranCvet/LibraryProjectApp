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
    public class CreateBookCopyModel : PageModel
    {
        private readonly IBookCopiesService bookCopiesService;
        private readonly IBookService bookService;

        public CreateBookCopyModel(IBookCopiesService bookCopiesService, IBookService bookService)
        {
            this.bookCopiesService = bookCopiesService;
            this.bookService = bookService;
        }
        [BindProperty]
        public BookCopies BookCopy { get; set; }
        [BindProperty]
        public Book Book { get; set; }
        public IActionResult OnGet(int id)
        {
            Book = bookService.GetBookById(id);
            if (Book == null)
            {
                return RedirectToPage("NotFound");
            }
            if (Book.BookCopies == null)
            {
                BookCopy = new BookCopies();
                return Page();
            }
            else 
            {
                TempData["Message"] = "Copies of this Book Already Exist!";
                return RedirectToPage("/Books/BookList");
            }
            
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                BookCopy.BookId = Book.Id;

                BookCopy = bookCopiesService.CreateBookCopy(BookCopy);

                bookCopiesService.Commit();
                return RedirectToPage("BookCopiesList", new { id = BookCopy.Id });
            }

            return Page();
        }
    }
}